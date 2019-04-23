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
	/// Логика взаимодействия для DocAdd.xaml
	/// </summary>
	public partial class DocAdd : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.documentsTableAdapter docs;

		DataSet.usersRow user;

		public DocAdd(DataSet.usersRow user)
		{
			InitializeComponent();

			dataSet = new DataSet();
			docs = new DataSetTableAdapters.documentsTableAdapter();
			docs.Fill(dataSet.documents);

			this.user = user;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var lastId = (from d in dataSet.documents select d.id).LastOrDefault() + 1;
			var name = nameTextBox.Text.Length == 0 ? "Не указано" : nameTextBox.Text;
			var source = srcTextBox.Text.Length == 0 ? "Не указано" : srcTextBox.Text;

			dataSet.Tables["documents"].Rows.Add(lastId, name, source, user.id, DateTime.Now);
			docs.Adapter.Update(dataSet, "documents");
			
			NavigationService.GoBack();
		}
		private void Back_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
	}
}
