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
    /// Выбор компании
    /// </summary>
    public partial class Select : Page
    {
		/// <summary>
		/// Перечисление имен полей для удобства обращения к полям БД.
		/// Числовые значения констант перечисления соответствуют id поля
		/// </summary>
		enum Data { q = 1, L_ln, L_ex, a, S, p_max, C_0, K_l, K_0, phee, n_at, n_cor, H_at, b_cor, W_i, C_cdp, K_g, J_i,
					p, t_atm, q_i, R_i, W_atm, L_at, P_i }

        private KeyValuePair<int, string> company;
        private Dictionary<string, double> array_vars;
        DIC objectDB = new DIC();

        public Select(KeyValuePair<int, string> company)
        {
            this.company = company;
            this.array_vars = new Dictionary<string, double>();
            InitializeComponent();
        }

        private void loaded_select_page(object sender, RoutedEventArgs e)
        {
            this.WindowTitle = "Выбрана компания - " + this.company.Value;
            var communications = objectDB.communication.Where(i => i.company_id == this.company.Key).ToList();
            if (communications != null) { 
                List<DTable> result = new List<DTable>(communications.Count());
                for (int i = 0; i < communications.Count; i++)
                {
                    int id = communications[i].field_id;
                    var field = objectDB.fields.Where(j => j.id == id).ToList();
                    if (field != null)
                    {
						//добавление значений из БД в список с округлением до двух знаков после запятой
                        result.Add(new DTable(field[0].name, Math.Round(double.Parse(communications[i].value), 2).ToString()));
                        this.array_vars.Add(field[0].name, double.Parse(communications[i].value));
                    }
                    else continue;
                }
                main_select_grid.ItemsSource = result;
            }
        }

		/// <summary>
		/// Считывание значения поля <paramref name="name"/> из БД
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		double v(Data name) { return array_vars[name.ToString()]; }
		/// <summary>
		/// Запись значения <paramref name="value"/> поля <paramref name="name"/> в БД
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		void sv(Data name, double value) { array_vars[name.ToString()] = value; }

		//Расчитать W_atm и L_at при известном q
		private void calculate_click(object sender, RoutedEventArgs e)
        {
			double p, t_atm, q_i, R_i, W_atm, L_at, P_i;

            p = v(Data.p_max) * v(Data.L_ex) * v(Data.C_0);
			p /= 1 + v(Data.phee) * v(Data.a);
			p /= v(Data.L_ex) * v(Data.C_0) + v(Data.K_l) * v(Data.C_0) + v(Data.K_0) * v(Data.L_ex);
			sv(Data.p, p);

			t_atm = v(Data.L_ln) - v(Data.L_ex);
			t_atm /= v(Data.a) * (1 - v(Data.S)) * v(Data.p);
			sv(Data.t_atm, t_atm);

			q_i = 24 * (v(Data.L_ln) - v(Data.L_ex));
			q_i /= v(Data.a) * (1 - v(Data.S)) * v(Data.t_atm);
			sv(Data.q_i, q_i);

			R_i = v(Data.a);
			R_i /= 1000 / v(Data.J_i) - v(Data.a);
			sv(Data.R_i, R_i);

			W_atm = v(Data.q) * v(Data.t_atm);
			sv(Data.W_atm, W_atm);

			L_at = v(Data.W_atm) / (v(Data.n_at) * v(Data.n_cor) * v(Data.b_cor) * v(Data.H_at));
			sv(Data.L_at, L_at);

			P_i = 0.8 * v(Data.C_cdp) + v(Data.K_g) * v(Data.L_at);
			sv(Data.P_i, P_i);

			//запись вычесленных значений в БД
			var comm = objectDB.communication.Where(i => i.company_id == company.Key).AsEnumerable();
			int count = comm.Count();
			for(int i = Data.p - Data.q; i < count;)
				comm.ElementAt(i).value = v((Data)(++i)).ToString();
			objectDB.SaveChanges();

			//вывод результатов расчета W_atm и L_at
			var comm1 = comm.Where(i => i.field_id == (int)Data.L_at || i.field_id == (int)Data.W_atm).ToArray();
			var field = objectDB.fields.Where(i => i.id == (int)Data.L_at || i.id == (int)Data.W_atm).ToArray();

			List<DTable> result = new List<DTable>(comm1.Count());
			for(int i = 0; i < result.Capacity; i++)
				result.Add(new DTable(field[i].name, Math.Round(double.Parse(comm1[i].value), 2).ToString()));

			result_select_grid.ItemsSource = result;
		}
		//Расчитать q при известном W_atm
		private void obr_calculate_Click(object sender, RoutedEventArgs e)
		{
			double p, t_atm, q_i, R_i, L_at, P_i, q;

			p = v(Data.p_max) * v(Data.L_ex) * v(Data.C_0);
			p /= 1 + v(Data.phee) * v(Data.a);
			p /= v(Data.L_ex) * v(Data.C_0) + v(Data.K_l) * v(Data.C_0) + v(Data.K_0) * v(Data.L_ex);
			sv(Data.p, p);

			t_atm = v(Data.L_ln) - v(Data.L_ex);
			t_atm /= v(Data.a) * (1 - v(Data.S)) * v(Data.p);
			sv(Data.t_atm, t_atm);

			q_i = 24 * (v(Data.L_ln) - v(Data.L_ex));
			q_i /= v(Data.a) * (1 - v(Data.S)) * v(Data.t_atm);
			sv(Data.q_i, q_i);

			R_i = v(Data.a);
			R_i /= 1000 / v(Data.J_i) - v(Data.a);
			sv(Data.R_i, R_i);

			//через W_atm
			q = v(Data.W_atm) / v(Data.t_atm);
			sv(Data.q, q);

			//вычисление L_at
			L_at = v(Data.W_atm) / (v(Data.n_at) * v(Data.n_cor) * v(Data.b_cor) * v(Data.H_at));
			sv(Data.L_at, L_at);

			P_i = 0.8 * v(Data.C_cdp) + v(Data.K_g) * v(Data.L_at);
			sv(Data.P_i, P_i);

			//запись вычесленных значений в БД
			var comm = objectDB.communication.Where(i => i.company_id == company.Key).AsEnumerable();
			int count = comm.Count();

			comm.ElementAt((int)Data.q - 1).value = v(Data.q).ToString();
			for(int i = Data.p - Data.q; i < count;)
				comm.ElementAt(i).value = v((Data)(++i)).ToString();
			objectDB.SaveChanges();

			//вывод результатов расчета q
			var comm1 = comm.Where(i => i.field_id == (int)Data.q).ToArray();
			var field = objectDB.fields.Where(i => i.id == (int)Data.q).ToArray();

			List<DTable> result = new List<DTable>(comm1.Count());
			for(int i = 0; i < result.Capacity; i++)
				result.Add(new DTable(field[i].name, Math.Round(double.Parse(comm1[i].value), 2).ToString()));

			result_select_grid.ItemsSource = result;
		}
		//Расчитать q при известном L_at
		private void obr_calculate2_Click(object sender, RoutedEventArgs e)
		{
			double p, t_atm, q_i, R_i, W_atm, P_i, q;

			p = v(Data.p_max) * v(Data.L_ex) * v(Data.C_0);
			p /= 1 + v(Data.phee) * v(Data.a);
			p /= v(Data.L_ex) * v(Data.C_0) + v(Data.K_l) * v(Data.C_0) + v(Data.K_0) * v(Data.L_ex);
			sv(Data.p, p);

			t_atm = v(Data.L_ln) - v(Data.L_ex);
			t_atm /= v(Data.a) * (1 - v(Data.S)) * v(Data.p);
			sv(Data.t_atm, t_atm);

			q_i = 24 * (v(Data.L_ln) - v(Data.L_ex));
			q_i /= v(Data.a) * (1 - v(Data.S)) * v(Data.t_atm);
			sv(Data.q_i, q_i);

			R_i = v(Data.a);
			R_i /= 1000 / v(Data.J_i) - v(Data.a);
			sv(Data.R_i, R_i);

			//через L_at
			q = v(Data.L_at) / v(Data.t_atm);
			q *= v(Data.n_at) * v(Data.n_cor) * v(Data.b_cor) * v(Data.H_at);
			sv(Data.q, q);

			//вычисление W_atm
			W_atm = v(Data.q) * v(Data.t_atm);
			sv(Data.W_atm, W_atm);

			P_i = 0.8 * v(Data.C_cdp) + v(Data.K_g) * v(Data.L_at);
			sv(Data.P_i, P_i);

			//запись вычесленных значений в БД
			var comm = objectDB.communication.Where(i => i.company_id == company.Key).AsEnumerable();
			int count = comm.Count();

			comm.ElementAt((int)Data.q - 1).value = v(Data.q).ToString();
			for(int i = Data.p - Data.q; i < count;)
				comm.ElementAt(i).value = v((Data)(++i)).ToString();
			objectDB.SaveChanges();

			//вывод результатов расчета q
			var comm1 = comm.Where(i => i.field_id == (int)Data.q).ToArray();
			var field = objectDB.fields.Where(i => i.id == (int)Data.q).ToArray();

			List<DTable> result = new List<DTable>(comm1.Count());
			for(int i = 0; i < result.Capacity; i++)
				result.Add(new DTable(field[i].name, Math.Round(double.Parse(comm1[i].value), 2).ToString()));

			result_select_grid.ItemsSource = result;
		}
		//Расчитать t_atm при известном W_atm и q
		private void t_calculate_Click(object sender, RoutedEventArgs e)
		{
			double p, t_atm, q_i, R_i, L_at, P_i;

			p = v(Data.p_max) * v(Data.L_ex) * v(Data.C_0);
			p /= 1 + v(Data.phee) * v(Data.a);
			p /= v(Data.L_ex) * v(Data.C_0) + v(Data.K_l) * v(Data.C_0) + v(Data.K_0) * v(Data.L_ex);
			sv(Data.p, p);

			//через W_atm и q
			t_atm = v(Data.W_atm) / v(Data.q);
			sv(Data.t_atm, t_atm);

			q_i = 24 * (v(Data.L_ln) - v(Data.L_ex));
			q_i /= v(Data.a) * (1 - v(Data.S)) * v(Data.t_atm);
			sv(Data.q_i, q_i);

			R_i = v(Data.a);
			R_i /= 1000 / v(Data.J_i) - v(Data.a);
			sv(Data.R_i, R_i);

			L_at = v(Data.W_atm) / (v(Data.n_at) * v(Data.n_cor) * v(Data.b_cor) * v(Data.H_at));
			sv(Data.L_at, L_at);

			P_i = 0.8 * v(Data.C_cdp) + v(Data.K_g) * v(Data.L_at);
			sv(Data.P_i, P_i);

			//запись вычесленных значений в БД
			var comm = objectDB.communication.Where(i => i.company_id == company.Key).AsEnumerable();
			int count = comm.Count();

			comm.ElementAt((int)Data.t_atm - 1).value = v(Data.t_atm).ToString();
			for(int i = Data.p - Data.q; i < count;)
				comm.ElementAt(i).value = v((Data)(++i)).ToString();
			objectDB.SaveChanges();

			//вывод результатов расчета t_atm
			var comm1 = comm.Where(i => i.field_id == (int)Data.t_atm).ToArray();
			var field = objectDB.fields.Where(i => i.id == (int)Data.t_atm).ToArray();

			List<DTable> result = new List<DTable>(comm1.Count());
			for(int i = 0; i < result.Capacity; i++)
				result.Add(new DTable(field[i].name, Math.Round(double.Parse(comm1[i].value), 2).ToString()));

			result_select_grid.ItemsSource = result;
		}
		//Расчитать t_atm при известном L_at и q
		private void t_calculate2_Click(object sender, RoutedEventArgs e)
		{
			double p, t_atm, q_i, R_i, W_atm, P_i;

			p = v(Data.p_max) * v(Data.L_ex) * v(Data.C_0);
			p /= 1 + v(Data.phee) * v(Data.a);
			p /= v(Data.L_ex) * v(Data.C_0) + v(Data.K_l) * v(Data.C_0) + v(Data.K_0) * v(Data.L_ex);
			sv(Data.p, p);

			//через L_at и q
			t_atm = v(Data.L_at) * v(Data.n_at) * v(Data.n_cor) * v(Data.b_cor) * v(Data.H_at);
			t_atm /= v(Data.q);
			sv(Data.t_atm, t_atm);

			q_i = 24 * (v(Data.L_ln) - v(Data.L_ex));
			q_i /= v(Data.a) * (1 - v(Data.S)) * v(Data.t_atm);
			sv(Data.q_i, q_i);

			R_i = v(Data.a);
			R_i /= 1000 / v(Data.J_i) - v(Data.a);
			sv(Data.R_i, R_i);

			W_atm = v(Data.q) * v(Data.t_atm);
			sv(Data.W_atm, W_atm);

			P_i = 0.8 * v(Data.C_cdp) + v(Data.K_g) * v(Data.L_at);
			sv(Data.P_i, P_i);

			//запись вычесленных значений в БД
			var comm = objectDB.communication.Where(i => i.company_id == company.Key).AsEnumerable();
			int count = comm.Count();

			comm.ElementAt((int)Data.t_atm - 1).value = v(Data.t_atm).ToString();
			for(int i = Data.p - Data.q; i < count;)
				comm.ElementAt(i).value = v((Data)(++i)).ToString();
			objectDB.SaveChanges();

			//вывод результатов расчета t_atm
			var comm1 = comm.Where(i => i.field_id == (int)Data.t_atm).ToArray();
			var field = objectDB.fields.Where(i => i.id == (int)Data.t_atm).ToArray();

			List<DTable> result = new List<DTable>(comm1.Count());
			for(int i = 0; i < result.Capacity; i++)
				result.Add(new DTable(field[i].name, Math.Round(double.Parse(comm1[i].value), 2).ToString()));

			result_select_grid.ItemsSource = result;
		}
	}
}