namespace Client
{
	partial class MainForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.menu = new System.Windows.Forms.ToolStripMenuItem();
			this.menuAddIndication = new System.Windows.Forms.ToolStripMenuItem();
			this.menuReadIndicationThroughAccount = new System.Windows.Forms.ToolStripMenuItem();
			this.menuReadIndicationThroughDate = new System.Windows.Forms.ToolStripMenuItem();
			this.counterTable = new System.Windows.Forms.TableLayoutPanel();
			this.indicationIdList = new System.Windows.Forms.ListBox();
			this.accountKeyList = new System.Windows.Forms.ListBox();
			this.counterKeyList = new System.Windows.Forms.ListBox();
			this.measureList = new System.Windows.Forms.ListBox();
			this.valueList = new System.Windows.Forms.ListBox();
			this.dateList = new System.Windows.Forms.ListBox();
			this.indicationIdLabel = new System.Windows.Forms.Label();
			this.accountKeyLabel = new System.Windows.Forms.Label();
			this.counterKeyLabel = new System.Windows.Forms.Label();
			this.measureLabel = new System.Windows.Forms.Label();
			this.valueLabel = new System.Windows.Forms.Label();
			this.dateLabel = new System.Windows.Forms.Label();
			this.menuStrip.SuspendLayout();
			this.counterTable.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(653, 24);
			this.menuStrip.TabIndex = 0;
			// 
			// menu
			// 
			this.menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddIndication,
            this.menuReadIndicationThroughAccount,
            this.menuReadIndicationThroughDate});
			this.menu.Name = "menu";
			this.menu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
			this.menu.Size = new System.Drawing.Size(53, 20);
			this.menu.Text = "Меню";
			// 
			// menuAddIndication
			// 
			this.menuAddIndication.Name = "menuAddIndication";
			this.menuAddIndication.Size = new System.Drawing.Size(289, 22);
			this.menuAddIndication.Text = "Добавить показание счётчика";
			this.menuAddIndication.Click += new System.EventHandler(this.menuAddIndication_Click);
			// 
			// menuReadIndicationThroughAccount
			// 
			this.menuReadIndicationThroughAccount.Name = "menuReadIndicationThroughAccount";
			this.menuReadIndicationThroughAccount.Size = new System.Drawing.Size(289, 22);
			this.menuReadIndicationThroughAccount.Text = "Просмотр показаний по номеру счёта";
			this.menuReadIndicationThroughAccount.Click += new System.EventHandler(this.menuReadIndicationThroughAccount_Click);
			// 
			// menuReadIndicationThroughDate
			// 
			this.menuReadIndicationThroughDate.Name = "menuReadIndicationThroughDate";
			this.menuReadIndicationThroughDate.Size = new System.Drawing.Size(289, 22);
			this.menuReadIndicationThroughDate.Text = "Просмотр показаний по диапазону дат";
			this.menuReadIndicationThroughDate.Click += new System.EventHandler(this.menuReadIndicationThroughDate_Click);
			// 
			// counterTable
			// 
			this.counterTable.ColumnCount = 6;
			this.counterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
			this.counterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
			this.counterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
			this.counterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
			this.counterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
			this.counterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.counterTable.Controls.Add(this.indicationIdList, 0, 0);
			this.counterTable.Controls.Add(this.accountKeyList, 1, 0);
			this.counterTable.Controls.Add(this.counterKeyList, 2, 0);
			this.counterTable.Controls.Add(this.measureList, 3, 0);
			this.counterTable.Controls.Add(this.valueList, 4, 0);
			this.counterTable.Controls.Add(this.dateList, 5, 0);
			this.counterTable.Location = new System.Drawing.Point(12, 72);
			this.counterTable.Name = "counterTable";
			this.counterTable.RowCount = 1;
			this.counterTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.counterTable.Size = new System.Drawing.Size(629, 336);
			this.counterTable.TabIndex = 1;
			// 
			// indicationIdList
			// 
			this.indicationIdList.FormattingEnabled = true;
			this.indicationIdList.Location = new System.Drawing.Point(3, 3);
			this.indicationIdList.Name = "indicationIdList";
			this.indicationIdList.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.indicationIdList.Size = new System.Drawing.Size(89, 329);
			this.indicationIdList.TabIndex = 0;
			// 
			// accountKeyList
			// 
			this.accountKeyList.FormattingEnabled = true;
			this.accountKeyList.Location = new System.Drawing.Point(98, 3);
			this.accountKeyList.Name = "accountKeyList";
			this.accountKeyList.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.accountKeyList.Size = new System.Drawing.Size(89, 329);
			this.accountKeyList.TabIndex = 1;
			// 
			// counterKeyList
			// 
			this.counterKeyList.FormattingEnabled = true;
			this.counterKeyList.Location = new System.Drawing.Point(193, 3);
			this.counterKeyList.Name = "counterKeyList";
			this.counterKeyList.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.counterKeyList.Size = new System.Drawing.Size(89, 329);
			this.counterKeyList.TabIndex = 2;
			// 
			// measureList
			// 
			this.measureList.FormattingEnabled = true;
			this.measureList.Location = new System.Drawing.Point(288, 3);
			this.measureList.Name = "measureList";
			this.measureList.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.measureList.Size = new System.Drawing.Size(89, 329);
			this.measureList.TabIndex = 3;
			// 
			// valueList
			// 
			this.valueList.FormattingEnabled = true;
			this.valueList.Location = new System.Drawing.Point(383, 3);
			this.valueList.Name = "valueList";
			this.valueList.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.valueList.Size = new System.Drawing.Size(89, 329);
			this.valueList.TabIndex = 4;
			// 
			// dateList
			// 
			this.dateList.FormattingEnabled = true;
			this.dateList.Location = new System.Drawing.Point(478, 3);
			this.dateList.Name = "dateList";
			this.dateList.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.dateList.Size = new System.Drawing.Size(148, 329);
			this.dateList.TabIndex = 5;
			// 
			// indicationIdLabel
			// 
			this.indicationIdLabel.AutoSize = true;
			this.indicationIdLabel.Location = new System.Drawing.Point(12, 43);
			this.indicationIdLabel.Name = "indicationIdLabel";
			this.indicationIdLabel.Size = new System.Drawing.Size(63, 26);
			this.indicationIdLabel.TabIndex = 2;
			this.indicationIdLabel.Text = "Индекс\r\nизмерения";
			// 
			// accountKeyLabel
			// 
			this.accountKeyLabel.AutoSize = true;
			this.accountKeyLabel.Location = new System.Drawing.Point(107, 43);
			this.accountKeyLabel.Name = "accountKeyLabel";
			this.accountKeyLabel.Size = new System.Drawing.Size(72, 13);
			this.accountKeyLabel.TabIndex = 3;
			this.accountKeyLabel.Text = "Номер счета";
			// 
			// counterKeyLabel
			// 
			this.counterKeyLabel.AutoSize = true;
			this.counterKeyLabel.Location = new System.Drawing.Point(202, 43);
			this.counterKeyLabel.Name = "counterKeyLabel";
			this.counterKeyLabel.Size = new System.Drawing.Size(74, 13);
			this.counterKeyLabel.TabIndex = 4;
			this.counterKeyLabel.Text = "Код счетчика";
			// 
			// measureLabel
			// 
			this.measureLabel.AutoSize = true;
			this.measureLabel.Location = new System.Drawing.Point(297, 43);
			this.measureLabel.Name = "measureLabel";
			this.measureLabel.Size = new System.Drawing.Size(63, 26);
			this.measureLabel.TabIndex = 5;
			this.measureLabel.Text = "Единица\r\nизмерения";
			// 
			// valueLabel
			// 
			this.valueLabel.AutoSize = true;
			this.valueLabel.Location = new System.Drawing.Point(392, 43);
			this.valueLabel.Name = "valueLabel";
			this.valueLabel.Size = new System.Drawing.Size(55, 26);
			this.valueLabel.TabIndex = 6;
			this.valueLabel.Text = "Значение\r\nсчетчика";
			// 
			// dateLabel
			// 
			this.dateLabel.AutoSize = true;
			this.dateLabel.Location = new System.Drawing.Point(487, 43);
			this.dateLabel.Name = "dateLabel";
			this.dateLabel.Size = new System.Drawing.Size(123, 13);
			this.dateLabel.TabIndex = 7;
			this.dateLabel.Text = "Дата создания записи";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(653, 420);
			this.Controls.Add(this.dateLabel);
			this.Controls.Add(this.valueLabel);
			this.Controls.Add(this.measureLabel);
			this.Controls.Add(this.counterKeyLabel);
			this.Controls.Add(this.accountKeyLabel);
			this.Controls.Add(this.indicationIdLabel);
			this.Controls.Add(this.counterTable);
			this.Controls.Add(this.menuStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.menuStrip;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Стажировка";
			this.EnabledChanged += new System.EventHandler(this.MainForm_EnabledChanged);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.counterTable.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem menu;
		private System.Windows.Forms.ToolStripMenuItem menuAddIndication;
		private System.Windows.Forms.ToolStripMenuItem menuReadIndicationThroughAccount;
		private System.Windows.Forms.ToolStripMenuItem menuReadIndicationThroughDate;
		private System.Windows.Forms.TableLayoutPanel counterTable;
		private System.Windows.Forms.ListBox indicationIdList;
		private System.Windows.Forms.ListBox accountKeyList;
		private System.Windows.Forms.ListBox counterKeyList;
		private System.Windows.Forms.ListBox measureList;
		private System.Windows.Forms.ListBox valueList;
		private System.Windows.Forms.ListBox dateList;
		private System.Windows.Forms.Label indicationIdLabel;
		private System.Windows.Forms.Label accountKeyLabel;
		private System.Windows.Forms.Label counterKeyLabel;
		private System.Windows.Forms.Label measureLabel;
		private System.Windows.Forms.Label valueLabel;
		private System.Windows.Forms.Label dateLabel;
	}
}

