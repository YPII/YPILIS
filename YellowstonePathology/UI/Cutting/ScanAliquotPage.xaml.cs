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

namespace YellowstonePathology.UI.Cutting
{    
	public partial class ScanAliquotPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;		

        public delegate void AliquotOrderSelectedEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.BarcodeReturnEventArgs eventArgs);
        public event AliquotOrderSelectedEventHandler AliquotOrderSelected;          

        public delegate void SignOutEventHandler(object sender, EventArgs e);
        public event SignOutEventHandler SignOut;

        public delegate void  UseLastMasterAccessionNoEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs);
        public event UseLastMasterAccessionNoEventHandler UseLastMasterAccessionNo;

        public delegate void ShowMasterAccessionNoEntryPageEventHandler(object sender, EventArgs eventArgs);
        public event ShowMasterAccessionNoEntryPageEventHandler ShowMasterAccessionNoEntryPage;

        public delegate void PageTimedOutEventHandler(object sender, EventArgs eventArgs);
        public event PageTimedOutEventHandler PageTimedOut;

        public delegate void PrintImmunosEventHandler(object sender, EventArgs eventArgs);
        public event PrintImmunosEventHandler PrintImmunos;        

        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;		
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        

        private string m_LastMasterAccessionNo;
        private System.Windows.Threading.DispatcherTimer m_PageTimeoutTimer;

		public ScanAliquotPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string lastMasterAccessionNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_LastMasterAccessionNo = lastMasterAccessionNo;            
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;			

			InitializeComponent();

			DataContext = this;

            this.m_PageTimeoutTimer = new System.Windows.Threading.DispatcherTimer();
            this.m_PageTimeoutTimer.Interval = TimeSpan.FromMinutes(15);
            this.m_PageTimeoutTimer.Tick += new EventHandler(PageTimeoutTimer_Tick);
            this.m_PageTimeoutTimer.Start();

            this.Loaded += new RoutedEventHandler(ScanBlockPage_Loaded);
            this.Unloaded += new RoutedEventHandler(ScanBlockPage_Unloaded);            
		}

        private void PageTimeoutTimer_Tick(object sender, EventArgs e)
        {            
            this.PageTimedOut(this, new EventArgs());
        } 
        
        private void ScanBlockPage_Loaded(object sender, RoutedEventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(Window.GetWindow(this));
			this.m_BarcodeScanPort.HistologyBlockScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologyBlockScanReceivedHandler(BarcodeScanPort_HistologyBlockScanReceived);
        }

        private void ScanBlockPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.HistologyBlockScanReceived -= BarcodeScanPort_HistologyBlockScanReceived;
            this.m_PageTimeoutTimer.Stop();
        }

		private void BarcodeScanPort_HistologyBlockScanReceived(Business.BarcodeScanning.Barcode barcode)
        {            
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
            {
                if (this.AliquotOrderSelected != null) this.AliquotOrderSelected(this, new CustomEventArgs.BarcodeReturnEventArgs(barcode));
            }
            ));
        }

        public string LastMasterAccessionNo
        {
            get { return this.m_LastMasterAccessionNo; }
        }                

        private void ButtonShowMasterAccessionNoEntryPage_Click(object sender, RoutedEventArgs e)
        {
            this.ShowMasterAccessionNoEntryPage(this, new EventArgs());
        }  

        private void ButtonPrintImmunos_Click(object sender, RoutedEventArgs e)
        {
            this.PrintImmunos(this, new EventArgs());
        }              

        private void ButtonLastMasterAccessionNo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_LastMasterAccessionNo) == false)
            {
                this.UseLastMasterAccessionNo(this, new CustomEventArgs.MasterAccessionNoReturnEventArgs(this.m_LastMasterAccessionNo));
            }
        }

        private void ButtonSignOut_Click(object sender, RoutedEventArgs e)
        {
            this.SignOut(this, new EventArgs());
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
