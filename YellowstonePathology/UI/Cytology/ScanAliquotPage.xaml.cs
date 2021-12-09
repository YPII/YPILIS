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

namespace YellowstonePathology.UI.Cytology
{   
	public partial class ScanAliquotPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;		

        public delegate void UseThisAliquotOrderIdEventHandler(object sender, string aliquotOrderId);
        public event UseThisAliquotOrderIdEventHandler UseThisAliquotOrderId;        

        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;		
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private string m_Message;

		public ScanAliquotPage(YellowstonePathology.Business.User.SystemIdentity systemIdentity, string message)
        {
            this.m_SystemIdentity = systemIdentity;
            this.m_Message = message;
			this.m_BarcodeScanPort = Business.BarcodeScanning.BarcodeScanPort.Instance;			

			InitializeComponent();

			DataContext = this;
			Loaded += new RoutedEventHandler(ScanContainerPage_Loaded);
			Unloaded += new RoutedEventHandler(ScanContainerPage_Unloaded);
		}

		private void ScanContainerPage_Loaded(object sender, RoutedEventArgs e)
		{
            Business.Persistence.DocumentGateway.Instance.Push(Window.GetWindow(this));
            this.m_BarcodeScanPort.AliquotOrderIdReceived += BarcodeScanPort_AliquotOrderIdReceived;
            this.m_BarcodeScanPort.ThinPrepSlideScanReceived += BarcodeScanPort_ThinPrepSlideScanReceived;
            this.m_BarcodeScanPort.HistologyBlockScanReceived += BarcodeScanPort_HistologyBlockScanReceived;
		}        

        private void ScanContainerPage_Unloaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.AliquotOrderIdReceived -= this.BarcodeScanPort_AliquotOrderIdReceived;
            this.m_BarcodeScanPort.ThinPrepSlideScanReceived -= this.BarcodeScanPort_ThinPrepSlideScanReceived;
            this.m_BarcodeScanPort.HistologyBlockScanReceived -= this.BarcodeScanPort_HistologyBlockScanReceived;
		}

        public string Message
        {
            get { return this.m_Message; }
        }				

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}		

        private void BarcodeScanPort_AliquotOrderIdReceived(string scanData)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.UseThisAliquotOrderId(this, scanData);
            }
            ));
        }

        private void BarcodeScanPort_ThinPrepSlideScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.UseThisAliquotOrderId(this, barcode.ID);
            }
            ));
        }

        private void BarcodeScanPort_HistologyBlockScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.UseThisAliquotOrderId(this, barcode.ID);
            }
            ));
        }

        public string SystemUserDisplayText
        {
            get
            {
                string result = "Current User: " + this.m_SystemIdentity.User.DisplayName;
                return result;
            }
        }        
	}
}
