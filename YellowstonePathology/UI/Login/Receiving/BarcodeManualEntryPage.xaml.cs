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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login.Receiving
{
	/// <summary>
	/// Interaction logic for BarcodeManualEntryPage.xaml
	/// </summary>
	public partial class BarcodeManualEntryPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, string containerId);
		public event ReturnEventHandler Return;

        public delegate void BackEventHandler();
        public event BackEventHandler Back;
		
		private string m_PageHeaderText = "Enter Barcode page";
		private string m_IdString;

		public BarcodeManualEntryPage()
		{
			this.m_IdString = string.Empty;
			InitializeComponent();
			DataContext = this;

			Loaded += new RoutedEventHandler(BarcodeManualEntryPage_Loaded);
		}

		private void BarcodeManualEntryPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.TextBoxContainerId.Focus();
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public string IdString
		{
			get { return this.m_IdString; }
			set
			{
				this.m_IdString = value;
				this.NotifyPropertyChanged("IdString");
			}
		}

		private void ButtonNumberPad_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			string tag = button.Tag.ToString();
			if (tag == "Clear")
			{
				this.IdString = string.Empty;
			}
			else if (tag == "Backspace")
			{
				if (string.IsNullOrEmpty(this.IdString) == false)
				{
					this.IdString = this.IdString.Substring(0, this.IdString.Length - 1);
				}
			}
			else
			{
				this.IdString += tag;
			}
		}
		
		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
            if(this.Back != null) this.Back();
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.IdString.Length > 0)
			{								
				string containerId = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetContainerIdByLast12Characters(this.IdString);				
				this.Return(this, containerId);				
			}
		}				
	}
}
