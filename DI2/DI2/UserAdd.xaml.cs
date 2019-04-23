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
    /// Логика взаимодействия для UserAdd.xaml
    /// </summary>
    public partial class UserAdd : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.usersTableAdapter users;

		public UserAdd()
        {
            InitializeComponent();

			dataSet = new DataSet();
			users = new DataSetTableAdapters.usersTableAdapter();
			users.Fill(dataSet.users);
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var lastId = (from u in dataSet.users select u.id).LastOrDefault() + 1;

			using(MD5 md5Hash = MD5.Create())
			{
				dataSet.Tables["users"].Rows.Add(lastId, nameTextBox.Text, loginTextBox.Text, GlobalClass.GetMD5Hash(md5Hash, passwordTextBox.Text), 0);
				users.Adapter.Update(dataSet, "users");
			}

			NavigationService.GoBack();
		}
	}
}
