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
using System.Xml.Linq;

namespace YellowstonePathology.UI.MaterialTracking
{
	public partial class MaterialBatchPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        public delegate void FinishEventHandler(object sender, EventArgs e);
        public event FinishEventHandler Finish;

        public delegate void ShowTrackingDocumentEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs e);
        public event ShowTrackingDocumentEventHandler ShowTrackingDocument;

		YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;		

		YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection m_MaterialTrackingLogCollection;
		YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch m_MaterialTrackingBatch;        
		YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection m_MaterialTrackingLogViewCollection;        

		private bool m_BackButtonVisible;
		private bool m_NextButtonVisible;
		private bool m_FinishButtonVisible;

        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;
        private YellowstonePathology.Business.Facility.Model.LocationCollection m_LocationCollection;

        private bool m_UserMasterAccessionNo;
        private string m_MasterAccessionNo;

        YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;


        public MaterialBatchPage(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch,
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection,
            bool backButtonVisible, bool nextButtonVisible, bool finishButtonVisible, 
			bool useMasterAccessionNo, string masterAccessionNo, YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{                        
            this.m_FacilityCollection = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetAllFacilities();
            this.m_LocationCollection = YellowstonePathology.Business.Facility.Model.LocationCollection.GetAllLocations();

            this.m_PageNavigator = pageNavigator;

			this.m_MaterialTrackingBatch = materialTrackingBatch;
            this.m_MaterialTrackingLogCollection = materialTrackingLogCollection;            
            
            this.m_UserMasterAccessionNo = useMasterAccessionNo;
            this.m_MasterAccessionNo = masterAccessionNo;

            if (this.m_UserMasterAccessionNo == true)
            {
                this.m_MaterialTrackingLogViewCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingLogViewCollectionByBatchIdMasterAccessionNo(this.m_MaterialTrackingBatch.MaterialTrackingBatchId, masterAccessionNo);			
            }
            else
            {
                this.m_MaterialTrackingLogViewCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingLogViewCollectionByBatchId(this.m_MaterialTrackingBatch.MaterialTrackingBatchId);			
            }            

			this.m_BackButtonVisible = backButtonVisible;
			this.m_NextButtonVisible = nextButtonVisible;
			this.m_FinishButtonVisible = finishButtonVisible;
			
			InitializeComponent();
			DataContext = this;

            Loaded += new RoutedEventHandler(MaterialBatchPage_Loaded);
            Unloaded += new RoutedEventHandler(MaterialBatchPage_Unloaded);			            
		}

		private void MaterialBatchPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
			this.m_BarcodeScanPort.HistologySlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologySlideScanReceivedHandler(HistologySlideScanReceived);
			this.m_BarcodeScanPort.HistologyBlockScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologyBlockScanReceivedHandler(BarcodeScanPort_HistologyBlockScanReceived);
            this.m_BarcodeScanPort.ContainerScanReceived += new Business.BarcodeScanning.BarcodeScanPort.ContainerScanReceivedHandler(BarcodeScanPort_ContainerScanReceived);
			this.m_BarcodeScanPort.USPostalServiceCertifiedMailReceived += new Business.BarcodeScanning.BarcodeScanPort.USPostalServiceCertifiedMailReceivedHandler(BarcodeScanPort_USPostalServiceCertifiedMailReceived);
            this.m_BarcodeScanPort.ThinPrepSlideScanReceived += BarcodeScanPort_ThinPrepSlideScanReceived;
		}

        private void BarcodeScanPort_ThinPrepSlideScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {
                        if (this.m_MaterialTrackingLogViewCollection.MaterialIdExists(barcode.ID) == false)
                        {
                            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = this.AddMaterialTrackingLogScan(barcode.ID, "PSLD", this.m_MaterialTrackingBatch.MaterialTrackingBatchId);
                            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView = new Business.MaterialTracking.Model.MaterialTrackingLogView();
                            materialTrackingLogView.FromScannedItemView(materialTrackingLog);
                            this.m_MaterialTrackingLogViewCollection.Add(materialTrackingLogView);
                            this.NotifyPropertyChanged("MaterialCount");
                        }
                        else
                        {
                            this.SelectMaterialTrackingLogView(barcode.ID);
                        }
                    }));
        }

        private void BarcodeScanPort_ContainerScanReceived(Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {
                        if (this.m_MaterialTrackingLogViewCollection.MaterialIdExists(containerBarcode.ID) == false)
                        {
                            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = this.AddMaterialTrackingLogScan(containerBarcode.ID, "Container", this.m_MaterialTrackingBatch.MaterialTrackingBatchId);
                            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView = new Business.MaterialTracking.Model.MaterialTrackingLogView();
                            materialTrackingLogView.FromScannedItemView(materialTrackingLog);
                            this.m_MaterialTrackingLogViewCollection.Add(materialTrackingLogView);
                            this.NotifyPropertyChanged("MaterialCount");
                        }
                        else
                        {
                            this.SelectMaterialTrackingLogView(containerBarcode.ID);
                        }
                    }));        
        }

        private void BarcodeScanPort_USPostalServiceCertifiedMailReceived(string scanData)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {
                        this.m_MaterialTrackingBatch.TrackingInformation = this.m_MaterialTrackingBatch.TrackingInformation + "US Postal Service Certified Mail Tracking Number: " + scanData;
                    }));             
        }        

		private void MaterialBatchPage_Unloaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.HistologySlideScanReceived -= HistologySlideScanReceived;
            this.m_BarcodeScanPort.USPostalServiceCertifiedMailReceived -= BarcodeScanPort_USPostalServiceCertifiedMailReceived;
            this.m_BarcodeScanPort.ContainerScanReceived -= BarcodeScanPort_ContainerScanReceived;
		}

        public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
        {
            get { return this.m_FacilityCollection; }
        }

        public YellowstonePathology.Business.Facility.Model.LocationCollection LocationCollection
        {
            get { return this.m_LocationCollection; }
        }

		public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection MaterialTrackingLogViewCollection
        {
            get { return this.m_MaterialTrackingLogViewCollection; }
        }

		public int MaterialCount
		{
			get 
            {
                return this.m_MaterialTrackingLogViewCollection.Count; 
            }			
		}

		public bool BackButtonVisible
		{
			get { return this.m_BackButtonVisible; }
		}

		public bool NextButtonVisible
		{
			get { return this.m_NextButtonVisible; }
		}

		public bool FinishButtonVisible
		{
			get { return this.m_FinishButtonVisible; }
		}

		public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch MaterialTrackingBatch
		{
			get { return this.m_MaterialTrackingBatch; }
		}

		public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection MaterialTrackingLogCollection
        {
            get { return this.m_MaterialTrackingLogCollection; }
        }


		private void BarcodeScanPort_HistologyBlockScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {
                        if (this.m_MaterialTrackingLogViewCollection.MaterialIdExists(barcode.ID) == false)
                        {
							YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = this.AddMaterialTrackingLogScan(barcode.ID, "Aliquot", this.m_MaterialTrackingBatch.MaterialTrackingBatchId);
							YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView = new Business.MaterialTracking.Model.MaterialTrackingLogView();
                            materialTrackingLogView.FromScannedItemView(materialTrackingLog);
                            this.m_MaterialTrackingLogViewCollection.Add(materialTrackingLogView);
                            this.NotifyPropertyChanged("MaterialCount");								
                        }
                        else
                        {
                            this.SelectMaterialTrackingLogView(barcode.ID);
                        }
                    }));             
        }

		private void HistologySlideScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
		{                        
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
				new Action(
					delegate()
					{
                        if (this.m_MaterialTrackingLogViewCollection.MaterialIdExists(barcode.ID) == false)
							{
								YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = this.AddMaterialTrackingLogScan(barcode.ID, "Slide", this.m_MaterialTrackingBatch.MaterialTrackingBatchId);
								YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView = new Business.MaterialTracking.Model.MaterialTrackingLogView();
                                materialTrackingLogView.FromScannedItemView(materialTrackingLog);
                                this.m_MaterialTrackingLogViewCollection.Add(materialTrackingLogView);
                                this.NotifyPropertyChanged("MaterialCount");								
							}
							else
							{
                                this.SelectMaterialTrackingLogView(barcode.ID);
							}
					}));             
		}

        private void SelectMaterialTrackingLogView(string materialId)
        {
            int index = 0;
			foreach (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView in this.ListViewMaterialTrackingLog.Items)
            {
                if (materialTrackingLogView.MaterialId == materialId)
                {
                    this.ListViewMaterialTrackingLog.SelectedIndex = index;
                    break;
                }
                index += 1;
            }
        }

		private YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog AddMaterialTrackingLogScan(string materialId, string materialType, string materialTrackingBatchId)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
            YellowstonePathology.Business.Facility.Model.LocationCollection locationCollection = Business.Facility.Model.LocationCollection.GetAllLocations();

			YellowstonePathology.Business.Facility.Model.Facility thisFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
			YellowstonePathology.Business.Facility.Model.Location thisLocation = locationCollection.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, materialId, materialTrackingBatchId, thisFacility.FacilityId, thisFacility.FacilityName, thisLocation.LocationId, thisLocation.Description, materialType);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingLog, this.m_PageNavigator.PrimaryMonitorWindow);
            this.m_MaterialTrackingLogCollection.Add(materialTrackingLog);
            return materialTrackingLog;
        }		

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
            if (this.IsOKToSave() == true)
            {
                this.Back(this, new EventArgs());
            }
		}
		
        private void ButtonShowTrackingDocument_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowTrackingDocument != null) ShowTrackingDocument(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(this.m_MaterialTrackingBatch, this.m_MaterialTrackingLogViewCollection));            
        }	

        private bool IsOKToSave()
        {
            bool result = true;
            if (string.IsNullOrEmpty(this.m_MaterialTrackingBatch.Description) == true)
            {
                MessageBox.Show("You must type a description for the batch before continuing.");
                result = false;
            }
            return result;
        }

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.IsOkToMoveNext() == true)
            {
                if (this.IsOKToSave() == true)
                {                    
                    this.Next(this, new EventArgs());
                }
            }
		}

        private bool IsOkToMoveNext()
        {
            bool result = true;
            bool updateIsRun = true;
			foreach (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog in this.m_MaterialTrackingLogCollection)
            {
                if (string.IsNullOrEmpty(materialTrackingLog.MasterAccessionNo) == true)
                {
                    updateIsRun = false;
                    break;
                }
            }

            if (updateIsRun == false)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("This batch may need to have the update run. Are you sure you want to continue.", "Continue?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    result = false;
                }
            }
            return result;
        }
		
		private void ButtonFinish_Click(object sender, RoutedEventArgs e)
		{            
            this.Finish(this, new EventArgs());            
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        	

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewMaterialTrackingLog.SelectedItems.Count > 0)
            {
				YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView = (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView)this.ListViewMaterialTrackingLog.SelectedItem;
				YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = this.m_MaterialTrackingLogCollection.Get(materialTrackingLogView.MaterialTrackingLogId);
                this.m_MaterialTrackingLogCollection.Remove(materialTrackingLog);                
                this.m_MaterialTrackingLogViewCollection.Remove(materialTrackingLogView);
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(materialTrackingLog, this.m_PageNavigator.PrimaryMonitorWindow);
            }
        }		

        private void ButtonPrintTrackingDocument_Click(object sender, RoutedEventArgs e)
        {
            MaterialTrackingBatchSummary materialTrackingBatchSummary = new MaterialTrackingBatchSummary(this.m_MaterialTrackingBatch, this.m_MaterialTrackingLogViewCollection);
            System.Printing.PrintQueue printQueue = new System.Printing.LocalPrintServer().DefaultPrintQueue;
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;
            printDialog.PrintQueue = printQueue;
            printDialog.PrintDocument(materialTrackingBatchSummary.FixedDocument.DocumentPaginator, "Material Tracking Batch Summary");                                     
        }

        private void ButtonPrintUpdateScans_Click(object sender, RoutedEventArgs e)
        {
            this.m_MaterialTrackingLogViewCollection.Clear();
            bool anItemCouldNotBeFound = false;

			foreach (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog in this.m_MaterialTrackingLogCollection)
            {
				YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView materialTrackingScannedItemView = null; 
                switch (materialTrackingLog.MaterialType)
                {
                    case "PSLD":
                    case "NGYNSLD":
                    case "FNASLD":
                    case "Aliquot":
                    case "Block":
                    case "FrozenBlock":
						materialTrackingScannedItemView = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMaterialTrackingScannedItemViewByAliquotOrderId(materialTrackingLog.MaterialId);                
                        break;
                    case "Slide":
						materialTrackingScannedItemView = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMaterialTrackingScannedItemViewBySlideOrderId(materialTrackingLog.MaterialId);
                        break;
                    case "Container":
                        materialTrackingScannedItemView = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMaterialTrackingScannedItemViewByContainerId(materialTrackingLog.MaterialId);
                        break;
                }

				YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView = new Business.MaterialTracking.Model.MaterialTrackingLogView();
                if (materialTrackingScannedItemView != null)
                {
                    materialTrackingLogView.FromScannedItemView(materialTrackingScannedItemView, materialTrackingLog);
                    materialTrackingLog.Update(materialTrackingScannedItemView);
                }
                else
                {
                    anItemCouldNotBeFound = true;
                    materialTrackingLogView.PFirstName = "Not Found";
                    materialTrackingLogView.PLastName = "Not Found";                    
                }
                                               
                this.m_MaterialTrackingLogViewCollection.Add(materialTrackingLogView);
            }

            if (anItemCouldNotBeFound == true)
            {
                MessageBox.Show("One or more scanned items could not be found in the database.");
            }
        }        	     
	}
}
