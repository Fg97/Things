using System;
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
	/// Логика взаимодействия для Users.xaml
	/// </summary>
	public partial class Users : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.usersTableAdapter users;

		DataSet.usersRow user;
		int[] roles;

		public Users(DataSet.usersRow user)
		{
			InitializeComponent();

			dataSet = new DataSet();
			users = new DataSetTableAdapters.usersTableAdapter();

			this.user = user;

			var items = roleComboBox.Items.Cast<ComboBoxItem>().ToArray();

			roles = new int[items.Length];
			for(int i = 0, k = -1; i < roles.Length; i++)
			{
				switch(items[i].Content.ToString())
				{
					case "Менеджер": k = 0; break;
					case "Админ": k = 1; break;

					default: k = -1; break;
				}
				roles[i] = k;
			}

			if(user.role == 0)
			{
				add.Visibility = Visibility.Collapsed;
				edit.Visibility = Visibility.Collapsed;
				remove.Visibility = Visibility.Collapsed;
			}
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			users.Fill(dataSet.users);
			FillDataGrid();
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new UserAdd());
		}
		private void Edit_Click(object sender, RoutedEventArgs e)
		{

		}
		private void Remove_Click(object sender, RoutedEventArgs e)
		{
			foreach(var user in usersDataGrid.SelectedItems.Cast<DGTable>())
			{
				var userRow = (from u in dataSet.users
							   where u.id == user.id
							   select u).First();

				users.Delete(userRow.id, userRow.name, userRow.login, userRow.password, userRow.role);
				dataSet.users.Rows.Remove(userRow);
			}
			FillDataGrid();
		}
		private void Back_Click(object sender, RoutedEventArgs e)
		{
			if(NavigationService.CanGoBack)
				NavigationService.GoBack();
		}

		private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{ FillDataGrid(); }
		private void loginTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{ FillDataGrid(); }
		private void role_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(); }

		void FillDataGrid()
		{
			if(!this.IsLoaded) return;

			usersDataGrid.ItemsSource = (from u in dataSet.users
										 where (nameTextBox.Text.Length == 0) || u.name.ContainsIgnoreCase(nameTextBox.Text)
										 where (loginTextBox.Text.Length == 0) || u.login.ContainsIgnoreCase(loginTextBox.Text)
										 where (roleComboBox.SelectedIndex <= 0) || u.role == roles[roleComboBox.SelectedIndex]
										 select new DGTable(u.id, u.name, u.login, u.role.ToString())).ToArray();

			for(int i = 0; i < usersDataGrid.Columns.Count; i++)
				usersDataGrid.Columns[i].Header = DGTable.Names[i];
		}

		class DGTable
		{
			public static string[] Names = { "№ пользователя", "Ф.И.О.", "Логин", "Должность" };

			public DGTable(int id, string name, string login, string role)
			{
				this.id = id;
				this.name = name;
				this.login = login;
				this.role = role;
			}
			public int id { get; set; }
			public string name { get; set; }
			public string login { get; set; }
			public string role { get; set; }
		}
	}
}
