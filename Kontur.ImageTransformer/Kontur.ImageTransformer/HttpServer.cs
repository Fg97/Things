using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Kontur.ImageTransformer
{
	public sealed class HttpServer : IDisposable
	{
		readonly HttpListener listener;
		readonly HttpListenerContextHandler contextHandler;
		readonly QueryHandler queryHandler;

		Thread listenerThread;

		bool disposed;
		volatile bool isRunning;


		int maxQueryCount;
		/*public int MaxQueryCount
		{
			get { return maxQueryCount; }
			set { Interlocked.Exchange(ref maxQueryCount, value); }
		}*/
		
		public enum WaitFor
		{
			NotFullProcessing,
			FullProcessing,
			FullCancellation,

			Default = FullCancellation
		}
		WaitFor waitFor;
		const WaitFor NotCanceled = (WaitFor)(-1);

		//config
		int cancelRequestAfterMillisecondsTimeout;

		public HttpServer(int maxQueryCount)
		{
			listener = new HttpListener();
			contextHandler = new HttpListenerContextHandler();

			this.maxQueryCount = maxQueryCount;
			waitFor = NotCanceled;
			
			UpdateCancellationTimeout();
			
			queryHandler = new QueryHandler(this);
		}

		public void Start(string uri)
		{
			if(!isRunning)
				lock(listener)
				{
					listener.Prefixes.Clear();
					listener.Prefixes.Add(uri);
					listener.Start();

					listenerThread = new Thread(Listen)
					{
						IsBackground = true,
						Priority = ThreadPriority.Highest
					};
					listenerThread.Start();

					isRunning = true;
				}
		}
		public void Stop(WaitFor waitFor)
		{
			if(!isRunning)
				return;

			lock(listener)
			{
				this.waitFor = waitFor;
				listenerThread.Abort();

				var abortRequest = new HttpWebRequest[listener.Prefixes.Count];

				int i = 0;
				foreach(var prefix in listener.Prefixes)
				{
					string requestUriString = "";
					string[] str = prefix.Split('+', '*');

					if(str.Length == 1) requestUriString = str[0];
					else if(str.Length == 2) requestUriString = str[0] + "localhost" + str[1];
					
					abortRequest[i] = WebRequest.CreateHttp(requestUriString);
					abortRequest[i].GetResponseAsync();

					i++;
				}
				listenerThread.Join();
				
				listener.Stop();

				isRunning = false;
			}
		}
		public void Dispose()
		{
			if(disposed)
				return;

			disposed = true;
			
			Stop(WaitFor.Default);

			listener.Close();
		}
		
		public void AddContextHandler(IIdentifyUrl handler) { contextHandler.AddHandler(handler); }
		public void RemoveContextHandler(IIdentifyUrl handler) { contextHandler.RemoveHandler(handler); }

		public void UpdateCancellationTimeout()
		{
			Interlocked.Exchange(ref cancelRequestAfterMillisecondsTimeout,
				int.Parse(ConfigurationManager.AppSettings["CancelRequestAfterMillisecondsTimeout"]));
		}

		void Listen()
		{
			while(true)
			{
				try
				{
					if(listener.IsListening)
					{
						var listenerContext = listener.GetContext();

						queryHandler.RunHandler(listenerContext, new CancellationTokenSource(cancelRequestAfterMillisecondsTimeout));
					}
					else Thread.Sleep(1);
				}
				catch(ThreadAbortException)
				{
					ListenerThreadAbort();
					return;
				}
				catch(Exception e)
				{
					Console.WriteLine("In Listener Thread");
					Console.WriteLine("--" + e.GetType());
					Console.WriteLine("--" + e.Message);
				}
			}
		}
		void ListenerThreadAbort()
		{
			switch(waitFor)
			{
				case WaitFor.FullProcessing:
					queryHandler.QueueProcessing();
					queryHandler.QueriesProcessing();
					break;

				case WaitFor.NotFullProcessing:
					queryHandler.QueueCancellation();
					queryHandler.QueriesProcessing();
					break;

				case WaitFor.FullCancellation:
					var tasks = queryHandler.QueriesCancellation();
					queryHandler.QueueCancellation();

					try { Task.WaitAll(tasks); }
					catch(AggregateException)
					{

					}
					break;
			}
			Console.WriteLine("Abort is complete");
		}
		
		class QueryHandler
		{
			HttpServer server;

			volatile int queryCount;
			ConcurrentQueue<QueryContainer> queries;

			HandlingTaskContainer[] handlingList;

			public QueryHandler(HttpServer server)
			{
				this.server = server;

				queryCount = 0;
				queries = new ConcurrentQueue<QueryContainer>();

				handlingList = new HandlingTaskContainer[server.maxQueryCount];
				for(int i = 0; i < server.maxQueryCount; i++)
					handlingList[i] = new HandlingTaskContainer();
			}

			public void RunHandler(HttpListenerContext context, CancellationTokenSource source)
			{
				var query = new QueryContainer(context, source);
				if(Interlocked.Increment(ref queryCount) > server.maxQueryCount)
				{
					queries.Enqueue(query);
				}
				else Task.Run(() => HandleQuery(query));
			}
			void HandleQuery(QueryContainer query)
			{
				int index = 0;
				Enter();
				{
					for(; index < handlingList.Length; index++)
					{
						if(!handlingList[index].IsHandling)
						{
							handlingList[index].IsHandling = true;
							break;
						}
					}
				}
				Leave();

				handlingList[index].Task = Task.Run(() => server.contextHandler.Start(query.Context, query.Cancellation.Token), query.Cancellation.Token);
				handlingList[index].Cancellation = query.Cancellation;

				handlingList[index].Task.ContinueWith((t, task) =>
				{
					Enter();
					((HandlingTaskContainer)task).IsHandling = false;
					Leave();

					QueryContainer queryFromQueue;
					do
					{
						Interlocked.Decrement(ref queryCount);

						if(server.waitFor != NotCanceled)
						{
							if(server.waitFor == WaitFor.NotFullProcessing ||
								  server.waitFor == WaitFor.FullCancellation) return;
						}

						if(queries.TryDequeue(out queryFromQueue))
						{
							if(queryFromQueue.Cancellation.IsCancellationRequested)
							{
								server.contextHandler.SetStatusForCancelOperation(queryFromQueue.Context.Response);
								server.contextHandler.CloseContextStreams(queryFromQueue.Context);
							}
							else
							{
								HandleQuery(queryFromQueue);
								return;
							}
						}
					} while(queryFromQueue.Cancellation.IsCancellationRequested);
				}, handlingList[index]);
			}
			
			/// <summary>
			/// Ожидание завершения обработки обрабатываемых запросов
			/// </summary>
			public bool QueriesProcessing()
			{
				bool queryInProgress;
				do
				{
					queryInProgress = false;
					Enter();
					{
						for(int i = 0; i < handlingList.Length; i++)
							if(handlingList[i].IsHandling)
							{
								queryInProgress = true;
								break;
							}
					}
					Leave();
					Thread.Sleep(100);
				} while(queryInProgress);

				return true;
			}
			/// <summary>
			/// Ожидание завершения обработки запросов в очереди
			/// </summary>
			public bool QueueProcessing()
			{
				while(!queries.IsEmpty) Thread.Sleep(100);
				
				return true;
			}
			/// <summary>
			/// Запрос на отмену обрабатываемых запросов
			/// </summary>
			public Task[] QueriesCancellation()
			{
				var result = new List<Task>(handlingList.Length);

				Enter();
				foreach(var task in handlingList)
				{
					if(task.IsHandling)
					{
						result.Add(task.Task);
						task.Cancellation.Cancel();
					}
				}
				Leave();

				return result.ToArray();
			}
			/// <summary>
			/// Отмена обработки запросов, находящихся в очереди
			/// </summary>
			public bool QueueCancellation()
			{
				QueryContainer query;
				while(queries.TryDequeue(out query))
				{
					Interlocked.Decrement(ref queryCount);

					server.contextHandler.SetStatusForBadRequest(query.Context.Response);
					server.contextHandler.CloseContextStreams(query.Context);
				}
				return true;
			}


			int sync;
			void Enter()
			{
				while(Interlocked.Exchange(ref sync, 1) != 0) Thread.Sleep(0);
			}
			void Leave()
			{
				Interlocked.Exchange(ref sync, 0);
			}

			struct QueryContainer
			{
				public HttpListenerContext Context;
				public CancellationTokenSource Cancellation;

				public QueryContainer(HttpListenerContext listenerContext, CancellationTokenSource cancellationSource)
				{
					Context = listenerContext;
					Cancellation = cancellationSource;
				}
			}
			class HandlingTaskContainer
			{
				public bool IsHandling;
				public Task Task;
				public CancellationTokenSource Cancellation;
			}
		}
	}
}