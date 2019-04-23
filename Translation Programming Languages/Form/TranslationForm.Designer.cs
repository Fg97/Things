namespace TranslationForm
{
	partial class TranslationForm
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
			this.runButton = new System.Windows.Forms.Button();
			this.programTextBox = new System.Windows.Forms.TextBox();
			this.translationTextBox = new System.Windows.Forms.TextBox();
			this.programLabel = new System.Windows.Forms.Label();
			this.translationLabel = new System.Windows.Forms.Label();
			this.syntaxTreeView = new System.Windows.Forms.TreeView();
			this.syntaxLabel = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.listBox3 = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.structuredTreeView = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// runButton
			// 
			this.runButton.Location = new System.Drawing.Point(12, 501);
			this.runButton.Name = "runButton";
			this.runButton.Size = new System.Drawing.Size(60, 40);
			this.runButton.TabIndex = 4;
			this.runButton.Text = "Пуск";
			this.runButton.UseVisualStyleBackColor = true;
			this.runButton.Click += new System.EventHandler(this.runButton_Click);
			// 
			// programTextBox
			// 
			this.programTextBox.Location = new System.Drawing.Point(12, 29);
			this.programTextBox.Multiline = true;
			this.programTextBox.Name = "programTextBox";
			this.programTextBox.Size = new System.Drawing.Size(300, 400);
			this.programTextBox.TabIndex = 1;
			this.programTextBox.TextChanged += new System.EventHandler(this.programTextBox_TextChanged);
			// 
			// translationTextBox
			// 
			this.translationTextBox.Location = new System.Drawing.Point(318, 29);
			this.translationTextBox.Multiline = true;
			this.translationTextBox.Name = "translationTextBox";
			this.translationTextBox.ReadOnly = true;
			this.translationTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.translationTextBox.Size = new System.Drawing.Size(300, 400);
			this.translationTextBox.TabIndex = 2;
			// 
			// programLabel
			// 
			this.programLabel.AutoSize = true;
			this.programLabel.Location = new System.Drawing.Point(12, 9);
			this.programLabel.Name = "programLabel";
			this.programLabel.Size = new System.Drawing.Size(123, 17);
			this.programLabel.TabIndex = 0;
			this.programLabel.Text = "Текст программы";
			// 
			// translationLabel
			// 
			this.translationLabel.AutoSize = true;
			this.translationLabel.Location = new System.Drawing.Point(318, 9);
			this.translationLabel.Name = "translationLabel";
			this.translationLabel.Size = new System.Drawing.Size(169, 17);
			this.translationLabel.TabIndex = 0;
			this.translationLabel.Text = "Транслированный текст";
			// 
			// syntaxTreeView
			// 
			this.syntaxTreeView.Indent = 15;
			this.syntaxTreeView.Location = new System.Drawing.Point(624, 29);
			this.syntaxTreeView.Name = "syntaxTreeView";
			this.syntaxTreeView.Size = new System.Drawing.Size(300, 400);
			this.syntaxTreeView.TabIndex = 3;
			// 
			// syntaxLabel
			// 
			this.syntaxLabel.AutoSize = true;
			this.syntaxLabel.Location = new System.Drawing.Point(624, 9);
			this.syntaxLabel.Name = "syntaxLabel";
			this.syntaxLabel.Size = new System.Drawing.Size(167, 17);
			this.syntaxLabel.TabIndex = 0;
			this.syntaxLabel.Text = "Синтаксическое дерево";
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 16;
			this.listBox1.Location = new System.Drawing.Point(100, 500);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(212, 180);
			this.listBox1.TabIndex = 5;
			// 
			// listBox2
			// 
			this.listBox2.FormattingEnabled = true;
			this.listBox2.ItemHeight = 16;
			this.listBox2.Location = new System.Drawing.Point(318, 500);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(212, 180);
			this.listBox2.TabIndex = 6;
			// 
			// listBox3
			// 
			this.listBox3.FormattingEnabled = true;
			this.listBox3.ItemHeight = 16;
			this.listBox3.Location = new System.Drawing.Point(536, 500);
			this.listBox3.Name = "listBox3";
			this.listBox3.Size = new System.Drawing.Size(180, 180);
			this.listBox3.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(100, 480);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Идентификаторы";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(318, 480);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "Числа";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(536, 480);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(134, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "Десятичные числа";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(722, 480);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(168, 17);
			this.label4.TabIndex = 0;
			this.label4.Text = "Структурированный вид";
			// 
			// structuredTreeView
			// 
			this.structuredTreeView.Indent = 15;
			this.structuredTreeView.Location = new System.Drawing.Point(722, 500);
			this.structuredTreeView.Name = "structuredTreeView";
			this.structuredTreeView.ShowLines = false;
			this.structuredTreeView.Size = new System.Drawing.Size(180, 180);
			this.structuredTreeView.TabIndex = 8;
			// 
			// TranslationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(936, 753);
			this.Controls.Add(this.structuredTreeView);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBox3);
			this.Controls.Add(this.listBox2);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.syntaxTreeView);
			this.Controls.Add(this.syntaxLabel);
			this.Controls.Add(this.translationLabel);
			this.Controls.Add(this.programLabel);
			this.Controls.Add(this.translationTextBox);
			this.Controls.Add(this.programTextBox);
			this.Controls.Add(this.runButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "TranslationForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button runButton;
		private System.Windows.Forms.TextBox programTextBox;
		private System.Windows.Forms.TextBox translationTextBox;
		private System.Windows.Forms.Label programLabel;
		private System.Windows.Forms.Label translationLabel;
		private System.Windows.Forms.TreeView syntaxTreeView;
		private System.Windows.Forms.Label syntaxLabel;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.ListBox listBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TreeView structuredTreeView;
	}
}

