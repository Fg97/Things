namespace Client
{
	partial class MainForm_ReadDate
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.firstDate = new System.Windows.Forms.DateTimePicker();
			this.secondDate = new System.Windows.Forms.DateTimePicker();
			this.readButton = new System.Windows.Forms.Button();
			this.fromLabel = new System.Windows.Forms.Label();
			this.toLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// firstDate
			// 
			this.firstDate.CustomFormat = "dMMMMyyyy hh:mm:ss";
			this.firstDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.firstDate.Location = new System.Drawing.Point(45, 10);
			this.firstDate.Name = "firstDate";
			this.firstDate.Size = new System.Drawing.Size(165, 20);
			this.firstDate.TabIndex = 0;
			this.firstDate.Value = new System.DateTime(2017, 4, 5, 0, 0, 0, 0);
			this.firstDate.ValueChanged += new System.EventHandler(this.firstDate_ValueChanged);
			// 
			// secondDate
			// 
			this.secondDate.CustomFormat = "dMMMMyyyy hh:mm:ss";
			this.secondDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.secondDate.Location = new System.Drawing.Point(45, 36);
			this.secondDate.Name = "secondDate";
			this.secondDate.Size = new System.Drawing.Size(165, 20);
			this.secondDate.TabIndex = 1;
			this.secondDate.Value = new System.DateTime(2017, 4, 18, 0, 0, 0, 0);
			this.secondDate.ValueChanged += new System.EventHandler(this.secondDate_ValueChanged);
			// 
			// readButton
			// 
			this.readButton.Location = new System.Drawing.Point(88, 64);
			this.readButton.Name = "readButton";
			this.readButton.Size = new System.Drawing.Size(134, 40);
			this.readButton.TabIndex = 2;
			this.readButton.Text = "Просмотр";
			this.readButton.UseVisualStyleBackColor = true;
			this.readButton.Click += new System.EventHandler(this.readButton_Click);
			// 
			// fromLabel
			// 
			this.fromLabel.AutoSize = true;
			this.fromLabel.Location = new System.Drawing.Point(22, 13);
			this.fromLabel.Name = "fromLabel";
			this.fromLabel.Size = new System.Drawing.Size(17, 13);
			this.fromLabel.TabIndex = 3;
			this.fromLabel.Text = "C:";
			// 
			// toLabel
			// 
			this.toLabel.AutoSize = true;
			this.toLabel.Location = new System.Drawing.Point(15, 39);
			this.toLabel.Name = "toLabel";
			this.toLabel.Size = new System.Drawing.Size(24, 13);
			this.toLabel.TabIndex = 4;
			this.toLabel.Text = "По:";
			// 
			// MainForm_ReadDate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(234, 116);
			this.Controls.Add(this.toLabel);
			this.Controls.Add(this.fromLabel);
			this.Controls.Add(this.readButton);
			this.Controls.Add(this.secondDate);
			this.Controls.Add(this.firstDate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "MainForm_ReadDate";
			this.Text = "Просмотр показаний по диапазону дат";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_ReadDate_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker firstDate;
		private System.Windows.Forms.DateTimePicker secondDate;
		private System.Windows.Forms.Button readButton;
		private System.Windows.Forms.Label fromLabel;
		private System.Windows.Forms.Label toLabel;
	}
}