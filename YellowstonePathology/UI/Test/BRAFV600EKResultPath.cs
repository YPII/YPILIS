using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class BRAFV600EKResultPath : ResultPath
	{
		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		BRAFV600EKResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder m_PanelSetOrder;
		private System.Windows.Visibility m_BackButtonVisibility;

		public BRAFV600EKResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
			System.Windows.Visibility backButtonVisibility,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_BackButtonVisibility = backButtonVisibility;
		}

        protected override void ShowResultPage()
        {
			this.m_ResultPage = new BRAFV600EKResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator, this.m_BackButtonVisibility);
			this.m_ResultPage.Next += new BRAFV600EKResultPage.NextEventHandler(ResultPage_Next);
			this.m_ResultPage.Back += new BRAFV600EKResultPage.BackEventHandler(ResultPage_Back);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

		private void ResultPage_Back(object sender, EventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
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
			YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest panelSetLse = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest();
			YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest panelSetcccp = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();
            YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
			{
				result = true;
                string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardReflexTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true).ReportNo;
                YellowstonePathology.UI.Test.KRASStandardReflexResultPath resultPath = new YellowstonePathology.UI.Test.KRASStandardReflexResultPath(reportNo,
					this.m_AccessionOrder, this.m_PageNavigator, this.m_Window, System.Windows.Visibility.Visible);

				resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
                resultPath.Back += new KRASStandardReflexResultPath.BackEventHandler(ResultPath_Back);
				resultPath.Start();
			}
			else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetLse.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
			{
				result = true;
				string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLse.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true).ReportNo;
				YellowstonePathology.UI.Test.LynchSyndromeEvaluationResultPath resultPath = new YellowstonePathology.UI.Test.LynchSyndromeEvaluationResultPath(reportNo,
					this.m_AccessionOrder, this.m_PageNavigator, this.m_Window, System.Windows.Visibility.Visible);

				resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
				resultPath.Back += new LynchSyndromeEvaluationResultPath.BackEventHandler(ResultPath_Back);
				resultPath.Start();
			}
			else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetcccp.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
			{
				result = true;
				string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetcccp.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true).ReportNo;
				YellowstonePathology.UI.Test.ComprehensiveColonCancerProfilePath resultPath = new YellowstonePathology.UI.Test.ComprehensiveColonCancerProfilePath(reportNo,
					this.m_AccessionOrder, this.m_PageNavigator, this.m_Window, System.Windows.Visibility.Collapsed);

				resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
				resultPath.Back += new ComprehensiveColonCancerProfilePath.BackEventHandler(ResultPath_Back);
				resultPath.Start();
			}
			return result;
		}

		private void ResultPath_Back(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}

		private void ResultPath_Finish(object sender, EventArgs e)
		{
			base.Finished();
		}
	}
}
