using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
	[ServiceContract]
	public interface IService
	{
		[OperationContract]
		string AddIndication(int accountKey, int counterKey, int value);

		[OperationContract]
		List<CounterIndicationData> GetIndicationsThroughAccount(int accountKey);
		[OperationContract]
		List<CounterIndicationData> GetIndicationsThroughDate(DateTime begin, DateTime end);
	}

	public struct CounterIndicationData
	{
		public int IndicationId, AccountKey, CounterKey, Value;
		public string Measure;
		public DateTime Date;
	}
}