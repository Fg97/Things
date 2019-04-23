using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
	public class Service : IService
	{
		CounterContainer counterContext;
		public Service()
		{
			counterContext = new CounterContainer();
		}

		public string AddIndication(int accountKey, int counterKey, int value)
		{
			Func<int, string> SetMeasure = (arg) =>
			{
				switch(arg)
				{
					case 1: return "Вода";
					case 2: return "Газ";
					case 3: return "Электричество";
					default: return "Неизвестно";
				}
			};

			counterContext.CounterIndicationSet.Add(new CounterIndication()
			{
				AccountKey = accountKey,
				CounterKey = counterKey,
				Measure = SetMeasure(counterKey),
				Value = value,
				Date = DateTime.Now
			});

			if(counterContext.AccountSet.Where(c => c.AccountId == accountKey).ToArray().Length == 0)
				return "Задан не существующий номер счета";
			else if(counterContext.CounterSet.Where(c => c.CounterId == counterKey).ToArray().Length == 0)
				return "Задан не существующий код счетчика";

			counterContext.SaveChanges();

			return "";
		}

		public List<CounterIndicationData> GetIndicationsThroughAccount(int accountKey)
		{
			return GetIndications(c => c.AccountKey == accountKey);
		}

		public List<CounterIndicationData> GetIndicationsThroughDate(DateTime begin, DateTime end)
		{
			return GetIndications(c => c.Date >= begin && c.Date <= end);
		}

		List<CounterIndicationData> GetIndications(Func<CounterIndication, bool> predicate)
		{
			var indications = counterContext.CounterIndicationSet.Where(predicate);

			var cid = new List<CounterIndicationData>();
			foreach(var item in indications)
				cid.Add(new CounterIndicationData()
				{
					IndicationId = item.IndicationId,
					AccountKey = item.AccountKey,
					CounterKey = item.CounterKey,
					Measure = item.Measure,
					Value = item.Value,
					Date = item.Date
				});

			return cid;
		}
	}
}
