using System.Collections.Generic;

using static TranslationClassLibrary.Transliterator;
using static TranslationClassLibrary.Scanner;

namespace TranslationClassLibrary
{
	public class ScannerSymbolTree
	{
		public readonly char Symbol;
		public readonly WordType EndWordType;

		ScannerSymbolTree[] nodes;
		public ScannerSymbolTree[] Nodes => nodes;
		public ScannerSymbolTree[] CreateNodes(params ScannerSymbolTree[] nodes) => this.nodes = nodes;

		public ScannerSymbolTree(char symbol)
		{
			Symbol = symbol;
			EndWordType = WordType.None;
		}
		public ScannerSymbolTree(WordType endWordType)
		{
			Symbol = EndOfWord;
			EndWordType = endWordType;
		}
	}
	public class ScannerWord
	{
		public readonly string Hash;
		public readonly string Word;
		public readonly WordType Type;

		public ScannerWord(string hash, string word, WordType wordType)
		{
			Hash = hash;
			Word = word;
			Type = wordType;
		}
	}
	public class Scanner
	{
		///<Static>
		public static class Hash
		{
			public const string
				ExpressionSeparator = "EXPR_SEP",
				NewLine = "NEW_LINE",
				AnyExpression = "EXPR_ANY";
		}

		internal const char EndOfWord = '\0';

		const char ParentNodeChar = ' ';
		public static ScannerSymbolTree ParentNode => new ScannerSymbolTree(ParentNodeChar);
		
		static ScannerWord NoneWord(string word) => new ScannerWord("", word, WordType.None);
		static ScannerWord EndOfFile => new ScannerWord(Parser.ParentNode.Value, Parser.ParentNode.Value, WordType.EndOfFile);

		///<Instance>
		Transliterator tr;
		Dictionary<string, ScannerSymbolTree> wordsAlphabet;
		
		public enum WordType { None = -1, Identifier, Number, Operator, Bracket, ExpressionSeparator, NewLine, Comment, EndOfFile }

		public Scanner(Transliterator transliterator)
		{
			tr = transliterator;
			wordsAlphabet = new Dictionary<string, ScannerSymbolTree>();
		}
		public ScannerSymbolTree this[string name]
		{
			get => wordsAlphabet[name];
			set => wordsAlphabet.Add(name, value);
		}

		/// <summary>
		/// Считывает слово, ограниченное символом разделителя, из текста и перемещает позицию в тексте до следующего за разделителем символа
		/// </summary>
		ScannerWord ReadWord(string spacedWord, ref int readSymbolIndex)
		{
			foreach(var pair in wordsAlphabet)
			{
				ScannerSymbolTree parentNode;
				if(pair.Value.Symbol == ParentNodeChar) parentNode = pair.Value;
				else
				{
					parentNode = ParentNode;
					parentNode.CreateNodes(pair.Value);
				}

				int backup = readSymbolIndex;
				bool incorrect = !IsCorrectWord(parentNode, out string word, out WordType type, ref readSymbolIndex);

				if(incorrect && word == "")
				{
					readSymbolIndex = backup;
					continue;
				}
				else return new ScannerWord(pair.Key, word, type);
			}
			return NoneWord(spacedWord);

			///Local Func
			bool IsCorrectWord(ScannerSymbolTree parentNode, out string word, out WordType type, ref int readSymbolIndex__)
			{
				word = "";
				type = WordType.None;

				bool wordEnd = false;
				while(!wordEnd)
				{
					bool success = false;
					foreach(var childNode in parentNode.Nodes)
					{
						if(readSymbolIndex__ != spacedWord.Length &&
							childNode.Symbol == spacedWord[readSymbolIndex__])
						{
							word += childNode.Symbol.ToString();

							parentNode = childNode;
							success = true;

							readSymbolIndex__++; break;
						}
						else if(childNode.Symbol == EndOfWord)
						{
							type = childNode.EndWordType;
							success = wordEnd = true; break;
						}
					}

					if(!success) return false;
				}
				return true;
			}
		}

		/// <summary>
		/// Форматированный текст, разделенный по пробелам
		/// </summary>
		List<string> spacedWords;

		List<ScannerWord> readWords;
		/// <summary>
		/// Храненине всех считанных слов
		/// </summary>
		public List<ScannerWord> ReadWords => readWords;

		/// <summary>
		/// Считывает весь текст и заносит слова в список <see cref="ReadWords"/>
		/// </summary>
		public void ReadText()
		{
			spacedWords = new List<string>();

			string str = "";
			foreach(var symbol in tr.OutputText)
			{
				switch(symbol.Type)
				{
					case SymbolType.Space:
					case SymbolType.EndOfFile:
						if(str != "")
						{
							spacedWords.Add(str);
							str = "";
						}
						continue;
				}
				str += symbol.Symbol.ToString();
			}

			readWords = new List<ScannerWord>();
			var readWordsBuffer = new List<ScannerWord>();
			
			int readIndex = 0, readSymbolIndex = 0;
			while(readIndex < spacedWords.Count)
			{
				var spacedWord = spacedWords[readIndex];
				var readWord = ReadWord(spacedWord, ref readSymbolIndex);
				readWordsBuffer.Add(readWord);

				if(readWord.Type == WordType.None) readWords.Add(NoneWord(spacedWord));
				else if(readSymbolIndex == spacedWord.Length) readWords.AddRange(readWordsBuffer);
				else continue;

				readWordsBuffer.Clear();
				readSymbolIndex = 0; readIndex++;
			}
			readWords.Add(EndOfFile);
		}
	}
}