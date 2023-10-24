using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class TCellRecepterBetaGeneRearrangementResultPath : ResultPath
	{
        TCellRecepterBetaGeneRearrangementResultPage m_TCellRecepterBetaGeneRearrangementResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementTestOrder m_PanelSetOrder;

		public TCellRecepterBetaGeneRearrangementResultPath(string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_TCellRecepterBetaGeneRearrangementResultPage = new TCellRecepterBetaGeneRearrangementResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_TCellRecepterBetaGeneRearrangementResultPage.Next += new TCellRecepterBetaGeneRearrangementResultPage.NextEventHandler(TCellClonalityByPCRResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_TCellRecepterBetaGeneRearrangementResultPage);
		}

		private void TCellClonalityByPCRResultPage_Next(object sender, EventArgs e)
		{
			this.Finished();
		}

	}
}
