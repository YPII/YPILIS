using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class HER2AmplificationRecountResultPath : ResultPath
    {
        HER2AmplificationRecountResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder m_PanelSetOrder;

        public HER2AmplificationRecountResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new HER2AmplificationRecountResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new HER2AmplificationRecountResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            if(this.ShowHER2AmplificationSummaryResultPage() == false)
            {
                this.Finished();
            }
        }

        private bool ShowHER2AmplificationSummaryResultPage()
        {
            bool result = false;

            {
                YellowstonePathology.Business.Test.PanelSetOrder summaryTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetHER2Summary();
                if (summaryTestOrder != null)
                {
                    result = true;
                    HER2AmplificationSummaryResultPath resultPath = new HER2AmplificationSummaryResultPath(summaryTestOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
                    resultPath.Finish += HER2AmplificationSummaryResultPath_Finish;
                    resultPath.Start();
                }
            }
            return result;
        }

        private void HER2AmplificationSummaryResultPath_Finish(object sender, EventArgs e)
        {
            this.Finished();
        }
    }
}

