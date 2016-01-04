/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 12/31/2015
 * Time: 10:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.UI
{
	/// <summary>
	/// Description of AmendmentPath.
	/// </summary>
	public class AmendmentPath
	{
        public delegate void FinishEventHandler(object sender, EventArgs e);
        public event FinishEventHandler Finish;        

        private AmendmentUI m_AmendmentUI;
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public AmendmentPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
                             YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
                             YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
                             YellowstonePathology.Business.User.SystemIdentity systemIdentity,
                             YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{
        	this.m_AccessionOrder = accessionOrder;
        	this.m_ObjectTracker = objectTracker;
        	this.m_PanelSetOrder = panelSetOrder;
        	this.m_SystemIdentity = systemIdentity;
        	this.m_PageNavigator = pageNavigator;
        	
        	this.m_AmendmentUI = new AmendmentUI(this.m_AccessionOrder, this.m_ObjectTracker, this.m_PanelSetOrder, this.m_SystemIdentity);
		}

        public void Start()
        {
			if (Business.User.SystemIdentity.DoesLoggedInUserNeedToScanId() == true)
			{
                this.ShowScanSecurityBadgePage();
			}
			else
			{
				this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
                this.ShowAmendmentListPage();
            }
        }

        public void Start(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_SystemIdentity = systemIdentity;
            this.ShowAmendmentListPage();
        }

		private void ShowScanSecurityBadgePage()
		{
			YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new Login.ScanSecurityBadgePage(System.Windows.Visibility.Collapsed);
			scanSecurityBadgePage.AuthentificationSuccessful += new Login.ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
			this.m_PageNavigator.Navigate(scanSecurityBadgePage);
		}

		protected void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
		{
			this.m_SystemIdentity = e.SystemIdentity;
            this.ShowAmendmentListPage();
        }

        private void ShowAmendmentListPage()
        {
        	AmendmentListPage amendmentListPage = new AmendmentListPage(this.m_AmendmentUI);
        	amendmentListPage.Edit += AmendmentListPage_Edit;
        	amendmentListPage.Close += AmendmentListPage_Close;
            this.m_PageNavigator.Navigate(amendmentListPage);
		}

        private void AmendmentListPage_Edit(object sender, EventArgs e)
        {
			this.ShowAmendmentPage();
        }

        private void AmendmentListPage_Close(object sender, EventArgs e)
        {
			this.Finished();
        }

        private void ShowAmendmentPage()
        {
        	AmendmentPage amendmentPage = new AmendmentPage(this.m_AccessionOrder, this.m_ObjectTracker, this.m_AmendmentUI.SelectedAmendment, this.m_SystemIdentity);
        	amendmentPage.Back += AmendmentPage_Back;
        	amendmentPage.Finish += AmendmentPage_Finish;
        	this.m_PageNavigator.Navigate(amendmentPage);
        }

        private void AmendmentPage_Back(object sender, EventArgs e)
        {
			this.ShowAmendmentListPage();
        }

        private void AmendmentPage_Finish(object sender, EventArgs e)
        {
			this.Finished();
        }
        
        public void Finished()
        {
            if (this.Finish != null) this.Finish(this, new EventArgs());
        }
	}
}
