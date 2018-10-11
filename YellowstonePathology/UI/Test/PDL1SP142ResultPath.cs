using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class PDL1SP142ResultPath : ResultPath
    {
        PDL1SP142ResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142TestOrder m_PanelSetOrder;

        public PDL1SP142ResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new PDL1SP142ResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new PDL1SP142ResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            if (this.ShowAmendmentPage() == false)
            {
                if (this.ShowReflexTestPage() == false)
                {
                    this.Finished();
                }
            }
        }

        private bool ShowAmendmentPage()
        {
            bool result = false;
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                if (surgicalTestOrder.AmendmentCollection.HasAmendmentForReferenceReportNo(this.m_PanelSetOrder.ReportNo) == true)
                {
                    result = true;
                    YellowstonePathology.Business.Amendment.Model.Amendment amendment = surgicalTestOrder.AmendmentCollection.GetAmendmentForReferenceReportNo(this.m_PanelSetOrder.ReportNo);
                    AmendmentPage amendmentPage = new AmendmentPage(this.m_AccessionOrder, amendment, this.m_SystemIdentity);
                    amendmentPage.Back += AmendmentPage_Back;
                    amendmentPage.Finish += AmendmentPage_Finish;
                    this.m_PageNavigator.Navigate(amendmentPage);
                }
            }
            return result;
        }

        private void AmendmentPage_Finish(object sender, EventArgs e)
        {
            if (this.ShowReflexTestPage() == false)
            {
                this.Finished();
            }
        }

        private void AmendmentPage_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

        private bool ShowReflexTestPage()
        {
            bool result = false;
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest panelSet = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder testOrder = (YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSet.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                result = true;
                Test.EGFRToALKReflexPath egfrToALKReflexPath = new Test.EGFRToALKReflexPath(testOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window, System.Windows.Visibility.Visible);
                egfrToALKReflexPath.Finish += new Test.EGFRToALKReflexPath.FinishEventHandler(EGFRToALKReflexPath_Finish);
                egfrToALKReflexPath.Back += new EGFRToALKReflexPath.BackEventHandler(EGFRToALKReflexPath_Back);
                egfrToALKReflexPath.Start();
            }
            return result;
        }

        private void EGFRToALKReflexPath_Back(object sender, EventArgs e)
        {
            if (this.ShowAmendmentPage() == false)
            {
                this.ShowResultPage();
            }
        }

        private void EGFRToALKReflexPath_Finish(object sender, EventArgs e)
        {
            base.Finished();
        }
    }
}
