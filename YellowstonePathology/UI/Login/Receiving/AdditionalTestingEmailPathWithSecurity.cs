using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.Receiving
{
    public class AdditionalTestingEmailPathWithSecurity
    {        
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        private Login.Receiving.LoginPageWindow m_LoginPageWindow;        

        public AdditionalTestingEmailPathWithSecurity(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;            
        }

        public void Start()
        {
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            AdditionalTestingEMailPage additionalTestingEMailPage = new AdditionalTestingEMailPage(this.m_PanelSetOrder, this.m_AccessionOrder);
            additionalTestingEMailPage.Next += AdditionalTestingEMailPage_Finished;
            additionalTestingEMailPage.Back += AdditionalTestingEMailPage_Finished;
            this.m_LoginPageWindow.PageNavigator.Navigate(additionalTestingEMailPage);
            this.m_LoginPageWindow.ShowDialog();
        }                

        private void AdditionalTestingEMailPage_Finished(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }
    }
}
