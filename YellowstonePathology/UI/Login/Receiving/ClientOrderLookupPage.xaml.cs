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

namespace YellowstonePathology.UI.Login.Receiving
{
	/// <summary>
	/// Interaction logic for ClientOrderLookupPage.xaml
	/// </summary>
	public partial class ClientOrderLookupPage : UserControl
	{
		public delegate void ClientOrderFoundEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.ClientOrderReturnEventArgs e);
        public event ClientOrderFoundEventHandler ClientOrderFound;

        public delegate void MultipleClientOrdersFoundEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.ClientOrderCollectionReturnEventArgs e);
        public event MultipleClientOrdersFoundEventHandler MultipleClientOrdersFound;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

		private string m_PageHeaderText = "Client Order Lookup";
        private OrderTypeEnum m_ExpectedOrderType;

        public ClientOrderLookupPage(OrderTypeEnum ExpectedOrderType)
		{
            this.m_ExpectedOrderType = ExpectedOrderType;
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;

			InitializeComponent();

			DataContext = this;
			Loaded += new RoutedEventHandler(ClientOrderLookupPage_Loaded);
			Unloaded +=new RoutedEventHandler(ClientOrderLookupPage_Unloaded);
		}

		private void ClientOrderLookupPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.ContainerScanReceived += this.ContainerScanReceived;
			Keyboard.Focus(this.TextBoxKey);
			this.TextBoxKey.Focus();
		}

		private void ClientOrderLookupPage_Unloaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.ContainerScanReceived -= this.ContainerScanReceived;
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		private void TextBoxKey_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				this.GetClientOrderByKeyValue();
			}
		}

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
			this.GetClientOrderByKeyValue();
		}

		private void GetClientOrderByKeyValue()
		{
			string keyValue = this.TextBoxKey.Text;
			if (string.IsNullOrEmpty(keyValue) == false)
			{
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = new Business.ClientOrder.Model.ClientOrderCollection();
                switch (this.m_ExpectedOrderType)
                {
                    case OrderTypeEnum.EPIC:
                    case OrderTypeEnum.YPICONNECT:
                        clientOrderCollection = this.HandleEPICLookup(keyValue);
                        break;
                    case OrderTypeEnum.ECLINICALWORKS:
                        clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByExternalOrderId(keyValue);
                        break;
                }
                
				if (clientOrderCollection.Count == 0)
				{
                    MessageBox.Show("No order was found.");					
				}
                else if (clientOrderCollection.Count == 1)
                {
                    this.ReturnClientOrder(clientOrderCollection[0]);
                }
                else if (clientOrderCollection.Count > 1)
                {
                    this.ReturnClientOrderCollection(clientOrderCollection);
                }
			}
			else
			{
				MessageBox.Show("The value entered is not valid.");
			}
		}

        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection HandleEPICLookup(string keyValue)
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection result = new Business.ClientOrder.Model.ClientOrderCollection();
            if (this.IsTheEnteredKeyAnMrn(keyValue) == true)
            {
                result = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersBySvhMedicalRecord(keyValue);
            }
            else
            {
                result = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersBySvhAccountNo(keyValue);
            }
            return result;
        }        

		private bool IsTheEnteredKeyAnMrn(string keyValue)
		{
			bool result = false;
			if (keyValue.Substring(0, 1).ToUpper() == "A" ||
				keyValue.Substring(0, 1).ToUpper() == "R" ||
				keyValue.Substring(0, 1).ToUpper() == "V")
			{
				result = true;
			}
			return result;
		}

		private void ReturnClientOrderCollection(YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection)
		{
            YellowstonePathology.UI.CustomEventArgs.ClientOrderCollectionReturnEventArgs eventArgs = new CustomEventArgs.ClientOrderCollectionReturnEventArgs(clientOrderCollection);
            this.MultipleClientOrdersFound(this, eventArgs);
		}

        private void ReturnClientOrder(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {
            YellowstonePathology.UI.CustomEventArgs.ClientOrderReturnEventArgs eventArgs = new CustomEventArgs.ClientOrderReturnEventArgs(clientOrder);
            this.ClientOrderFound(this, eventArgs);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {			
			this.Back(this, new EventArgs());
		}		

		private void ContainerScanReceived(YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{
                this.GetClientOrderByContainerId(containerBarcode.ToString());
			}
			));
		}

		private void GetClientOrderByContainerId(string containerId)
		{
            string clientOrderId = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrderByContainerId(containerId);
            if(string.IsNullOrEmpty(clientOrderId) == false)
            {
                YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(clientOrderId, Window.GetWindow(this));
                if (clientOrder != null)
                {
                    this.ReturnClientOrder(clientOrder);
                }
                else
                {
                    MessageBox.Show("No order was found.");
                }
            }
            else
            {
                MessageBox.Show("No order was found.");
            }
		}       		
	}
}
