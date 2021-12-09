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

namespace YellowstonePathology.UI.Login
{    
	public partial class ClientLookupPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private Business.Client.Model.Client m_Client;
        private Business.Client.Model.ClientCollection m_ClientCollection;
        private Business.Client.Model.ClientCollection m_FavoriteClientCollection;
		private Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        
        private string m_PageHeaderText = "Select Client";		

		public ClientLookupPage()
		{
            this.m_FavoriteClientCollection = Business.Gateway.PhysicianClientGateway.GetFavoriteClients();
			this.m_BarcodeScanPort = Business.BarcodeScanning.BarcodeScanPort.Instance;

			InitializeComponent();

			this.DataContext = this;
            this.Loaded += new RoutedEventHandler(ClientLookupPage_Loaded);
            this.Unloaded += new RoutedEventHandler(ClientLookupPage_Unloaded);
		}                

        private void ClientLookupPage_Loaded(object sender, RoutedEventArgs e)
        {
			this.m_BarcodeScanPort.ClientScanReceived += new Business.BarcodeScanning.BarcodeScanPort.ClientScanReceivedHandler(BarcodeScanPort_ClientScanReceived);
            this.TextBoxClientName.Focus();
            System.Windows.Window window = Window.GetWindow(this);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(window);
        }

        public Business.Client.Model.ClientCollection ClientCollection
        {
            get { return this.m_ClientCollection; }
        }

		private void BarcodeScanPort_ClientScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
            {
                int clientId = Convert.ToInt32(barcode.ID);
				this.m_Client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(clientId);
                if (string.IsNullOrEmpty(this.m_Client.OrderType) == false)
                {                    
                    bool useRequisition = false;
                    List<object> returnData = new List<object>();
                    returnData.Add(this.m_Client);
                    returnData.Add(useRequisition);
                    UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, returnData);
                    this.Return(this, args);
                }
                else
                {
                    MessageBox.Show("This client does not have an OrderType selected.", "No OrderType", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            ));
        }

        private void ClientLookupPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.ClientScanReceived -= this.BarcodeScanPort_ClientScanReceived;
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }        
        
		public void GoNext()
		{			                        
            this.m_Client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_Client.ClientId);                            
			if (this.m_Client != null)
            {                
                bool useRequisition = false;
                if(this.CheckBoxUseRequisition.IsChecked == true) useRequisition = true;
                List<object> returnData = new List<object>();
                returnData.Add(this.m_Client);
                returnData.Add(useRequisition);
				UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, returnData);
                this.Return(this, args);					
			}            
            this.ListViewFavoriteClients.SelectedIndex = -1;
		}		

        private void TextBoxClientName_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.TextBoxClientName.Text.Length > 0)
            {
                string text = this.TextBoxClientName.Text;
				this.m_ClientCollection = Business.Gateway.PhysicianClientGateway.GetClientsByClientName(text);
                this.NotifyPropertyChanged(string.Empty);
            }
        }		        

        public Business.Client.Model.ClientCollection FavoriteClientCollection
        {
            get { return this.m_FavoriteClientCollection; }
            set 
            {
                this.m_FavoriteClientCollection = value;
                this.NotifyPropertyChanged("FavoriteClientCollection");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListViewFavoriteClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewFavoriteClients.SelectedItems.Count != 0)
            {
                this.m_Client = (Business.Client.Model.Client)this.ListViewFavoriteClients.SelectedItem;
                this.ButtonNext.IsEnabled = true;
                this.GoNext();
            }
        }		

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.GoNext();
        }        

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close(); 
        }

        private void ListViewClientSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListViewClientSearch.SelectedItem != null)
            {
                this.m_Client = (Business.Client.Model.Client)this.ListViewClientSearch.SelectedItem;
                this.ButtonNext.IsEnabled = true;
            }            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);			
		}		
	}
}
