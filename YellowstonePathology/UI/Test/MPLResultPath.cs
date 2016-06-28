using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class MPLResultPath : ResultPath
	{
		MPLResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.MPL.PanelSetOrderMPL m_PanelSetOrder;

		public MPLResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.MPL.PanelSetOrderMPL)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_ResultPage = new MPLResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_ResultPage.Next += new MPLResultPage.NextEventHandler(ResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

		private void ResultPage_Next(object sender, EventArgs e)
        {
			if(this.ShowMPNExtendedReflexResultPage() == false) this.Finished();
        }

		private bool ShowMPNExtendedReflexResultPage()
		{
			bool result = false;
			YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexTest panelSet = new YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId) == true)
			{
				result = true;
                YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex testOrder = (YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSet.PanelSetId);
                MPNExtendedReflexPage mPNExtendedReflexPage = new MPNExtendedReflexPage(testOrder, this.m_AccessionOrder, this.m_SystemIdentity);
				mPNExtendedReflexPage.Back += new MPNExtendedReflexPage.BackEventHandler(MPNExtendedReflexPage_Back);
				mPNExtendedReflexPage.Finish += new MPNExtendedReflexPage.FinishEventHandler(MPNExtendedReflexPage_Finish);
				mPNExtendedReflexPage.OrderTest += new MPNExtendedReflexPage.OrderTestEventHandler(MPNExtendedReflexPage_OrderTest);
				this.m_PageNavigator.Navigate(mPNExtendedReflexPage);
			}

			return result;
		}

		private void MPNExtendedReflexPage_OrderTest(object sender, YellowstonePathology.UI.CustomEventArgs.TestOrderInfoEventArgs e)
		{
            YellowstonePathology.UI.Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_AccessionOrder, this.m_PageNavigator, PageNavigationModeEnum.Inline, this.m_Window);
			reportOrderPath.Finish += new Login.Receiving.ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
			reportOrderPath.Start(e.TestOrderInfo);
		}

		private void MPNExtendedReflexPage_Back(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}

		private void MPNExtendedReflexPage_Finish(object sender, EventArgs e)
		{
			this.Finished();
		}

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
			this.ShowMPNExtendedReflexResultPage();
		}
	}
}
