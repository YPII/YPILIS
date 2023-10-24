using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class IHCQCResultPath : ResultPath
    {
        private IHCQCResultPage m_ResultPage;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.IHCQC.IHCQCTestOrder m_IHCQCTestOrder;

        public IHCQCResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_IHCQCTestOrder = (YellowstonePathology.Business.Test.IHCQC.IHCQCTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
            this.m_ResultPage = new IHCQCResultPage(this.m_IHCQCTestOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new IHCQCResultPage.NextEventHandler(IHCQCResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void IHCQCResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
		}				
	}
}
