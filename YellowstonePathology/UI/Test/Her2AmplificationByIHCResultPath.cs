using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class Her2AmplificationByIHCResultPath : ResultPath
	{
		Her2AmplificationByIHCResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC m_PanelSetOrder;

		public Her2AmplificationByIHCResultPath(string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new Her2AmplificationByIHCResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_ResultPage.Next += new Her2AmplificationByIHCResultPage.NextEventHandler(ResultPage_Next);
            this.m_ResultPage.OrderHER2Recount += ResultPage_OrderHER2Recount;
			this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultPage_OrderHER2Recount(object sender, EventArgs e)
        {
            YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest her2AmplificationRecountTest = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(her2AmplificationRecountTest, orderTarget, false);

            YellowstonePathology.UI.Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_AccessionOrder, this.m_PageNavigator, PageNavigationModeEnum.Inline, this.m_Window);
            reportOrderPath.Finish += new Login.Receiving.ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
            reportOrderPath.Start(testOrderInfo);
        }

        private void ReportOrderPath_Finish(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
		{
            if (this.ShowRecountPage() == false)
            {
                if (this.ShowHER2AmplificationSummaryResultPage() == false)
                {
                    this.Finished();
                }
            }
		}

        private bool ShowRecountPage()
        {
            bool result = false;

            YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest her2AmplificationRecountTest = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(her2AmplificationRecountTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                result = true;
                YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder her2AmplificationRecountTestOrder = (Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(her2AmplificationRecountTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                HER2AmplificationRecountResultPath recountPath = new HER2AmplificationRecountResultPath(her2AmplificationRecountTestOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
                recountPath.Finish += HER2AmplificationRecountResultPath_Finish;
                recountPath.Start();
            }

            return result;
        }

        private void HER2AmplificationRecountResultPath_Finish(object sender, EventArgs e)
        {
            this.Finished();
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
