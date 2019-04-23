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
    /// Справка
    /// </summary>
    public partial class Help : Page
    {
        DIC objectDB = new DIC();
        public Help()
        {
            InitializeComponent();
        }

        private void help_load(object sender, RoutedEventArgs e)
		{
			this.WindowTitle = "Справка по переменным";
			var fields = objectDB.fields.ToList();
			if(fields != null)
			{
				List<HTable> result = new List<HTable>(fields.Count());
				for(int i = 0; i < fields.Count; i++)
				{
					result.Add(new HTable(fields[i].name, fields[i].description, fields[i].measure));
				}
				main_grid.ItemsSource = result;
			}
		}
    }
}
