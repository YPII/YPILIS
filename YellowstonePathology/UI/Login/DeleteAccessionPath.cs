using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Login
{
    public class DeleteAccessionPath
    {
        public delegate bool CloseOpenTabsEventHandler(object sender, EventArgs e);
        public event CloseOpenTabsEventHandler CloseOpenTabs;

        private YellowstonePathology.UI.Login.Receiving.LoginPageWindow m_LoginPageWindow;
        private int m_OpenTabCount;

        public DeleteAccessionPath(int openTabCount)
        {
            this.m_LoginPageWindow = new Receiving.LoginPageWindow();

            this.m_OpenTabCount = openTabCount;
        }

        public void Start()
        {
            this.ShowDeleteAccessionLookupPage();
            this.m_LoginPageWindow.Height = 500;
            this.m_LoginPageWindow.Width = 650;
            this.m_LoginPageWindow.ShowDialog();
        }

        private void ShowDeleteAccessionLookupPage()
        {
            DeleteAccessionLookupPage deleteAccessionLookupPage = new DeleteAccessionLookupPage();
            deleteAccessionLookupPage.Next += DeleteAccessionLookupPage_Next;
            deleteAccessionLookupPage.Back += DeleteAccessionLookupPage_Back;
            this.m_LoginPageWindow.PageNavigator.Navigate(deleteAccessionLookupPage);
        }

        private void DeleteAccessionLookupPage_Next(object sender, CustomEventArgs.MasterAccessionNoReturnEventArgs e)
        {
            Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(e.MasterAccessionNo, this);
            this.ShowDeleteAccessionPage(accessionOrder);
        }

        private void DeleteAccessionLookupPage_Back(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void ShowDeleteAccessionPage(Business.Test.AccessionOrder accessionOrder)
        {
            DeleteAccessionPage deleteAccessionPage = new DeleteAccessionPage(accessionOrder, this);
            deleteAccessionPage.Back += DeleteAccessionPage_Back;
            deleteAccessionPage.Close += DeleteAccessionPage_Close;
            deleteAccessionPage.CloseTabs += DeleteAccessionPage_CloseTabs;
            this.m_LoginPageWindow.PageNavigator.Navigate(deleteAccessionPage);
        }

        private bool DeleteAccessionPage_CloseTabs(object sender, EventArgs e)
        {
            bool result = true;
            if (this.m_OpenTabCount > 0)
            {
                result = this.CloseOpenTabs(this, new EventArgs());
                this.m_OpenTabCount = 0;
            }
            return result;
        }

        private void DeleteAccessionPage_Back(object sender, EventArgs e)
        {
            this.ShowDeleteAccessionLookupPage();
        }

        private void DeleteAccessionPage_Close(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }
    }
}
