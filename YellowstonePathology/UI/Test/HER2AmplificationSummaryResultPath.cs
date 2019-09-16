using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class HER2AmplificationSummaryResultPath : ResultPath
    {
        HER2AmplificationSummaryResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTestOrder m_PanelSetOrder;

        public HER2AmplificationSummaryResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new HER2AmplificationSummaryResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += ResultPage_Next;
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            if (this.ShowAmendmentPage() == false)
            {
                this.Finished();
            }
        }

        private bool ShowAmendmentPage()
        {
            bool result = false;
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                if (this.m_PanelSetOrder.Result != YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationResultEnum.Equivocal.ToString())
                {
                    YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                    YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(surgicalTestOrder.ReportNo);
                    if (amendmentCollection.HasAmendmentForReferenceReportNo(this.m_PanelSetOrder.ReportNo) == true)
                    {
                        result = true;
                        YellowstonePathology.Business.Amendment.Model.Amendment amendment = amendmentCollection.GetAmendmentForReferenceReportNo(this.m_PanelSetOrder.ReportNo);
                        AmendmentPage amendmentPage = new AmendmentPage(this.m_AccessionOrder, amendment, this.m_SystemIdentity);
                        amendmentPage.Back += AmendmentPage_Back;
                        amendmentPage.Finish += AmendmentPage_Finish;
                        this.m_PageNavigator.Navigate(amendmentPage);
                    }
                }
            }
            return result;
        }

        private void AmendmentPage_Finish(object sender, EventArgs e)
        {
            base.Finished();
        }

        private void AmendmentPage_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }
    }
}
