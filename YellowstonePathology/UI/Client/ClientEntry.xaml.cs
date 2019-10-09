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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Client
{
	/// <summary>
	/// Interaction logic for ClientEntry.xaml
	/// </summary>
	public partial class ClientEntry: Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private Business.Client.Model.Client m_Client;
        private Business.Client.Model.Client m_ClientClone;
        private Business.Billing.Model.InsuranceTypeCollection m_InsuranceTypeCollection;
		private List<string> m_FacilityTypes;
		private Business.ReportDistribution.Model.DistributionTypeList m_DistributionTypeList;
        private Business.View.ClientPhysicianView m_ClientPhysicianView;
		private Business.Domain.PhysicianCollection m_PhysicianCollection;
		private Business.Billing.Model.BillingRuleSetCollection m_BillingRuleSetCollection;
		private Business.Client.Model.ClientSupplyOrderCollection m_ClientSupplyOrderCollection;
        private Business.Client.Model.PhysicianClientNameCollection m_ReferringProviderClientCollection;
        private Business.Facility.Model.FacilityCollection m_PathGroupFacilities;
        private Business.Client.Model.PlaceOfServiceCollection m_PlaceOfServiceCodes;

        private Business.User.SystemIdentity m_SystemIdentity;

        public ClientEntry(Business.Client.Model.Client client)
        {
            this.m_Client = client;
            YellowstonePathology.Business.Persistence.ObjectCloner objectCloner = new Business.Persistence.ObjectCloner();
            this.m_ClientClone = (Business.Client.Model.Client)objectCloner.Clone(this.m_Client);

            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;

            this.m_PathGroupFacilities = Business.Facility.Model.FacilityCollection.GetPathGroupFacilities();
            this.m_ClientPhysicianView = Business.Gateway.PhysicianClientGateway.GetClientPhysicianViewByClientIdV2(this.m_Client.ClientId);

            this.m_PlaceOfServiceCodes = new Business.Client.Model.PlaceOfServiceCollection();

            if (this.m_ClientPhysicianView == null)
            {
                this.m_ClientPhysicianView = new Business.View.ClientPhysicianView();
            }

            this.m_InsuranceTypeCollection = new Business.Billing.Model.InsuranceTypeCollection(true);

            this.m_FacilityTypes = new List<string>();
            this.m_FacilityTypes.Add("Hospital");
            this.m_FacilityTypes.Add("Hospital Owned Clinic");
            this.m_FacilityTypes.Add("Non-Grandfathered Hospital");
            this.m_FacilityTypes.Add("Non-Hospital");

            this.m_DistributionTypeList = new YellowstonePathology.Business.ReportDistribution.Model.DistributionTypeList();
            this.m_BillingRuleSetCollection = YellowstonePathology.Business.Billing.Model.BillingRuleSetCollection.GetAllRuleSets();
            this.m_ClientSupplyOrderCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientSupplyOrderCollectionByClientId(this.m_Client.ClientId);

            InitializeComponent();

            this.DataContext = this;
            Closing += ClientEntry_Closing;
        }

        private void ClientEntry_Closing(object sender, CancelEventArgs e)
        {
            if (this.CanSave() == true)
            {
                if(this.HasDistributionTypeChanged() == true)
                {
                    MessageBoxResult result =  MessageBox.Show("The distribution type has changed." + Environment.NewLine +
                        "The distributions for the client membership will be updated to reflect this changed." + Environment.NewLine +
                        Environment.NewLine + "Press OK to continue or Cancel to return to the form.", "Distribution Change",
                        MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
                        this.m_Client.ResetDistributions();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
                }
            }
            else e.Cancel = true;
        }

        private bool HasDistributionTypeChanged()
        {
            bool result = false;
            if (string.IsNullOrEmpty(this.m_ClientClone.DistributionType) == false)
            {
                if (this.m_ClientClone.DistributionType != this.m_Client.DistributionType)
                {
                    result = true;
                }
            }

            return result;
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        public Business.Client.Model.PlaceOfServiceCollection PlaceOfServiceCodes
        {
            get { return this.m_PlaceOfServiceCodes; }
        }

        public YellowstonePathology.Business.Facility.Model.FacilityCollection PathGroupFacilities
        {
            get { return this.m_PathGroupFacilities; }
        }

        public ObservableCollection<YellowstonePathology.Business.Domain.Physician> Physicians
		{
			get { return this.m_ClientPhysicianView.Physicians; }
		}

		public List<string> FacilityTypes
		{
			get { return this.m_FacilityTypes; }
		}

		public YellowstonePathology.Business.ReportDistribution.Model.DistributionTypeList DistributionTypeList
		{
            get { return this.m_DistributionTypeList; }
		}

        public YellowstonePathology.Business.Billing.Model.InsuranceTypeCollection InsuranceTypeCollection
		{
			get { return this.m_InsuranceTypeCollection; }
		}

		public YellowstonePathology.Business.Client.Model.Client Client
		{
			get { return this.m_Client; }
		}

		public YellowstonePathology.Business.Domain.PhysicianCollection PhysicianCollection
		{
			get { return this.m_PhysicianCollection; }
		}

		public YellowstonePathology.Business.Billing.Model.BillingRuleSetCollection BillingRuleSetCollection
		{
			get { return this.m_BillingRuleSetCollection; }
		}

		public YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection ClientSupplyOrderCollection
		{
			get { return this.m_ClientSupplyOrderCollection; }
		}

        public YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection ReferringProviderClientCollection
        {
            get { return this.m_ReferringProviderClientCollection; }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{            
            if(this.CanSave() == true)
            {
                Close();
            }
		}

        private bool CanSave()
        {
            bool result = this.MaskNumberIsValid(this.MaskedTextBoxTelephone);
            if(result == true) result = this.MaskNumberIsValid(this.MaskedTextBoxFax);
            if (result == true && string.IsNullOrEmpty(this.m_Client.ClientName) == true)
            {
                result = false;
                MessageBox.Show("The Client name may not be blank.");
            }
            if (result == true)
            {
                if (string.IsNullOrEmpty(this.m_Client.DistributionType) == true)
                {
                    result = false;
                    MessageBox.Show("The Distribution Type may not be blank.");
                }
                else if(this.m_Client.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC ||
                    this.m_Client.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA ||
                    this.m_Client.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH ||
                    this.m_Client.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW)
                {
                    if (string.IsNullOrEmpty(this.m_Client.AlternateDistributionType) == true)
                    {
                        result = false;
                        MessageBox.Show("The Alternate Distribution Type must be set when the Distribution Type is " + this.m_Client.DistributionType + ".");
                    }
                }
            }

            if(result == true)
            {
                if (string.IsNullOrEmpty(this.m_Client.AlternateDistributionType) == false)
                {
                    YellowstonePathology.Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection incompatibleDistributionTypeCollection = new Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection();
                    if(incompatibleDistributionTypeCollection.TypesAreIncompatible(this.m_Client.DistributionType, this.m_Client.AlternateDistributionType) == true)
                    {
                        result = false;
                        MessageBox.Show("The Alternate Distribution Type may not be " + this.m_Client.AlternateDistributionType + " when the Distribution Type is " + 
                            this.m_Client.DistributionType + ".");
                    }
                }
            }
            return result;
        }

		private void ButtonAddToClient_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxAvailableProviders.SelectedItem != null)
			{
				YellowstonePathology.Business.Domain.Physician physician = (YellowstonePathology.Business.Domain.Physician)this.ListBoxAvailableProviders.SelectedItem;
				if (this.m_ClientPhysicianView.PhysicianExists(physician.PhysicianId) == false)
				{					
					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.Domain.PhysicianClient physicianClient = new Business.Domain.PhysicianClient(objectId, objectId, physician.PhysicianId, physician.ObjectId, this.m_Client.ClientId);
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(physicianClient, this);                    
					this.m_ClientPhysicianView.Physicians.Add(physician);
					this.NotifyPropertyChanged("Physicians");

                    string distributionObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new Business.Client.Model.PhysicianClientDistribution(distributionObjectId, physicianClient.PhysicianClientId, physicianClient.PhysicianClientId, this.m_Client.DistributionType);
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(physicianClientDistribution, this);
                }
            }
		}

		private void ButtonRemoveFromClient_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxPhysicians.SelectedItem != null)
			{
				MessageBoxResult result = MessageBox.Show("Remove selected physician?", "Remove", MessageBoxButton.OKCancel);
				if (result == MessageBoxResult.OK)
				{
					YellowstonePathology.Business.Domain.Physician physician = (YellowstonePathology.Business.Domain.Physician)this.ListBoxPhysicians.SelectedItem;
					YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(physician.ObjectId, this.m_Client.ClientId);
                    YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanRemoveMember(physicianClient);
                    if (methodResult.Success == true)
                    {
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(physicianClient, this);                        
                        this.m_ClientPhysicianView.Physicians.Remove(physician);
                        this.NotifyPropertyChanged("Physicians");
                    }
                    else
                    {
                        MessageBox.Show(methodResult.Message, "Unable to remove membership.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
		}

        private YellowstonePathology.Business.Rules.MethodResult CanRemoveMember(YellowstonePathology.Business.Domain.PhysicianClient physicianClient)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            YellowstonePathology.Business.Client.Model.PhysicianClientDistributionCollection physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionByPhysicianClientId(physicianClient.PhysicianClientId);
            if (physicianClientDistributionCollection.Count > 0)
            {
                result.Success = false;
                result.Message = "This provider has distributions set up for this client.  These distributions must be removed before the provider can be removed from the client membership.";
            }
            return result;
        }

        private void TextBoxProviderName_KeyUp(object sender, KeyEventArgs e)
		{
			if (this.TextBoxProviderName.Text.Length > 0)
			{
				string[] splitName = this.TextBoxProviderName.Text.Split(',');
				string lastName = splitName[0].Trim();
				string firstName = null;
				if (splitName.Length > 1)
				{
					firstName = splitName[1].Trim();
				}
				this.m_PhysicianCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysiciansByName(firstName, lastName);
				NotifyPropertyChanged("PhysicianCollection");
			}
		}

		private void ButtonNewSupplyOrder_Click(object sender, RoutedEventArgs e)
		{
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = new Business.Client.Model.ClientSupplyOrder(objectId, this.m_Client);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(clientSupplyOrder, this);

            ClientSupplyOrderDialog clientSupplyOrderDialog = new ClientSupplyOrderDialog(clientSupplyOrder.ClientSupplyOrderId);
			clientSupplyOrderDialog.ShowDialog();
			this.m_ClientSupplyOrderCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientSupplyOrderCollectionByClientId(this.m_Client.ClientId);
            this.NotifyPropertyChanged("ClientSupplyOrderCollection");
		}

		private void ButtonDeleteSupplyOrder_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewOrderDetails.SelectedItem != null)
			{
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this supply order?", "Delete?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = (YellowstonePathology.Business.Client.Model.ClientSupplyOrder)this.ListViewOrderDetails.SelectedItem;
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(clientSupplyOrder, this);
                    this.ClientSupplyOrderCollection.Remove(clientSupplyOrder);
                    this.NotifyPropertyChanged("ClientSupplyOrderCollection");
                }
			}
		}

		private void ListViewOrderDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.ListViewOrderDetails.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = (YellowstonePathology.Business.Client.Model.ClientSupplyOrder)this.ListViewOrderDetails.SelectedItem;
                ClientSupplyOrderDialog clientSupplyOrderDialog = new ClientSupplyOrderDialog(clientSupplyOrder.ClientSupplyOrderId);
				clientSupplyOrderDialog.ShowDialog();
			}
		}

        private void AddClientLocation(string orderType)
        {            
            if (this.m_Client.ClientLocationCollection.Exists("Medical Records") == false)
            {
                string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                int locationId = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetLargestClientLocationId();
                locationId++;

                YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = new Business.Client.Model.ClientLocation();
                clientLocation.ObjectId = objectId;
                clientLocation.ClientLocationId = locationId;
                clientLocation.ClientId = this.m_Client.ClientId;
                clientLocation.Location = "Medical Records";
                clientLocation.OrderType = orderType;
                clientLocation.SpecimenTrackingInitiated = "Ypii Lab";
                clientLocation.AllowMultipleOrderTypes = true;
                clientLocation.DefaultOrderPanelSetId = 13;
                clientLocation.AllowMultipleOrderDetailTypes = false;
                clientLocation.DefaultOrderDetailTypeCode = "SRGCL";
                this.m_Client.ClientLocationCollection.Add(clientLocation);
            }
        }

        private void ButtonAddClientLocationReq_Click(object sender, RoutedEventArgs e)
        {
            AddClientLocation("REQUISITION");
        }

        private void ButtonAddClientLocationEPIC_Click(object sender, RoutedEventArgs e)
        {
            AddClientLocation("EPIC");
        }

        private void ButtonAddReferringProviderClient_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListBoxReferringProviders.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.PhysicianClientName physicianClientName = (YellowstonePathology.Business.Client.Model.PhysicianClientName)this.ListBoxReferringProviders.SelectedItem;
                this.m_Client.ReferringProviderClientId = physicianClientName.PhysicianClientId;
                this.m_Client.ReferringProviderClientName = physicianClientName.DisplayName;
                this.m_Client.HasReferringProvider = true;
            }
        }

        private void ButtonRemoveReferringProviderClient_Click(object sender, RoutedEventArgs e)
        {
            this.m_Client.ReferringProviderClientId = null;
            this.m_Client.ReferringProviderClientName = null;
            this.m_Client.HasReferringProvider = false;
        }

        private void TextBoxReferringProviderClient_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.TextBoxReferringProviderClient.Text.Length > 0)
            {
                string[] splitName = this.TextBoxReferringProviderClient.Text.Split(' ');
                if (splitName.Length > 1)
                {
                    string providerName = splitName[0].Trim();
                    string clientName = splitName[1].Trim();
                    this.m_ReferringProviderClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientNameCollectionV2(clientName, providerName);
                    NotifyPropertyChanged("ReferringProviderClientCollection");
                }
            }
        }

        private void ButtonPrintFedexReturnLabel_Click(object sender, RoutedEventArgs e)
        {            
            for(int i=0; i<5; i++)
            {
                Business.MaterialTracking.Model.FedexAccountProduction fedExAccount = new Business.MaterialTracking.Model.FedexAccountProduction();
                Business.MaterialTracking.Model.FedexReturnLabelRequest returnLabelRequest = new Business.MaterialTracking.Model.FedexReturnLabelRequest(this.m_Client.ClientName, this.m_Client.Telephone, this.m_Client.Address, null, this.m_Client.City, this.m_Client.State, this.m_Client.ZipCode, fedExAccount);
                Business.MaterialTracking.Model.FedexProcessShipmentReply result = returnLabelRequest.RequestShipment();

                Business.Label.Model.ZPLPrinterTCP zplPrinter = new Business.Label.Model.ZPLPrinterTCP(Business.User.UserPreferenceInstance.Instance.UserPreference.FedExLabelPrinter);
                zplPrinter.Print(Business.Label.Model.ZPLPrinterTCP.DecodeZPLFromBase64(result.ZPLII));
            }                

            MessageBox.Show("Fedex labels have been sent to the printer.");            
        }

        private bool MaskNumberIsValid(Xceed.Wpf.Toolkit.MaskedTextBox maskedTextBox)
        {
            bool result = false;
            if (maskedTextBox.IsMaskFull == true && maskedTextBox.HasValidationError == false && maskedTextBox.HasParsingError == false)
            {
                result = true;
            }
            else if(maskedTextBox.IsMaskCompleted == false && maskedTextBox.HasValidationError == false && maskedTextBox.HasParsingError == false)
            {
                result = true;
            }

            if (result == false) MessageBox.Show("The Fax (or phone) number must be 10 digits or empty.");
            return result;
        }        

        private void ButtonCopyStVPhysicians_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_SystemIdentity.User.UserId == 5001 || this.m_SystemIdentity.User.UserId == 5091)
            {
                Business.Domain.PhysicianCollection physicianCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysiciansByClientIdV2(558);
                NotifyPropertyChanged("PhysicianCollection");
                foreach (YellowstonePathology.Business.Domain.Physician physician in physicianCollection)
                {
                    if (this.m_ClientPhysicianView.PhysicianExists(physician.PhysicianId) == false)
                    {
                        string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                        YellowstonePathology.Business.Domain.PhysicianClient physicianClient = new Business.Domain.PhysicianClient(objectId, objectId, physician.PhysicianId, physician.ObjectId, this.m_Client.ClientId);
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(physicianClient, this);
                        this.m_ClientPhysicianView.Physicians.Add(physician);
                        this.AddDistribution(physicianClient);
                        this.NotifyPropertyChanged("Physicians");
                    }
                }
            }
        }

        private void AddDistribution(YellowstonePathology.Business.Domain.PhysicianClient newPhysicianClient)
        {
            bool oktoAdd = true;
            List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> physicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(newPhysicianClient.PhysicianClientId);
            foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView in physicianClientDistributionViewList)
            {
                if (physicianClientDistributionView.PhysicianClientDistribution.DistributionID == newPhysicianClient.PhysicianClientId)
                {
                    oktoAdd = false;
                    break;
                }
            }

            if(oktoAdd == true)
            {
                string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new Business.Client.Model.PhysicianClientDistribution(objectId, newPhysicianClient.PhysicianClientId, newPhysicianClient.PhysicianClientId, this.m_Client.DistributionType);
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(physicianClientDistribution, this);
            }
        }
    }
}
