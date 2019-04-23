using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace DI
{
    /// <summary>
    /// Главная страница
    /// </summary>
    public partial class Home : Page
    {

        DIC objectDB = new DIC();
        public Home()
        {
            InitializeComponent();
        }

        private void home_load(object sender, RoutedEventArgs e)
        {
			WindowTitle = "Программа для расчета очистительных установок";
            var object_companies = objectDB.companies;
            this.label_count.Content = object_companies.Count();
            var data = object_companies.ToList();
            for (int i = 0; i < data.Count; i++)
            {
                this.list_companies.Items.Add(new KeyValuePair<int, string>(data[i].id, data[i].name));
                Console.Write(data[i].name);
            }

        }

        private void add_сlick(object sender, RoutedEventArgs e)
        {
            Add expenseAdd = new Add();
            this.NavigationService.Navigate(expenseAdd);
        }

        private void select_сlick(object sender, RoutedEventArgs e)
        {
            this.errors.Content = "";
            if (this.list_companies.SelectedIndex == -1) this.errors.Content = "Выберите компанию";
            else {
                var dictionary = (KeyValuePair<int, string>)this.list_companies.SelectedValue;
                Select expenseSelect = new Select(dictionary);
                this.NavigationService.Navigate(expenseSelect);
            }
        }

        private void delete_сlick(object sender, RoutedEventArgs e)
        {
            this.errors.Content = "";
            if (this.list_companies.SelectedIndex == -1) this.errors.Content = "Выберите компанию";
            else {
                var object_companies = objectDB.companies;
                var dictionary = (KeyValuePair<int, string>)this.list_companies.SelectedValue;
                var company = object_companies.Where(i => i.id == dictionary.Key).Single();
                if (company != null)
                {
                    var communications = objectDB.communication.Where(j => j.company_id == company.id).ToList();
                    object_companies.Remove(company);
                    for(int k = 0;k < communications.Count;k++)
                        objectDB.communication.Remove(communications[k]);
                    objectDB.SaveChanges();
                    this.list_companies.Items.RemoveAt(this.list_companies.SelectedIndex);
                    this.label_count.Content = (int)this.label_count.Content - 1;
                }
                
            }
        }

        private void update_сlick(object sender, RoutedEventArgs e)
        {
            this.errors.Content = "";
            if (this.list_companies.SelectedIndex == -1) this.errors.Content = "Выберите компанию";
            else {
                var dictionary = (KeyValuePair<int, string>)this.list_companies.SelectedValue;
                Updata expenseUpdata = new Updata(dictionary);
                this.NavigationService.Navigate(expenseUpdata);
            }
        }

        private void add_var_click(object sender, RoutedEventArgs e)
        {
            VAdd expenseVAdd = new VAdd();
            this.NavigationService.Navigate(expenseVAdd);
		}
		private void help_click(object sender, RoutedEventArgs e)
		{
			Help help = new Help();
			this.NavigationService.Navigate(help);
		}
	}
}
