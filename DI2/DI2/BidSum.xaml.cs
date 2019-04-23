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
    /// Логика взаимодействия для BidSum.xaml
    /// </summary>
    public partial class BidSum : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.bidsTableAdapter bids;
		DataSetTableAdapters.guestsTableAdapter guests;
		DataSetTableAdapters.equipmentsTableAdapter equips;
		DataSetTableAdapters.sharesTableAdapter shares;
		DataSetTableAdapters.link_bid_equipTableAdapter bid_equip;
		DataSetTableAdapters.link_share_equipTableAdapter share_equip;

		DataSet.usersRow user;

		enum Type { Client, Manager }

		public BidSum(DataSet.usersRow user)
		{
			InitializeComponent();

			dataSet = new DataSet();
			bids = new DataSetTableAdapters.bidsTableAdapter();
			guests = new DataSetTableAdapters.guestsTableAdapter();
			equips = new DataSetTableAdapters.equipmentsTableAdapter();
			shares = new DataSetTableAdapters.sharesTableAdapter();
			bid_equip = new DataSetTableAdapters.link_bid_equipTableAdapter();
			share_equip = new DataSetTableAdapters.link_share_equipTableAdapter();

			bids.Fill(dataSet.bids);
			guests.Fill(dataSet.guests);
			equips.Fill(dataSet.equipments);
			shares.Fill(dataSet.shares);
			bid_equip.Fill(dataSet.link_bid_equip);
			share_equip.Fill(dataSet.link_share_equip);

			this.user = user;
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
		private void clientTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			clientComboBox.Items.Clear();

			var clients = (from g in dataSet.guests
						   where (clientTextBox.Text.Length != 0) && g.name.ContainsIgnoreCase(clientTextBox.Text)
						   select new[] { g.id.ToString(), g.name, g.data }).ToArray();

			clientComboBox.IsEnabled = (clients.Length != 0);
			if(!clientComboBox.IsEnabled) return;

			for(int client = 0; client < clients.Length; client++)
			{
				var panel = new StackPanel { Orientation = clientPattern.Orientation };
				for(int i = 0; i < clientPattern.Children.Count; i++)
				{
					var pattern = (TextBlock)clientPattern.Children[i];

					panel.Children.Add(new TextBlock
					{
						Text = clients[client][i],
						HorizontalAlignment = pattern.HorizontalAlignment,
						VerticalAlignment = pattern.VerticalAlignment,
						TextAlignment = pattern.TextAlignment,
						MinHeight = pattern.MinHeight,
						MinWidth = pattern.MinWidth,
					});
				}
				clientComboBox.Items.Add(panel);
			}
		}

		private void clientComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var client = ((ComboBox)sender).SelectedItem as StackPanel;

			if(client == null)
			{
				clientDataGrid.ItemsSource = null;
				return;
			}

			var guest = (from g in dataSet.guests
						 where g.id.ToString() == ((TextBlock)client.Children[0]).Text
						 select g).First();

			clientDataGrid.ItemsSource = new DGTable[]
			{
				new DGTable(Type.Client, guest.id, guest.name, guest.data, CountingSum(guest).ToString()),
				new DGTable(Type.Manager, user.id, user.name, "", "")
			};

			for(int i = 0; i < clientDataGrid.Columns.Count; i++)
				clientDataGrid.Columns[i].Header = DGTable.Names[i];
		}
		int CountingSum(DataSet.guestsRow guest)
		{
			var sum = from b in dataSet.bids join l in dataSet.link_bid_equip
					  on b.id equals l.bid_id join e in dataSet.equipments
					  on l.equipment_id equals e.id join l2 in dataSet.link_share_equip
					  on e.id equals l2.equipment_id join s in dataSet.shares
					  on l2.share_id equals s.id
					  where b.guest_id == guest.id
					  select (int)((b.amount * e.price) * (1 - s.discount / 100.0f));
			
			return sum.Sum();
		}

		class DGTable
		{
			public static string[] Names = { "Пользователь", "Id в БД", "Ф.И.О.", "Данные клиента", "Сумма заявки" };

			public DGTable(Type type, int id, string name, string clientData, string clientSum)
			{
				this.type = type;
				this.id = id;
				this.name = name;
				this.clientData = clientData;
				this.clientSum = clientSum;
			}
			public Type type { get; set; }
			public int id { get; set; }
			public string name { get; set; }
			public string clientData { get; set; }
			public string clientSum { get; set; }
		}
	}
}
