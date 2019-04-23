using System;
using System.Collections.Generic;

using static TranslationClassLibrary.Scanner;
using static TranslationClassLibrary.Scanner.Hash;
using static TranslationClassLibrary.ParserWordTree;

namespace TranslationClassLibrary
{
	public class ParserWordTree
	{
		/// <summary>
		/// Значение, зависящее от режима хранения
		/// </summary>
		public readonly string Value;

		public readonly StorageMode Mode;
		public enum StorageMode
		{
			/// <summary>
			/// <see cref="Value"/> хранит значение слова
			/// </summary>
			Word,
			/// <summary>
			/// <see cref="Value"/> хранит хэш слова
			/// </summary>
			Hash,
			/// <summary>
			/// <see cref="Value"/> хранит тип слова
			/// </summary>
			Type,
			/// <summary>
			/// <see cref="Value"/> хранит тип выражения
			/// </summary>
			Node
		}

		ParserWordTree[] nodes;
		public ParserWordTree[] Nodes => nodes;
		public ParserWordTree[] CreateNodes(params ParserWordTree[] nodes) => this.nodes = nodes;

		public ParserWordTree() : this(Parser.EndOfExpression, StorageMode.Hash) { }
		public ParserWordTree(string value, StorageMode mode = StorageMode.Hash)
		{
			Value = value;
			Mode = mode;
		}
	}
	public class ParserExpressionElement
	{
		public readonly ParserExpression Node;
		public readonly ScannerWord Value;

		public bool IsNode => Value is null;
		public bool IsValue => Node is null;

		public ParserExpressionElement(ParserExpression node) => Node = node ?? throw new ArgumentNullException();
		public ParserExpressionElement(ScannerWord word) => Value = word ?? throw new ArgumentNullException();
	}
	public class ParserExpression
	{
		public readonly Parser.ExpressionType Type;
		public readonly List<ParserExpressionElement> Value;

		public ParserExpression(Parser.ExpressionType exprType)
		{
			Type = exprType;
			Value = new List<ParserExpressionElement>();
		}

		public void Add(ParserExpression expression) => Value.Add(new ParserExpressionElement(expression));
		public void Add(ScannerWord word) => Value.Add(new ParserExpressionElement(word));
	}

	public class Parser
	{
		///<Static>
		public static string GetMainExprHash(ExpressionType type) => type.ToString() + "_";
		public static string GetSupportExprHash(string name) => ExpressionType.Support.ToString() + "_" + name;
		public static ExpressionType GetExprType(string hash)
		{
			var stringType = hash.Split('_')[0];
			for(ExpressionType type = 0; (int)type <= ExpressionTypeLength; type++)
				if(type.ToString() == stringType) return type;

			return ExpressionType.None;
		}

		///<Instance>
		Scanner sc;
		Dictionary<string, ParserWordTree> exprsAlphabet;

		const int ExpressionTypeLength = (int)ExpressionType.Support;
		public enum ExpressionType
		{
			None = -1,
			S, D, K, A, /*Comment,*/
			/// <summary>
			/// Не использовать в качестве главного типа
			/// </summary>
			Support
		}

		internal const string EndOfExpression = "EXPR_END";

		const string ParentNodeString = "";
		public static ParserWordTree ParentNode => new ParserWordTree(ParentNodeString, StorageMode.Hash);

		public Parser(Scanner scanner)
		{
			sc = scanner;
			exprsAlphabet = new Dictionary<string, ParserWordTree>();
		}
		public ParserWordTree this[ExpressionType type]
		{
			get => exprsAlphabet[GetMainExprHash(type)];
			set => exprsAlphabet.Add(GetMainExprHash(type), value);
		}
		public ParserWordTree this[string name]
		{
			get => exprsAlphabet[GetSupportExprHash(name)];
			set => exprsAlphabet.Add(GetSupportExprHash(name), value);
		}

		/// <summary>
		/// Проход по списку считанных слов, хранящихся в <see cref="Scanner.ReadWords"/>
		/// </summary>
		int readWordsIndex;

		/// <summary>
		/// Считывает выражение, ограниченное символом разделителя, из текста и перемещает позицию в тексте до следующего за разделителем символа
		/// </summary>
		public ParserExpression ReadExpression()
		{
			var buffer = new List<ParserExpression>();

			ParserWordTree[] expectedNodes;
			for(ExpressionType et = 0; (int)et < ExpressionTypeLength; et++)
			{
				var readExpression = new ParserExpression(et);

				int backup = readWordsIndex;
				if(IsCorrectExpression(exprsAlphabet[GetMainExprHash(et)], readExpression, false))
				{
					if(sc.ReadWords[readWordsIndex].Hash == ExpressionSeparator)
						readExpression.Add(sc.ReadWords[readWordsIndex++]);
					else
						readExpression.Add(new ScannerWord("", "Expect - " + sc[ExpressionSeparator].Symbol, WordType.None));

					return readExpression;
				}
				readWordsIndex = backup;

				if(expectedNodes != null) buffer.Add(readExpression);
			}

			/// Переход к следующему разделителю выражений
			do
			{
				if(readWordsIndex == sc.ReadWords.GetCount())
					break;
			} while(sc.ReadWords[readWordsIndex++].Hash != ExpressionSeparator);

			return (buffer.Count != 0) ? (buffer[0]) : (new ParserExpression(ExpressionType.None));

			///Local Func
			bool IsCorrectExpression(ParserWordTree parentNode, ParserExpression expression, bool expectedNodesEnabled)
			{
				expectedNodes = null;

				bool expressionEnd = false;
				while(!expressionEnd)
				{
					bool success = false;
					foreach(var childNode in parentNode.Nodes)
					{
						var readWord = sc.ReadWords[readWordsIndex];

						if(childNode.Mode == StorageMode.Node)
						#region Подвыражение
						{
							var subExpression = new ParserExpression(GetExprType(childNode.Value));

							int backup = readWordsIndex;
							if(IsCorrectExpression(exprsAlphabet[childNode.Value], subExpression, true))
							{
								parentNode = childNode;
								success = true;
							}

							if(subExpression.Value.Count == 0) readWordsIndex = backup;
							else { expression.Add(subExpression); break; }
						}
						#endregion
						else
						#region Значение
						{
							string str = "";
							switch(childNode.Mode)
							{
								case StorageMode.Word: str = readWord.Word; break;
								case StorageMode.Hash: str = readWord.Hash; break;
								case StorageMode.Type: str = readWord.Type.ToString(); break;
							}

							if(childNode.Value == str || childNode.Value == AnyExpression)
							{
								expression.Add(readWord);

								parentNode = childNode;
								expectedNodesEnabled = success = true;

								readWordsIndex++; break;
							}
							else if(childNode.Value == EndOfExpression)
							{ success = expressionEnd = true; break; }

							if(str == NewLine)
							{
								success = true;
								readWordsIndex++; break;
							}
						}
						#endregion

						if(readWord.Type == WordType.EndOfFile) break;
					}

					if(!success)
					{
						if(expectedNodesEnabled && expectedNodes == null)
						{
							expectedNodes = parentNode.Nodes;
							foreach(var expectedNode in expectedNodes)
								switch(expectedNode.Mode)
								{
									case StorageMode.Word:
									case StorageMode.Hash:
									case StorageMode.Type:
										expression.Add(new ScannerWord("", "Expect - " + expectedNode.Value, WordType.None));
										break;
									case StorageMode.Node:
										expression.Add(new ScannerWord("", "Expect node - " + GetExprType(expectedNode.Value), WordType.None));
										break;
								}
						}
						return false;
					}
				}
				return true;
			}
		}

		List<ParserExpression> readExpressions;
		/// <summary>
		/// Храненине всех считанных выражений
		/// </summary>
		public List<ParserExpression> ReadExpressions => readExpressions;
		/// <summary>
		/// Считывает весь текст и заносит выражения в список
		/// </summary>
		public void ReadText()
		{
			readWordsIndex = 0;
			readExpressions = new List<ParserExpression>();
			while(readWordsIndex < sc.ReadWords.GetCount())
			{
				var expr = ReadExpression();
				if(expr.Type == ExpressionType.None) break;

				readExpressions.Add(expr);
			}
		}
	}
}