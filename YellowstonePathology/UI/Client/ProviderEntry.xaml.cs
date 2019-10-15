using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Diagnostics;
using System.Xml;

namespace YellowstonePathology.UI.Client
{
    public partial class ProviderEntry : Window, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		
		private YellowstonePathology.Business.Domain.Physician m_Physician;
		private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HpvStandingOrders;
		private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HPV1618StandingOrderCollection;
		private YellowstonePathology.Business.View.PhysicianClientView m_PhysicianClientView;
		private YellowstonePathology.Business.Client.Model.ClientCollection m_ClientCollection;
		private List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> m_PhysicianClientDistributionViewList;
        private YellowstonePathology.Business.Client.Model.HPVStandingOrderCollection m_HPVStandingOrderCollection;
        private YellowstonePathology.Business.Client.Model.HPVStandingOrder m_HPVStandingOrder;

        private YellowstonePathology.Business.Client.Model.HPVRuleCollection m_HPVRuleAgeCollection;
        private YellowstonePathology.Business.Client.Model.HPVRuleCollection m_HPVRulePAPResultCollection;
        private YellowstonePathology.Business.Client.Model.HPVRuleCollection m_HPVRulePreviousTestingCollection;
        private YellowstonePathology.Business.Client.Model.HPVRuleCollection m_HPVRuleEndocervicalCollection;

        private string m_PhysicianClientId;
        private Window m_ParentWindow;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public ProviderEntry(YellowstonePathology.Business.Domain.Physician physician)
        {
            this.m_Physician = physician;            

            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;
			this.m_PhysicianClientView = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientView(this.m_Physician.ObjectId);
			this.m_HpvStandingOrders = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPVStandingOrders();
			this.m_HPV1618StandingOrderCollection = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPV1618StandingOrders();
			this.m_ClientCollection = new YellowstonePathology.Business.Client.Model.ClientCollection();
            this.m_HPVStandingOrderCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetHPVStandingOrderCollectionByPhysicianId(this.m_Physician.PhysicianId);

            this.m_HPVRuleAgeCollection = Business.Client.Model.HPVRuleCollection.GetHPVRuleAgeCollection();
            this.m_HPVRulePAPResultCollection = Business.Client.Model.HPVRuleCollection.GetHPVRulePAPResultCollection();
            this.m_HPVRulePreviousTestingCollection = Business.Client.Model.HPVRuleCollection.GetHPVRulePreviousTestingCollection();
            this.m_HPVRuleEndocervicalCollection = Business.Client.Model.HPVRuleCollection.GetHPVRuleEndocervicalCollection();

            InitializeComponent();

            this.m_ParentWindow = Window.GetWindow(this);
            this.DataContext = this;
            Loaded += ProviderEntry_Loaded;
            Closing += ProviderEntry_Closing;
        }

        private void ProviderEntry_Loaded(object sender, RoutedEventArgs e)
        {
           if(this.ProviderClients.Count > 0)
            {
                this.ListBoxClientMembership.SelectedIndex = 0;
            }
        }

        private void ProviderEntry_Closing(object sender, CancelEventArgs e)
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

        public YellowstonePathology.Business.Domain.Physician Physician
        {
            get { return this.m_Physician; }            
        }

		public YellowstonePathology.Business.Client.Model.StandingOrderCollection HpvStandingOrders
		{
			get { return this.m_HpvStandingOrders; }
		}

		public YellowstonePathology.Business.Client.Model.StandingOrderCollection HPV1618StandingOrderCollection
		{
			get { return this.m_HPV1618StandingOrderCollection; }
		}


		public ObservableCollection<YellowstonePathology.Business.Client.Model.Client> ProviderClients
		{
			get { return this.m_PhysicianClientView.Clients; }
		}

		public YellowstonePathology.Business.Client.Model.ClientCollection ClientCollection
		{
			get { return this.m_ClientCollection; }
		}

		public List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> PhysicianClientDistributionViewList
		{
			get { return this.m_PhysicianClientDistributionViewList; }
		}

        public YellowstonePathology.Business.Client.Model.HPVStandingOrderCollection HPVStandingOrderCollection
        {
            get { return this.m_HPVStandingOrderCollection; }
        }

        public YellowstonePathology.Business.Client.Model.HPVStandingOrder HPVStandingOrder
        {
            get { return this.m_HPVStandingOrder; }
        }

        public YellowstonePathology.Business.Client.Model.HPVRuleCollection HPVRuleAgeCollection
        {
            get { return this.m_HPVRuleAgeCollection; }
        }

        public YellowstonePathology.Business.Client.Model.HPVRuleCollection HPVRulePAPResultCollection
        {
            get { return this.m_HPVRulePAPResultCollection; }
        }

        public YellowstonePathology.Business.Client.Model.HPVRuleCollection HPVRulePreviousTestingCollection
        {
            get { return this.m_HPVRulePreviousTestingCollection; }
        }

        public YellowstonePathology.Business.Client.Model.HPVRuleCollection HPVRuleEndocervicalCollection
        {
            get { return this.m_HPVRuleEndocervicalCollection; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            if (this.CanSave() == true)
            {
                Close();
            }
		}

        private bool CanSave()
        {
            bool result = true;

            Business.Audit.Model.AuditCollection auditCollection = new Business.Audit.Model.AuditCollection { new Business.Audit.Model.ProviderDisplayNameAudit(this.m_Physician.DisplayName),
                new YellowstonePathology.Business.Audit.Model.ProviderNpiAudit(this.m_Physician),
                new Business.Audit.Model.ProviderHomeBaseAudit(this.m_Physician),
                new Business.Audit.Model.ProviderClientsHaveDistributionSetAudit(this.m_Physician.ObjectId, this.m_PhysicianClientView) };

            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = auditCollection.Run2();
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(auditResult.Message + "  Do you want to continue?", "Missing Information", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    result = false;
                }
            }
            return result;
        }

		private void ButtonAddToClient_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxClientSelection.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClientSelection.SelectedItem;
				if (this.m_PhysicianClientView.ClientExists(client.ClientId) == false)
				{					
					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.Domain.PhysicianClient physicianClient = new Business.Domain.PhysicianClient(objectId, objectId, this.m_Physician.PhysicianId, this.m_Physician.ObjectId, client.ClientId);
					YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(physicianClient, this.m_ParentWindow);					
					this.m_PhysicianClientView.Clients.Add(client);

                    string distributionObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new Business.Client.Model.PhysicianClientDistribution(distributionObjectId, physicianClient.PhysicianClientId, physicianClient.PhysicianClientId, client.DistributionType);
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(physicianClientDistribution, this);
                }
            }
		}

		private void ButtonRemoveFromClient_Click(object sender, RoutedEventArgs e)
		{
			if(this.ListBoxClients.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClients.SelectedItem;
				YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_PhysicianClientView.ObjectId, client.ClientId);
                YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanRemoveMember(physicianClient);
                if (methodResult.Success == true)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(physicianClient, this.m_ParentWindow);                    
                    this.m_PhysicianClientView.Clients.Remove(client);
                }
                else
                {
                    MessageBox.Show(methodResult.Message, "Unable to remove membership.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
			}
		}

        private YellowstonePathology.Business.Rules.MethodResult CanRemoveMember(YellowstonePathology.Business.Domain.PhysicianClient physicianClient)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            List<Business.Client.Model.PhysicianClientDistributionView> physicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(physicianClient.PhysicianClientId);
            if(physicianClientDistributionViewList.Count > 0)
            {
                result.Success = false;
                result.Message = "This provider has distributions set up for this client.  These distributions must be removed before the provider can be removed from the client membership.";
            }
            return result;
        }

        private void TextBoxClientName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.TextBoxClientName.Text.Length > 0)
			{
				this.m_ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByClientName(this.TextBoxClientName.Text);
				NotifyPropertyChanged("ClientCollection");
			}
		}

		private void ButtonAddToDistribution_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxNewDistributionSelection.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client clientToAdd = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxNewDistributionSelection.SelectedItem;
				if (this.ListBoxClientMembership.SelectedItems.Count != 0)
				{
                    YellowstonePathology.Business.Client.Model.Client clientExisting = (Business.Client.Model.Client)this.ListBoxClientMembership.SelectedItem;

                    YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_Physician.ObjectId, clientToAdd.ClientId);
					string physicianClientId = physicianClient.PhysicianClientId;
					foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView in this.m_PhysicianClientDistributionViewList)
					{
						if (physicianClientDistributionView.PhysicianClientDistribution.DistributionID == physicianClientId)
						{
							MessageBox.Show("The item has already been added.");
							return;
						}
					}

                    string distributionType = clientToAdd.DistributionType;
                    if (AreDistributionTypesIncompatible(clientExisting.DistributionType, clientToAdd.DistributionType) == true)
                    {
                        distributionType = clientToAdd.AlternateDistributionType;
                    }

                    string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new Business.Client.Model.PhysicianClientDistribution(objectId, this.m_PhysicianClientId, physicianClientId, distributionType);
					YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(physicianClientDistribution, this);					

					this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientId);
					this.NotifyPropertyChanged("PhysicianClientDistributionViewList");
				}
			}
		}

        private bool AreDistributionTypesIncompatible(string existingDistributionType, string distributionTypeToAdd)
        {
            YellowstonePathology.Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection incompatibleDistributionTypeCollection = new Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection();
            bool result = incompatibleDistributionTypeCollection.TypesAreIncompatible(existingDistributionType, distributionTypeToAdd);
            return result;
        }

        private void ButtonRemoveFromDistribution_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxDistributionSelection.SelectedItem != null)
			{
				MessageBoxResult result = MessageBox.Show("Remove the selected item?", "Remove", MessageBoxButton.OKCancel);
				if (result == MessageBoxResult.OK)
				{
					YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView = (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView)this.ListBoxDistributionSelection.SelectedItem;
					YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(physicianClientDistributionView.PhysicianClientDistribution, this.m_ParentWindow);					

					this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientId);
					this.NotifyPropertyChanged("PhysicianClientDistributionViewList");
				}
			}
		}

		private void ListBoxClientMembership_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(this.ListBoxClientMembership.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClientMembership.SelectedItem;
				YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_Physician.ObjectId, client.ClientId);
				this.m_PhysicianClientId = physicianClient.PhysicianClientId;
				this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientId);
				this.NotifyPropertyChanged("PhysicianClientDistributionViewList");
			}
		}

        private void TextBoxNames_KeyUp(object sender, KeyEventArgs e)
        {
            this.CreateDisplayName();
        }

        private void CreateDisplayName()
        {
            string firstName = this.TextBoxFirstName.Text;
            string middleInitial = this.TextBoxMiddleInitial.Text;
            string lastName = this.TextBoxLastName.Text;
            string credentials = this.TextBoxCredentials.Text;

            StringBuilder displayName = new StringBuilder();
            if (string.IsNullOrEmpty(firstName) == false)
            {
                displayName.Append(firstName);
            }

            if (string.IsNullOrEmpty(middleInitial) == false)
            {
                if(displayName.Length > 0)
                {
                    displayName.Append(" ");
                }
                displayName.Append(middleInitial);
                displayName.Append(".");
            }

            if (string.IsNullOrEmpty(lastName) == false)
            {
                if (displayName.Length > 0)
                {
                    displayName.Append(" ");
                }
                displayName.Append(lastName);
            }

            if(string.IsNullOrEmpty(credentials) == false)
            {
                if (displayName.Length > 0)
                {
                    displayName.Append(", ");
                }
                displayName.Append(credentials);
            }
            this.m_Physician.DisplayName = displayName.ToString();
        }

        private void ComboBoxHomeBase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ComboBoxHomeBase.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.Client selectedHomeBaseClient = (YellowstonePathology.Business.Client.Model.Client)this.ComboBoxHomeBase.SelectedItem;
                this.m_Physician.HomeBaseClientId = selectedHomeBaseClient.ClientId;
            }
        }

        private void ListViewStandingOrders_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if(this.ListViewStandingOrders.SelectedItem != null)
            {
                this.m_HPVStandingOrder = (YellowstonePathology.Business.Client.Model.HPVStandingOrder)this.ListViewStandingOrders.SelectedItem;
                this.NotifyPropertyChanged("HPVStandingOrder");
            }
        }

        private void ButtonAddHPVStandingOrder_Click(object sender, RoutedEventArgs e)
        {
        }

        private void HyperLinkShowReflexLetter_Click(object sender, RoutedEventArgs e)
        {
            string reflexLetterPath = @"\\CFILESERVER\Documents\Clients\ProviderReflexLetter.xml";
            XmlDocument letterTemplate = new XmlDocument();            
            letterTemplate.Load(reflexLetterPath);

            XmlNamespaceManager nameSpaceManager = new XmlNamespaceManager(letterTemplate.NameTable);
            nameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
            
            XmlNode nodeProviderName = letterTemplate.SelectSingleNode("//w:t[.='provider_name']", nameSpaceManager);
            nodeProviderName.InnerText = this.m_Physician.DisplayName;

            Business.Client.Model.StandingOrder hpvStandingOrder = Business.Client.Model.StandingOrderCollection.GetByStandingOrderCode(this.Physician.HPVStandingOrderCode);
            XmlNode nodeHPVStandingOrder = letterTemplate.SelectSingleNode("//w:t[.='hpv_standing_order']", nameSpaceManager);
            nodeHPVStandingOrder.InnerText = hpvStandingOrder.Description;

            Business.Client.Model.StandingOrder hpv1618StandingOrder = Business.Client.Model.StandingOrderCollection.GetByStandingOrderCode(this.Physician.HPV1618StandingOrderCode);
            XmlNode nodeHPV1618StandingOrder = letterTemplate.SelectSingleNode("//w:t[.='hpv1618_standing_order']", nameSpaceManager);
            nodeHPV1618StandingOrder.InnerText = hpv1618StandingOrder.Description;

            string thisProviderLetterPath = @"\\CFILESERVER\Documents\Clients\Provider_" + this.m_Physician.LastName + this.m_Physician.PhysicianId + ".doc";
            letterTemplate.Save(thisProviderLetterPath);

            Process p1 = new Process();
            p1.StartInfo = new ProcessStartInfo("winword.exe", thisProviderLetterPath);
            p1.Start();
            p1.Close();
        }
    }
}
