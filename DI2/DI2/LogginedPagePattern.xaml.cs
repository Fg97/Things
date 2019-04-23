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
    /// Логика взаимодействия для LogginedPagePattern.xaml
    /// </summary>
    public partial class LogginedPagePattern : Page
    {
		DataSet.usersRow user;
		
		public LogginedPagePattern(DataSet.usersRow user)
		{
			InitializeComponent();
			label_FIO.Text += user.name;

			this.user = user;
			innerPage.Content = new Bids(user);
		}
		
		private void Bids_Click(object sender, RoutedEventArgs e)
		{
			innerPage.NavigationService.Navigate(new Bids(user));
		}
		private void Clients_Click(object sender, RoutedEventArgs e)
		{
			innerPage.NavigationService.Navigate(new Clients(user));
		}
		private void Users_Click(object sender, RoutedEventArgs e)
		{
			innerPage.NavigationService.Navigate(new Users(user));
		}
		private void Docs_Click(object sender, RoutedEventArgs e)
		{
			innerPage.NavigationService.Navigate(new Docs(user));
		}
		private void Equips_Click(object sender, RoutedEventArgs e)
		{
			innerPage.NavigationService.Navigate(new Equips(user));
		}
		private void Shares_Click(object sender, RoutedEventArgs e)
		{
			innerPage.NavigationService.Navigate(new Shares(user));
		}
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
	}
}
