using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class BCL2t1418ByFISHResultPath : ResultPath
    {
        BCL2t1418ByFISHResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.BCL2t1418ByFISH.BCL2t1418ByFISHTestOrder m_PanelSetOrder;

        public BCL2t1418ByFISHResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (YellowstonePathology.Business.Test.BCL2t1418ByFISH.BCL2t1418ByFISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new BCL2t1418ByFISHResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new BCL2t1418ByFISHResultPage.NextEventHandler(ResultPage_Next);
            this.m_ResultPage.CPTCode += ResultPage_CPTCode;
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }

        private void ResultPage_CPTCode(object sender, EventArgs e)
        {
            Billing.AddFISHCPTCodePage addCPTCodePage = new Billing.AddFISHCPTCodePage(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder);
            addCPTCodePage.Next += CPTCodePage_Next;
            this.m_PageNavigator.Navigate(addCPTCodePage);
        }

        private void CPTCodePage_Next(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }
    }
}
