using System;
using System.Data;
using System.Linq;
using System.ServiceModel;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			//AddConstData();

			using(var host = new ServiceHost(typeof(Server.Service)))
			{
				host.Open();
				Console.WriteLine("Host created");
				Console.ReadLine();
			}
			Console.WriteLine("Host closed");
			Console.ReadLine();
		}

		static void AddConstData()
		{
			CounterContainer counterContext = new CounterContainer();

			Random rand = new Random();
			for(int i = 0; i < 5; i++)
				counterContext.AccountSet.Add(new Account()
				{
					Date = new DateTime(2016, rand.Next(1, 12), rand.Next(1, 28),
										rand.Next(1, 23), rand.Next(0, 59), rand.Next(0, 59))
				});

			counterContext.CounterSet.Add(new Counter()
			{
				Type = "Вода",
				Description = "Счетчик измеряет расход воды в литрах"
			});
			counterContext.CounterSet.Add(new Counter()
			{
				Type = "Газ",
				Description = "Счетчик измеряет расход газа в кубических метрах"
			});
			counterContext.CounterSet.Add(new Counter()
			{
				Type = "Электричество",
				Description = "Счетчик измеряет расход электроэнергии в киловатт-часах"
			});

			counterContext.SaveChanges();
		}
	}
}