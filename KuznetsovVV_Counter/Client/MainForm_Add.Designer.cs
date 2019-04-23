namespace Client
{
	partial class MainForm_Add
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
			this.counterInput = new System.Windows.Forms.TextBox();
			this.valueInput = new System.Windows.Forms.TextBox();
			this.addButton = new System.Windows.Forms.Button();
			this.accountLabel = new System.Windows.Forms.Label();
			this.counterLabel = new System.Windows.Forms.Label();
			this.labels = new System.Windows.Forms.Panel();
			this.valueLabel = new System.Windows.Forms.Label();
			this.inputs = new System.Windows.Forms.Panel();
			this.labels.SuspendLayout();
			this.inputs.SuspendLayout();
			this.SuspendLayout();
			// 
			// accountInput
			// 
			this.accountInput.Location = new System.Drawing.Point(0, 0);
			this.accountInput.Name = "accountInput";
			this.accountInput.Size = new System.Drawing.Size(134, 20);
			this.accountInput.TabIndex = 0;
			this.accountInput.TextChanged += new System.EventHandler(this.accountInput_TextChanged);
			// 
			// counterInput
			// 
			this.counterInput.Location = new System.Drawing.Point(0, 30);
			this.counterInput.Name = "counterInput";
			this.counterInput.Size = new System.Drawing.Size(134, 20);
			this.counterInput.TabIndex = 1;
			this.counterInput.TextChanged += new System.EventHandler(this.counterInput_TextChanged);
			// 
			// valueInput
			// 
			this.valueInput.Location = new System.Drawing.Point(0, 60);
			this.valueInput.Name = "valueInput";
			this.valueInput.Size = new System.Drawing.Size(134, 20);
			this.valueInput.TabIndex = 2;
			this.valueInput.TextChanged += new System.EventHandler(this.valueInput_TextChanged);
			// 
			// addButton
			// 
			this.addButton.Enabled = false;
			this.addButton.Location = new System.Drawing.Point(108, 102);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(134, 40);
			this.addButton.TabIndex = 3;
			this.addButton.Text = "Добавить";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// accountLabel
			// 
			this.accountLabel.AutoSize = true;
			this.accountLabel.Location = new System.Drawing.Point(0, 0);
			this.accountLabel.Name = "accountLabel";
			this.accountLabel.Size = new System.Drawing.Size(72, 13);
			this.accountLabel.TabIndex = 4;
			this.accountLabel.Text = "Номер счета";
			// 
			// counterLabel
			// 
			this.counterLabel.AutoSize = true;
			this.counterLabel.Location = new System.Drawing.Point(0, 30);
			this.counterLabel.Name = "counterLabel";
			this.counterLabel.Size = new System.Drawing.Size(89, 13);
			this.counterLabel.TabIndex = 5;
			this.counterLabel.Text = "Номер счетчика";
			// 
			// labels
			// 
			this.labels.AutoSize = true;
			this.labels.Controls.Add(this.valueLabel);
			this.labels.Controls.Add(this.accountLabel);
			this.labels.Controls.Add(this.counterLabel);
			this.labels.Location = new System.Drawing.Point(10, 15);
			this.labels.Name = "labels";
			this.labels.Size = new System.Drawing.Size(92, 80);
			this.labels.TabIndex = 7;
			// 
			// valueLabel
			// 
			this.valueLabel.AutoSize = true;
			this.valueLabel.Location = new System.Drawing.Point(0, 60);
			this.valueLabel.Name = "valueLabel";
			this.valueLabel.Size = new System.Drawing.Size(63, 13);
			this.valueLabel.TabIndex = 6;
			this.valueLabel.Text = "Показание";
			// 
			// inputs
			// 
			this.inputs.AutoSize = true;
			this.inputs.Controls.Add(this.accountInput);
			this.inputs.Controls.Add(this.counterInput);
			this.inputs.Controls.Add(this.valueInput);
			this.inputs.Location = new System.Drawing.Point(108, 12);
			this.inputs.Name = "inputs";
			this.inputs.Size = new System.Drawing.Size(137, 84);
			this.inputs.TabIndex = 8;
			// 
			// MainForm_Add
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(254, 154);
			this.Controls.Add(this.inputs);
			this.Controls.Add(this.labels);
			this.Controls.Add(this.addButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "MainForm_Add";
			this.Text = "Добавить показание счётчика";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Add_FormClosing);
			this.labels.ResumeLayout(false);
			this.labels.PerformLayout();
			this.inputs.ResumeLayout(false);
			this.inputs.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox accountInput;
		private System.Windows.Forms.TextBox counterInput;
		private System.Windows.Forms.TextBox valueInput;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Label accountLabel;
		private System.Windows.Forms.Label counterLabel;
		private System.Windows.Forms.Panel labels;
		private System.Windows.Forms.Panel inputs;
		private System.Windows.Forms.Label valueLabel;
	}
}