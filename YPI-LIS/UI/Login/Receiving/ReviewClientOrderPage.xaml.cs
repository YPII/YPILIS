﻿using System;
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

namespace YellowstonePathology.UI.Login.Receiving
{	
	public partial class ReviewClientOrderPage : UserControl, INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

        public delegate void ViewAccessionOrderEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs e);
        public event ViewAccessionOrderEventHandler ViewAccessionOrder;

        public delegate void CreateNewAccessionEventHandler(object sender, EventArgs e);
        public event CreateNewAccessionEventHandler CreateNewAccessionOrder;

        public delegate void SelectedClientOrdersEventHandler(object sender, CustomEventArgs.ClientOrderCollectionReturnEventArgs e);
        public event SelectedClientOrdersEventHandler SelectedClientOrders;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;		
		private YellowstonePathology.Business.Rules.Surgical.PatientRecentAccessions m_PatientRecentAccessions;
		private string m_PageHeaderText = "Review Client Order Page";
		private UI.Navigation.PageNavigator m_PageNavigator;        
		private YellowstonePathology.Business.Domain.Physician m_Physician;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;

        private Visibility m_CloseButtonVisibility;
        private Visibility m_NextButtonVisibility;

		public ReviewClientOrderPage(YellowstonePathology.Business.Rules.Surgical.PatientRecentAccessions patientRecentAccessions,
			YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder,
			UI.Navigation.PageNavigator pageNavigator)
		{
			this.m_PatientRecentAccessions = patientRecentAccessions;
			this.m_ClientOrder = clientOrder;            			
			this.m_PageNavigator = pageNavigator;

            this.SetClientOrderCollection();

            InitializeComponent();

			this.DataContext = this;

            if (this.m_ClientOrder.Accessioned == true)
            {
                this.m_CloseButtonVisibility = System.Windows.Visibility.Collapsed;
                this.m_NextButtonVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.m_CloseButtonVisibility = System.Windows.Visibility.Visible;
                this.m_NextButtonVisibility = System.Windows.Visibility.Collapsed;
            }

			this.Loaded += new RoutedEventHandler(ReviewClientOrderPage_Loaded);
		}

		private void ReviewClientOrderPage_Loaded(object sender, RoutedEventArgs e)
		{
            this.HandleProviderMapping();
            foreach(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in this.ListViewClientOrders.Items)
            {
                if(clientOrder.ClientOrderId == this.m_ClientOrder.ClientOrderId)
                {
                    this.ListViewClientOrders.SelectedItem = clientOrder;
                    break;
                }
            }
        }

        private void HandleProviderMapping()
        {
            if (this.m_ClientOrder.SystemInitiatingOrder == "EPIC")
            {                
                this.m_Physician = Business.Gateway.PhysicianClientGateway.GetPhysicianByNpi(this.m_ClientOrder.ProviderId);
                this.NotifyPropertyChanged("Physician");                
            }
            else if (this.m_ClientOrder.SystemInitiatingOrder == "NMH")
            {
                this.m_Physician = Business.Gateway.PhysicianClientGateway.GetPhysicianByClientPhysicianId(this.m_ClientOrder.ProviderId);
                if(this.m_Physician != null) this.m_ClientOrder.ProviderId = this.m_Physician.Npi;
                this.NotifyPropertyChanged("Physician");
            }
        }

        public Visibility CloseButtonVisibility
        {
            get { return this.m_CloseButtonVisibility; }
        }

        public Visibility NextButtonVisibility
        {
            get { return this.m_NextButtonVisibility; }
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

		public YellowstonePathology.Business.View.RecentAccessionViewCollection RecentAccessionViewCollection
		{
			get { return this.m_PatientRecentAccessions.RecentAccessions; }
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
		{
			get { return this.m_ClientOrder; }
		}

		public YellowstonePathology.Business.Domain.Physician Physician
		{
			get { return this.m_Physician; }
			set
			{
				this.m_Physician = value;
				this.NotifyPropertyChanged("Physician");
			}
		}

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection ClientOrderCollection
        {
            get { return this.m_ClientOrderCollection; }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null) this.Next(this, new EventArgs());
        }

		private void ButtonNewAccession_Click(object sender, RoutedEventArgs e)
		{
            if (this.m_ClientOrder.Accessioned == false)
            {
                if (this.m_ClientOrder.SystemInitiatingOrder == "EPIC" && this.m_Physician == null)
                {
                    MessageBox.Show("This order cannot be accessioned because the provider cannot be mapped.", "Unable to map the provider.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else if(this.m_ClientOrder.SystemInitiatingOrder == "EPIC" && this.PhysicianHasDistributionsForThisClient() == false)
                {
                    MessageBox.Show("This order cannot be accessioned because the provider has no report distributions for the client.", "Unable to create report distributions.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else if(this.m_PatientRecentAccessions.RecentAccessions.Count > 0)
                {                    
                    if (this.m_PatientRecentAccessions.ItemsExistFromPast24Hours() == true)
                    {
                        this.HyperLinkCreateNewAccessionRecentAccessionsExist.Visibility = Visibility.Visible;
                        MessageBox.Show("One or more recent Accessions exist in the past 24hrs, you must acknowledge them before you can create a new accession.");
                    }
                    else
                    {
                        if (this.CreateNewAccessionOrder != null)
                        {
                            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrders = this.GetClientOrdersForExternalOrderIds();
                            if (clientOrders != null)
                            {
                                this.CreateNewAccessionOrder(this, new EventArgs());
                            }
                        }
                    }
                }                
                else
                {
                    if (this.CreateNewAccessionOrder != null)
                    {
                        YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrders = this.GetClientOrdersForExternalOrderIds();
                        if (clientOrders != null)
                        {
                            this.CreateNewAccessionOrder(this, new EventArgs());
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This client order is already accessioned.");
            }
		}
		
        private void ButtonViewSelectedAccession_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_ClientOrder.Accessioned == false)
            {
                if (this.ListViewAccessionOrders.SelectedItem != null)
                {
                    if (this.ViewAccessionOrder != null)
                    {
                        YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrders = this.GetClientOrdersForExternalOrderIds();
                        if (clientOrders != null)
                        {
                            YellowstonePathology.Business.View.RecentAccessionView recentAccessionView = (YellowstonePathology.Business.View.RecentAccessionView)this.ListViewAccessionOrders.SelectedItem;
                            YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs args = new CustomEventArgs.MasterAccessionNoReturnEventArgs(recentAccessionView.MasterAccessionNo);
                            this.ViewAccessionOrder(this, args);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must selected an Accession from the list.");
                }
            }
            else
            {
                MessageBox.Show("This client order has already been accessioned.");
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null)
            {
                this.Back(this, new EventArgs());
            }
		}        

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();            
        }		

		private void ClientFaxPage_Back(object sender, EventArgs e)
		{
			this.m_PageNavigator.Navigate(this);
		}

		private void ClientFaxPage_Next(object sender, EventArgs e)
		{
			this.m_PageNavigator.Navigate(this);
		}

		private void ButtonShowFaxPage_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void ButtonShowEventsPage_Click(object sender, RoutedEventArgs e)
		{
			OrderCommentPage orderCommentPage = new OrderCommentPage(this.m_ClientOrder);
			orderCommentPage.Back += new OrderCommentPage.BackEventHandler(OrderCommentPage_Back);
			orderCommentPage.Next += new OrderCommentPage.NextEventHandler(OrderCommentPage_Next);
			this.m_PageNavigator.Navigate(orderCommentPage);
		}

		private void OrderCommentPage_Back(object sender, EventArgs e)
		{
			this.m_PageNavigator.Navigate(this);
		}

		private void OrderCommentPage_Next(object sender, EventArgs e)
		{
			this.m_PageNavigator.Navigate(this);
		}
        
		private void ButtonShowClientOrderProviderSelectionPage_Click(object sender, RoutedEventArgs e)
		{
			Receiving.ClientOrderProviderSelectionPage clientOrderProviderSelectionPage = new Receiving.ClientOrderProviderSelectionPage(this.m_ClientOrder);
            clientOrderProviderSelectionPage.ProviderSelected += new Receiving.ClientOrderProviderSelectionPage.ProviderSelectedEventHandler(ProviderSelectionPage_ProviderSelected);
            clientOrderProviderSelectionPage.Back += new Receiving.ClientOrderProviderSelectionPage.BackEventHandler(ProviderSelectionPage_Back);
            this.m_PageNavigator.Navigate(clientOrderProviderSelectionPage);
		}

        private void ProviderSelectionPage_ProviderSelected(object sender, YellowstonePathology.UI.CustomEventArgs.PhysicianClientReturnEventArgs e)
		{
            this.m_ClientOrder.ClientId = e.PhysicianClient.ClientId;
            this.m_ClientOrder.ClientName = e.PhysicianClient.ClientName;
            this.m_ClientOrder.ProviderId = e.PhysicianClient.NPI;
            this.m_ClientOrder.ProviderName = e.PhysicianClient.PhysicianName;

            this.HandleProviderMapping();
			this.m_PageNavigator.Navigate(this);
			this.NotifyPropertyChanged("ClientOrder");
		}

        private void ProviderSelectionPage_Back(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);            
        }
        
        private bool PhysicianHasDistributionsForThisClient()
        {
            bool result = false;
            YellowstonePathology.Business.Domain.PhysicianClient physicianClient = Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_Physician.ObjectId, this.m_ClientOrder.ClientId);
            if (physicianClient != null)
            {
                YellowstonePathology.Business.Client.Model.PhysicianClientDistributionCollection physicianClientDistributionCollection = Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionByPhysicianClientId(physicianClient.PhysicianClientId);
                if (physicianClientDistributionCollection.Count > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        private void HyperLinkCreateNewAccessionRecentAccessionsExist_Click(object sender, RoutedEventArgs e)
        {
            if (this.CreateNewAccessionOrder != null)
            {
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrders = this.GetClientOrdersForExternalOrderIds();
                if(clientOrders != null)
                {
                    this.CreateNewAccessionOrder(this, new EventArgs());
                }
            }
        }

        private void SetClientOrderCollection()
        {
            if (string.IsNullOrEmpty(this.m_ClientOrder.SvhAccountNo) == false)
            {
                this.m_ClientOrderCollection = Business.Gateway.ClientOrderGateway.GetRecentClientOrdersBySvhAccountNo(this.m_ClientOrder.SvhAccountNo);
            }
            else
            {
                this.m_ClientOrderCollection = new Business.ClientOrder.Model.ClientOrderCollection();
                this.m_ClientOrderCollection.Add(this.m_ClientOrder);
            }
        }

        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection GetClientOrdersForExternalOrderIds()
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection result = null;
            if(this.ListViewClientOrders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select one or more Client Orders from the Recent Client Orders List.");
            }
            else
            {
                result = new Business.ClientOrder.Model.ClientOrderCollection();
                foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in this.ListViewClientOrders.SelectedItems)
                {
                    if (string.IsNullOrEmpty(clientOrder.ExternalOrderId) == false)
                    {
                        if (clientOrder.PanelSetId.HasValue && result.PanelSetIdExists(clientOrder.PanelSetId.Value) == true)
                        {
                            MessageBox.Show("Only one Client Order for " + clientOrder.OrderType + "may be used.  Choose one of the Client Orders to use.");
                            result = null;
                            break;
                        }
                        else if (result.ExternalOrderIdExists(clientOrder.ExternalOrderId) == false)
                        {
                            result.Add(clientOrder);
                        }
                    }
                    else
                    {
                        result.Add(clientOrder);
                    }
                }
            }
            if(result != null && this.SelectedClientOrders != null)
            {
                this.SelectedClientOrders(this, new CustomEventArgs.ClientOrderCollectionReturnEventArgs(result));
            }
            return result;
        }

        private void ButtonShowSpecialInstructionsPage_Click(object sender, RoutedEventArgs e)
        {
            SpecialInstructionsWindow specialInstructionsWindow = new SpecialInstructionsWindow(this.m_ClientOrder);
            specialInstructionsWindow.ShowDialog();
        }
    }
}
