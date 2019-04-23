using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace TranslationClassLibrary
{
	public class HashTables
	{
		///<Static>
		public static string ComputeHash(string value)
		{
			var sha1 = SHA1.Create();
			var data = sha1.ComputeHash(Encoding.UTF32.GetBytes(value));
			var @string = new StringBuilder();

			for(int i = 0; i < data.Length; i++)
				@string.Append(data[i].ToString("x2"));

			return @string.ToString();
		}

		///<Instance>
		Dictionary<string, string> Identificators, Numbers, Others;

		public HashTables()
		{
			Identificators = new Dictionary<string, string>();
			Numbers = new Dictionary<string, string>();
			Others = new Dictionary<string, string>();
		}

		public void AddIdentificator(string identificator) => Add(Identificators, identificator);
		public void AddNumber(string number) => Add(Numbers, number);
		public void AddOther(string other) => Add(Others, other);

		public string GetIdentificator(string identificator) => Get(Identificators, identificator);
		public string GetNumber(string number) => Get(Numbers, number);
		public string GetOther(string other) => Get(Others, other);

		void Add(Dictionary<string, string> dict, string value)
		{
			string hash = ComputeHash(value);
			if(!dict.ContainsKey(hash)) dict.Add(hash, value);
			else throw new System.Exception("Уже есть");
		}
		string Get(Dictionary<string, string> dict, string value)
		{
			string hash = ComputeHash(value);
			if(!dict.ContainsKey(hash)) return null;

			return dict[hash];
		}
	}
}