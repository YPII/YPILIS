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

namespace YellowstonePathology.UI.Common
{
	/// <summary>
	/// Interaction logic for NumberChooser.xaml
	/// </summary>
	public partial class NumberChooser : Window
	{
        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler CloseWindow;

        public delegate void OKEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.ReportNoReturnEventArgs e);
        public event OKEventHandler OK;

		public NumberChooser()
		{
			InitializeComponent();
            this.TextBlockReportNo.Text = DateTime.Today.Year.ToString().Substring(2, 2) + "-";
		}		

		private void ButtonNumber_Click(object sender, RoutedEventArgs e)
		{
            if (this.TextBlockReportNo.Text.Length < 11)
			{
                this.TextBlockReportNo.Text += ((Button)sender).Content.ToString();
			}
		}

		private void ButtonBackSpace_Click(object sender, RoutedEventArgs e)
		{
            if (this.TextBlockReportNo.Text.Length > 0)
			{
                this.TextBlockReportNo.Text = this.TextBlockReportNo.Text.Substring(0, this.TextBlockReportNo.Text.Length - 1);
			}
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.TextBlockReportNo.Text);
            if (orderIdParser.ReportNo == null)
            {
                MessageBox.Show("The report number entered is invalid.");
            }
            else
            {         
                this.OK(this, new CustomEventArgs.ReportNoReturnEventArgs(orderIdParser.ReportNo));
            }			
		}		

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {            
            this.CloseWindow(this, new EventArgs());
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            this.TextBlockReportNo.Text = string.Empty;
        }		
	}
}
