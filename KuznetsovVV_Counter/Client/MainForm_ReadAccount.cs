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
	public partial class MainForm_ReadAccount : Form
	{
		int accountKey;

		public MainForm_ReadAccount()
		{
			InitializeComponent();
			readButton.Validation(accountInput);
		}

		private void MainForm_ReadAccount_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.CloseNewFormDialog();
		}

		private void accountInput_TextChanged(object sender, EventArgs e)
		{
			accountInput.Validate(readButton, out accountKey);
		}

		private void readButton_Click(object sender, EventArgs e)
		{
			MainForm.Buffer = MainForm.Client.GetIndicationsThroughAccount(accountKey);
			this.Close();
		}
	}
}