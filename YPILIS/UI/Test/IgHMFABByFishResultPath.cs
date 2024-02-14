using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    class IgHMFABByFishResultPath : ResultPath
    {
        IgHMFABByFishResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.IgHMFABByFish.IgHMFABByFishTestOrder m_TestOrder;

        public IgHMFABByFishResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_TestOrder = (YellowstonePathology.Business.Test.IgHMFABByFish.IgHMFABByFishTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new IgHMFABByFishResultPage(this.m_TestOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new IgHMFABByFishResultPage.NextEventHandler(ResultPage_Next);
            this.m_ResultPage.CPTCode += ResultPage_CPTCode;
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }

        private void ResultPage_CPTCode(object sender, EventArgs e)
        {
            Billing.AddFISHCPTCodePage addCPTCodePage = new Billing.AddFISHCPTCodePage(this.m_TestOrder.ReportNo, this.m_AccessionOrder);
            addCPTCodePage.Next += CPTCodePage_Next;
            this.m_PageNavigator.Navigate(addCPTCodePage);
        }

        private void CPTCodePage_Next(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }
    }
}
