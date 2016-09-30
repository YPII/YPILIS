using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	class MPNExtendedReflexPath: ResultPath
	{
		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		MPNExtendedReflexPage m_MPNExtendedReflexPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex m_PanelSetOrderMPNExtendedReflex;        

		public MPNExtendedReflexPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderMPNExtendedReflex = (YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_MPNExtendedReflexPage = new MPNExtendedReflexPage(this.m_PanelSetOrderMPNExtendedReflex, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_MPNExtendedReflexPage.Back += new MPNExtendedReflexPage.BackEventHandler(MPNExtendedReflexPage_Back);
			this.m_MPNExtendedReflexPage.Finish += new MPNExtendedReflexPage.FinishEventHandler(MPNExtendedReflexPage_Finish);
            this.m_MPNExtendedReflexPage.OrderTest += new MPNExtendedReflexPage.OrderTestEventHandler(MPNExtendedReflexPage_OrderTest);			
			this.m_PageNavigator.Navigate(this.m_MPNExtendedReflexPage);
        }

        private void MPNExtendedReflexPage_OrderTest(object sender, YellowstonePathology.UI.CustomEventArgs.TestOrderInfoEventArgs e)
        {
            YellowstonePathology.UI.Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_AccessionOrder, this.m_PageNavigator, PageNavigationModeEnum.Inline, this.m_Window);
            reportOrderPath.Finish += new Login.Receiving.ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
            reportOrderPath.Start(e.TestOrderInfo);
        }

		private void MPNExtendedReflexPage_Back(object sender, EventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
			else this.Finished();
		}

		private void MPNExtendedReflexPage_Finish(object sender, EventArgs e)
        {
            this.Finished();
        }				

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
			this.m_PageNavigator.Navigate(this.m_MPNExtendedReflexPage);
		}
	}
}
