using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class ProviderDistributionPath
	{
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;        

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_ReportNo;        
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;        

        private System.Windows.Visibility m_NextButtonVisibility;
        private System.Windows.Visibility m_CloseButtonVisibility;
        private System.Windows.Visibility m_BackButtonVisibility;

        public ProviderDistributionPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            System.Windows.Visibility nextButtonVisibility,
            System.Windows.Visibility closeButtonVisibility,
            System.Windows.Visibility backButtonVisibility)
        {
            this.m_ReportNo = reportNo;
            this.m_AccessionOrder = accessionOrder;

            this.m_NextButtonVisibility = nextButtonVisibility;
            this.m_CloseButtonVisibility = closeButtonVisibility;
            this.m_BackButtonVisibility = backButtonVisibility;
        }        

        public void Start()
        {
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            this.m_LoginPageWindow.Width = System.Windows.SystemParameters.PrimaryScreenWidth * 0.80;

            FinalizeAccession.ProviderDistributionPage providerDistributionPage = new FinalizeAccession.ProviderDistributionPage(this.m_ReportNo, this.m_AccessionOrder, this.m_LoginPageWindow.PageNavigator,
                   this.m_NextButtonVisibility, this.m_CloseButtonVisibility, this.m_BackButtonVisibility);
            providerDistributionPage.Close += new FinalizeAccession.ProviderDistributionPage.CloseEventHandler(ProviderDetailPage_Close);
            providerDistributionPage.Next += new ProviderDistributionPage.NextEventHandler(ProviderDistributionPage_Next);
            providerDistributionPage.Back += new ProviderDistributionPage.BackEventHandler(ProviderDistributionPage_Back);
            this.m_LoginPageWindow.PageNavigator.Navigate(providerDistributionPage);

            this.m_LoginPageWindow.ShowDialog();
        }        		

        private void ProviderDistributionPage_Next(object sender, EventArgs e)
        {
            if (this.Next != null) this.Next(this, new EventArgs());   
        }

        private void ProviderDistributionPage_Back(object sender, EventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());   
        }

		private void ProviderDetailPage_Close(object sender, EventArgs e)
        {			
            this.m_LoginPageWindow.Close();			
		}
	}
}
