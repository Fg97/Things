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
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
		DataSet dataSet;
		DataSetTableAdapters.usersTableAdapter users;

		public Authorization()
        {
            InitializeComponent();

			dataSet = new DataSet();
			users = new DataSetTableAdapters.usersTableAdapter();

			users.Fill(dataSet.users);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			using(MD5 md5Hash = MD5.Create())
			{
				var user = (from u in dataSet.users
							where u.login == loginTextBox.Text
							where u.password == GlobalClass.GetMD5Hash(md5Hash, passwordTextBox.Text)
							select u).FirstOrDefault();

				if(user != null)
					NavigationService.Navigate(new LogginedPagePattern(user));
			}
		}
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			((MainWindow)Parent).Close();
		}
	}
}
