/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/6/2016
 * Time: 9:13 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Description of TCellSubsetAnalysisResultPath.
	/// </summary>
	public class TCellSubsetAnalysisResultPath : ResultPath
	{
		TCellSubsetAnalysisResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.TCellSubsetAnalysis.TCellSubsetAnalysisTestOrder m_PanelSetOrder;

		public TCellSubsetAnalysisResultPath(string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.TCellSubsetAnalysis.TCellSubsetAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new TCellSubsetAnalysisResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_ResultPage.Next += new TCellSubsetAnalysisResultPage.NextEventHandler(ResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

		private void ResultPage_Next(object sender, EventArgs e)
		{
			this.Finished();
		}
	}
}
