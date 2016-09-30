using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class MultipleMyelomaMGUSByFishResultPath : ResultPath
	{
		MultipleMyelomaMGUSByFishResultPage m_MultipleMyelomaMGUSByFishResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish.MultipleMyelomaMGUSByFishTestOrder m_PanelSetOrder;

		public MultipleMyelomaMGUSByFishResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish.MultipleMyelomaMGUSByFishTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
            this.m_MultipleMyelomaMGUSByFishResultPage = new MultipleMyelomaMGUSByFishResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_MultipleMyelomaMGUSByFishResultPage.Next += new MultipleMyelomaMGUSByFishResultPage.NextEventHandler(MultipleMyelomaMGUSByFishResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_MultipleMyelomaMGUSByFishResultPage);
        }

		private void MultipleMyelomaMGUSByFishResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }

	}
}
