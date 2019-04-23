using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    class DTable
    {
        public DTable(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
        public string key { get; set; }
        public string value { get; set; }
	}
	class HTable
	{
		public HTable(string key, string description, string measure)
		{
			this.key = key;
			this.description = description;
			this.measure = measure;
		}
		public string key { get; set; }
		public string description { get; set; }
		public string measure { get; set; }
	}
}
