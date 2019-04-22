namespace ClientFormApp
{
	partial class ClientForm
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
			this.choiceButton = new System.Windows.Forms.Button();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.sendButton = new System.Windows.Forms.Button();
			this.grayscaleRadioButton = new System.Windows.Forms.RadioButton();
			this.sepiaRadioButton = new System.Windows.Forms.RadioButton();
			this.thresholdRadioButton = new System.Windows.Forms.RadioButton();
			this.filtersGroup = new System.Windows.Forms.GroupBox();
			this.thresholdTextBox = new System.Windows.Forms.TextBox();
			this.rectangleTextBox = new System.Windows.Forms.TextBox();
			this.rectangleLabel = new System.Windows.Forms.Label();
			this.uriTextBox = new System.Windows.Forms.TextBox();
			this.uriLabel = new System.Windows.Forms.Label();
			this.sendManyButton = new System.Windows.Forms.Button();
			this.queriesCountTextBox = new System.Windows.Forms.TextBox();
			this.TransformGroup = new System.Windows.Forms.GroupBox();
			this.flipVRadioButton = new System.Windows.Forms.RadioButton();
			this.flipHRadioButton = new System.Windows.Forms.RadioButton();
			this.rotateCCWRadioButton = new System.Windows.Forms.RadioButton();
			this.rotateCWRadioButton = new System.Windows.Forms.RadioButton();
			this.sendCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.filtersGroup.SuspendLayout();
			this.TransformGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// choiceButton
			// 
			this.choiceButton.Location = new System.Drawing.Point(12, 446);
			this.choiceButton.Name = "choiceButton";
			this.choiceButton.Size = new System.Drawing.Size(150, 45);
			this.choiceButton.TabIndex = 0;
			this.choiceButton.Text = "Выбрать файл";
			this.choiceButton.UseVisualStyleBackColor = true;
			this.choiceButton.Click += new System.EventHandler(this.choiceButton_Click);
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(12, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(761, 428);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 2;
			this.pictureBox.TabStop = false;
			// 
			// sendButton
			// 
			this.sendButton.Location = new System.Drawing.Point(12, 580);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(150, 45);
			this.sendButton.TabIndex = 0;
			this.sendButton.Text = "Отправить";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
			// 
			// grayscaleRadioButton
			// 
			this.grayscaleRadioButton.AutoSize = true;
			this.grayscaleRadioButton.Checked = true;
			this.grayscaleRadioButton.Location = new System.Drawing.Point(6, 15);
			this.grayscaleRadioButton.Name = "grayscaleRadioButton";
			this.grayscaleRadioButton.Size = new System.Drawing.Size(93, 21);
			this.grayscaleRadioButton.TabIndex = 3;
			this.grayscaleRadioButton.TabStop = true;
			this.grayscaleRadioButton.Text = "Grayscale";
			this.grayscaleRadioButton.UseVisualStyleBackColor = true;
			// 
			// sepiaRadioButton
			// 
			this.sepiaRadioButton.AutoSize = true;
			this.sepiaRadioButton.Location = new System.Drawing.Point(6, 42);
			this.sepiaRadioButton.Name = "sepiaRadioButton";
			this.sepiaRadioButton.Size = new System.Drawing.Size(65, 21);
			this.sepiaRadioButton.TabIndex = 3;
			this.sepiaRadioButton.Text = "Sepia";
			this.sepiaRadioButton.UseVisualStyleBackColor = true;
			// 
			// thresholdRadioButton
			// 
			this.thresholdRadioButton.AutoSize = true;
			this.thresholdRadioButton.Location = new System.Drawing.Point(6, 69);
			this.thresholdRadioButton.Name = "thresholdRadioButton";
			this.thresholdRadioButton.Size = new System.Drawing.Size(93, 21);
			this.thresholdRadioButton.TabIndex = 3;
			this.thresholdRadioButton.Text = "Threshold";
			this.thresholdRadioButton.UseVisualStyleBackColor = true;
			// 
			// filtersGroup
			// 
			this.filtersGroup.Controls.Add(this.thresholdTextBox);
			this.filtersGroup.Controls.Add(this.thresholdRadioButton);
			this.filtersGroup.Controls.Add(this.sepiaRadioButton);
			this.filtersGroup.Controls.Add(this.grayscaleRadioButton);
			this.filtersGroup.Location = new System.Drawing.Point(405, 446);
			this.filtersGroup.Name = "filtersGroup";
			this.filtersGroup.Size = new System.Drawing.Size(189, 101);
			this.filtersGroup.TabIndex = 4;
			this.filtersGroup.TabStop = false;
			// 
			// thresholdTextBox
			// 
			this.thresholdTextBox.Location = new System.Drawing.Point(105, 69);
			this.thresholdTextBox.Name = "thresholdTextBox";
			this.thresholdTextBox.Size = new System.Drawing.Size(78, 22);
			this.thresholdTextBox.TabIndex = 4;
			this.thresholdTextBox.Text = "50";
			// 
			// rectangleTextBox
			// 
			this.rectangleTextBox.Location = new System.Drawing.Point(405, 580);
			this.rectangleTextBox.Name = "rectangleTextBox";
			this.rectangleTextBox.Size = new System.Drawing.Size(189, 22);
			this.rectangleTextBox.TabIndex = 5;
			this.rectangleTextBox.Text = "0,0,1000,1000";
			// 
			// rectangleLabel
			// 
			this.rectangleLabel.AutoSize = true;
			this.rectangleLabel.Location = new System.Drawing.Point(402, 560);
			this.rectangleLabel.Name = "rectangleLabel";
			this.rectangleLabel.Size = new System.Drawing.Size(167, 17);
			this.rectangleLabel.TabIndex = 6;
			this.rectangleLabel.Text = "Прямоугольная область";
			// 
			// uriTextBox
			// 
			this.uriTextBox.Location = new System.Drawing.Point(12, 539);
			this.uriTextBox.Name = "uriTextBox";
			this.uriTextBox.Size = new System.Drawing.Size(261, 22);
			this.uriTextBox.TabIndex = 5;
			this.uriTextBox.Text = "http://localhost:5080/process/";
			// 
			// uriLabel
			// 
			this.uriLabel.AutoSize = true;
			this.uriLabel.Location = new System.Drawing.Point(9, 517);
			this.uriLabel.Name = "uriLabel";
			this.uriLabel.Size = new System.Drawing.Size(80, 17);
			this.uriLabel.TabIndex = 6;
			this.uriLabel.Text = "URI строка";
			// 
			// sendManyButton
			// 
			this.sendManyButton.Location = new System.Drawing.Point(633, 451);
			this.sendManyButton.Name = "sendManyButton";
			this.sendManyButton.Size = new System.Drawing.Size(106, 86);
			this.sendManyButton.TabIndex = 7;
			this.sendManyButton.Text = "Отправить N запросов";
			this.sendManyButton.UseVisualStyleBackColor = true;
			this.sendManyButton.Click += new System.EventHandler(this.sendManyButton_Click);
			// 
			// queriesCountTextBox
			// 
			this.queriesCountTextBox.Location = new System.Drawing.Point(612, 555);
			this.queriesCountTextBox.Name = "queriesCountTextBox";
			this.queriesCountTextBox.Size = new System.Drawing.Size(126, 22);
			this.queriesCountTextBox.TabIndex = 8;
			// 
			// TransformGroup
			// 
			this.TransformGroup.Controls.Add(this.flipVRadioButton);
			this.TransformGroup.Controls.Add(this.flipHRadioButton);
			this.TransformGroup.Controls.Add(this.rotateCCWRadioButton);
			this.TransformGroup.Controls.Add(this.rotateCWRadioButton);
			this.TransformGroup.Location = new System.Drawing.Point(405, 608);
			this.TransformGroup.Name = "TransformGroup";
			this.TransformGroup.Size = new System.Drawing.Size(206, 72);
			this.TransformGroup.TabIndex = 5;
			this.TransformGroup.TabStop = false;
			// 
			// flipVRadioButton
			// 
			this.flipVRadioButton.AutoSize = true;
			this.flipVRadioButton.Location = new System.Drawing.Point(135, 42);
			this.flipVRadioButton.Name = "flipVRadioButton";
			this.flipVRadioButton.Size = new System.Drawing.Size(65, 21);
			this.flipVRadioButton.TabIndex = 3;
			this.flipVRadioButton.Text = "Flip-V";
			this.flipVRadioButton.UseVisualStyleBackColor = true;
			// 
			// flipHRadioButton
			// 
			this.flipHRadioButton.AutoSize = true;
			this.flipHRadioButton.Location = new System.Drawing.Point(135, 15);
			this.flipHRadioButton.Name = "flipHRadioButton";
			this.flipHRadioButton.Size = new System.Drawing.Size(66, 21);
			this.flipHRadioButton.TabIndex = 3;
			this.flipHRadioButton.Text = "Flip-H";
			this.flipHRadioButton.UseVisualStyleBackColor = true;
			// 
			// rotateCCWRadioButton
			// 
			this.rotateCCWRadioButton.AutoSize = true;
			this.rotateCCWRadioButton.Location = new System.Drawing.Point(6, 42);
			this.rotateCCWRadioButton.Name = "rotateCCWRadioButton";
			this.rotateCCWRadioButton.Size = new System.Drawing.Size(107, 21);
			this.rotateCCWRadioButton.TabIndex = 3;
			this.rotateCCWRadioButton.Text = "Rotate-CCW";
			this.rotateCCWRadioButton.UseVisualStyleBackColor = true;
			// 
			// rotateCWRadioButton
			// 
			this.rotateCWRadioButton.AutoSize = true;
			this.rotateCWRadioButton.Checked = true;
			this.rotateCWRadioButton.Location = new System.Drawing.Point(6, 15);
			this.rotateCWRadioButton.Name = "rotateCWRadioButton";
			this.rotateCWRadioButton.Size = new System.Drawing.Size(98, 21);
			this.rotateCWRadioButton.TabIndex = 3;
			this.rotateCWRadioButton.TabStop = true;
			this.rotateCWRadioButton.Text = "Rotate-CW";
			this.rotateCWRadioButton.UseVisualStyleBackColor = true;
			// 
			// sendCheckBox
			// 
			this.sendCheckBox.AutoSize = true;
			this.sendCheckBox.Location = new System.Drawing.Point(12, 631);
			this.sendCheckBox.Name = "sendCheckBox";
			this.sendCheckBox.Size = new System.Drawing.Size(164, 21);
			this.sendCheckBox.TabIndex = 9;
			this.sendCheckBox.Text = "Применить фильтр?";
			this.sendCheckBox.UseVisualStyleBackColor = true;
			// 
			// ClientForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(785, 689);
			this.Controls.Add(this.sendCheckBox);
			this.Controls.Add(this.TransformGroup);
			this.Controls.Add(this.queriesCountTextBox);
			this.Controls.Add(this.sendManyButton);
			this.Controls.Add(this.uriLabel);
			this.Controls.Add(this.rectangleLabel);
			this.Controls.Add(this.uriTextBox);
			this.Controls.Add(this.rectangleTextBox);
			this.Controls.Add(this.filtersGroup);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.sendButton);
			this.Controls.Add(this.choiceButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "ClientForm";
			this.Text = "ImageTransformer";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.filtersGroup.ResumeLayout(false);
			this.filtersGroup.PerformLayout();
			this.TransformGroup.ResumeLayout(false);
			this.TransformGroup.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button choiceButton;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Button sendButton;
		private System.Windows.Forms.RadioButton grayscaleRadioButton;
		private System.Windows.Forms.RadioButton sepiaRadioButton;
		private System.Windows.Forms.RadioButton thresholdRadioButton;
		private System.Windows.Forms.GroupBox filtersGroup;
		private System.Windows.Forms.TextBox thresholdTextBox;
		private System.Windows.Forms.TextBox rectangleTextBox;
		private System.Windows.Forms.Label rectangleLabel;
		private System.Windows.Forms.TextBox uriTextBox;
		private System.Windows.Forms.Label uriLabel;
		private System.Windows.Forms.Button sendManyButton;
		private System.Windows.Forms.TextBox queriesCountTextBox;
		private System.Windows.Forms.GroupBox TransformGroup;
		private System.Windows.Forms.RadioButton flipHRadioButton;
		private System.Windows.Forms.RadioButton rotateCCWRadioButton;
		private System.Windows.Forms.RadioButton rotateCWRadioButton;
		private System.Windows.Forms.RadioButton flipVRadioButton;
		private System.Windows.Forms.CheckBox sendCheckBox;
	}
}

