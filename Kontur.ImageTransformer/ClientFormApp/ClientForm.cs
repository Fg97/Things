using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientFormApp
{
	public partial class ClientForm : Form
	{
		string fileName;

		public ClientForm()
		{
			InitializeComponent();
		}
		
		private void choiceButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog choiceFile = new OpenFileDialog();
			choiceFile.ShowDialog();
			
			if(choiceFile.FileName != string.Empty)
			{
				fileName = choiceFile.FileName;
				if(choiceFile.FileName.EndsWith(".png"))
					pictureBox.Load(choiceFile.FileName);
			}
		}

		private async void sendButton_Click(object sender, EventArgs e)
		{
			string requestString = BuildRequestString();

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestString);
			request.KeepAlive = true;
			request.Method = "POST";
			
			FileStream file = File.OpenRead(fileName);
			file.CopyTo(request.GetRequestStream());
			file.Close();
			//Image.FromFile(fileName).Save(request.GetRequestStream(), ImageFormat.Png);

			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse)await request.GetResponseAsync();
				pictureBox.Image = Image.FromStream(response.GetResponseStream());
			}
			catch(Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void sendManyButton_Click(object sender, EventArgs e)
		{
			string requestString = BuildRequestString();

			int count = int.Parse(queriesCountTextBox.Text);
			for(int i = 0; i < count; i++)
			{
				Task.Run(async () =>
				{
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestString);
					request.KeepAlive = true;
					request.Method = "POST";

					FileStream file = File.OpenRead(fileName);
					file.CopyTo(request.GetRequestStream());
					file.Close();

					HttpWebResponse response = null;
					Image image = null;
					try
					{
						response = (HttpWebResponse)await request.GetResponseAsync();
						image = Image.FromStream(response.GetResponseStream());
					}
					catch(Exception exception)
					{
						//MessageBox.Show(exception.Message);
					}
				});
			}
		}

		string BuildRequestString()
		{
			string query;
			if(sendCheckBox.Checked)
			{
				if(grayscaleRadioButton.Checked) query = grayscaleRadioButton.Text.ToLower();
				else if(sepiaRadioButton.Checked) query = sepiaRadioButton.Text.ToLower();
				else query = thresholdRadioButton.Text.ToLower() + "(" + thresholdTextBox.Text + ")";
			}
			else
			{
				if(rotateCWRadioButton.Checked) query = rotateCWRadioButton.Text.ToLower();
				else if(rotateCCWRadioButton.Checked) query = rotateCCWRadioButton.Text.ToLower();
				else if(flipHRadioButton.Checked) query = flipHRadioButton.Text.ToLower();
				else query = flipVRadioButton.Text.ToLower();
			}
			query += "/" + rectangleTextBox.Text;
			return string.Concat(uriTextBox.Text, query);
		}
	}
}