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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login.Receiving
{
	public partial class ItemsReceivedPage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        public delegate void BarcodeWontScanEventHandler(object sender, EventArgs e);
        public event BarcodeWontScanEventHandler BarcodeWontScan;

        public delegate void ShowClientOrderDetailsPageEventHandler(object sender, CustomEventArgs.ClientOrderDetailReturnEventArgs e);
        public event ShowClientOrderDetailsPageEventHandler ShowClientOrderDetailsPage;

		public delegate void ContainScanReceivedEventHandler(object sender, YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode);
        public event ContainScanReceivedEventHandler ContainerScannedReceived;

		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;		
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderMediaCollection m_ClientOrderMediaCollection;
		private string m_PageHeaderText = "Scan the containers received for this case.";
        private YellowstonePathology.UI.Login.Receiving.ClientOrderReceivingHandler m_ClientOrderReceivingHandler;

        public ItemsReceivedPage(YellowstonePathology.UI.Login.Receiving.ClientOrderReceivingHandler clientOrderReceivingHandler)
		{
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
            this.m_ClientOrderReceivingHandler = clientOrderReceivingHandler;

            this.m_ClientOrderMediaCollection = new Business.ClientOrder.Model.ClientOrderMediaCollection();
            this.m_ClientOrderReceivingHandler.ClientOrder.ClientOrderDetailCollection.LoadMedia(this.m_ClientOrderMediaCollection);

			InitializeComponent();

			DataContext = this;

			Loaded += new RoutedEventHandler(ItemsReceivedPage_Loaded);
			Unloaded += new RoutedEventHandler(ItemsReceivedPage_Unloaded);
		}

		private void ItemsReceivedPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.ContainerScanReceived += this.ContainerScanReceived;			
            this.ButtonNext.Focus();
		}

		private void ItemsReceivedPage_Unloaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.ContainerScanReceived -= this.ContainerScanReceived;			
		}

		private void ContainerScanReceived(YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{                
                this.ContainerScannedReceived(this, containerBarcode);
			}
			));
		}

        private void ButtonEnterNewContainerId_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode = YellowstonePathology.Business.BarcodeScanning.ContainerBarcode.Parse();
			this.ContainerScanReceived(containerBarcode);            
        }        
		
		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderMediaCollection ClientOrderMediaCollection
		{
			get { return this.m_ClientOrderMediaCollection; }
		}

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }				        

		private void ButtonCaseNotes_Click(object sender, RoutedEventArgs e)
		{
			
		}

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null ) this.Back(this, new EventArgs());
		}

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsOkToGoNext() == true)
            {
                if (this.Next != null)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                    this.Next(this, new EventArgs());
                }
            }
        }

        private bool IsOkToGoNext()
        {
            bool result = true;
            if (this.m_ClientOrderReceivingHandler.ClientOrder.ClientOrderDetailCollection.HasItemsWithNoContainerId() == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("There are containers that do not have container ID's assigned. Are you sure you want to continue.", "Continue?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    result = false;
                }
            }
            return result;
        }

        private void ButtonBarcodeWontScan_Click(object sender, RoutedEventArgs e)
        {
            this.BarcodeWontScan(this, new EventArgs());
        }		

        private void ListBoxContainers_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListBoxClientOrderMedia.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia clientOrderMedia = (YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia)this.ListBoxClientOrderMedia.SelectedItem;
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = this.m_ClientOrderReceivingHandler.ClientOrder.ClientOrderDetailCollection.GetByClientOrderDetailId(clientOrderMedia.ClientOrderDetailId);                
				this.ShowClientOrderDetailsPage(this, new CustomEventArgs.ClientOrderDetailReturnEventArgs(clientOrderDetail));
            }
        }

        private void HyperLinkDeleteSpecimen_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperLink = (Hyperlink)e.Source;
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia clientOrderMedia = (YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia)hyperLink.Tag;
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = this.m_ClientOrderReceivingHandler.ClientOrder.ClientOrderDetailCollection.GetByClientOrderDetailId(clientOrderMedia.ClientOrderDetailId);

            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this specimen?", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (messageBoxResult == MessageBoxResult.Yes)
            {                
                this.m_ClientOrderReceivingHandler.ClientOrder.ClientOrderDetailCollection.Remove(clientOrderDetail);                
                this.m_ClientOrderReceivingHandler.Save(false);
                this.m_ClientOrderMediaCollection = new Business.ClientOrder.Model.ClientOrderMediaCollection();
                this.m_ClientOrderReceivingHandler.ClientOrder.ClientOrderDetailCollection.LoadMedia(this.m_ClientOrderMediaCollection);
                this.NotifyPropertyChanged("ClientOrderMediaCollection");
            }            
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
