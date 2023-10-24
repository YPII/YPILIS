using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class BRAFMutationAnalysisResultPath : ResultPath
    {
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        BRAFMutationAnalysisResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder m_PanelSetOrder;

        public BRAFMutationAnalysisResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new BRAFMutationAnalysisResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
            this.m_ResultPage.Next += new BRAFMutationAnalysisResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Back(object sender, EventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            if (this.ShowReflexTestPage() == false)
            {
                this.Finished();
            }
        }

        private bool ShowReflexTestPage()
        {
            bool result = false;
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest panelSetLse = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest();
            YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest panelSetcccp = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();
            YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest();
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest egfrToALKReflexAnalysisTest = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                result = true;
                string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardReflexTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true).ReportNo;
                YellowstonePathology.UI.Test.KRASStandardReflexResultPath resultPath = new YellowstonePathology.UI.Test.KRASStandardReflexResultPath(reportNo,
                    this.m_AccessionOrder, this.m_PageNavigator, this.m_Window, System.Windows.Visibility.Visible);

                resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
                resultPath.Back += new KRASStandardReflexResultPath.BackEventHandler(ResultPath_Back);
                resultPath.Start();
            }
            else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetLse.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                result = true;
                string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLse.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true).ReportNo;
                YellowstonePathology.UI.Test.LynchSyndromeEvaluationResultPath resultPath = new YellowstonePathology.UI.Test.LynchSyndromeEvaluationResultPath(reportNo,
                    this.m_AccessionOrder, this.m_PageNavigator, this.m_Window, System.Windows.Visibility.Visible);

                resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
                resultPath.Back += new LynchSyndromeEvaluationResultPath.BackEventHandler(ResultPath_Back);
                resultPath.Start();
            }
            else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetcccp.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                result = true;
                string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetcccp.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true).ReportNo;
                YellowstonePathology.UI.Test.ComprehensiveColonCancerProfilePath resultPath = new YellowstonePathology.UI.Test.ComprehensiveColonCancerProfilePath(reportNo,
                    this.m_AccessionOrder, this.m_PageNavigator, this.m_Window, System.Windows.Visibility.Collapsed);

                resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
                resultPath.Back += new ComprehensiveColonCancerProfilePath.BackEventHandler(ResultPath_Back);
                resultPath.Start();
            }
            else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(egfrToALKReflexAnalysisTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                result = true;
                string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(egfrToALKReflexAnalysisTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true).ReportNo;
                YellowstonePathology.UI.Test.EGFRToALKReflexPath resultPath = new YellowstonePathology.UI.Test.EGFRToALKReflexPath(reportNo,
                    this.m_AccessionOrder, this.m_PageNavigator, this.m_Window, System.Windows.Visibility.Collapsed);

                resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
                resultPath.Back += new EGFRToALKReflexPath.BackEventHandler(ResultPath_Back);
                resultPath.Start();
            }
            return result;
        }

        private void ResultPath_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

        private void ResultPath_Finish(object sender, EventArgs e)
        {
            base.Finished();
        }
    }
}
