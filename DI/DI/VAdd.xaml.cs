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
	/// Добавление новой переменной
	/// </summary>
	public partial class VAdd : Page
    {
        DIC objectDB = new DIC();
        public VAdd()
        {
            InitializeComponent();
        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            errors.Content = "";
            var value = var_name.Text;
            if (value == "") errors.Content = "Заполните поле";
            else {
                if (objectDB.fields.Where(i => i.name == value).Count() > 0) errors.Content = "Такая переменная уже существует";
                else {
                    var field = new fields() { name = value };
                    objectDB.fields.Add(field);
                    objectDB.SaveChanges();
                    errors.Content = "Переменная успешно добавлена";
                    var_name.Clear();
                }
            }
        }
    }
}
