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
	public partial class MainForm_Add : Form
	{
		int accountKey, counterKey, value;

		public MainForm_Add()
		{
			InitializeComponent();
			addButton.Validation(accountInput, counterInput, valueInput);
		}

		#region Events
		private void MainForm_Add_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.CloseNewFormDialog();
		}

		private void accountInput_TextChanged(object sender, EventArgs e)
		{
			accountInput.Validate(addButton, out accountKey);
		}

		private void counterInput_TextChanged(object sender, EventArgs e)
		{
			counterInput.Validate(addButton, out counterKey);
		}

		private void valueInput_TextChanged(object sender, EventArgs e)
		{
			valueInput.Validate(addButton, out value);
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			try
			{
				string error = MainForm.Client.AddIndication(accountKey, counterKey, value);

				if(error == string.Empty) this.Close();
				else MessageBox.Show(error);
			}
			catch(System.ServiceModel.FaultException)
			{
				MessageBox.Show("Что-то произошло при добавлении данных");
				return;
			}
		}
		#endregion
	}
}
