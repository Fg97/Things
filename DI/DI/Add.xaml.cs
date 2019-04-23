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
	/// Добавление компаний
	/// </summary>
	public partial class Add : Page
    {
        DIC objectDB = new DIC();
        private static int var_count;
        public Add()
        {
            InitializeComponent();
            this.add_data_collumn();
        }
        private void add_data_collumn()
        {
            var fields = objectDB.fields.ToList();
            Add.var_count = fields.Count();
            if (fields != null)
            {
                List<DTable> result = new List<DTable>(Add.var_count);
                for (int i = 0; i < Add.var_count; i++)
                {
                    result.Add(new DTable(fields[i].name, null));
                }
                add_data.ItemsSource = result;
            }
        }
        private void add_click(object sender, RoutedEventArgs e)
        {
            errors.Content = "";
            var name_company = copany_name.Text;
            if (name_company == "") errors.Content = "Заполните поле имени компании";
            else
            {
                if (objectDB.companies.Where(k => k.name == name_company).Count() > 0) errors.Content = "Такая компания уже существует"; 
                else {
                    var company = new companies() { name = name_company };
                    objectDB.companies.Add(company);
                    objectDB.SaveChanges();
                    var company_id = company.id;

                    add_data.SelectAll();
                    List<DTable> res = add_data.SelectedItems.OfType<DTable>().ToList();
                    for (int i = 0; i < res.Count(); i++)
                    {
                        var key = res[i].key.ToString();
                        var field_id = objectDB.fields.Where(j => j.name == key).Single().id;
                        var field_value = res[i].value;
                        if (field_value == "" || field_value == null) {
                            errors.Content = "Заполните все поля значений";

                            objectDB.companies.Remove(company);
                            objectDB.SaveChanges();
                            break;
                        }
                        else {
                            var com = new communication() { field_id = field_id, value = field_value, company_id = company_id };
                            objectDB.communication.Add(com);
                            objectDB.SaveChanges();
                            errors.Content = "Запись успешно добавлена";
                            copany_name.Clear();
                            add_data.ItemsSource = null;
                            this.add_data_collumn();
                        }
                    }
                }
            }
        }
    }
}
