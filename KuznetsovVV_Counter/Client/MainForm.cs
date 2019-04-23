using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Client
{
	public partial class MainForm : Form
	{
		internal static CounterService.ServiceClient Client { get; private set; }
		internal static CounterService.CounterIndicationData[] Buffer { get; set; }

		MainForm_Add addForm;
		MainForm_ReadAccount readAccountForm;
		MainForm_ReadDate readDateForm;

		public MainForm()
		{
			InitializeComponent();
			Client = new CounterService.ServiceClient("BasicHttpBinding_IService");
		}

		#region Events
		private void menuAddIndication_Click(object sender, EventArgs e)
		{
			addForm = new MainForm_Add();
			this.OpenNewFormDialog(addForm);
		}

		private void menuReadIndicationThroughAccount_Click(object sender, EventArgs e)
		{
			readAccountForm = new MainForm_ReadAccount();
			this.OpenNewFormDialog(readAccountForm);
		}

		private void menuReadIndicationThroughDate_Click(object sender, EventArgs e)
		{
			readDateForm = new MainForm_ReadDate();
			this.OpenNewFormDialog(readDateForm);
		}
		
		private void MainForm_EnabledChanged(object sender, EventArgs e)
		{
			if(this.Enabled && Buffer != null)
			{
				FillTheList(indicationIdList, "IndicationId");
				FillTheList(accountKeyList, "AccountKey");
				FillTheList(counterKeyList, "CounterKey");
				FillTheList(measureList, "Measure");
				FillTheList(valueList, "Value");
				FillTheList(dateList, "Date");
			}
		}
		#endregion

		void FillTheList(ListBox list, string property)
		{
			list.Items.Clear();
			for(int i = 0; i < Buffer.Length; i++)
				list.Items.Add(Buffer[i].GetType().GetProperty(property).GetValue(Buffer[i]).ToString());
		}
	}
}