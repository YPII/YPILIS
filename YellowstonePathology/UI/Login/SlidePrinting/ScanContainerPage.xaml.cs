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

namespace YellowstonePathology.UI.Login.SlidePrinting
{
	public partial class ScanContainerPage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		public delegate void ContainScanReceivedEventHandler(object sender, YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode);
        public event ContainScanReceivedEventHandler ContainerScannedReceived;

		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;				

        public ScanContainerPage()
		{
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;            

			InitializeComponent();

			DataContext = this;

            Loaded += new RoutedEventHandler(ScanContainerPage_Loaded);
            Unloaded += new RoutedEventHandler(ScanContainerPage_Unloaded);
		}

		private void ScanContainerPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.ContainerScanReceived += this.ContainerScanReceived;			
            this.ButtonNext.Focus();
		}

        private void ScanContainerPage_Unloaded(object sender, RoutedEventArgs e)
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

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null ) this.Back(this, new EventArgs());
		}

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {            
            if (this.Next != null) this.Next(this, new EventArgs());            
        }        		    

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonEnterNewContainerId_Click(object sender, RoutedEventArgs e)
        {

        }
	}
}
