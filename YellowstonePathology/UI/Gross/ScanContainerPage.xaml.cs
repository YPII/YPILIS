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

namespace YellowstonePathology.UI.Gross
{
    /// <summary>
	/// Interaction logic for ScanContainerPage.xaml
    /// </summary>
	public partial class ScanContainerPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;		

        public delegate void UseThisContainerEventHandler(object sender, string containerId);
        public event UseThisContainerEventHandler UseThisContainer;

        public delegate void PageTimedOutEventHandler(object sender, EventArgs e);
        public event PageTimedOutEventHandler PageTimedOut;

        public delegate void BarcodeWontScanEventHandler(object sender, EventArgs e);
        public event BarcodeWontScanEventHandler BarcodeWontScan;

        public delegate void SignOutEventHandler(object sender, EventArgs e);
        public event SignOutEventHandler SignOut;

        public delegate void ScanAliquotEventHandler(object sender, EventArgs e);
        public event ScanAliquotEventHandler ScanAliquot;

        public delegate void ShowEQCLabelPageEventHandler(object sender, EventArgs e);
        public event ShowEQCLabelPageEventHandler ShowEQCLabelPage;

        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
		private System.Windows.Threading.DispatcherTimer m_PageTimeOutTimer;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private string m_Message;
        private bool m_ShowEQCOption;        

		public ScanContainerPage(YellowstonePathology.Business.User.SystemIdentity systemIdentity, string message, bool showEQCOption)
        {            
            this.m_SystemIdentity = systemIdentity;
            this.m_Message = message;
            this.m_ShowEQCOption = showEQCOption;
			this.m_BarcodeScanPort = Business.BarcodeScanning.BarcodeScanPort.Instance;

			this.m_PageTimeOutTimer = new System.Windows.Threading.DispatcherTimer();
			this.m_PageTimeOutTimer.Interval = new TimeSpan(0, 20, 0);
			this.m_PageTimeOutTimer.Tick += new EventHandler(PageTimeOutTimer_Tick);

			InitializeComponent();

			DataContext = this;
			Loaded += new RoutedEventHandler(ScanContainerPage_Loaded);
			Unloaded += new RoutedEventHandler(ScanContainerPage_Unloaded);
            if (this.m_ShowEQCOption == true) this.ButtonEQCLabels.Visibility = Visibility.Visible;
		}

		private void ScanContainerPage_Loaded(object sender, RoutedEventArgs e)
		{
            Business.Persistence.DocumentGateway.Instance.Push(Window.GetWindow(this));
			this.m_BarcodeScanPort.ContainerScanReceived += this.ContainerScanReceived;
			this.m_PageTimeOutTimer.Start();
		}

		private void ScanContainerPage_Unloaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.ContainerScanReceived -= this.ContainerScanReceived;
			this.m_PageTimeOutTimer.Stop();
		}

        public string Message
        {
            get { return this.m_Message; }
        }						

		private void ContainerScanReceived(YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{
                this.UseThisContainer(this, containerBarcode.ToString());				
			}
			));
		}        

		private void PageTimeOutTimer_Tick(object sender, EventArgs e)
		{
			this.m_PageTimeOutTimer.Stop();

            EventArgs eventArgs = new EventArgs();
            this.PageTimedOut(this, eventArgs);			
		}

		private void ButtonBarcodeDidNotScan_Click(object sender, RoutedEventArgs e)
		{
            EventArgs eventArgs = new EventArgs();
            this.BarcodeWontScan(this, eventArgs);			
		}

		private void ButtonEnterNewContainerId_Click(object sender, RoutedEventArgs e)
		{
            //this.UseThisContainer(this, "CTNR2D7AABC6-5463-451D-89BF-6F08F5DA3B8D");				

            var app = Application.Current as App;            
            this.m_BarcodeScanPort.SimulateScanReceived(app.ContainerList[app.CurrentContainerIndex]);

            if (app.CurrentContainerIndex != app.ContainerList.Count - 1)
            {
                app.CurrentContainerIndex += 1;
            }
            else
            {
                app.CurrentContainerIndex = 0;
            }
        }

        public string SystemUserDisplayText
        {
            get
            {
                string result = "Current User: " + this.m_SystemIdentity.User.DisplayName;
                return result;
            }
        }

        private void ButtonSignOut_Click(object sender, RoutedEventArgs e)
        {
            this.SignOut(this, new EventArgs());
        }

        private void ButtonScanAliquot_Click(object sender, RoutedEventArgs e)
        {
            this.ScanAliquot(this, new EventArgs());
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonEQCLabels_Click(object sender, RoutedEventArgs e)
        {
            if(this.ShowEQCLabelPage != null)
            {
                this.ShowEQCLabelPage(this, new EventArgs());
            }
        }
    }
}
