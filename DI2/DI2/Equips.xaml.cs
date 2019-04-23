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
	/// Логика взаимодействия для Equips.xaml
	/// </summary>
	public partial class Equips : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.equipmentsTableAdapter equips;

		public Equips(DataSet.usersRow user)
		{
			InitializeComponent();

			dataSet = new DataSet();
			equips = new DataSetTableAdapters.equipmentsTableAdapter();

			if(user.role == 0)
			{
				add.Visibility = Visibility.Collapsed;
				edit.Visibility = Visibility.Collapsed;
				remove.Visibility = Visibility.Collapsed;
			}
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			equips.Fill(dataSet.equipments);
			FillDataGrid();
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new EquipAdd());
		}
		private void Edit_Click(object sender, RoutedEventArgs e)
		{

		}
		private void Remove_Click(object sender, RoutedEventArgs e)
		{
			foreach(var equip in equipsDataGrid.SelectedItems.Cast<DGTable>())
			{
				var equipRow = (from eq in dataSet.equipments
								where eq.id == equip.id
								select eq).First();

				equips.Delete(equipRow.id, equipRow.title, equipRow.description, equipRow.image, equipRow.price, equipRow.goods);
				dataSet.equipments.Rows.Remove(equipRow);
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
		private void priceTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{ FillDataGrid(); }
		private void priceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(); }
		private void countTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{ FillDataGrid(); }
		private void countComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(); }

		void FillDataGrid()
		{
			if(!IsLoaded) return;

			equipsDataGrid.ItemsSource = (from e in dataSet.equipments
										  where (nameTextBox.Text.Length == 0) || e.title.ContainsIgnoreCase(nameTextBox.Text)
										  where (priceTextBox.Text.Length == 0) ||
													FindInComboBox(priceComboBox, e.price, int.Parse(priceTextBox.Text))
										  where (countTextBox.Text.Length == 0) ||
													FindInComboBox(countComboBox, e.goods, int.Parse(countTextBox.Text))
										  select new DGTable(e.id, e.title, e.description, e.image, e.price, e.goods)).ToArray();

			for(int i = 0; i < equipsDataGrid.Columns.Count; i++)
				equipsDataGrid.Columns[i].Header = DGTable.Names[i];
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

		class DGTable
		{
			public static string[] Names = { "№ снаряжения", "Название", "Описание", "Рисунок", "Цена", "Количество" };

			public DGTable(int id, string title, string description, string image, int price, int goods)
			{
				this.id = id;
				this.title = title;
				this.description = description;
				this.image = image;
				this.price = price;
				this.goods = goods;
			}
			public int id { get; set; }
			public string title { get; set; }
			public string description { get; set; }
			public string image { get; set; }
			public int price { get; set; }
			public int goods { get; set; }
		}
	}
}
