using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	class CalreticulinMutationAnalysisResultPath : ResultPath
    {
		CalreticulinMutationAnalysisResultPage m_CalreticulinMutationAnalysisResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder m_ReportOrderCalreticulinMutationAnalysis;

		public CalreticulinMutationAnalysisResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_ReportOrderCalreticulinMutationAnalysis = (YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_CalreticulinMutationAnalysisResultPage = new CalreticulinMutationAnalysisResultPage(this.m_ReportOrderCalreticulinMutationAnalysis, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_CalreticulinMutationAnalysisResultPage.Next += new CalreticulinMutationAnalysisResultPage.NextEventHandler(CalreticulinMutationAnalysisResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_CalreticulinMutationAnalysisResultPage);
        }

		private void CalreticulinMutationAnalysisResultPage_Next(object sender, EventArgs e)
        {
            if (this.ShowReflexTestPage() == false)
            {
				this.Finished();
			}
        }        

        private bool ShowReflexTestPage()
        {
            bool result = false;
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasReflexTestingPlan() == true)
            {
                result = true;
				YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexTest panelSetMPNExtendedReflex = new YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexTest();
				YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex panelSetOrderMPNExtendedReflex = (YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMPNExtendedReflex.PanelSetId);
				Test.MPNExtendedReflexPath MPNExtendedReflexPath = new Test.MPNExtendedReflexPath(panelSetOrderMPNExtendedReflex.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
				MPNExtendedReflexPath.Finish += new Test.MPNExtendedReflexPath.FinishEventHandler(MPNExtendedReflexPath_Finish);
				MPNExtendedReflexPath.Back += new MPNExtendedReflexPath.BackEventHandler(MPNExtendedReflexPath_Back);
				MPNExtendedReflexPath.Start();
			}
            return result;
        }

		private void MPNExtendedReflexPath_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

		private void MPNExtendedReflexPath_Finish(object sender, EventArgs e)
        {
            base.Finished();
		}

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
			this.ShowReflexTestPage();
		}
	}
}
