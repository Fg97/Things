using System.Collections.Generic;

namespace TranslationClassLibrary
{
	public static class Global
	{
		public static int GetCount(this List<TransliteratorSymbol> readSymbols) => readSymbols.Count - 1;
		public static int GetCount(this List<ScannerWord> readWords) => readWords.Count - 1;
	}
}