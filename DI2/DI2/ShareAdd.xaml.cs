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
	/// Логика взаимодействия для ShareAdd.xaml
	/// </summary>
	public partial class ShareAdd : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.equipmentsTableAdapter equips;
		DataSetTableAdapters.sharesTableAdapter shares;
		DataSetTableAdapters.link_share_equipTableAdapter share_equip;

		public ShareAdd()
		{
			InitializeComponent();
			
			equipListBox.Items.Remove(equipPattern);

			dataSet = new DataSet();
			equips = new DataSetTableAdapters.equipmentsTableAdapter();
			shares = new DataSetTableAdapters.sharesTableAdapter();
			share_equip = new DataSetTableAdapters.link_share_equipTableAdapter();

			equips.Fill(dataSet.equipments);
			shares.Fill(dataSet.shares);
			share_equip.Fill(dataSet.link_share_equip);
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var lastShareId = (from s in dataSet.shares select s.id).LastOrDefault() + 1;
			var lastLinkId = (from l in dataSet.link_share_equip select l.id).LastOrDefault();

			dataSet.Tables["shares"].Rows.Add(
				lastShareId,
				nameTextBox.Text,
				descTextBox.Text,
				startDate.SelectedDate.Value,
				endDate.SelectedDate.Value,
				byte.Parse(discountTextBox.Text));
			shares.Adapter.Update(dataSet, "shares");

			foreach(var panel in equipListBox.Items.Cast<StackPanel>())
			{
				lastLinkId++;

				dataSet.Tables["link_share_equip"].Rows.Add(
					lastLinkId,
					lastShareId,
					int.Parse(((TextBox)panel.Children[0]).Text));
			}
			share_equip.Adapter.Update(dataSet, "link_share_equip");

			NavigationService.GoBack();
		}

		private void EquipAdd_Click(object sender, RoutedEventArgs e)
		{
			var panel = new StackPanel { Orientation = equipPattern.Orientation };

			var textPattern = (TextBox)equipPattern.Children[0];
			var text = new TextBox
			{
				BorderThickness = textPattern.BorderThickness,
				Text = textPattern.Text,
				HorizontalAlignment = textPattern.HorizontalAlignment,
				VerticalAlignment = textPattern.VerticalAlignment,
				TextAlignment = textPattern.TextAlignment,
				Width = textPattern.Width
			};
			text.TextChanged += equipListBoxTextBoxes_TextChanged;

			panel.Children.Add(text);

			var gridPattern = (DataGrid)equipPattern.Children[1];
			var grid = new DataGrid
			{
				HorizontalAlignment = gridPattern.HorizontalAlignment,
				VerticalAlignment = gridPattern.VerticalAlignment
			};
			FillDataGrid(grid, text);

			panel.Children.Add(grid);

			equipListBox.Items.Add(panel);
		}
		private void EquipDelete_Click(object sender, RoutedEventArgs e)
		{
			var selected = new List<object>();
			foreach(var item in equipListBox.SelectedItems) selected.Add(item);
			foreach(var item in selected) equipListBox.Items.Remove(item);
		}

		private void equipListBoxTextBoxes_TextChanged(object sender, TextChangedEventArgs e)
		{
			var panel = ((TextBox)sender).Parent as StackPanel;
			FillDataGrid((DataGrid)panel.Children[1], (TextBox)sender);
		}
		void FillDataGrid(DataGrid dataGrid, TextBox equipId)
		{
			dataGrid.ItemsSource = (from e in dataSet.equipments
									where e.id.ToString() == equipId.Text
									select new DGTable(e.title, e.description, e.image, e.price, e.goods)).ToArray();

			for(int i = 0; i < dataGrid.Columns.Count; i++)
				dataGrid.Columns[i].Header = DGTable.Names[i];
		}

		class DGTable
		{
			public static string[] Names = { "Название", "Описание", "Рисунок", "Цена", "Количество" };

			public DGTable(string title, string description, string image, int price, int goods)
			{
				this.title = title;
				this.description = description;
				this.image = image;
				this.price = price;
				this.goods = goods;
			}
			public string title { get; set; }
			public string description { get; set; }
			public string image { get; set; }
			public int price { get; set; }
			public int goods { get; set; }
		}
	}
}
