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
	/// Логика взаимодействия для Docs.xaml
	/// </summary>
	public partial class Docs : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.documentsTableAdapter docs;
		DataSetTableAdapters.usersTableAdapter users;

		DataSet.usersRow user;

		public Docs(DataSet.usersRow user)
		{
			InitializeComponent();

			dataSet = new DataSet();
			docs = new DataSetTableAdapters.documentsTableAdapter();
			users = new DataSetTableAdapters.usersTableAdapter();

			if(user.role == 0)
			{
				add.Visibility = Visibility.Collapsed;
				edit.Visibility = Visibility.Collapsed;
				remove.Visibility = Visibility.Collapsed;

				loginTextBox.Text = user.login;
				docFindUser.Visibility = Visibility.Collapsed;
				docFind.Items.Remove(docFindUser);
			}

			this.user = user;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			docs.Fill(dataSet.documents);
			users.Fill(dataSet.users);
			FillDataGrid();
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new DocAdd(user));
		}
		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			/*foreach(var dg in docsDataGrid.ItemsSource.Cast<DGTable>())
			{
				var docRow = (from d in dataSet.documents
							  where d.id == dg.idDoc
							  select d).FirstOrDefault();

				var table = dataSet.Tables["documents"];
				if(docRow == null)
					table.Rows.Add(
						dg.idDoc,
						dg.nameDoc,
						dg.source,
						dg.idUser,
						dg.createDate);
				else
					table.Rows[table.Rows.IndexOf(docRow)].ItemArray = new object[] { dg.idDoc, dg.nameDoc, dg.source, dg.idUser, dg.createDate };
			}
			docs.Adapter.Update(dataSet, "documents");*/
		}
		private void Remove_Click(object sender, RoutedEventArgs e)
		{
			foreach(var doc in docsDataGrid.SelectedItems.Cast<DGTable>())
			{
				var docRow = (from d in dataSet.documents
							  where d.id == doc.id
							  select d).First();

				docs.Delete(docRow.id, docRow.name, docRow.src, docRow.user_id, docRow.create_date);
				dataSet.documents.Rows.Remove(docRow);
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
		private void createDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(); }

		void FillDataGrid()
		{
			docsDataGrid.ItemsSource = (from d in dataSet.documents join u in dataSet.users
										on d.user_id equals u.id
										where (nameTextBox.Text.Length == 0) || d.name.ContainsIgnoreCase(nameTextBox.Text)
										where (loginTextBox.Text.Length == 0) || u.login.ContainsIgnoreCase(loginTextBox.Text)
										where !createDate.SelectedDate.HasValue ||
											  createDate.SelectedDate.Value.ToShortDateString() == d.create_date.ToShortDateString()
										select new DGTable(d.id, d.name, d.src, u.login, d.create_date)).ToArray();

			for(int i = 0; i < docsDataGrid.Columns.Count; i++)
				docsDataGrid.Columns[i].Header = DGTable.Names[i];
		}

		class DGTable
		{
			public static string[] Names = { "№ документа", "Название", "Расположение", "Логин пользователя", "Дата создания" };

			public DGTable(int id, string name, string source, string login, DateTime createDate)
			{
				this.id = id;
				this.name = name;
				this.source = source;
				this.login = login;
				this.createDate = createDate.ToShortDateString() + " " + createDate.ToLongTimeString();
			}
			public int id { get; set; }
			public string name { get; set; }
			public string source { get; set; }
			public string login { get; set; }
			public string createDate { get; set; }
		}
	}
}
