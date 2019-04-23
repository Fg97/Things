namespace Client
{
	partial class MainForm_ReadAccount
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
			this.accountInput = new System.Windows.Forms.TextBox();
			this.readButton = new System.Windows.Forms.Button();
			this.accountLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// accountInput
			// 
			this.accountInput.Location = new System.Drawing.Point(88, 12);
			this.accountInput.Name = "accountInput";
			this.accountInput.Size = new System.Drawing.Size(134, 20);
			this.accountInput.TabIndex = 0;
			this.accountInput.TextChanged += new System.EventHandler(this.accountInput_TextChanged);
			// 
			// readButton
			// 
			this.readButton.Enabled = false;
			this.readButton.Location = new System.Drawing.Point(88, 64);
			this.readButton.Name = "readButton";
			this.readButton.Size = new System.Drawing.Size(134, 40);
			this.readButton.TabIndex = 1;
			this.readButton.Text = "Просмотр";
			this.readButton.UseVisualStyleBackColor = true;
			this.readButton.Click += new System.EventHandler(this.readButton_Click);
			// 
			// accountLabel
			// 
			this.accountLabel.AutoSize = true;
			this.accountLabel.Location = new System.Drawing.Point(10, 15);
			this.accountLabel.Name = "accountLabel";
			this.accountLabel.Size = new System.Drawing.Size(72, 13);
			this.accountLabel.TabIndex = 2;
			this.accountLabel.Text = "Номер счета";
			// 
			// MainForm_ReadAccount
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(234, 116);
			this.Controls.Add(this.accountLabel);
			this.Controls.Add(this.readButton);
			this.Controls.Add(this.accountInput);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "MainForm_ReadAccount";
			this.Text = "Просмотр показаний по номеру счёта";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_ReadAccount_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox accountInput;
		private System.Windows.Forms.Button readButton;
		private System.Windows.Forms.Label accountLabel;
	}
}