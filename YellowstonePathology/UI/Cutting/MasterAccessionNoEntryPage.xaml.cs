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

namespace YellowstonePathology.UI.Cutting
{    
	public partial class MasterAccessionNoEntryPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void UseThisMasterAccessionNoEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs);
        public event UseThisMasterAccessionNoEventHandler UseThisMasterAccessionNo;

        public delegate void CloseEventHandler(object sender, EventArgs eventArgs);
        public event CloseEventHandler Close;

        public MasterAccessionNoEntryPage()
        {            
			InitializeComponent();
            this.TextMasterAccessionNo.Text = DateTime.Today.Year.ToString().Substring(2, 2) + "-";
			DataContext = this;            
		}

        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            if (this.TextMasterAccessionNo.Text.Length < 11)
            {
                this.TextMasterAccessionNo.Text += ((Button)sender).Content.ToString();
            }
        }

        private void ButtonBackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (this.TextMasterAccessionNo.Text.Length > 0)
            {
                this.TextMasterAccessionNo.Text = this.TextMasterAccessionNo.Text.Substring(0, this.TextMasterAccessionNo.Text.Length - 1);
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.TextMasterAccessionNo.Text);
            if (orderIdParser.MasterAccessionNo == null)
            {
                MessageBox.Show("The report number entered is invalid.");
            }
            else
            {
                this.UseThisMasterAccessionNo(this, new CustomEventArgs.MasterAccessionNoReturnEventArgs(orderIdParser.MasterAccessionNo));
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(this, new EventArgs());
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            this.TextMasterAccessionNo.Text = string.Empty;
        }				          

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
	}
}
