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

namespace YellowstonePathology.UI.MaterialTracking
{
	/// <summary>
	/// Interaction logic for MaterialTrackingCasePage.xaml
	/// </summary>
    public partial class MaterialTrackingCasePage : UserControl, INotifyPropertyChanged
	{
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection m_MaterialTrackingLogCollection;
        private List<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog> m_MaterialTrackingLogList;
		
		private string m_PageHeaderText;
        private List<string> m_MaterialIdList;

		public MaterialTrackingCasePage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection)
		{
            this.m_MaterialIdList = new List<string>();

			this.m_AccessionOrder = accessionOrder;
			this.m_MaterialTrackingLogCollection = materialTrackingLogCollection;
            this.m_MaterialTrackingLogList = this.m_MaterialTrackingLogCollection.ToList<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog>();
            
			this.m_PageHeaderText = "Material Tracking For Case: " + this.m_AccessionOrder.MasterAccessionNo + " - " + this.m_AccessionOrder.PatientDisplayName;
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;			

			InitializeComponent();
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(MaterialTrackingCasePage_Loaded);
			this.Unloaded += new RoutedEventHandler(MaterialTrackingCasePage_Unloaded);
		}

		private void MaterialTrackingCasePage_Loaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.HistologyBlockScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologyBlockScanReceivedHandler(BarcodeScanPort_HistologyBlockScanReceived);
			this.m_BarcodeScanPort.HistologySlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologySlideScanReceivedHandler(BarcodeScanPort_HistologySlideScanReceived);
		}

        private void MaterialTrackingCasePage_Unloaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.HistologyBlockScanReceived -= BarcodeScanPort_HistologyBlockScanReceived;
			this.m_BarcodeScanPort.HistologySlideScanReceived -= BarcodeScanPort_HistologySlideScanReceived;
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public List<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog> MaterialTrackingLogList
		{
            get { return this.m_MaterialTrackingLogList; }
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		private void BarcodeScanPort_HistologyBlockScanReceived(Business.BarcodeScanning.Barcode barcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{
				this.HandleBlockScanReceived(barcode.ID);
			}
			));
		}

		private void HandleBlockScanReceived(string aliquotOrderId)
		{
			YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(aliquotOrderId);
			if(aliquotOrder != null)
			{
				this.AddMaterialTrackingLog(aliquotOrder);
			}
			else
			{
				MessageBox.Show("The block scanned does not belong to this case.");
			}
		}

		private void AddMaterialTrackingLog(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
		{
			YellowstonePathology.Business.Facility.Model.Facility thisFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
			string thisLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;
			string scanLocation = "Block Scanned At " + thisLocation;

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, aliquotOrder.AliquotOrderId, null, thisFacility.FacilityId, thisFacility.FacilityName,
				thisLocation, "Block Scanned", scanLocation, "Aliquot", this.m_AccessionOrder.MasterAccessionNo, aliquotOrder.Label, aliquotOrder.ClientAccessioned);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingLog, Window.GetWindow(this));			
			this.m_MaterialTrackingLogCollection.Insert(0, materialTrackingLog);
		}

		private void BarcodeScanPort_HistologySlideScanReceived(Business.BarcodeScanning.Barcode barcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{
				this.HandleSlideScanReceived(barcode.ID);
			}
			));
		}

		private void HandleSlideScanReceived(string slideOrderId)
		{
			YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSlideOrder(slideOrderId);
			if (slideOrder != null)
			{
				this.AddMaterialTrackingLog(slideOrder);
			}
			else
			{
				MessageBox.Show("This slide does not belong to this case.");
			}
		}

		private void AddMaterialTrackingLog(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
		{
			YellowstonePathology.Business.Facility.Model.Facility thisFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
			string thisLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;
			string scanLocation = "Slide Scanned At " + thisLocation;

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, slideOrder.SlideOrderId, null, thisFacility.FacilityId, thisFacility.FacilityName,
				thisLocation, "Slide Scan", scanLocation, "SlideOrder", this.m_AccessionOrder.MasterAccessionNo, slideOrder.Label, slideOrder.ClientAccessioned);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingLog, Window.GetWindow(this));			
			this.m_MaterialTrackingLogCollection.Insert(0, materialTrackingLog);
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
            Window.GetWindow(this).Close();			
		}                     		

        private void CheckBoxSlideOrder_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = (YellowstonePathology.Business.Slide.Model.SlideOrder)checkBox.Tag;
            if (this.m_MaterialIdList.Contains(slideOrder.SlideOrderId) == false)
            {
                this.m_MaterialIdList.Add(slideOrder.SlideOrderId);
                this.m_MaterialTrackingLogList = this.m_MaterialTrackingLogCollection.GetList(this.m_MaterialIdList);
                this.NotifyPropertyChanged("MaterialTrackingLogList");
            }
        }

        private void CheckBoxSlideOrder_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = (YellowstonePathology.Business.Slide.Model.SlideOrder)checkBox.Tag;
            if (this.m_MaterialIdList.Contains(slideOrder.SlideOrderId) == true)
            {                
                this.m_MaterialIdList.Remove(slideOrder.SlideOrderId);
                if (this.m_MaterialIdList.Count > 0)
                {
                    this.m_MaterialTrackingLogList = this.m_MaterialTrackingLogCollection.GetList(this.m_MaterialIdList);
                }
                else
                {
                    this.m_MaterialTrackingLogList = this.m_MaterialTrackingLogCollection.ToList<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog>();
                }
                this.NotifyPropertyChanged("MaterialTrackingLogList");
            }
        }

        private void CheckBoxAliquotOrder_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)checkBox.Tag;
            if (this.m_MaterialIdList.Contains(aliquotOrder.AliquotOrderId) == false)
            {
                this.m_MaterialIdList.Add(aliquotOrder.AliquotOrderId);
                this.m_MaterialTrackingLogList = this.m_MaterialTrackingLogCollection.GetList(this.m_MaterialIdList);
                this.NotifyPropertyChanged("MaterialTrackingLogList");
            }
        }

        private void CheckBoxAliquotOrder_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)checkBox.Tag;
            if (this.m_MaterialIdList.Contains(aliquotOrder.AliquotOrderId) == true)
            {
                this.m_MaterialIdList.Remove(aliquotOrder.AliquotOrderId);
                if (this.m_MaterialIdList.Count > 0)
                {
                    this.m_MaterialTrackingLogList = this.m_MaterialTrackingLogCollection.GetList(this.m_MaterialIdList);
                }
                else
                {
                    this.m_MaterialTrackingLogList = this.m_MaterialTrackingLogCollection.ToList<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog>();
                }

                this.NotifyPropertyChanged("MaterialTrackingLogList");
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
