using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class PDL1ResultPath : ResultPath
    {
        PDL1ResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.PDL1.PDL1TestOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        public PDL1ResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (YellowstonePathology.Business.Test.PDL1.PDL1TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            this.m_ObjectTracker = objectTracker;
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new PDL1ResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
            this.m_ResultPage.Next += new PDL1ResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
    }
}
