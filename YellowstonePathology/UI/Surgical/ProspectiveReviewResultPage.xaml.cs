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
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Surgical
{	
	public partial class ProspectiveReviewResultPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;
        
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;
        private ObservableCollection<YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder> m_ProspectiveReviewTestOrderCollection;
        private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;
        private YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTypeCollection m_ProspectiveReviewTypeCollection;

        public ProspectiveReviewResultPage(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            this.m_ProspectiveReviewTypeCollection = new YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTypeCollection();                        

            this.m_PanelSetOrder = panelSetOrder;
			this.m_AccessionOrder = accessionOrder;            			

			this.m_PageHeaderText = "Peer Review for: " + this.m_AccessionOrder.PatientDisplayName;
            this.m_ProspectiveReviewTestOrderCollection = this.m_AccessionOrder.PanelSetOrderCollection.GetProspectiveReviewCollection();
            this.m_PathologistUsers = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);

			InitializeComponent();            

			DataContext = this;
		}

        public YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTypeCollection ProspectiveReviewTypeCollection
        {
            get { return this.m_ProspectiveReviewTypeCollection; }
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
        {
            get { return this.m_PathologistUsers; }
        }

        public ObservableCollection<YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder> ProspectiveReviewTestOrderCollection
        {
            get { return this.m_ProspectiveReviewTestOrderCollection; }
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

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
		}

        private void AddPeerReview(int pathologistId)
        {
            if (string.IsNullOrEmpty(this.m_PanelSetOrder.ProspectiveReviewRequestType) == false)
            {                
                YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTest peerReviewTest = new YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTest();
                string reportNo = this.m_AccessionOrder.GetNextReportNo(peerReviewTest);
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder peerReviewTestOrder = new YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder(this.m_AccessionOrder.MasterAccessionNo, reportNo, objectId, peerReviewTest, null, false);
                peerReviewTestOrder.AssignedToId = pathologistId;
                peerReviewTestOrder.HoldForProspectiveReview = true;
                peerReviewTestOrder.ProspectiveReviewRequestType = Business.Test.ProspectiveReview.ProspectiveReviewTypeEnum.Mandatory.ToString();
                this.m_AccessionOrder.PanelSetOrderCollection.Add(peerReviewTestOrder);
                this.m_ProspectiveReviewTestOrderCollection = this.m_AccessionOrder.PanelSetOrderCollection.GetProspectiveReviewCollection();
                this.NotifyPropertyChanged("ProspectiveReviewTestOrderCollection");                
            }
            else
            {
                MessageBox.Show("You must select the type of peer review before adding a reviewer.");
            }
        }

        private void HyperLinkAddDrBrown_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5010);
        }

        private void HyperLinkAddDrDurden_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5102);
        }

        private void HyperLinkAddDrEmerick_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5088);
        }

        private void HyperLinkAddDrNero_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5111);
        }

        private void HyperLinkAddDrSchultz_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5007);
        }

        private void HyperLinkAddDrClegg_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5061);
        }

        private void HyperLinkAddDrMatthews_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5136);
        }

        private void HyperLinkAddDrMorrison_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5143);
        }

        private void HyperLinkDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = (Hyperlink)sender;
            YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder prospectiveReviewTestOrder = (YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder)hyperlink.Tag;
            if (prospectiveReviewTestOrder.Final == false)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this.", "Delete?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.m_AccessionOrder.PanelSetOrderCollection.Remove(prospectiveReviewTestOrder);
                    this.m_ProspectiveReviewTestOrderCollection = this.m_AccessionOrder.PanelSetOrderCollection.GetProspectiveReviewCollection();
                    this.NotifyPropertyChanged("ProspectiveReviewTestOrderCollection");
                }
            }
            else
            {
                MessageBox.Show("You cannot delete this peer review because it is final.");
            }
        }        

        private void HyperLinkFinal_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = (Hyperlink)sender;
            YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder prospectiveReviewTestOrder = (YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder)hyperlink.Tag;            
            
            if (prospectiveReviewTestOrder.AssignedToId == Business.User.SystemIdentity.Instance.User.UserId)
            {
                prospectiveReviewTestOrder.Accept();
                prospectiveReviewTestOrder.Finish(this.m_AccessionOrder);
            }
            else
            {
                MessageBox.Show("You cannot finalize this review because it is not assigned to you.");
            }            
        }        

        private void HyperLinkUnfinal_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = (Hyperlink)sender;
            YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder peerReviewTestOrder = (YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder)hyperlink.Tag;
            peerReviewTestOrder.Unfinalize();
            peerReviewTestOrder.Unaccept();
        }

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.CaseDocumentViewer caseDocumentViewer = new CaseDocumentViewer();
            caseDocumentViewer.View(this.m_AccessionOrder, this.m_PanelSetOrder);
        }

        private void HyperLinkAddDrSmirnow_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5133);
        }

        private void HyperLinkAddDrBibbey_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5147);
        }

        private void HyperLinkAddDrLuem_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5148);
        }

        private void HyperLinkAddDrMessner_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5149);
        }

        private void HyperLinkAddDrSchneider_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5150);
        }

        private void HyperLinkAddDrRozelle_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5153);
        }
    }
}
