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
	/// Логика взаимодействия для Bids.xaml
	/// </summary>
	public partial class Bids : Page
	{
		DataSet dataSet;
		DataSetTableAdapters.bidsTableAdapter bids;
		DataSetTableAdapters.usersTableAdapter users;
		DataSetTableAdapters.guestsTableAdapter guests;
		DataSetTableAdapters.equipmentsTableAdapter equips;
		DataSetTableAdapters.link_bid_equipTableAdapter bid_equip;

		DataSet.usersRow user;

		BidState state;
		enum BidState { All = -1, Open, Using, Close }
		
		public Bids(DataSet.usersRow user)
		{
			InitializeComponent();

			dataSet = new DataSet();
			bids = new DataSetTableAdapters.bidsTableAdapter();
			users = new DataSetTableAdapters.usersTableAdapter();
			guests = new DataSetTableAdapters.guestsTableAdapter();
			equips = new DataSetTableAdapters.equipmentsTableAdapter();
			bid_equip = new DataSetTableAdapters.link_bid_equipTableAdapter();

			if(user.role == 0)
			{
				add.Visibility = Visibility.Collapsed;
				remove.Visibility = Visibility.Collapsed;
			}

			this.user = user;
			state = BidState.All;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			bids.Fill(dataSet.bids);
			users.Fill(dataSet.users);
			guests.Fill(dataSet.guests);
			equips.Fill(dataSet.equipments);
			bid_equip.Fill(dataSet.link_bid_equip);

			FillDataGrid(state);
		}

		private void BidDelete_Click(object sender, RoutedEventArgs e)
		{
			foreach(var bid in bidsDataGrid.SelectedItems.Cast<DGTable>())
			{
				var linkRow = (from l in dataSet.link_bid_equip
							   where l.bid_id == bid.idBid
							   select l).First();

				bid_equip.Delete(linkRow.id, linkRow.bid_id, linkRow.equipment_id);
				dataSet.link_bid_equip.Rows.Remove(linkRow);

				var bidRow = (from b in dataSet.bids
							  where b.id == bid.idBid
							  select b).First();

				bids.Delete(bidRow.id, bidRow.guest_id, bidRow.user_id, bidRow.state, bidRow.amount, bidRow.create_date, bidRow.equipments_refund_date);
				dataSet.bids.Rows.Remove(bidRow);
			}
			FillDataGrid(state);
		}
		private void BidAdd_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new BidAdd(user));
		}
		private void BidSum_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new BidSum(user));
		}
		private void BidToUsing_Click(object sender, RoutedEventArgs e)
		{
			UpdateBidState(BidState.Using);
		}
		private void BidToClose_Click(object sender, RoutedEventArgs e)
		{
			UpdateBidState(BidState.Close);
		}
		void UpdateBidState(BidState state)
		{
			var table = dataSet.Tables["bids"];
			var tableEq = dataSet.Tables["equipments"];
			var selected = bidsDataGrid.SelectedItems.Cast<DGTable>();
			foreach(var bid in selected)
			{
				if(bid.state == (int)state) continue;
				bid.state = (byte)state;

				var bidRow = (from b in dataSet.bids
							  where b.id == bid.idBid
							  select b).First();

				table.Rows[table.Rows.IndexOf(bidRow)].ItemArray =
					new object[] { bidRow.id, bidRow.guest_id, bidRow.user_id, bid.state,
						bidRow.amount, bidRow.create_date, bidRow.equipments_refund_date };

				var equipRow = (from e in dataSet.equipments
								where e.id == bid.idEquipment
								select e).First();

				int sign = 0;
				switch(state)
				{
					case BidState.Using: sign = -1; break;
					case BidState.Close: sign = 1; break;
				}

				tableEq.Rows[tableEq.Rows.IndexOf(equipRow)].ItemArray =
					new object[] { equipRow.id, equipRow.title, equipRow.description,
						equipRow.image, equipRow.price, equipRow.goods + sign * bid.amount };
			}
			bids.Adapter.Update(table);
			equips.Adapter.Update(tableEq);
		}
		
		private void clientTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{ FillDataGrid(state); }
		private void dataTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{ FillDataGrid(state); }
		private void userTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{ FillDataGrid(state); }
		private void equipTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{ FillDataGrid(state); }
		private void takeDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(state); }
		private void takeDateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(state); }
		private void refundDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(state); }
		private void refundDateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{ FillDataGrid(state); }
		private void allRadioButton_Checked(object sender, RoutedEventArgs e)
		{ FillDataGrid(BidState.All); }
		private void usingRadioButton_Checked(object sender, RoutedEventArgs e)
		{ FillDataGrid(BidState.Using); }
		private void openRadioButton_Checked(object sender, RoutedEventArgs e)
		{ FillDataGrid(BidState.Open); }
		private void closeRadioButton_Checked(object sender, RoutedEventArgs e)
		{ FillDataGrid(BidState.Close); }

		void FillDataGrid(BidState state)
		{
			if(!IsLoaded) return;

			this.state = state;

			var list = new List<DGTable>();
			foreach(var bid in dataSet.bids)
			{
				var client = (from g in dataSet.guests
							  where g.id == bid.guest_id
							  select g).First();
				var user = (from u in dataSet.users
							where u.id == bid.user_id
							select u).First();

				if(clientTextBox.Text.Length != 0 && !client.name.ContainsIgnoreCase(clientTextBox.Text)) continue;
				if(dataTextBox.Text.Length != 0 && !client.data.ContainsIgnoreCase(dataTextBox.Text)) continue;
				if(userTextBox.Text.Length != 0 && !user.login.ContainsIgnoreCase(userTextBox.Text)) continue;

				if(takeDate.SelectedDate.HasValue &&
					!FindInComboBox(takeDateComboBox, bid.create_date.Date, takeDate.SelectedDate.Value.Date)) continue;
				if(refundDate.SelectedDate.HasValue &&
					!FindInComboBox(takeDateComboBox, bid.equipments_refund_date.Date, refundDate.SelectedDate.Value.Date)) continue;

				if(bid.state == (int)state || state == BidState.All)
					foreach(var link in dataSet.link_bid_equip)
					{
						if(equipTextBox.Text.Length != 0 && link.equipment_id.ToString() != equipTextBox.Text) continue;

						if(link.bid_id == bid.id)
						{
							list.Add(new DGTable(bid.id, client.name, client.data, user.login, link.equipment_id, bid.state,
												 bid.amount, bid.create_date, bid.equipments_refund_date));
						}
					}
			}
			bidsDataGrid.ItemsSource = list;

			for(int i = 0; i < bidsDataGrid.Columns.Count; i++)
				bidsDataGrid.Columns[i].Header = DGTable.Names[i];
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
			public static string[] Names = { "№ заявки", "Ф.И.О. клиента", "Данные клиента", "Логин пользователя", "№ снаряжения",
											 "Статус", "Количество", "Дата выдачи", "Дата возврата" };

			public DGTable(int idBid, string client, string data, string user, int idEquipment, byte state, int amount, DateTime createDate, DateTime refundDate)
			{
				this.idBid = idBid;
				this.client = client;
				this.data = data;
				this.user = user;
				this.idEquipment = idEquipment;
				this.state = state;
				this.amount = amount;
				this.createDate = createDate.ToShortDateString() + " " + createDate.ToLongTimeString();
				this.refundDate = refundDate.ToShortDateString() + " " + refundDate.ToLongTimeString();
			}
			public int idBid { get; set; }
			public string client { get; set; }
			public string data { get; set; }
			public string user { get; set; }
			public int idEquipment { get; set; }
			public byte state { get; set; }
			public int amount { get; set; }
			public string createDate { get; set; }
			public string refundDate { get; set; }
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			if(NavigationService.CanGoBack)
				NavigationService.GoBack();
		}
	}
}
