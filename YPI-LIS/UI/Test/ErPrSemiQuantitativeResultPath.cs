using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class ErPrSemiQuantitativeResultPath : ResultPath
	{
		ErPrSemiQuantitativeResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTestOrder m_PanelSetOrder;

		public ErPrSemiQuantitativeResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_ResultPage = new ErPrSemiQuantitativeResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
			this.m_ResultPage.Next += new ErPrSemiQuantitativeResultPage.NextEventHandler(ResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

		private void ResultPage_Next(object sender, EventArgs e)
		{
			if (this.ShowReflexTestPage() == false)
			{
				this.Finished();
			}
		}

		private bool ShowReflexTestPage()
		{
			bool result = false;
			YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest panelSetInvasiveBreastPanel = new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetInvasiveBreastPanel.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
			{
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetInvasiveBreastPanel.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
				result = true;
				YellowstonePathology.UI.Test.InvasiveBreastPanelPath resultPath = new Test.InvasiveBreastPanelPath(panelSetOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
				resultPath.Finish += new Test.InvasiveBreastPanelPath.FinishEventHandler(ResultPath_Finish);
				resultPath.Start();
			}
			return result;
		}

		private void ResultPath_Finish(object sender, EventArgs e)
		{
			base.Finished();
		}
	}
}
