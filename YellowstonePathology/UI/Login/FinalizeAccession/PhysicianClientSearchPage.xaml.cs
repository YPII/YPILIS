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

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	/// <summary>
	/// Interaction logic for PhysicianClientSearchPage.xaml
	/// </summary>
    public partial class PhysicianClientSearchPage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

        public delegate void NextEventHandler(object sender, CustomEventArgs.PhysicianClientDistributionReturnEventArgs e);
        public event NextEventHandler Next;

		private YellowstonePathology.Business.Interface.IOrder m_AccessionOrder;
        private YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList m_PhysicianClientDistributionCollection;
		
		private bool m_ShowNavigationButtons;
		private string m_PageHeaderText = "Provider Lookup";

		public PhysicianClientSearchPage(YellowstonePathology.Business.Interface.IOrder accessionOrder, bool showNavigationButtons)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ShowNavigationButtons = showNavigationButtons;
            this.m_PhysicianClientDistributionCollection = new Business.Client.Model.PhysicianClientDistributionList();

			InitializeComponent();

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(PhysicianClientSearchPage_Loaded);
		}

		public PhysicianClientSearchPage(YellowstonePathology.Business.Interface.IOrder accessionOrder, int clientId, bool showNavigationButtons)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_ShowNavigationButtons = showNavigationButtons;
            this.m_PhysicianClientDistributionCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionByClientIdV2(clientId);            

            InitializeComponent();

            this.DataContext = this;
			this.listViewPhysicianClient.SelectedIndex = -1;
			this.Loaded += new RoutedEventHandler(PhysicianClientSearchPage_Loaded);
        }

		private void PhysicianClientSearchPage_Loaded(object sender, RoutedEventArgs e)
		{
			Keyboard.Focus(this.TextBoxClientSearch);
			if (!this.m_ShowNavigationButtons)
			{
				this.ButtonBack.Visibility = System.Windows.Visibility.Collapsed;
			}
		}

        public YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList PhysicianClientDistributionCollection
		{
            get { return this.m_PhysicianClientDistributionCollection; }
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}		

		public void TextBoxClientSearch_KeyUp(object sender, RoutedEventArgs args)
		{
            YellowstonePathology.Business.Client.Model.PhysicianNameHelper physicianNameHelper = new Business.Client.Model.PhysicianNameHelper(this.TextBoxClientSearch.Text);
            if (physicianNameHelper.IsValid == true)
            {
                if (physicianNameHelper.IsLastNameOnly == false)
                {
                    this.m_PhysicianClientDistributionCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionByPhysicianFirstLastNameV2(physicianNameHelper.FirstName, physicianNameHelper.LastName);
                }
                else
                {
                    this.m_PhysicianClientDistributionCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionByPhysicianLastNameV2(physicianNameHelper.LastName);
                }

                this.listViewPhysicianClient.SelectedIndex = -1;
                this.NotifyPropertyChanged("PhysicianClientDistributionCollection");
            }            
		}		

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.listViewPhysicianClient.SelectedItem != null)
            {
                Business.Client.Model.PhysicianClientDistributionListItem item = (Business.Client.Model.PhysicianClientDistributionListItem)this.listViewPhysicianClient.SelectedItem;
                if (this.Next != null) this.Next(this, new CustomEventArgs.PhysicianClientDistributionReturnEventArgs(item));
            }
            else
            {
                MessageBox.Show("You must select a provider before you can continue.");
            }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
            if (this.Back != null) this.Back(this, new EventArgs());
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
