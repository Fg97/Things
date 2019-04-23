using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Client
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
	#region Extensions
	static class ExtensionForm
	{
		static Form baseForm;

		public static void OpenNewFormDialog(this Form form, Form formDialog)
		{
			formDialog.Show();
			form.Enabled = false;

			baseForm = form;
		}
		public static void CloseNewFormDialog(this Form form)
		{
			baseForm.Enabled = true;

			baseForm = null;
		}
	}

	static class ExtensionTextBox
	{
		static Dictionary<TextBox, bool> messageBoxWasShowed = new Dictionary<TextBox, bool>();
		static Dictionary<TextBox, bool> valid = new Dictionary<TextBox, bool>();

		public static bool CheckValid(this TextBox textBox) => !valid.ContainsKey(textBox) ? false : valid[textBox];

		public static void Validate(this TextBox textBox, Button button, out int parameter)
		{
			if(!messageBoxWasShowed.ContainsKey(textBox)) messageBoxWasShowed.Add(textBox, false);
			if(!valid.ContainsKey(textBox)) valid.Add(textBox, false);

			if(int.TryParse(textBox.Text, out parameter))
			{
				valid[textBox] = true;
				button.CheckValid();

				messageBoxWasShowed[textBox] = false;
			}
			else
			{
				valid[textBox] = false;
				button.CheckValid();

				if(textBox.Text == String.Empty) messageBoxWasShowed[textBox] = false;
				else if(!messageBoxWasShowed[textBox])
				{
					messageBoxWasShowed[textBox] = true;
					MessageBox.Show("Введите корректное число");
				}
			}
		}
	}

	static class ExtensionButton
	{
		static Dictionary<Button, TextBox[]> validation = new Dictionary<Button, TextBox[]>();

		public static void Validation(this Button button, params TextBox[] validTextBoxes)
		{
			if(validTextBoxes.Length == 0) return;

			if(!validation.ContainsKey(button)) validation.Add(button, validTextBoxes);
		}

		public static void CheckValid(this Button button)
		{
			bool checkValid = true;
			TextBox[] textBoxes = validation[button];

			for(int i = 0; i < textBoxes.Length; checkValid = checkValid & textBoxes[i++].CheckValid()) ;

			button.Enabled = checkValid;
		}
	}
	#endregion
}