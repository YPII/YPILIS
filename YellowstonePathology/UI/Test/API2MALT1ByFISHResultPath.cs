using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class API2MALT1ByFISHResultPath : ResultPath
	{
        API2MALT1ByFISHResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.API2MALT1ByFISH.API2MALT1ByFISHTestOrder m_PanelSetOrder;

		public API2MALT1ByFISHResultPath(string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.API2MALT1ByFISH.API2MALT1ByFISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new API2MALT1ByFISHResultPage((YellowstonePathology.Business.Test.API2MALT1ByFISH.API2MALT1ByFISHTestOrder)this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_ResultPage.Next += new API2MALT1ByFISHResultPage.NextEventHandler(ResultsPage_Next);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultsPage_Next(object sender, EventArgs e)
		{
			this.Finished();
		}
	}
}
