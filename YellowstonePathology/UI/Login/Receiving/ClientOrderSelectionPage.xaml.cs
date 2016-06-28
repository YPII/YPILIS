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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.Receiving
{
	/// <summary>
	/// Interaction logic for ClientOrderSelectionPage.xaml
	/// </summary>
	public partial class ClientOrderSelectionPage : UserControl
	{
        public delegate void ClientOrderSelectedEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.ClientOrderReturnEventArgs clientOrderEventArgs);
		public event ClientOrderSelectedEventHandler ClientOrderSelected;        

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;
		private string m_PageHeaderText = "Client Order Lookup";

        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;

		public ClientOrderSelectionPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection, YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{
			this.m_ClientOrderCollection = clientOrderCollection;
            this.m_PageNavigator = pageNavigator;

			InitializeComponent();

			DataContext = this;            
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}		

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection ClientOrderCollection
		{
			get { return this.m_ClientOrderCollection; }
		}		

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{			
            this.Back(this, new EventArgs());
		}		

        private void ViewClientOrderPage_Back(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void ViewClientOrderPage_UseThisClientOrder(object sender, CustomEventArgs.ClientOrderReturnEventArgs e)
        {            
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(e.ClientOrder.ClientOrderId, this.m_PageNavigator.PrimaryMonitorWindow);
            YellowstonePathology.UI.CustomEventArgs.ClientOrderReturnEventArgs clientOrderReturnEventArgs = new CustomEventArgs.ClientOrderReturnEventArgs(clientOrder);
            this.ClientOrderSelected(this, clientOrderReturnEventArgs);
        }

        private void ButtonViewClientOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewClientOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = (YellowstonePathology.Business.ClientOrder.Model.ClientOrder)this.ListViewClientOrders.SelectedItem;
                ViewClientOrderPage viewClientOrderPage = new ViewClientOrderPage(clientOrder);
                viewClientOrderPage.UseThisClientOrder += new ViewClientOrderPage.UseThisClientOrderEventHandler(ViewClientOrderPage_UseThisClientOrder);
                viewClientOrderPage.Back += new ViewClientOrderPage.BackEventHandler(ViewClientOrderPage_Back);
                this.m_PageNavigator.Navigate(viewClientOrderPage);
            }
            else
            {
                System.Windows.MessageBox.Show("You must select an order to view.");
            }
        }        
	}
}
