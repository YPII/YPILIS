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

		private YellowstonePathology.Business.Client.Model.Client m_Client;
        private YellowstonePathology.Business.View.ClientLocationViewCollection m_ClientCollection;
        private YellowstonePathology.Business.View.ClientLocationViewCollection m_FavoriteClientCollection;
		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        
        private string m_PageHeaderText = "Select Client";		

		public ClientLookupPage()
		{
            this.m_FavoriteClientCollection = Business.View.ClientLocationViewCollection.GetFavorites();
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;

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

		private void BarcodeScanPort_ClientScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
            {
                int clientId = Convert.ToInt32(barcode.ID);
				this.m_Client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(clientId);
                this.m_Client.ClientLocationCollection.SetCurrentLocationToMedicalRecordsOrFirst();

                UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, this.m_Client);
                this.Return(this, args);	
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
            YellowstonePathology.Business.View.ClientLocationView clientLocationView = null;
            if (this.ListViewFavoriteClients.SelectedItem != null)
			{
				clientLocationView = this.ListViewFavoriteClients.SelectedItem as YellowstonePathology.Business.View.ClientLocationView;		
			}
			else if (this.ListViewClientSearch.SelectedItem != null)
			{
				clientLocationView = this.ListViewClientSearch.SelectedItem as YellowstonePathology.Business.View.ClientLocationView;				
			}

            if (clientLocationView != null)
            {
				this.m_Client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(clientLocationView.ClientId);                
                this.m_FavoriteClientCollection.AddRecent(this.m_Client);

				if (this.m_Client != null)
                {
                    this.m_Client.ClientLocationCollection.SetCurrentLocation(clientLocationView.ClientLocationId);                    
					UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, this.m_Client);
                    this.Return(this, args);					
				}
            }
            this.ListViewFavoriteClients.SelectedIndex = -1;
		}		

        private void TextBoxClientName_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.TextBoxClientName.Text.Length > 0)
            {
                string text = this.TextBoxClientName.Text;
				this.ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientLocationViewByClientName(text);
            }
        }		

        public YellowstonePathology.Business.View.ClientLocationViewCollection ClientCollection
        {
            get { return this.m_ClientCollection; }
            set 
            { 
                this.m_ClientCollection = value;
                this.NotifyPropertyChanged("ClientCollection");
            }
        }

        public YellowstonePathology.Business.View.ClientLocationViewCollection FavoriteClientCollection
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
            this.ButtonNext.IsEnabled = true;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);			
		}		
	}
}
