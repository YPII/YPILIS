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

		private YellowstonePathology.Business.Client.Model.ProviderClientCollection m_ProviderCollection;
		private YellowstonePathology.Business.Client.Model.ClientCollection m_ClientCollection;
        private YellowstonePathology.Business.Client.Model.ClientGroupCollection m_ClientGroupCollection;

        public ProviderLookupDialog()
		{
            this.m_ClientGroupCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupCollection();
			InitializeComponent();
			DataContext = this;
            this.TextBoxProviderName.Focus();

            Closing += ProviderLookupDialog_Closing;
		}

        private void ProviderLookupDialog_Closing(object sender, CancelEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        public YellowstonePathology.Business.Client.Model.ProviderClientCollection ProviderCollection
		{
			get { return this.m_ProviderCollection; }
			private set
			{
				this.m_ProviderCollection = value;
				NotifyPropertyChanged("ProviderCollection");
			}
		}

        public YellowstonePathology.Business.Client.Model.ClientGroupCollection ClientGroupCollection
        {
            get { return this.m_ClientGroupCollection; }
        }

		public YellowstonePathology.Business.Client.Model.ClientCollection ClientCollection
        {
            get { return this.m_ClientCollection; }
        }

        private void ButtonNewProvider_Click(object sender, RoutedEventArgs e)
		{
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			int physicianId = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetLargestPhysicianId() + 1;
			YellowstonePathology.Business.Domain.Physician physician = new Business.Domain.Physician(objectId, physicianId, "New Physician", "New Physician");
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(physician, this);

            ProviderEntry providerEntry = new ProviderEntry(physician);
            providerEntry.ShowDialog();
		}

        private void ButtonDeleteProvider_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewProviders.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.ProviderClient providerClient = (YellowstonePathology.Business.Client.Model.ProviderClient)this.ListViewProviders.SelectedItem;
                YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanDeleteProvider(providerClient.Physician);
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
            int clientId = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetLargestClientId() + 1;
            YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client(objectId, "New Client", clientId);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(client, this);

            ClientEntry clientEntry = new ClientEntry(client);
            clientEntry.ShowDialog();
            if (this.m_ClientCollection == null)
            {
                this.m_ClientCollection = new Business.Client.Model.ClientCollection();
            }

            this.m_ClientCollection.Insert(0, client);
        }

        private void ButtonDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewClients.SelectedItem != null)
            {

                YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListViewClients.SelectedItem;
                YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanDeleteClient(client);
                if(methodResult.Success == true)
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

        private void ButtonEnvelope_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewClients.SelectedItems.Count != 0)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListViewClients.SelectedItem;
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
                YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListViewClients.SelectedItem;
                YellowstonePathology.Business.Audit.Model.PhoneNumberAudit phoneNumberAudit = new Business.Audit.Model.PhoneNumberAudit(client.Telephone);
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
                YellowstonePathology.Business.Client.Model.ProviderClient providerClient =  (YellowstonePathology.Business.Client.Model.ProviderClient)this.ListViewProviders.SelectedItem;                
                YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullPhysician(providerClient.Physician.PhysicianId, this);

                ProviderEntry providerEntry = new ProviderEntry(physician);
				providerEntry.ShowDialog();
			}
		}

        private void DoClientSearch(string clientName)
        {            
			this.m_ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByClientName(clientName);
            NotifyPropertyChanged("ClientCollection");
			this.ListViewProviders.SelectedIndex = -1;
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
                    this.m_ProviderCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetHomeBaseProviderClientListByProviderFirstLastName(firstName, lastName);
            }
            else if (string.IsNullOrEmpty(lastName) == false)
            {
                this.m_ProviderCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetHomeBaseProviderClientListByProviderLastName(lastName);
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
				YellowstonePathology.Business.Client.Model.Client listClient = (YellowstonePathology.Business.Client.Model.Client)this.ListViewClients.SelectedItem;
                YellowstonePathology.Business.Client.Model.Client pulledClient = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClient(listClient.ClientId, this);

                ClientEntry clientEntry = new ClientEntry(pulledClient);
				clientEntry.ShowDialog();
            }
        }

        private YellowstonePathology.Business.Rules.MethodResult CanDeleteProvider(YellowstonePathology.Business.Domain.Physician physician)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            int accessionCount = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetAccessionCountByPhysicianId(physician.PhysicianId);
            if (accessionCount > 0)
            {
                result.Success = false;
                result.Message = physician.DisplayName + " has accessions and can not be deleted.";
            }
            else
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine(physician.DisplayName);
                YellowstonePathology.Business.Domain.PhysicianClientCollection physicianClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientCollectionByProviderId(physician.ObjectId);
                foreach (YellowstonePathology.Business.Domain.PhysicianClient physicianClient in physicianClientCollection)
                {
                    YellowstonePathology.Business.Client.Model.PhysicianClientDistributionCollection physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionByPhysicianClientId(physicianClient.PhysicianClientId);
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

        private void DeleteProvider(YellowstonePathology.Business.Domain.Physician physician)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(physician, this);            
        }

        private YellowstonePathology.Business.Rules.MethodResult CanDeleteClient(YellowstonePathology.Business.Client.Model.Client client)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            int accessionCount = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetAccessionCountByClientId(client.ClientId);
            if (accessionCount > 0)
            {
                result.Success = false;
                result.Message = client.ClientName + " has accessions and can not be deleted.";
            }
            else
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine(client.ClientName);
                YellowstonePathology.Business.Domain.PhysicianClientCollection physicianClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientCollectionByClientId(client.ClientId);
                foreach (YellowstonePathology.Business.Domain.PhysicianClient physicianClient in physicianClientCollection)
                {
                    YellowstonePathology.Business.Client.Model.PhysicianClientDistributionCollection physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionByPhysicianClientId(physicianClient.PhysicianClientId);
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

        private void DeleteClient(YellowstonePathology.Business.Client.Model.Client client)
        {
            YellowstonePathology.Business.Client.Model.ClientLocationCollection clientLocationCollection = client.ClientLocationCollection;            
            for (int i = clientLocationCollection.Count - 1; i > -1; i--)
            {
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(clientLocationCollection[i], this);                
            }
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(client, this);            
        }

        private void ListViewClientGroups_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewClientGroups.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.ClientGroup clientGroup = (YellowstonePathology.Business.Client.Model.ClientGroup)this.ListViewClientGroups.SelectedItem;
                ClientGroupEntry clientGroupEntry = new ClientGroupEntry(clientGroup);
                clientGroupEntry.ShowDialog();
            }
        }
    }
}
