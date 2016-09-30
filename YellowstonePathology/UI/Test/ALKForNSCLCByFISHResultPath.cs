using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class ALKForNSCLCByFISHResultPath : ResultPath
	{
        private ALKForNSCLCByFISHResultPage m_ResultPage;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder m_TestOrder;

        public ALKForNSCLCByFISHResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_TestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new ALKForNSCLCByFISHResultPage(this.m_TestOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new ALKForNSCLCByFISHResultPage.NextEventHandler(ResultPage_Next);
            this.RegisterCancelATest(this.m_ResultPage);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
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
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest panelSet = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId, this.m_TestOrder.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder testOrder = (YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSet.PanelSetId, this.m_TestOrder.OrderedOnId, true);
                result = true;
				Test.EGFRToALKReflexPath eGFRToALKReflexPath = new Test.EGFRToALKReflexPath(testOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window, System.Windows.Visibility.Visible);
				eGFRToALKReflexPath.Finish += new Test.EGFRToALKReflexPath.FinishEventHandler(EGFRToALKReflexPath_Finish);
                eGFRToALKReflexPath.Back += EGFRToALKReflexPath_Back;
				eGFRToALKReflexPath.Start();
			}
            return result;
        }

        private void EGFRToALKReflexPath_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

        private void EGFRToALKReflexPath_Finish(object sender, EventArgs e)
        {
            base.Finished();
		}

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
			this.ShowReflexTestPage();
		}
	}
}
