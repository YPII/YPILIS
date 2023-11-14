using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class BoneMarrowSummaryResultPath : ResultPath
    {
        private BoneMarrowSummaryResultPage m_BoneMarrowSummaryResultPage;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_TestOrder;

        public BoneMarrowSummaryResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_TestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_BoneMarrowSummaryResultPage = new BoneMarrowSummaryResultPage(this.m_TestOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
            this.m_BoneMarrowSummaryResultPage.Next += new BoneMarrowSummaryResultPage.NextEventHandler(BoneMarrowSummaryResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_BoneMarrowSummaryResultPage);
        }

        private void BoneMarrowSummaryResultPage_Next(object sender, EventArgs e)
        {
            if (this.ShowAmendmentSuggestedPage() == false)
            {
                if(this.ShowAmendmentPage() == false)
                this.Finished();
            }
        }

        private bool SurgicalAmendmentExistsForThisSummary()
        {
            bool result = false;
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(surgicalTestOrder.ReportNo);
                if (amendmentCollection.HasAmendmentForReferenceReportNo(this.m_TestOrder.ReportNo) == true)
                {
                    result = true;
                }
            }
            return result;
        }

        private bool ShowAmendmentSuggestedPage()
        {
            bool result = false;
            if (this.m_TestOrder.Final == true)
            {
                if (this.SurgicalAmendmentExistsForThisSummary() == false)
                {
                    result = true;
                    AmendmentSuggestedPage amendmentSuggestedPage = new UI.AmendmentSuggestedPage(this.m_TestOrder, this.m_AccessionOrder);
                    amendmentSuggestedPage.Next += AmendmentSuggestedPage_Next;
                    this.m_PageNavigator.Navigate(amendmentSuggestedPage);
                }
            }
            return result;
        }

        private void AmendmentSuggestedPage_Next(object sender, EventArgs e)
        {
            if (this.ShowAmendmentPage() == false)
                this.Finished();
        }

        private bool ShowAmendmentPage()
        {
            bool result = false;
            if (this.SurgicalAmendmentExistsForThisSummary() == true)
            {
                result = true;
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendments = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(surgicalTestOrder.ReportNo);
                YellowstonePathology.Business.Amendment.Model.Amendment amendment = amendments.GetAmendmentForReferenceReportNo(this.m_TestOrder.ReportNo);
                AmendmentPage amendmentPage = new AmendmentPage(this.m_AccessionOrder, amendment, this.m_SystemIdentity);
                amendmentPage.Back += AmendmentPage_Back;
                amendmentPage.Finish += AmendmentPage_Finish;
                this.m_PageNavigator.Navigate(amendmentPage);
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
