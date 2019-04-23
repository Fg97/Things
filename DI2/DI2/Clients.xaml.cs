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
	/// Логика взаимодействия для Clients.xaml
	/// </summary>
	public partial class Clients : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.guestsTableAdapter guests;
		
		public Clients(DataSet.usersRow user)
		{
			InitializeComponent();

			dataSet = new DataSet();
			guests = new DataSetTableAdapters.guestsTableAdapter();

			if(user.role == 0)
			{
				add.Visibility = Visibility.Collapsed;
				edit.Visibility = Visibility.Collapsed;
				remove.Visibility = Visibility.Collapsed;
			}
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			guests.Fill(dataSet.guests);
			FillDataGrid();
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new ClientAdd());
		}
		private void Edit_Click(object sender, RoutedEventArgs e)
		{

		}
		private void Remove_Click(object sender, RoutedEventArgs e)
		{
			foreach(var client in clientsDataGrid.SelectedItems.Cast<DGTable>())
			{
				var guestRow = (from g in dataSet.guests
							   where g.id == client.id
							   select g).First();

				guests.Delete(guestRow.id, guestRow.name, guestRow.data, guestRow.create_date);
				dataSet.guests.Rows.Remove(guestRow);
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
		private void dataTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{ FillDataGrid(); }
		private void createDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(); }

		void FillDataGrid()
		{
			clientsDataGrid.ItemsSource = (from g in dataSet.guests
										   where (nameTextBox.Text.Length == 0) || g.name.ContainsIgnoreCase(nameTextBox.Text.ToUpper())
										   where (dataTextBox.Text.Length == 0) || g.data.ContainsIgnoreCase(dataTextBox.Text.ToUpper())
										   where !createDate.SelectedDate.HasValue ||
												 createDate.SelectedDate.Value.ToShortDateString() == g.create_date.ToShortDateString()
										   select new DGTable(g.id, g.name, g.data, g.create_date)).ToArray();

			for(int i = 0; i < clientsDataGrid.Columns.Count; i++)
				clientsDataGrid.Columns[i].Header = DGTable.Names[i];
		}

		class DGTable
		{
			public static string[] Names = { "№ клиента", "Ф.И.О.", "Данные", "Дата создания" };

			public DGTable(int id, string name, string data, DateTime createDate)
			{
				this.id = id;
				this.name = name;
				this.data = data;
				this.createDate = createDate.ToShortDateString() + " " + createDate.ToLongTimeString();
			}
			public int id { get; set; }
			public string name { get; set; }
			public string data { get; set; }
			public string createDate { get; set; }
		}
	}
}
