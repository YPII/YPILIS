using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	class MPNStandardReflexPath : ResultPath
	{
		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		MPNStandardReflexPage m_MPNStandardReflexPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex m_PanelSetOrderMPNStandardReflex;

		public MPNStandardReflexPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderMPNStandardReflex = (YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_MPNStandardReflexPage = new MPNStandardReflexPage(this.m_PanelSetOrderMPNStandardReflex, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_MPNStandardReflexPage.Back += new MPNStandardReflexPage.BackEventHandler(MPNStandardReflexPage_Back);
			this.m_MPNStandardReflexPage.Finish += new MPNStandardReflexPage.FinishEventHandler(MPNStandardReflexPage_Finish);
			this.m_PageNavigator.Navigate(this.m_MPNStandardReflexPage);
        }

		private void MPNStandardReflexPage_Back(object sender, EventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
			else this.Finished();
		}

		private void MPNStandardReflexPage_Finish(object sender, EventArgs e)
        {
            this.Finished();
        }
	}
}
