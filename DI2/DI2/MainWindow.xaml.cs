using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DI2
{
	static class GlobalClass
	{
		public static string GetMD5Hash(MD5 md5Hash, string input)
		{
			var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			var str = new StringBuilder();
			for(int i = 0; i < data.Length; i++)
				str.Append(data[i].ToString("x2"));

			return str.ToString();
		}
		public static bool ContainsIgnoreCase(this String @string, string value)
		{
			return @string.ToUpper().IndexOf(value.ToUpper()) >= 0;
		}
	}
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : NavigationWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}
	}
}
