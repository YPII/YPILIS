using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class SlideTrackingResultPath : ResultPath
	{
		SlideTrackingResultPage m_SlideTrackingResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

		public SlideTrackingResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_SlideTrackingResultPage = new SlideTrackingResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_SlideTrackingResultPage.Next += new SlideTrackingResultPage.NextEventHandler(SlideTrackingResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_SlideTrackingResultPage);
        }

		private void SlideTrackingResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
	}
}
