using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class TCellRecepterGammaGeneRearrangementResultPath : ResultPath
	{
        TCellRecepterGammaGeneRearrangementResultPage m_TCellRecepterGammaGeneRearrangementResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.TCellRecepterGammaGeneRearrangement.TCellRecepterGammaGeneRearrangementTestOrder m_PanelSetOrder;

		public TCellRecepterGammaGeneRearrangementResultPath(string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.TCellRecepterGammaGeneRearrangement.TCellRecepterGammaGeneRearrangementTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_TCellRecepterGammaGeneRearrangementResultPage = new TCellRecepterGammaGeneRearrangementResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_TCellRecepterGammaGeneRearrangementResultPage.Next += new TCellRecepterGammaGeneRearrangementResultPage.NextEventHandler(TCellClonalityByPCRResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_TCellRecepterGammaGeneRearrangementResultPage);
		}

		private void TCellClonalityByPCRResultPage_Next(object sender, EventArgs e)
		{
			this.Finished();
		}

	}
}
