using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TranslationClassLibrary;

using static TranslationClassLibrary.Transliterator;
using static TranslationClassLibrary.Scanner;
using static TranslationClassLibrary.Scanner.Hash;
using static TranslationClassLibrary.Parser;
using static TranslationClassLibrary.ParserWordTree;

namespace TranslationForm
{
	public partial class TranslationForm : Form
	{
		Transliterator transliterator;
		Scanner scanner;
		Parser parser;

		HashTables hashTables;
		TreeNode node;

		const string
			Hash_Identifier_1 = "(?=cb)[a-d]+",
			Hash_Number_1 = "(010)*001(000)*";

		public TranslationForm()
		{
			InitializeComponent();

			transliterator = new Transliterator();
			transliterator[SymbolType.Letter] = "abcd";
			transliterator[SymbolType.Digit] = "01";
			transliterator[SymbolType.Special] = ";()~|&!:/*";
			transliterator[SymbolType.Space] = " \t\v\f";
			transliterator[SymbolType.NewLine] = "\r\n";
			transliterator[SymbolType.EndOfFile] = "\0";

			scanner = new Scanner(transliterator);
			scanner[Hash_Identifier_1] = CreateIdentifier_1();
			scanner[Hash_Number_1] = CreateNumber_1();
			scanner["("] = CreateOneSymbolWord('(', WordType.Bracket);
			scanner[")"] = CreateOneSymbolWord(')', WordType.Bracket);
			scanner["~"] = CreateOneSymbolWord('~', WordType.Operator);
			scanner["|"] = CreateOneSymbolWord('|', WordType.Operator);
			scanner["&"] = CreateOneSymbolWord('&', WordType.Operator);
			scanner["!(:)?"] = CreateOperator_1();
			scanner[NewLine] = CreateNewLine();
			//scanner["/*"] = CreateComment_1();
			//scanner["*/"] = CreateComment_2();
			scanner[ExpressionSeparator] = CreateOneSymbolWord(';', WordType.ExpressionSeparator);

			parser = new Parser(scanner);
			parser[ExpressionType.S] = CreateExpression_S();
			parser[ExpressionType.D] = CreateExpression_D();
			parser[ExpressionType.K] = CreateExpression_K();
			parser[ExpressionType.A] = CreateExpression_A();
			//parser[ExpressionType.Comment, "/**/"] = CreateExpression_Comment_MultiLine();

			hashTables = new HashTables();
		}

		private void runButton_Click(object sender, EventArgs e)
		{
			ClearAllElements();

			transliterator.ReadText();
			for(int i = 0; i < transliterator.OutputText.GetCount();)
			{
				var symbol = transliterator.OutputText[i++];
				translationTextBox.Text += i + ". ";
				translationTextBox.Text += (symbol.Type != SymbolType.NewLine ? symbol.Symbol.ToString() : " ");
				translationTextBox.Text += " is " + symbol.Type + Environment.NewLine;
			}
			translationTextBox.Text = "";

			scanner.ReadText();
			for(int i = 0; i < scanner.ReadWords.GetCount();)
			{
				var word = scanner.ReadWords[i++];
				translationTextBox.Text += i + ". ";
				translationTextBox.Text += (word.Type != WordType.NewLine ? word.Word : " ");
				translationTextBox.Text += " is " + word.Type + Environment.NewLine;
			}
			translationTextBox.Text += Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

			parser.ReadText();
			foreach(var expr in parser.ReadExpressions)
			{
				node = syntaxTreeView.Nodes.Add(expr.Type.ToString());
				CreateSyntaxSubTree(expr.Value, node.Nodes, structuredTreeView.Nodes);
			}

			BuildStructuredView();
		}

		private void programTextBox_TextChanged(object sender, EventArgs e) => ClearAllElements();

		void CreateSyntaxSubTree(IList<ParserExpressionElement> elements, TreeNodeCollection nodes, TreeNodeCollection nodesS)
		{
			foreach(var elem in elements)
			{
				if(elem.IsNode)
				{
					var subNodes = nodes.Add(elem.Node.Type.ToString());
					CreateSyntaxSubTree(elem.Node.Value, subNodes.Nodes, nodesS);
				}
				else
				{
					nodes.Add(elem.Value.Word);
					nodesS.Add(elem.Value.Word);

					var value = elem.Value.Word;
					try
					{
						switch(elem.Value.Type)
						{
							case WordType.Identifier:
								hashTables.AddIdentificator(value);
								listBox1.Items.Add(value);
								break;
							case WordType.Number:
								hashTables.AddNumber(value);
								listBox2.Items.Add(value);

								listBox3.Items.Add(FromBinaryToDecimal(value));
								break;
						}
					}
					catch
					{
						translationTextBox.Text += "Идентификатор " + value + " уже есть." + Environment.NewLine;
						syntaxTreeView.Nodes.Remove(node);
					}
				}
			}

			///Local Func
			string FromBinaryToDecimal(string value)
			{
				var binary = value.ToCharArray();
				int @decimal = 0;

				for(int i = 0; i < binary.Length; i++)
				{
					if(binary[i] == '1')
						@decimal += (int)Math.Pow(2, binary.Length - i - 1);
				}
				return @decimal.ToString();
			}
		}
		void BuildStructuredView()
		{
			var structNodes = new List<TreeNode>();
			foreach(var node in structuredTreeView.Nodes)
				structNodes.Add((TreeNode)node);

			structuredTreeView.Nodes.Clear();
			structuredTreeView.Nodes.Add(structNodes[0]);

			for(int i = 0; i < structNodes.Count - 1;)
			{
				structNodes[i].Nodes.Add(structNodes[++i]);
				if(structNodes[i].Text == ";" && i != structNodes.Count - 1)
					structuredTreeView.Nodes.Add(structNodes[++i]);
			}
		}

		void ClearAllElements()
		{
			transliterator.InputText = programTextBox.Text;
			translationTextBox.Text = "";

			syntaxTreeView.Nodes.Clear();
			structuredTreeView.Nodes.Clear();

			hashTables = new HashTables();
			listBox1.Items.Clear();
			listBox2.Items.Clear();
			listBox3.Items.Clear();
		}

		//Создание слов
		ScannerSymbolTree CreateNumber_1()
		{
			ScannerSymbolTree
				node0 = new ScannerSymbolTree('0'),
				node1 = new ScannerSymbolTree('1'),
				node2 = new ScannerSymbolTree('0'),
				node3 = new ScannerSymbolTree('0'),
				node4 = new ScannerSymbolTree('1'),
				node5 = new ScannerSymbolTree('0'),
				node6 = new ScannerSymbolTree('0'),
				node7 = new ScannerSymbolTree('0'),
				nodeFin = new ScannerSymbolTree(WordType.Number);

			node0.CreateNodes(node1, node3);
			node1.CreateNodes(node2);
			node2.CreateNodes(node0);
			node3.CreateNodes(node4);
			node4.CreateNodes(node5, nodeFin);
			node5.CreateNodes(node6);
			node6.CreateNodes(node7);
			node7.CreateNodes(node5, nodeFin);

			return node0;
		}
		ScannerSymbolTree CreateIdentifier_1()
		{
			ScannerSymbolTree
				node0 = new ScannerSymbolTree('c'),
				node1 = new ScannerSymbolTree('b'),
				node2 = new ScannerSymbolTree('a'),
				node3 = new ScannerSymbolTree('b'),
				node4 = new ScannerSymbolTree('c'),
				node5 = new ScannerSymbolTree('d'),
				nodeFin = new ScannerSymbolTree(WordType.Identifier);

			node0.CreateNodes(node1);
			node1.CreateNodes(
				node2.CreateNodes(
					node3.CreateNodes(
						node4.CreateNodes(
							node5.CreateNodes(node2, node3, node4, node5, nodeFin)))));

			return node0;
		}

		ScannerSymbolTree CreateOneSymbolWord(char symbol, WordType type)
		{
			ScannerSymbolTree
				node0 = new ScannerSymbolTree(symbol),
				nodeFin = new ScannerSymbolTree(type);

			node0.CreateNodes(nodeFin);

			return node0;
		}
		ScannerSymbolTree CreateOperator_1()
		{
			ScannerSymbolTree
				node0 = new ScannerSymbolTree('!'),
				node1 = new ScannerSymbolTree(':'),
				nodeFin = new ScannerSymbolTree(WordType.Operator);

			node0.CreateNodes(node1, nodeFin);
			node1.CreateNodes(nodeFin);

			return node0;
		}
		ScannerSymbolTree CreateNewLine()
		{
			ScannerSymbolTree
				node0 = new ScannerSymbolTree('\r'),
				node1 = new ScannerSymbolTree('\n'),
				nodeFin = new ScannerSymbolTree(WordType.NewLine);

			node0.CreateNodes(node1);
			node1.CreateNodes(nodeFin);

			return node0;
		}
		/*
		ScannerSymbolTree CreateComment_1()
		{
			ScannerSymbolTree
				node0 = new ScannerSymbolTree('/'),
				node1 = new ScannerSymbolTree('*'),
				nodeFin = new ScannerSymbolTree(EndOfWord);

			node0.Nodes = new ScannerSymbolTree[] { node1 };
			node1.Nodes = new ScannerSymbolTree[] { nodeFin };

			return node0;
		}
		ScannerSymbolTree CreateComment_2()
		{
			ScannerSymbolTree
				node0 = new ScannerSymbolTree('*'),
				node1 = new ScannerSymbolTree('/'),
				nodeFin = new ScannerSymbolTree(EndOfWord);

			node0.Nodes = new ScannerSymbolTree[] { node1 };
			node1.Nodes = new ScannerSymbolTree[] { nodeFin };

			return node0;
		}
		*/
		//Создание выражений
		ParserWordTree CreateExpression_S()
		{
			ParserWordTree
				node0 = Parser.ParentNode,
				node1 = new ParserWordTree(Hash_Number_1),
				node2 = new ParserWordTree("("),
				node3 = new ParserWordTree(GetMainExprHash(ExpressionType.D), StorageMode.Node),
				node4 = new ParserWordTree("!:", StorageMode.Word),
				node5 = new ParserWordTree(GetMainExprHash(ExpressionType.S), StorageMode.Node),
				node6 = new ParserWordTree("!", StorageMode.Word),
				node7 = new ParserWordTree(GetMainExprHash(ExpressionType.S), StorageMode.Node),
				node8 = new ParserWordTree(")"),
				nodeFin = new ParserWordTree();

			node0.CreateNodes(node1, node2);
			node1.CreateNodes(nodeFin);
			node2.CreateNodes(node3);
			node3.CreateNodes(node4);
			node4.CreateNodes(node5);
			node5.CreateNodes(node6);
			node6.CreateNodes(node7);
			node7.CreateNodes(node8);
			node8.CreateNodes(nodeFin);

			return node0;
		}
		ParserWordTree CreateExpression_D()
		{
			ParserWordTree
				node0 = Parser.ParentNode,
				node1 = new ParserWordTree(GetMainExprHash(ExpressionType.K), StorageMode.Node),
				node2 = new ParserWordTree("|"),
				node3 = new ParserWordTree(GetMainExprHash(ExpressionType.D), StorageMode.Node),
				nodeFin = new ParserWordTree();

			node0.CreateNodes(node1);
			node1.CreateNodes(node2, nodeFin);
			node2.CreateNodes(node3);
			node3.CreateNodes(nodeFin);

			return node0;
		}
		ParserWordTree CreateExpression_K()
		{
			ParserWordTree
				node0 = Parser.ParentNode,
				node1 = new ParserWordTree(GetMainExprHash(ExpressionType.A), StorageMode.Node),
				node2 = new ParserWordTree("&"),
				node3 = new ParserWordTree(GetMainExprHash(ExpressionType.K), StorageMode.Node),
				nodeFin = new ParserWordTree();

			node0.CreateNodes(node1);
			node1.CreateNodes(node2, nodeFin);
			node2.CreateNodes(node3);
			node3.CreateNodes(nodeFin);
			
			return node0;
		}
		ParserWordTree CreateExpression_A()
		{
			ParserWordTree
				node0 = Parser.ParentNode,
				node1 = new ParserWordTree(Hash_Identifier_1),
				node2 = new ParserWordTree("("),
				node3 = new ParserWordTree(GetMainExprHash(ExpressionType.D), StorageMode.Node),
				node4 = new ParserWordTree(")"),
				node5 = new ParserWordTree("~"),
				node6 = new ParserWordTree(GetMainExprHash(ExpressionType.A), StorageMode.Node),
				nodeFin = new ParserWordTree();

			node0.CreateNodes(node1, node2, node5);
			node1.CreateNodes(nodeFin);
			node2.CreateNodes(node3);
			node3.CreateNodes(node4);
			node4.CreateNodes(nodeFin);
			node5.CreateNodes(node6);
			node6.CreateNodes(nodeFin);

			return node0;
		}

		ParserWordTree CreateExpression_Comment_MultiLine()
		{
			ParserWordTree
				node0 = Parser.ParentNode,
				node1 = new ParserWordTree("/*"),
				node2 = new ParserWordTree("*/"),
				node3 = new ParserWordTree(AnyExpression),
				nodeFin = new ParserWordTree();

			node0.CreateNodes(node1);
			node1.CreateNodes(
				node3.CreateNodes(node2, node3));
			node2.CreateNodes(nodeFin);

			return node0;
		}
	}
}