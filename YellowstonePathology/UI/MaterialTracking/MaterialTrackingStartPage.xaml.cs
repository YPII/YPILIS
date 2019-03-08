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

        private void HyperlinkSendMaterial_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility toFacility = new YellowstonePathology.Business.Facility.Model.Facility();

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string fromLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, null, fromFacility, fromLocation, toFacility, null, this.m_MasterAccessionNo);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));            
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkReceiveMaterial_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = new YellowstonePathology.Business.Facility.Model.Facility();
            string fromLocation = null;

            YellowstonePathology.Business.Facility.Model.Facility toFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, null, fromFacility, fromLocation, toFacility, null, this.m_MasterAccessionNo);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));
            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkSendMaterialToDrClegg_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string fromLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            YellowstonePathology.Business.Facility.Model.Facility toFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPCDY");


            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Dr. Clegg", fromFacility, fromLocation, toFacility, null, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkReceiveMaterialFromDrClegg_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPCDY");

            YellowstonePathology.Business.Facility.Model.Facility toFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string toLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Dr. Clegg", fromFacility, null, toFacility, toLocation, this.m_MasterAccessionNo);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkSendMaterialFromCodyToBillings_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPCDY");
            string fromLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            YellowstonePathology.Business.Facility.Model.Facility toFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Billings", fromFacility, fromLocation, toFacility, null, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkReceiveMaterialFromBillings_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            YellowstonePathology.Business.Facility.Model.Facility toFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPICDY");
            string toLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Billings", fromFacility, null, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkSendMaterialToDrKurtzman_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string fromLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            YellowstonePathology.Business.Facility.Model.Facility toFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("MTDPTJC");

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Dr. Kurtzman", fromFacility, fromLocation, toFacility, null, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkReceiveMaterialFromDrKurtzman_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("MTDPTJC");

            YellowstonePathology.Business.Facility.Model.Facility toFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string toLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Dr. Kurtzman", fromFacility, null, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));

        }

        private void HyperlinkSendMaterialToDrShannon_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string fromLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            YellowstonePathology.Business.Facility.Model.Facility toFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("BTTPTHLGY");

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Dr. Shannon", fromFacility, fromLocation, toFacility, null, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkReceiveMaterialFromDrShannon_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("BTTPTHLGY");

            YellowstonePathology.Business.Facility.Model.Facility toFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string toLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Dr. Shannon", fromFacility, null, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkSendMaterialToDrMatthews_Click(object sender, RoutedEventArgs e)
        {

            YellowstonePathology.Business.Facility.Model.Facility fromFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string fromLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            YellowstonePathology.Business.Facility.Model.Facility toFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("BLGSCLNC");

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Dr. Matthews", fromFacility, fromLocation, toFacility, null, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkReceiveMaterialFromDrMatthews_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("BLGSCLNC");

            YellowstonePathology.Business.Facility.Model.Facility toFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string toLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Dr. Matthews", fromFacility, null, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkSendMaterialFromBillingsToBozeman_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string fromLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            YellowstonePathology.Business.Facility.Model.Facility toFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBZMN");

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to Bozeman.", fromFacility, fromLocation, toFacility, null, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkReceiveMaterialFromBozeman_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBZMN");

            YellowstonePathology.Business.Facility.Model.Facility toFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string toLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from Bozeman", fromFacility, null, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
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

                if (this.m_UseMasterAccessionNo == true)
                {
                    materialTrackingBatch = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullMaterialTrackingBatchWithMasterAccessionNo(materialTrackingBatchId, this.m_MasterAccessionNo, Window.GetWindow(this));
                }
                else
                {
                   materialTrackingBatch = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullMaterialTrackingBatch(materialTrackingBatchId, Window.GetWindow(this));
                }

                this.ViewBatch(this, new CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
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

        private void HyperlinkSendMaterialToOnsiteStorage_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string fromLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            YellowstonePathology.Business.Facility.Model.Facility toFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("PNSTSTRG");

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to onsite storage.", fromFacility, fromLocation, toFacility, null, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkReceiveMaterialFromOnsiteStorage_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("PNSTSTRG");

            YellowstonePathology.Business.Facility.Model.Facility toFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string toLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from onsite storage.", fromFacility, null, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkSendMaterialToOffsiteStorage_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string fromLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            YellowstonePathology.Business.Facility.Model.Facility toFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("PFFSTSTRG");

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Send material to offsite storage.", fromFacility, fromLocation, toFacility, null, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }

        private void HyperlinkReceiveMaterialFromOffsiteStorage_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility fromFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("PFFSTSTRG");

            YellowstonePathology.Business.Facility.Model.Facility toFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            string toLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch(objectId, "Receive material from offsite storage.", fromFacility, null, toFacility, toLocation, this.m_MasterAccessionNo);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingBatch, Window.GetWindow(this));

            this.ViewBatch(this, new YellowstonePathology.UI.CustomEventArgs.MaterialTrackingBatchEventArgs(materialTrackingBatch));
        }
    }
}
