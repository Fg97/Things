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
    /// Логика взаимодействия для BidAdd.xaml
    /// </summary>
    public partial class BidAdd : Page
    {
		DataSet dataSet;
		DataSetTableAdapters.bidsTableAdapter bids;
		DataSetTableAdapters.guestsTableAdapter guests;
		DataSetTableAdapters.equipmentsTableAdapter equips;
		DataSetTableAdapters.sharesTableAdapter shares;
		DataSetTableAdapters.link_bid_equipTableAdapter bid_equip;
		DataSetTableAdapters.link_share_equipTableAdapter share_equip;

		DataSet.usersRow user;
		
		public BidAdd(DataSet.usersRow user)
        {
            InitializeComponent();

			equipListBox.Items.Remove(equipPattern);

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
		private void EquipAdd_Click(object sender, RoutedEventArgs e)
		{
			var panel = new StackPanel { Orientation = equipPattern.Orientation };

			for(int i = 0; i < equipPattern.Children.Count; i++)
			{
				var pattern = (TextBox)equipPattern.Children[i];
				var text = new TextBox
				{
					BorderThickness = pattern.BorderThickness,
					Text = pattern.Text,
					HorizontalAlignment = pattern.HorizontalAlignment,
					VerticalAlignment = pattern.VerticalAlignment,
					TextAlignment = pattern.TextAlignment,
					Width = pattern.Width
				};
				text.TextChanged += equipListBoxTextBoxes_TextChanged;

				panel.Children.Add(text);
			}

			equipListBox.Items.Add(panel);
		}
		private void EquipDelete_Click(object sender, RoutedEventArgs e)
		{
			var selected = new List<object>();
			foreach(var item in equipListBox.SelectedItems) selected.Add(item);
			foreach(var item in selected) equipListBox.Items.Remove(item);
			
			CountingSum();
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var lastBidId = (from b in dataSet.bids select b.id).LastOrDefault();
			var lastLinkId = (from l in dataSet.link_bid_equip select l.id).LastOrDefault();

			var equipTakeDate = takeDate.SelectedDate.Value.Date;
			var equipRefundDate = refundDate.SelectedDate.Value.Date;
			string[] takeTimeString = takeTime.Text.Split(':'),
				refundTimeString = refundTime.Text.Split(':');

			equipTakeDate.Add(new TimeSpan(
				int.Parse(takeTimeString[0]),
				int.Parse(takeTimeString[1]),
				int.Parse(takeTimeString[2])));
			equipRefundDate.Add(new TimeSpan(
				int.Parse(refundTimeString[0]),
				int.Parse(refundTimeString[1]),
				int.Parse(refundTimeString[2])));

			byte state = 0;
			if(usingRadioButton.IsChecked.Value)
			{
				state = 1;
				equipTakeDate = DateTime.Now;
			}

			foreach(var panel in equipListBox.Items.Cast<StackPanel>())
			{
				lastBidId++; lastLinkId++;
				var textBoxes = (from box in panel.Children.Cast<TextBox>() select box).ToArray();

				var client = ((StackPanel)clientComboBox.SelectedItem)
								.Children.Cast<TextBlock>().First();

				dataSet.Tables["bids"].Rows.Add(
					lastBidId,
					int.Parse(client.Text),
					user.id,
					state,
					int.Parse(textBoxes[1].Text),
					equipTakeDate,
					equipRefundDate);

				dataSet.Tables["link_bid_equip"].Rows.Add(
					lastLinkId,
					lastBidId,
					int.Parse(textBoxes[0].Text));

				if(state == 1)
				{
					var row = (from eq in dataSet.equipments
							   where eq.id.ToString() == textBoxes[0].Text
							   select eq).First();

					var table = dataSet.Tables["equipments"];
					table.Rows[table.Rows.IndexOf(row)].ItemArray =
						new object[] { row.id, row.title, row.description, row.image, row.price, row.goods - int.Parse(textBoxes[1].Text) };
					equips.Adapter.Update(table);
				}
			}
			bids.Adapter.Update(dataSet, "bids");
			bid_equip.Adapter.Update(dataSet, "link_bid_equip");

			NavigationService.GoBack();
		}

		private void equipListBoxTextBoxes_TextChanged(object sender, TextChangedEventArgs e)
		{
			CountingSum();
		}
		void CountingSum()
		{
			int sum = 0, sumShare = 0;
			foreach(var panel in equipListBox.Items.Cast<StackPanel>())
			{
				var textBoxes = (from box in panel.Children.Cast<TextBox>() select box).ToArray();
				TextBox equipTextBox = textBoxes[0], countTextBox = textBoxes[1];

				if(equipTextBox.Text.Length != 0 && countTextBox.Text.Length != 0)
				{
					var price = (from e in dataSet.equipments
								 where e.id.ToString() == equipTextBox.Text
								 select e.price).FirstOrDefault();

					int p = int.Parse(countTextBox.Text) * price;
					sum += p;

					var discount = (from e in dataSet.equipments join l in dataSet.link_share_equip
									on e.id equals l.equipment_id join s in dataSet.shares
									on l.share_id equals s.id
									where e.id.ToString() == equipTextBox.Text
									select s.discount).FirstOrDefault();
					sumShare += (int)(p * (1 - discount / 100.0f));
				}
			}
			sumTextBox.Text = sum.ToString();
			sumShareTextBox.Text = sumShare.ToString();
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

		private void usingRadioButton_Checked(object sender, RoutedEventArgs e)
		{
			takeDate.IsEnabled = false;
		}
		private void openRadioButton_Checked(object sender, RoutedEventArgs e)
		{
			if(IsLoaded) takeDate.IsEnabled = true;
		}
	}
}
