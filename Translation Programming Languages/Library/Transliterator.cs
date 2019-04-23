using System.Collections.Generic;

namespace TranslationClassLibrary
{
	public struct TransliteratorSymbol
	{
		public readonly char Symbol;
		public readonly Transliterator.SymbolType Type;

		public TransliteratorSymbol(char symbol, Transliterator.SymbolType symbolType)
		{
			Symbol = symbol;
			Type = symbolType;
		}
	}
    public class Transliterator
	{
		string[] alphabet;

		int readIndex;
		string readText;
		public string InputText
		{
			get => readText;
			set
			{
				readIndex = 0;
				readText = value ?? "";
			}
		}

		const int SymbolTypeLength = (int)SymbolType.EndOfFile + 1;
		public enum SymbolType { None = -1, Letter, Digit, Special, Space, NewLine, EndOfFile }

		public bool EndOfFile => readIndex == readText.Length;

		public Transliterator()
		{
			alphabet = new string[SymbolTypeLength];
			readText = "";
		}
		public string this[SymbolType type]
		{
			get => alphabet[(int)type];
			set => alphabet[(int)type] = value;
		}

		/// <summary>
		/// Считывает символ из текста и перемещает позицию в тексте на один символ вперед
		/// </summary>
		TransliteratorSymbol ReadSymbol()
		{
			if(EndOfFile) return new TransliteratorSymbol(alphabet[(int)SymbolType.EndOfFile][0], SymbolType.EndOfFile);

			var symbol = readText[readIndex++];
			var type = GetSymbolType();

			return new TransliteratorSymbol(symbol, type);
			
			SymbolType GetSymbolType()
			{
				for(int st = 0; st < SymbolTypeLength; st++)
				{
					foreach(var s in alphabet[st])
						if(symbol == s) return (SymbolType)st;
				}
				return SymbolType.None;
			}
		}

		List<TransliteratorSymbol> formattedText;
		public List<TransliteratorSymbol> OutputText => formattedText;
		/// <summary>
		/// Считывает и форматирует весь текст
		/// </summary>
		public void ReadText()
		{
			readIndex = 0;
			formattedText = new List<TransliteratorSymbol>();

			bool oneSpaceHasBeenAlready = true;
			TransliteratorSymbol symbol;
			do
			{
				symbol = ReadSymbol();
				if(symbol.Type != SymbolType.Space || oneSpaceHasBeenAlready)
				{
					oneSpaceHasBeenAlready = true;
					formattedText.Add(symbol);
				}

				if(symbol.Type == SymbolType.Space && oneSpaceHasBeenAlready)
					oneSpaceHasBeenAlready = false;

			} while(symbol.Type != SymbolType.EndOfFile);
		}
	}
}