using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace YellowstonePathology.UI.Test
{
    public class MissingInformationResultPath : ResultPath
    {        
        private MissingInformationResultPage m_ResultPage;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.MissingInformation.MissingInformationTestOrder m_MissingInformationTestOrder;

        public MissingInformationResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,            
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_MissingInformationTestOrder = (YellowstonePathology.Business.Test.MissingInformation.MissingInformationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new MissingInformationResultPage(this.m_MissingInformationTestOrder, this.m_AccessionOrder);            
            this.m_ResultPage.Next += new MissingInformationResultPage.NextEventHandler(ResultPage_Next);
            this.m_ResultPage.ShowICDEntry += ResultPage_ShowICDEntry;
            this.m_ResultPage.ShowFaxPage += ResultPage_ShowFaxPage;
            this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultPage_ShowFaxPage(object sender, EventArgs e)
        {
            YellowstonePathology.UI.Login.ClientFaxPage clientFaxPage = new Login.ClientFaxPage(this.m_AccessionOrder, this.m_SystemIdentity);
            clientFaxPage.Next += ClientFaxPage_Next;
            clientFaxPage.Back += ClientFaxPage_Back;
            this.m_PageNavigator.Navigate(clientFaxPage);
        }

        private void ClientFaxPage_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

        private void ClientFaxPage_Next(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            base.Finished();
        }        

        private void ResultPage_ShowICDEntry(object sender, EventArgs e)
        {
            UI.Login.ICDEntryPage icdEntryPage = new Login.ICDEntryPage(this.m_AccessionOrder, this.m_MissingInformationTestOrder.ReportNo);
            icdEntryPage.Next += IcdEntryPage_Next;
            icdEntryPage.Back += IcdEntryPage_Back;
            this.m_PageNavigator.Navigate(icdEntryPage);
        }

        private void IcdEntryPage_Next(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

        private void IcdEntryPage_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }
    }
}
