using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace YellowstonePathology.UI.Client
{
	/// <summary>
	/// Interaction logic for ProviderLookupDialog.xaml
	/// </summary>
	public partial class ProviderLookupDialog : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private Business.Client.Model.ProviderClientCollection m_ProviderCollection;
		private Business.Client.Model.ClientCollection m_ClientCollection;
        private Business.Client.Model.ClientGroupCollection m_ClientGroupCollection;
        private Business.Facility.Model.FacilityCollection m_FacilityCollection;

        public ProviderLookupDialog()
		{
            this.m_ClientGroupCollection = Business.Gateway.PhysicianClientGateway.GetClientGroupCollection();
            this.m_FacilityCollection = Business.Gateway.PhysicianClientGateway.GetAllFacilities();
            InitializeComponent();
			DataContext = this;
            this.TextBoxProviderName.Focus();

            Closing += ProviderLookupDialog_Closing;
		}

        private void ProviderLookupDialog_Closing(object sender, CancelEventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        public Business.Client.Model.ProviderClientCollection ProviderCollection
		{
			get { return this.m_ProviderCollection; }
			private set
			{
				this.m_ProviderCollection = value;
				NotifyPropertyChanged("ProviderCollection");
			}
		}

        public Business.Client.Model.ClientGroupCollection ClientGroupCollection
        {
            get { return this.m_ClientGroupCollection; }
        }

		public Business.Client.Model.ClientCollection ClientCollection
        {
            get { return this.m_ClientCollection; }
        }

        public Business.Facility.Model.FacilityCollection FacilityCollection
        {
            get { return this.m_FacilityCollection; }
        }

        private void ButtonNewProvider_Click(object sender, RoutedEventArgs e)
		{
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			int physicianId = Business.Gateway.PhysicianClientGateway.GetLargestPhysicianId() + 1;
			Business.Domain.Physician physician = new Business.Domain.Physician(objectId, physicianId, "New Physician", "New Physician");
            Business.Persistence.DocumentGateway.Instance.InsertDocument(physician, this);

            ProviderEntry providerEntry = new ProviderEntry(physician);
            providerEntry.ShowDialog();
		}

        private void ButtonDeleteProvider_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewProviders.SelectedItem != null)
            {
                Business.Client.Model.ProviderClient providerClient = (Business.Client.Model.ProviderClient)this.ListViewProviders.SelectedItem;
                Business.Rules.MethodResult methodResult = this.CanDeleteProvider(providerClient.Physician);
                if (methodResult.Success == true)
                {
                    this.DeleteProvider(providerClient.Physician);
                    this.DoProviderSearch();
                }
                else
                {
                    MessageBox.Show(methodResult.Message, "Unable to delete provider.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void ButtonNewClient_Click(object sender, RoutedEventArgs e)
        {
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            int clientId = Business.Gateway.PhysicianClientGateway.GetLargestClientId() + 1;
            Business.Client.Model.Client client = new Business.Client.Model.Client(objectId, "New Client", clientId);
            Business.Persistence.DocumentGateway.Instance.InsertDocument(client, this);

            ClientEntry clientEntry = new ClientEntry(client);
            clientEntry.ShowDialog();
            if (this.m_ClientCollection == null)
            {
                this.m_ClientCollection = new Business.Client.Model.ClientCollection();
            }

            this.m_ClientCollection.Insert(0, client);
        }

        private void ButtonClientFedX_Click(object sender, RoutedEventArgs e)
        {
            Business.Facility.Model.Facility facility = null;
            if (this.ListViewClients.SelectedItem != null)
            {
                Business.Client.Model.Client client = (Business.Client.Model.Client)this.ListViewClients.SelectedItem;
                facility = Business.Facility.Model.FacilityCollection.Instance.GetByClientId(client.ClientId);
                if (facility != null)
                {
                    ClientFedxDialog dlg = new Client.ClientFedxDialog(facility);
                    dlg.ShowDialog();
                }
                else
                {
                    MessageBox.Show("The selected client is not associated with a facility.  Select from the facility tab.");
                }
            }
            else
            {
                MessageBox.Show("No client is selected.");
            }
        }

        private void MenuItemDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewClients.SelectedItem != null)
            {

                Business.Client.Model.Client client = (Business.Client.Model.Client)this.ListViewClients.SelectedItem;
                Business.Rules.MethodResult methodResult = this.CanDeleteClient(client);
                if (methodResult.Success == true)
                {
                    this.DeleteClient(client);
                    this.DoClientSearch(this.TextBoxClientName.Text);
                }
                else
                {
                    MessageBox.Show(methodResult.Message, "Unable to delete client.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void ButtonNewFacility_Click(object sender, RoutedEventArgs e)
        {
            FacilityEntry facilityEntry = new FacilityEntry(null, true);
            facilityEntry.ShowDialog();
            this.m_FacilityCollection = Business.Gateway.PhysicianClientGateway.GetAllFacilities();
            this.NotifyPropertyChanged("FacilityCollection");
        }

        private void ButtonDeleteFacility_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewFacilities.SelectedItem != null)
            {
                Business.Facility.Model.Facility facility = (Business.Facility.Model.Facility)this.ListViewFacilities.SelectedItem;
                Business.Rules.MethodResult methodResult = this.CanDeleteFacililty(facility);
                if (methodResult.Success == true)
                {
                    this.DeleteFacility(facility);
                    Business.Facility.Model.FacilityCollection.Refresh();
                    this.m_FacilityCollection = Business.Gateway.PhysicianClientGateway.GetAllFacilities();
                    this.NotifyPropertyChanged("FacilityCollection");
                }
                else
                {
                    MessageBox.Show(methodResult.Message, "Unable to delete at this time", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void ButtonEnvelope_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewClients.SelectedItems.Count != 0)
			{
				Business.Client.Model.Client client = (Business.Client.Model.Client)this.ListViewClients.SelectedItem;
				Envelope envelope = new Envelope();
				string address = client.Address;
				string name = client.ClientName;
				string city = client.City;
				string state = client.State;
				string zip = client.ZipCode;
				envelope.PrintEnvelope(name, address, city, state, zip);
			}
		}

		private void ButtonRequisition_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewClients.SelectedItems.Count != 0)
			{
                Business.Client.Model.Client client = (Business.Client.Model.Client)this.ListViewClients.SelectedItem;
                Business.Audit.Model.PhoneNumberAudit phoneNumberAudit = new Business.Audit.Model.PhoneNumberAudit(client.Telephone);
                phoneNumberAudit.Run();
                if (phoneNumberAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    MessageBox.Show(phoneNumberAudit.Message.ToString(), "Unable to print requisitions", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
				Client.RequisitionOptionsDialog requisitionOptionsDialog = new RequisitionOptionsDialog(client.ClientId, client.ClientName);
				requisitionOptionsDialog.ShowDialog();
			}
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            this.Close();
		}

		private void TextBoxProviderName_KeyUp(object sender, KeyEventArgs e)
		{
			if (!string.IsNullOrEmpty(this.TextBoxProviderName.Text))
			{
				this.DoProviderSearch();
			}
		}

        private void TextBoxClientName_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TextBoxClientName.Text))
            {
                this.DoClientSearch(this.TextBoxClientName.Text);
            }
        }

        private void ListBoxProviders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.ListViewProviders.SelectedItem != null)
			{
                Business.Client.Model.ProviderClient providerClient =  (Business.Client.Model.ProviderClient)this.ListViewProviders.SelectedItem;                
                Business.Domain.Physician physician = Business.Persistence.DocumentGateway.Instance.PullPhysician(providerClient.Physician.PhysicianId, this);

                ProviderEntry providerEntry = new ProviderEntry(physician);
				providerEntry.ShowDialog();
			}
		}

        private void DoClientSearch(string clientName)
        {            
			this.m_ClientCollection = Business.Gateway.PhysicianClientGateway.GetClientsByClientName(clientName);
            NotifyPropertyChanged("ClientCollection");
			this.ListViewClients.SelectedIndex = -1;
        }

		private void DoProviderSearch()
		{
			string firstName = string.Empty;
			string lastName = string.Empty;
			string[] commaSplit = this.TextBoxProviderName.Text.Split(',');
			lastName = commaSplit[0].Trim();
            if (commaSplit.Length > 1)
            {
                firstName = commaSplit[1].Trim();
            }
            if (string.IsNullOrEmpty(lastName) == false && string.IsNullOrEmpty(firstName) == false)
            {
                    this.m_ProviderCollection = Business.Gateway.PhysicianClientGateway.GetHomeBaseProviderClientListByProviderFirstLastName(firstName, lastName);
            }
            else if (string.IsNullOrEmpty(lastName) == false)
            {
                this.m_ProviderCollection = Business.Gateway.PhysicianClientGateway.GetHomeBaseProviderClientListByProviderLastName(lastName);
            }

            NotifyPropertyChanged("ProviderCollection");
			this.ListViewProviders.SelectedIndex = -1;
		}

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        

        private void ListBoxClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
			if (this.ListViewClients.SelectedItem != null)
            {
				Business.Client.Model.Client listClient = (Business.Client.Model.Client)this.ListViewClients.SelectedItem;
                Business.Client.Model.Client pulledClient = Business.Persistence.DocumentGateway.Instance.PullClient(listClient.ClientId, this);

                ClientEntry clientEntry = new ClientEntry(pulledClient);
				clientEntry.ShowDialog();
            }
        }

        private void ListBoxFacility_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewFacilities.SelectedItem != null)
            {
                Business.Facility.Model.Facility listFacility = (Business.Facility.Model.Facility)this.ListViewFacilities.SelectedItem;
                Business.Facility.Model.Facility pulledFacility = Business.Persistence.DocumentGateway.Instance.PullFacility (listFacility.FacilityId, this);

                FacilityEntry facilityEntry = new FacilityEntry(pulledFacility, false);
                facilityEntry.ShowDialog();
                this.m_FacilityCollection = Business.Gateway.PhysicianClientGateway.GetAllFacilities();
                this.NotifyPropertyChanged("FacilityCollection");
            }
        }

        private Business.Rules.MethodResult CanDeleteProvider(Business.Domain.Physician physician)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            int accessionCount = Business.Gateway.PhysicianClientGateway.GetAccessionCountByPhysicianId(physician.PhysicianId);
            if (accessionCount > 0)
            {
                result.Success = false;
                result.Message = physician.DisplayName + " has accessions and can not be deleted.";
            }
            else
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine(physician.DisplayName);
                Business.Domain.PhysicianClientCollection physicianClientCollection = Business.Gateway.PhysicianClientGateway.GetPhysicianClientCollectionByProviderId(physician.ObjectId);
                foreach (Business.Domain.PhysicianClient physicianClient in physicianClientCollection)
                {
                    Business.Client.Model.PhysicianClientDistributionCollection physicianClientDistributionCollection = Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionByPhysicianClientId(physicianClient.PhysicianClientId);
                    if (physicianClientDistributionCollection.Count > 0)
                    {
                        result.Success = false;
                        msg.AppendLine("- has existing distributions.  The distributions must be removed before the provider may be deleted.");
                        break;
                    }
                }

                if (physicianClientCollection.Count > 0)
                {
                    result.Success = false;
                    msg.Append("- is a member of " + physicianClientCollection.Count.ToString() + " client/s.  The membership must be removed before the provider may be deleted.");
                }
                result.Message = msg.ToString();
            }

            return result;
        }

        private void DeleteProvider(Business.Domain.Physician physician)
        {
            Business.Persistence.DocumentGateway.Instance.DeleteDocument(physician, this);            
        }

        private Business.Rules.MethodResult CanDeleteClient(Business.Client.Model.Client client)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            int accessionCount = Business.Gateway.PhysicianClientGateway.GetAccessionCountByClientId(client.ClientId);
            if (accessionCount > 0)
            {
                result.Success = false;
                result.Message = client.ClientName + " has accessions and can not be deleted.";
            }
            else
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine(client.ClientName);
                Business.Domain.PhysicianClientCollection physicianClientCollection = Business.Gateway.PhysicianClientGateway.GetPhysicianClientCollectionByClientId(client.ClientId);
                foreach (Business.Domain.PhysicianClient physicianClient in physicianClientCollection)
                {
                    Business.Client.Model.PhysicianClientDistributionCollection physicianClientDistributionCollection = Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionByPhysicianClientId(physicianClient.PhysicianClientId);
                    if(physicianClientDistributionCollection.Count > 0)
                    {
                        result.Success = false;
                        msg.AppendLine("- has distributions.  The distributions must be removed before the client can be deleted.");
                        break;
                    }
                }

                if (physicianClientCollection.Count > 0)
                {
                    result.Success = false;
                    msg.AppendLine("- has membereship.  The membership must be removed before the client can be deleted.");
                }
                result.Message = msg.ToString();
            }

            return result;
        }

        private void DeleteClient(Business.Client.Model.Client client)
        {            
            Business.Persistence.DocumentGateway.Instance.DeleteDocument(client, this);            
        }

        private Business.Rules.MethodResult CanDeleteFacililty(Business.Facility.Model.Facility facility)
        {
            Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            result.Success = false;
            result.Message = "See Sid";            
            return result;
        }

        private void DeleteFacility(Business.Facility.Model.Facility facility)
        {
            Business.Persistence.DocumentGateway.Instance.DeleteDocument(facility, this);
        }

        private void ListViewClientGroups_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewClientGroups.SelectedItem != null)
            {
                Business.Client.Model.ClientGroup clientGroup = (Business.Client.Model.ClientGroup)this.ListViewClientGroups.SelectedItem;
                ClientGroupEntry clientGroupEntry = new ClientGroupEntry(clientGroup);
                clientGroupEntry.ShowDialog();
            }
        }

        private void ButtonFacilityFedx_Click(object sender, RoutedEventArgs e)
        {
            Business.Facility.Model.Facility facility = null;
            if (this.ListViewFacilities.SelectedItem != null)
            {
                facility = (Business.Facility.Model.Facility)this.ListViewFacilities.SelectedItem;
                ClientFedxDialog dlg = new Client.ClientFedxDialog(facility);
                dlg.ShowDialog();
            }
            else
            {
                MessageBox.Show("No Facility is selected.");
            }
        }

        private void ButtonNPICheck_Click(object sender, RoutedEventArgs e)
        {
            this.m_ProviderCollection = Business.Gateway.PhysicianClientGateway.GetHomeBaseProviderClientListByMissingNPI();
            this.NotifyPropertyChanged(string.Empty);
        }

        private void MenuItemEditFacility_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemAddFacility_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemEditClientGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemAddClientGroup_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
