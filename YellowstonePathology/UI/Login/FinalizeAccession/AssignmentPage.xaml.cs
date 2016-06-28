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

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	/// <summary>
	/// Interaction logic for AssignmentPage.xaml
	/// </summary>
	public partial class AssignmentPage : UserControl
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

        private bool m_IsLoaded;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText = "Case Asssignment Page";
		private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;
        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;

		public AssignmentPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            this.m_IsLoaded = false;
			this.m_AccessionOrder = accessionOrder;
            this.m_FacilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

			this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);
			
			InitializeComponent();

			DataContext = this;
            this.Loaded += new RoutedEventHandler(AssignmentPage_Loaded);
            Unloaded += AssignmentPage_Unloaded;
		}

        private void AssignmentPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_IsLoaded = true;
             
        }

        private void AssignmentPage_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
		{
			get { return this.m_PathologistUsers; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

        public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
        {
            get { return this.m_FacilityCollection; }
        }

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyTest panelSetTechnicalOnly = new Business.Test.TechnicalOnly.TechnicalOnlyTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasGrossBeenOrdered() == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrderByTestId(48);
                if (panelSetOrder.AssignedToId == 0)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("A Gross Only has been ordered but the case has not been assigned.  Are you sure you want to continue?", "Assignement", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                        UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
                        this.Return(this, args);
                    }
                }
                else
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                    UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
                    this.Return(this, args);
                }
            }
            else if (this.m_AccessionOrder.PanelSetOrderCollection.HasUnassignedPanelSetOrder(panelSetTechnicalOnly.PanelSetId) == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("There is an order that has not been assigned are you sure you want to continue?", "Assignement", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                    UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
                    this.Return(this, args);
                }
            }
            else
            {
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
                this.Return(this, args);
            }
		}		

        private void ComboBoxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.m_IsLoaded == true)
            {
                ComboBox comboBox = (ComboBox)sender;
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = (YellowstonePathology.Business.Test.PanelSetOrder)comboBox.Tag;
                YellowstonePathology.Business.User.SystemUser systemUser = (YellowstonePathology.Business.User.SystemUser)comboBox.SelectedItem;
                YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings ypiBLGS = new Business.Facility.Model.YellowstonePathologyInstituteBillings();
                YellowstonePathology.Business.Facility.Model.ButtePathology buttePathology = new Business.Facility.Model.ButtePathology();

                YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings ypBLGS = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();                

                if (systemUser.UserId == 5132 || systemUser.UserId == 5133) //Dr Shannon and Dr Smirnoff
                {
                    panelSetOrder.TechnicalComponentFacilityId = ypiBLGS.FacilityId;
                    panelSetOrder.TechnicalComponentBillingFacilityId = ypiBLGS.FacilityId;
                    panelSetOrder.ProfessionalComponentFacilityId = buttePathology.FacilityId;
                    panelSetOrder.ProfessionalComponentBillingFacilityId = buttePathology.FacilityId;
                }
                else
                {                                        
                    panelSetOrder.TechnicalComponentFacilityId = ypiBLGS.FacilityId;
                    panelSetOrder.TechnicalComponentBillingFacilityId = ypiBLGS.FacilityId;

                    if(systemUser.UserId == 5061) //Dr Clegg
                    {
                        YellowstonePathology.Business.Facility.Model.YellowstonePathologistCody ypCody = new Business.Facility.Model.YellowstonePathologistCody();
                        YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteCody ypiCody = new Business.Facility.Model.YellowstonePathologyInstituteCody();
                        panelSetOrder.ProfessionalComponentFacilityId = ypCody.FacilityId;
                        panelSetOrder.ProfessionalComponentBillingFacilityId = ypiCody.FacilityId;
                    }
                    else
                    {
                        panelSetOrder.ProfessionalComponentFacilityId = ypBLGS.FacilityId;
                        panelSetOrder.ProfessionalComponentBillingFacilityId = ypiBLGS.FacilityId;
                    }                    
                }
            }
        }
	}
}
