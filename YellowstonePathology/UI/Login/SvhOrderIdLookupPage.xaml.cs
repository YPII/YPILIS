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
	/// Interaction logic for SvhOrderIdLookupPage.xaml
	/// </summary>
	public partial class SvhOrderIdLookupPage : UserControl
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;
		private string m_PageHeaderText = "SVH Account Number Lookup";

		public SvhOrderIdLookupPage()
		{
			InitializeComponent();
			Loaded +=new RoutedEventHandler(SvhOrderIdLookupPage_Loaded);
		}

		private void SvhOrderIdLookupPage_Loaded(object sender, RoutedEventArgs e)
		{
			Keyboard.Focus(this.TextBoxSvhAccountNo);
			this.TextBoxSvhAccountNo.Focus();
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

        private void ButtonSearchBySvhAccountNo_Click(object sender, RoutedEventArgs e)
        {
			this.GetMRN();
		}

		private void TextBoxSvhAccountNo_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				this.GetMRN();
			}
		}

		private void GetMRN()
		{
			if (string.IsNullOrEmpty(this.TextBoxSvhAccountNo.Text) == false)
			{
				YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersBySvhAccountNo(this.TextBoxSvhAccountNo.Text);
                if (clientOrderCollection.Count == 1)
                {
                    UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, clientOrderCollection[0]);
                    this.Return(this, args);
                }
                else if (clientOrderCollection.Count > 1)
                {
                    MessageBox.Show("More than one order was found. Please hold for IT.");
                }
                else
                {
                    MessageBox.Show("No order was found. You will need to call the client and have them place an order.");
                }
			}
			else
			{
				MessageBox.Show("The SVH Account No entered is not valid.");
			}
		}

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }		
	}
}
