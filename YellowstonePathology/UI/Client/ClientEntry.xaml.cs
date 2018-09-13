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
	public partial class ClientEntry : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private YellowstonePathology.Business.Client.Model.Client m_Client;
		private YellowstonePathology.Business.Billing.Model.InsuranceTypeCollection m_InsuranceTypeCollection;
		private List<string> m_FacilityTypes;
		private YellowstonePathology.Business.ReportDistribution.Model.DistributionTypeList m_DistributionTypeList;
		private YellowstonePathology.Business.View.ClientPhysicianView m_ClientPhysicianView;
		private YellowstonePathology.Business.Domain.PhysicianCollection m_PhysicianCollection;
		private YellowstonePathology.Business.Billing.Model.BillingRuleSetCollection m_BillingRuleSetCollection;
		private YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection m_ClientSupplyOrderCollection;
        private YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection m_ReferringProviderClientCollection;
        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_PathGroupFacilities;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public ClientEntry(YellowstonePathology.Business.Client.Model.Client client)
        {
            this.m_Client = client;                        
            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;

            this.m_PathGroupFacilities = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetPathGroupFacilities();
            this.m_ClientPhysicianView = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientPhysicianViewByClientIdV2(this.m_Client.ClientId);

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
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
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
                result.Message = "This provider has distributions for this client.  These distributions must be removed before the provider can be removed from the client membership.";
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

        private void ButtonAddClientLocation_Click(object sender, RoutedEventArgs e)
        {
            string location = "Medical Records";
            if (this.m_Client.ClientLocationCollection.Exists(location) == false)
            {

                string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                int locationId = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetLargestClientLocationId();
                locationId++;

                YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = new Business.Client.Model.ClientLocation();
                clientLocation.ObjectId = objectId;
                clientLocation.ClientLocationId = locationId;
                clientLocation.ClientId = this.m_Client.ClientId;
                clientLocation.Location = location;
                clientLocation.OrderType = "REQUISITION";
                clientLocation.SpecimenTrackingInitiated = "Ypii Lab";
                clientLocation.AllowMultipleOrderTypes = true;
                clientLocation.DefaultOrderPanelSetId = 13;
                clientLocation.AllowMultipleOrderDetailTypes = false;
                clientLocation.DefaultOrderDetailTypeCode = "SRGCL";
                this.m_Client.ClientLocationCollection.Add(clientLocation);                
            }
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
    }
}
