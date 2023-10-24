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
	/// <summary>
	/// Interaction logic for ScanSecurityBadgePage.xaml
	/// </summary>
	public partial class ScanSecurityBadgePage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void AuthentificationSuccessfulEventHandler(object sender, EventArgs e);
		public event AuthentificationSuccessfulEventHandler AuthentificationSuccessful;

        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private Visibility m_CloseButtonVisibility;

        public ScanSecurityBadgePage(Visibility closeButtonVisibility)
		{
            this.m_CloseButtonVisibility = closeButtonVisibility;
			InitializeComponent();

			DataContext = this;

            YellowstonePathology.Business.User.SystemIdentity.Instance.EnableManualSecurityBadgeScan();
            Business.User.SystemIdentity.Instance.SetToLoggedInUser();
			this.m_BarcodeScanPort = Business.BarcodeScanning.BarcodeScanPort.Instance;
			this.m_BarcodeScanPort.SecurityBadgeScanReceived += new Business.BarcodeScanning.BarcodeScanPort.SecurityBadgeScanReceivedHandler(BarcodeScanPort_SecurityBadgeScanReceived);            
		}

        public Visibility CloseButtonVisibility
        {
            get { return this.m_CloseButtonVisibility; }
        }

        private void ButtonAutoScan_Click(object sender, RoutedEventArgs e)
        {
            Business.BarcodeScanning.Barcode bc = new Business.BarcodeScanning.Barcode();
            bc.ID = "5001";
            this.BarcodeScanPort_SecurityBadgeScanReceived(bc);
        }

		private void BarcodeScanPort_SecurityBadgeScanReceived(Business.BarcodeScanning.Barcode barcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{				                
                Business.User.SystemIdentity.Instance.BarcodeScanPort_SecurityBadgeScanReceived(barcode);
				this.m_BarcodeScanPort.SecurityBadgeScanReceived -= BarcodeScanPort_SecurityBadgeScanReceived;
				this.AuthentificationSuccessful(this, new EventArgs());				
			}
			));
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}				

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(this, new EventArgs());
        }        
	}
}
