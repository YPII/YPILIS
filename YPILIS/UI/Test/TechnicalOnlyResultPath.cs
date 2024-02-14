using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class TechnicalOnlyResultPath : ResultPath
    {
        YellowstonePathology.UI.Surgical.PublishedDocumentFinalPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.PanelSetOrder m_TestOrder;

        public TechnicalOnlyResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, 
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_TestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new YellowstonePathology.UI.Surgical.PublishedDocumentFinalPage(this.m_TestOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Close += ResultPage_Close;
            this.m_ResultPage.ShowDocument += ResultPage_ShowDocument;
            this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultPage_ShowDocument(object sender, EventArgs e)
        {
            PublishedDocumentResultPage publishedDocumentResultPage = new PublishedDocumentResultPage(this.m_AccessionOrder, this.m_TestOrder);
            publishedDocumentResultPage.Next += PublishedDocumentResultPage_Next;
            this.m_PageNavigator.Navigate(publishedDocumentResultPage);
        }

        private void PublishedDocumentResultPage_Next(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

        private void ResultPage_Close(object sender, EventArgs e)
        {
            this.Finished();
        }        
	}
}
