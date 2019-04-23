using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для ClientAdd.xaml
    /// </summary>
    public partial class ClientAdd : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.guestsTableAdapter guests;

		public ClientAdd()
        {
			InitializeComponent();
			
			dataSet = new DataSet();
			guests = new DataSetTableAdapters.guestsTableAdapter();
			guests.Fill(dataSet.guests);
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var lastId = (from g in dataSet.guests select g.id).LastOrDefault() + 1;

			dataSet.Tables["guests"].Rows.Add(lastId, nameTextBox.Text, dataTextBox.Text, DateTime.Now);
			guests.Adapter.Update(dataSet, "guests");

			NavigationService.GoBack();
		}
	}
}