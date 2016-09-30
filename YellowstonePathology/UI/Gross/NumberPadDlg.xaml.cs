using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Gross
{
	/// <summary>
	/// Interaction logic for NumberSelectorDlg.xaml
	/// </summary>
	public partial class NumberPadDlg : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_NumberString;

		public NumberPadDlg()
		{
			this.m_NumberString = string.Empty;
			InitializeComponent();
			DataContext = this;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public int? Number
		{
			get
			{
				int? result = null;
				if (string.IsNullOrEmpty(this.m_NumberString) == false)
				{
					result = Convert.ToInt32(this.m_NumberString);
				}
				return result;
			}
		}

		public string NumberString
		{
			get { return this.m_NumberString; }
			private set
			{
				this.m_NumberString = value;
				this.NotifyPropertyChanged("NumberString");
			}
		}

		private void ButtonNumberPad_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			string tag = button.Tag.ToString();
			if (tag == "Clear")
			{
				this.NumberString = string.Empty;
			}
			else
			{
				this.NumberString += tag;
			}
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.NumberString = string.Empty;
			this.DialogResult = false;
			Close();
		}
	}
}
