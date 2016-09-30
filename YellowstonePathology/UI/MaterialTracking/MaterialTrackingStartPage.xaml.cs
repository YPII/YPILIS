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
	public partial class MaterialTrackingStartPage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void FacilitySelectionEventHandler(object sender, CustomEventArgs.FacilitySelectionReturnEventArgs e);
		public event FacilitySelectionEventHandler FacilitySelection;

		public delegate void ViewBatchEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs e);
        public event ViewBatchEventHandler ViewBatch;        

        private YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection m_MaterialTrackingBatchCollection;
        private bool m_UseMasterAccessionNo;
        private string m_MasterAccessionNo;        

		public MaterialTrackingStartPage(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection materialTrackingBatchCollection)
		{
            this.m_MaterialTrackingBatchCollection = materialTrackingBatchCollection;            
			InitializeComponent();
            this.DataContext = this;
            this.Loaded += MaterialTrackingStartPage_Loaded;
		}        

        public MaterialTrackingStartPage(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection materialTrackingBatchCollection, string masterAccessionNo)
        {
            this.m_MaterialTrackingBatchCollection = materialTrackingBatchCollection;
            this.m_UseMasterAccessionNo = true;
            this.m_MasterAccessionNo = masterAccessionNo;            

            InitializeComponent();
            this.DataContext = this;


            this.Loaded += MaterialTrackingStartPage_Loaded;
        }

        private void MaterialTrackingStartPage_Loaded(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(Window.GetWindow(this));
        }

        public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection MaterialTrackingBatchCollection
        {
            get { return this.m_MaterialTrackingBatchCollection; }
        }

		private void HyperlinkNewOutbound_Click(object sender, RoutedEventArgs e)
		{
            this.FacilitySelection(this, new CustomEventArgs.FacilitySelectionReturnEventArgs("Sent"));
		}        

        private void HyperlinkSendMaterial_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.NullFacility();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location fromLocation = fromFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, null, fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));            
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
        }

        private void HyperlinkReceiveMaterial_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.NullFacility();
            YellowstonePathology.Business.Facility.Model.Location fromLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location toLocation = toFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, null, fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();			
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
        }

        private void HyperlinkSendMaterialToDrClegg_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location fromLocation = fromFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistCody();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.PamCleggOffice();

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Dr. Clegg", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();			
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
        }

        private void HyperlinkReceiveMaterialFromDrClegg_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistCody();
            YellowstonePathology.Business.Facility.Model.Location fromLocation =  new YellowstonePathology.Business.Facility.Model.LocationDefinitions.PamCleggOffice();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location toLocation = toFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Dr. Clegg", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();			
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
        }

        private void HyperlinkSendMaterialFromCodyToBillings_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistCody();
            YellowstonePathology.Business.Facility.Model.Location fromLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.PamCleggOffice();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Billings", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();            
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
        }

        private void HyperlinkReceiveMaterialFromBillings_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            YellowstonePathology.Business.Facility.Model.Location fromLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteCody();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new Business.Facility.Model.LocationDefinitions.PamCleggOffice();

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Billings", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();            
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
        }

        private void HyperlinkSendMaterialToDrKurtzman_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location fromLocation = fromFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.MontanaDepartmentofJustice();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Dr. Kurtzman", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();            
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
        }

        private void HyperlinkReceiveMaterialFromDrKurtzman_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.MontanaDepartmentofJustice();
            YellowstonePathology.Business.Facility.Model.Location fromLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location toLocation = toFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Dr. Kurtzman", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();            
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));

        }

        private void HyperlinkSendMaterialToDrShannon_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location fromLocation = fromFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.ButtePathology();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Dr. Shannon", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();            
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
        }

        private void HyperlinkReceiveMaterialFromDrShannon_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.ButtePathology();
            YellowstonePathology.Business.Facility.Model.Location fromLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location toLocation = toFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Dr. Shannon", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();            
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
        }

        private void HyperlinkSendMaterialToDrMatthews_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location fromLocation = fromFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.BillingsClinic();
            YellowstonePathology.Business.Facility.Model.Location toLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Dr. Matthews", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
        }

        private void HyperlinkReceiveMaterialFromDrMatthews_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.BillingsClinic();
            YellowstonePathology.Business.Facility.Model.Location fromLocation = new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation();

            YellowstonePathology.Business.Facility.Model.Facility toFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location toLocation = toFacility.Locations.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Dr. Matthews", fromFacility, fromLocation, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = new Business.MaterialTracking.Model.MaterialTrackingLogCollection();
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
        }


        private void HyperlinkNewInbound_Click(object sender, RoutedEventArgs e)
		{
			this.FacilitySelection(this, new CustomEventArgs.FacilitySelectionReturnEventArgs("Received"));
		}

        private void HyperlinkViewSelectedBatch_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxMaterialTrackingBatch.SelectedItem != null)
            {
                string materialTrackingBatchId = ((YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch)this.ListBoxMaterialTrackingBatch.SelectedItem).MaterialTrackingBatchId;

                YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullMaterialTrackingBatch(materialTrackingBatchId, Window.GetWindow(this));
				YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = null;

                if (this.m_UseMasterAccessionNo == true)
                {
                    materialTrackingLogCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingLogCollectionByBatchIdMasterAccessionNo(materialTrackingBatch.MaterialTrackingBatchId, this.m_MasterAccessionNo);
                }
                else
                {
                    materialTrackingLogCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingLogCollectionByBatchId(materialTrackingBatch.MaterialTrackingBatchId);
                }                                
                this.ViewBatch(this, new CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch, materialTrackingLogCollection));
            }
            else
            {
                MessageBox.Show("Please select a batch to view.");
            }
        }

        private void HyperlinkDeleteSelectedBatch_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxMaterialTrackingBatch.SelectedItem != null)
            {
				YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch)this.ListBoxMaterialTrackingBatch.SelectedItem;
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(materialTrackingBatch, Window.GetWindow(this));
                this.m_MaterialTrackingBatchCollection.Remove(materialTrackingBatch);
                this.NotifyPropertyChanged(string.Empty);
            }
            else
            {
                MessageBox.Show("Please select a batch to delete.");
            }
        }        
		
		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
            Window.GetWindow(this).Close();			
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
