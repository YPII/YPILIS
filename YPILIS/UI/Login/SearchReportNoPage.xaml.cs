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

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for SearchReportNoPage.xaml
	/// </summary>
	public partial class SearchReportNoPage : UserControl
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private string m_PageHeaderText = "Report No Search";

		public SearchReportNoPage()
		{
			InitializeComponent();
			DataContext = this;
			Loaded += new RoutedEventHandler(SearchReportNoPage_Loaded);            
		}

		private void SearchReportNoPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.TextBoxReportNo.Focus();
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.TextBoxReportNo.Text.Length > 0)
			{
                this.HandleReportNoSearch();
			}
		}		              

        private void HandleReportNoSearch()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.TextBoxReportNo.Text);
			if(orderIdParser.ReportNo != null)
            {
                UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, this.TextBoxReportNo.Text);
                this.Return(this, args);
            }
            else
            {
                MessageBox.Show("The Report No is not valid.");
            }
        }
	}
}
