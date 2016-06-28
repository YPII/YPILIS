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
	public partial class PeerReviewResultPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;
        
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;
        private ObservableCollection<YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder> m_PeerReviewTestOrderCollection;
        private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;
        private YellowstonePathology.Business.Test.PeerReview.PeerReviewTypeCollection m_PeerReviewTypeCollection;

        public PeerReviewResultPage(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            this.m_PeerReviewTypeCollection = new YellowstonePathology.Business.Test.PeerReview.PeerReviewTypeCollection();                        

            this.m_PanelSetOrder = panelSetOrder;
			this.m_AccessionOrder = accessionOrder;            			

			this.m_PageHeaderText = "Peer Review for: " + this.m_AccessionOrder.PatientDisplayName;
            this.m_PeerReviewTestOrderCollection = this.m_AccessionOrder.PanelSetOrderCollection.GetPeerReviewCollection();
            this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);

			InitializeComponent();            

			DataContext = this;
		}

        public YellowstonePathology.Business.Test.PeerReview.PeerReviewTypeCollection PeerReviewTypeCollection
        {
            get { return this.m_PeerReviewTypeCollection; }
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
        {
            get { return this.m_PathologistUsers; }
        }

        public ObservableCollection<YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder> PeerReviewTestOrderCollection
        {
            get { return this.m_PeerReviewTestOrderCollection; }
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
            if (string.IsNullOrEmpty(this.m_PanelSetOrder.PeerReviewRequestType) == false)
            {                
                YellowstonePathology.Business.Test.PeerReview.PeerReviewTest peerReviewTest = new YellowstonePathology.Business.Test.PeerReview.PeerReviewTest();
                string reportNo = this.m_AccessionOrder.GetNextReportNo(peerReviewTest);
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder peerReviewTestOrder = new YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder(this.m_AccessionOrder.MasterAccessionNo, reportNo, objectId, peerReviewTest, null, false);
                peerReviewTestOrder.AssignedToId = pathologistId;
                peerReviewTestOrder.HoldForPeerReview = true;
                peerReviewTestOrder.PeerReviewRequestType = YellowstonePathology.Business.Test.PeerReview.PeerReviewTypeEnum.Mandatory.ToString();
                this.m_AccessionOrder.PanelSetOrderCollection.Add(peerReviewTestOrder);
                this.m_PeerReviewTestOrderCollection = this.m_AccessionOrder.PanelSetOrderCollection.GetPeerReviewCollection();
                this.NotifyPropertyChanged("PeerReviewTestOrderCollection");                
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

        private void HyperLinkAddDrGallager_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5087);
        }

        private void HyperLinkAddDrMatthews_Click(object sender, RoutedEventArgs e)
        {
            this.AddPeerReview(5136);
        }

        private void HyperLinkDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = (Hyperlink)sender;
            YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder peerReviewTestOrder = (YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder)hyperlink.Tag;
            if (peerReviewTestOrder.Final == false)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this.", "Delete?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.m_AccessionOrder.PanelSetOrderCollection.Remove(peerReviewTestOrder);
                    this.m_PeerReviewTestOrderCollection = this.m_AccessionOrder.PanelSetOrderCollection.GetPeerReviewCollection();
                    this.NotifyPropertyChanged("PeerReviewTestOrderCollection");
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
            YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder peerReviewTestOrder = (YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder)hyperlink.Tag;            
            
            if (peerReviewTestOrder.AssignedToId == YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId)
            {
                peerReviewTestOrder.Accept();
                peerReviewTestOrder.Finish(this.m_AccessionOrder);
            }
            else
            {
                MessageBox.Show("You cannot finalize this review because it is not assigned to you.");
            }            
        }        

        private void HyperLinkUnfinal_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = (Hyperlink)sender;
            YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder peerReviewTestOrder = (YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder)hyperlink.Tag;
            peerReviewTestOrder.Unfinalize();
            peerReviewTestOrder.Unaccept();
        }

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.CaseDocumentViewer caseDocumentViewer = new CaseDocumentViewer();
            caseDocumentViewer.View(this.m_AccessionOrder, this.m_PanelSetOrder);
        }        
    }
}
