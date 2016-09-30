using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class ROS1ResultPath : ResultPath
    {
		ROS1ResultPage m_ROS1ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder m_ROS1ByFISHTestOrder;

        public ROS1ResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window) 
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ROS1ByFISHTestOrder = (YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
            this.m_ROS1ResultPage = new ROS1ResultPage(this.m_ROS1ByFISHTestOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ROS1ResultPage.Next += new ROS1ResultPage.NextEventHandler(ROS1ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ROS1ResultPage);
        }

        private void ROS1ResultPage_Next(object sender, EventArgs e)
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
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId, this.m_ROS1ByFISHTestOrder.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder testOrder = (YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSet.PanelSetId, this.m_ROS1ByFISHTestOrder.OrderedOnId, true);
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
