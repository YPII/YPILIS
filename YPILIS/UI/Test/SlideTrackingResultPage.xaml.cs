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

namespace YellowstonePathology.UI.Test
{	
	public partial class SlideTrackingResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private Business.Slide.Model.VantageSlideViewCollection m_VantageSlideViewCollection;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

        public SlideTrackingResultPage(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(panelSetOrder, accessionOrder)
		{
			this.m_PanelSetOrder = panelSetOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

            this.m_VantageSlideViewCollection = new Business.Slide.Model.VantageSlideViewCollection(this.m_AccessionOrder.MasterAccessionNo);
            this.m_BarcodeScanPort = Business.BarcodeScanning.BarcodeScanPort.Instance;

            this.m_PageHeaderText = "Slide Tracking Result For: " + this.m_AccessionOrder.PatientDisplayName;

            InitializeComponent();

			DataContext = this;

            this.Loaded += SlideTrackingResultPage_Loaded;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);            
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        private void SlideTrackingResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.VantageSlideScanReceived += BarcodeScanPort_VantageSlideScanReceived;
        }

        private void BarcodeScanPort_VantageSlideScanReceived(string scanData)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_VantageSlideViewCollection.HandleSlideScan(scanData);
                this.NotifyPropertyChanged(string.Empty);
            }
            ));
        }

        public Business.Slide.Model.VantageSlideViewCollection VantageSlideViewCollection
        {
            get { return this.m_VantageSlideViewCollection; }
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}		

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null) this.Next(this, new EventArgs());
		}

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Final == true)
            {
               MessageBox.Show("This case cannot be finalized because it is already finalized.");
            }
            else
            {
                this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Final == false)
            {
                MessageBox.Show("This case cannot be unfinalized because it is not final.");
            }
            else
            {
                this.m_PanelSetOrder.Unfinalize();
            }
        }

        private void HyperLinkSimulateScan_Click(object sender, RoutedEventArgs e)
        {
            string data = Business.BarcodeScanning.VantageBarcode.SimulateScan();
            this.BarcodeScanPort_VantageSlideScanReceived(data);
        }

        private void HyperLinkSendToBozeman_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility facility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBZMN");
            this.m_VantageSlideViewCollection.SetLocation(facility.FacilityId);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void HyperLinkSendToBillings_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility facility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_VantageSlideViewCollection.SetLocation(facility.FacilityId);
            this.NotifyPropertyChanged(string.Empty);
        }
    }
}
