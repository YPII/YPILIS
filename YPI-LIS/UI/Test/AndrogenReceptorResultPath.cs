using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    class AndrogenReceptorResultPath : ResultPath
    {
        AndrogenReceptorResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.AndrogenReceptor.AndrogenReceptorTestOrder m_PanelSetOrder;

        public AndrogenReceptorResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (YellowstonePathology.Business.Test.AndrogenReceptor.AndrogenReceptorTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new AndrogenReceptorResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new AndrogenReceptorResultPage.NextEventHandler(ResultPage_Next);
            this.m_ResultPage.ShowPublishedDocument += ResultPage_ShowPublishedDocument;
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_ShowPublishedDocument(object sender, EventArgs e)
        {
            PublishedDocumentResultPath publishedDocumentResultPath = new Test.PublishedDocumentResultPath(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
            publishedDocumentResultPath.Finish += PublishedDocumentResultPath_Finish;
            publishedDocumentResultPath.Start();
        }

        private void PublishedDocumentResultPath_Finish(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
    }
}
