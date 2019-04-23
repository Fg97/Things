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
	/// Логика взаимодействия для Shares.xaml
	/// </summary>
	public partial class Shares : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.sharesTableAdapter shares;
		DataSetTableAdapters.link_share_equipTableAdapter share_equip;

		public Shares(DataSet.usersRow user)
		{
			InitializeComponent();

			dataSet = new DataSet();
			shares = new DataSetTableAdapters.sharesTableAdapter();
			share_equip = new DataSetTableAdapters.link_share_equipTableAdapter();

			if(user.role == 0)
			{
				add.Visibility = Visibility.Collapsed;
				edit.Visibility = Visibility.Collapsed;
				remove.Visibility = Visibility.Collapsed;
			}
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			shares.Fill(dataSet.shares);
			share_equip.Fill(dataSet.link_share_equip);
			FillDataGrid();
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new ShareAdd());
		}
		private void Edit_Click(object sender, RoutedEventArgs e)
		{

		}
		private void Remove_Click(object sender, RoutedEventArgs e)
		{
			foreach(var share in sharesDataGrid.SelectedItems.Cast<DGTable>())
			{
				var linkRows = (from l in dataSet.link_share_equip
							   where l.share_id == share.id
							   select l).ToArray();
				foreach(var linkRow in linkRows)
				{
					share_equip.Delete(linkRow.id, linkRow.share_id, linkRow.equipment_id);
					dataSet.link_share_equip.Rows.Remove(linkRow);
				}

				var shareRow = (from s in dataSet.shares
								where s.id == share.id
								select s).First();

				shares.Delete(shareRow.id, shareRow.name, shareRow.description, shareRow.start_date, shareRow.end_date, shareRow.discount);
				dataSet.shares.Rows.Remove(shareRow);
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
		private void discountTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{ FillDataGrid(); }
		private void discountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(); }
		private void startDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(); }
		private void startDateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(); }
		private void endDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(); }
		private void endDateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(); }

		void FillDataGrid()
		{
			if(!IsLoaded) return;

			sharesDataGrid.ItemsSource = (from s in dataSet.shares
										  where (nameTextBox.Text.Length == 0) || s.name.ContainsIgnoreCase(nameTextBox.Text)
										  where (discountTextBox.Text.Length == 0) ||
													FindInComboBox(discountComboBox, s.discount, int.Parse(discountTextBox.Text))
										  where !startDate.SelectedDate.HasValue ||
													FindInComboBox(startDateComboBox, s.start_date.Date, startDate.SelectedDate.Value.Date)
										  where !endDate.SelectedDate.HasValue ||
													FindInComboBox(endDateComboBox, s.end_date.Date, endDate.SelectedDate.Value.Date)
										  select new DGTable(s.id, s.name, s.description, s.start_date, s.end_date, s.discount)).ToArray();

			for(int i = 0; i < sharesDataGrid.Columns.Count; i++)
				sharesDataGrid.Columns[i].Header = DGTable.Names[i];
		}
		bool FindInComboBox(ComboBox comboBox, int value, int model)
		{
			switch(((ComboBoxItem)comboBox.SelectedItem).Content.ToString())
			{
				case "=": return value == model;
				case ">": return value > model;
				case "<": return value < model;
			}
			return false;
		}
		bool FindInComboBox(ComboBox comboBox, DateTime value, DateTime model)
		{
			switch(((ComboBoxItem)comboBox.SelectedItem).Content.ToString())
			{
				case "=": return value == model;
				case ">": return value > model;
				case "<": return value < model;
			}
			return false;
		}

		class DGTable
		{
			public static string[] Names = { "№ акции", "Название", "Описание", "Дата начала", "Дата окончания", "Скидка" };

			public DGTable(int id, string name, string description, DateTime startDate, DateTime endDate, byte discount)
			{
				this.id = id;
				this.name = name;
				this.description = description;
				this.startDate = startDate.ToShortDateString();
				this.endDate = endDate.ToShortDateString();
				this.discount = discount;
			}
			public int id { get; set; }
			public string name { get; set; }
			public string description { get; set; }
			public string startDate { get; set; }
			public string endDate { get; set; }
			public byte discount { get; set; }
		}
	}
}
