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
	/// Логика взаимодействия для EquipAdd.xaml
	/// </summary>
	public partial class EquipAdd : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.equipmentsTableAdapter equips;

		public EquipAdd()
		{
			InitializeComponent();

			dataSet = new DataSet();
			equips = new DataSetTableAdapters.equipmentsTableAdapter();
			equips.Fill(dataSet.equipments);
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var lastId = (from eq in dataSet.equipments select eq.id).LastOrDefault() + 1;

			dataSet.Tables["equipments"].Rows.Add(
				lastId,
				nameTextBox.Text,
				descTextBox.Text,
				imageTextBox.Text,
				int.Parse(priceTextBox.Text),
				int.Parse(goodsTextBox.Text));
			equips.Adapter.Update(dataSet, "equipments");

			NavigationService.GoBack();
		}
	}
}
