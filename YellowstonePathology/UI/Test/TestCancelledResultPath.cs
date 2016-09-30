using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class TestCancelledResultPath : ResultPath
    {
        TestCancelledResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder m_TestOrder;

        public TestCancelledResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, 
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_TestOrder = (YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new TestCancelledResultPage(this.m_TestOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new TestCancelledResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultPage_Next(object sender, EventArgs e)
        {
			this.Finished();
        }
	}
}
