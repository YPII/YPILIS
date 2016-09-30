using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class HighGradeLargeBCellLymphomaResultPath : ResultPath
	{
		HighGradeLargeBCellLymphomaResultPage m_HighGradeLargeBCellLymphomaResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma.PanelSetOrderHighGradeLargeBCellLymphoma m_PanelSetOrderHighGradeLargeBCellLymphoma;

		public HighGradeLargeBCellLymphomaResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderHighGradeLargeBCellLymphoma = (YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma.PanelSetOrderHighGradeLargeBCellLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_HighGradeLargeBCellLymphomaResultPage = new HighGradeLargeBCellLymphomaResultPage(this.m_PanelSetOrderHighGradeLargeBCellLymphoma, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_HighGradeLargeBCellLymphomaResultPage.Next += new HighGradeLargeBCellLymphomaResultPage.NextEventHandler(HighGradeLargeBCellLymphomaResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_HighGradeLargeBCellLymphomaResultPage);
        }

		private void HighGradeLargeBCellLymphomaResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
	}
}
