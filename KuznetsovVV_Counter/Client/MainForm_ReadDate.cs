using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
	public partial class MainForm_ReadDate : Form
	{
		DateTime firstDateBackup, secondDateBackup;

		public MainForm_ReadDate()
		{
			InitializeComponent();

			firstDateBackup = firstDate.Value;
			secondDateBackup = secondDate.Value;
		}

		private void MainForm_ReadDate_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.CloseNewFormDialog();
		}

		private void firstDate_ValueChanged(object sender, EventArgs e)
		{
			if(firstDate.Value > secondDate.Value)
			{
				MessageBox.Show("Начальная дата не может быть больше конечной");
				firstDate.Value = firstDateBackup;
			}
			else firstDateBackup = firstDate.Value;
		}

		private void secondDate_ValueChanged(object sender, EventArgs e)
		{
			if(secondDate.Value < firstDate.Value)
			{
				MessageBox.Show("Конечная дата не может быть меньше начальной");
				secondDate.Value = secondDateBackup;
			}
			else secondDateBackup = secondDate.Value;
		}

		private void readButton_Click(object sender, EventArgs e)
		{
			MainForm.Buffer = MainForm.Client.GetIndicationsThroughDate(firstDate.Value, secondDate.Value);
			this.Close();
		}
	}
}