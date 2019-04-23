using System;
using System.Collections.Generic;
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
    /// Редактирование переменных
    /// </summary>
    public partial class Updata : Page
    {
        private KeyValuePair<int, string> company;
        DIC objectDB = new DIC();
        public Updata(KeyValuePair<int, string> company)
        {
            this.company = company;
            InitializeComponent();
        }

        private void loaded_update_page(object sender, RoutedEventArgs e)
		{
			this.WindowTitle = "Редактирование компании - " + this.company.Value;
            var communications = objectDB.communication.Where(i => i.company_id == company.Key).ToList();
            if (communications != null)
            {
                List<DTable> result = new List<DTable>(communications.Count());
                for (int i = 0; i < communications.Count; i++)
                {
                    int id = communications[i].field_id;
                    var field = objectDB.fields.Where(j => j.id == id).ToList();
                    if (field != null)
                        result.Add(new DTable(field[0].name, Math.Round(double.Parse(communications[i].value), 2).ToString()));
                    else continue;
                }
                data_grid_updata.ItemsSource = result;
            }
		}

		//Сохранение по выходе из режима редактирования
		private void Page_Unloaded(object sender, RoutedEventArgs e)
		{
			var comm = objectDB.communication.Where(i => i.company_id == company.Key).AsEnumerable();
			int count = comm.Count();
			for(int i = 0; i < count; i++)
			{
				DTable dtable = data_grid_updata.Items[i] as DTable;
				comm.ElementAt(i).value = dtable.value;
			}
			objectDB.SaveChanges();
		}
	}
}
