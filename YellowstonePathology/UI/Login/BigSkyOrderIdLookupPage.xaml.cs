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
	/// Interaction logic for BigSkyOrderIdLookupPage.xaml
	/// </summary>
	public partial class BigSkyOrderIdLookupPage : UserControl
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private string m_PageHeaderText = "Order ID Lookup";

		public BigSkyOrderIdLookupPage()
		{
			InitializeComponent();
			Loaded += new RoutedEventHandler(BigSkyOrderIdLookupPage_Loaded);
		}

		private void BigSkyOrderIdLookupPage_Loaded(object sender, RoutedEventArgs e)
		{
			Keyboard.Focus(this.TextBoxBigSkyControlId);
			this.TextBoxBigSkyControlId.Focus();
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

        private void ButtonSearchByExternalOrderId_Click(object sender, RoutedEventArgs e)
        {
			this.ClientOrderSearch();
        }

		private void TextBoxBigSkyControlId_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				this.ClientOrderSearch();
			}
		}

		private void ClientOrderSearch()
		{
			if (string.IsNullOrEmpty(this.TextBoxBigSkyControlId.Text) == false)
			{
				YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrderByExternalOrderId(this.TextBoxBigSkyControlId.Text);
				UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, clientOrder);
				this.Return(this, args);
			}
			else
			{
				MessageBox.Show("The Big Sky control Id entered is not valid.");
			}
		}

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}        

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Close();
        }
	}
}
