using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Kontur.ImageTransformer
{
	public sealed class HttpListenerContextHandler
	{
		List<IIdentifyUrl> handlers;
		//Dictionary<Type, IIdentifyUrl> handlers;

		public HttpListenerContextHandler()
		{
			handlers = new List<IIdentifyUrl>();
		}
		
		public void Start(HttpListenerContext listenerContext) => Start(listenerContext, CancellationToken.None);
		public void Start(HttpListenerContext listenerContext, CancellationToken cancellationToken)
		{
			try
			{
				cancellationToken.ThrowIfCancellationRequested();
				for(int i = 0; i < handlers.Count; i++)
				{
					var parseResult = handlers[i].RegexForParseUrl.Match(listenerContext.Request.RawUrl);
					
					if(parseResult.Success)
					{
						cancellationToken.ThrowIfCancellationRequested();

						if(handlers[i].IsCorrectRequest(listenerContext.Request))
						{
							handlers[i].CorrectUrl(listenerContext, parseResult, cancellationToken);

							cancellationToken.ThrowIfCancellationRequested();
							return;
						}
						break;
					}
				}

				SetStatusForBadRequest(listenerContext.Response);
			}
			catch(OperationCanceledException)
			{
				SetStatusForCancelOperation(listenerContext.Response);

				throw;
			}
			catch(Exception e)
			{
				SetStatusForBadRequest(listenerContext.Response);

				Console.WriteLine("In Context Handler");
				Console.WriteLine("--" + e.GetType());
				Console.WriteLine("--" + e.Message);

				throw;
			}
			finally
			{
				CloseContextStreams(listenerContext);
			}
		}

		public void CloseContextStreams(HttpListenerContext listenerContext)
		{
			listenerContext.Request.InputStream.Close();
			listenerContext.Response.OutputStream.Close();
		}

		public void AddHandler(IIdentifyUrl handler) { handlers.Add(handler); }
		public void RemoveHandler(IIdentifyUrl handler) { handlers.Remove(handler); }

		//StatusCodes
		public void SetStatusForCancelOperation(HttpListenerResponse listenerResponse)
		{
			listenerResponse.StatusCode = (int)HttpStatusCode.GatewayTimeout;
		}
		public void SetStatusForBadRequest(HttpListenerResponse listenerResponse)
		{
			listenerResponse.StatusCode = (int)HttpStatusCode.BadRequest;
		}
	}
}