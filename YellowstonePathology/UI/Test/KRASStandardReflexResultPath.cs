using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class KRASStandardReflexResultPath : ResultPath
	{
		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		private KRASStandardReflexResultPage m_ResultPage;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder m_KRASStandardReflexTestOrder;
		private System.Windows.Visibility m_BackButtonVisibility;


        public KRASStandardReflexResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
			System.Windows.Window window,
            System.Windows.Visibility backButtonVisibility)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_KRASStandardReflexTestOrder = (YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_BackButtonVisibility = backButtonVisibility;
		}

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new KRASStandardReflexResultPage(this.m_KRASStandardReflexTestOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator, this.m_BackButtonVisibility);
            this.m_ResultPage.Next += new KRASStandardReflexResultPage.NextEventHandler(ResultPage_Next);
            this.m_ResultPage.Back += new KRASStandardReflexResultPage.BackEventHandler(ResultPage_Back);
            this.m_ResultPage.OrderBRAF += new KRASStandardReflexResultPage.OrderBRAFEventHandler(ResultPage_OrderBRAF);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

		private void ResultPage_Next(object sender, EventArgs e)
        {
			if (this.ShowReflexTestPage() == false)
			{
				this.Finished();
			}
		}

		private void ResultPage_Back(object sender, EventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
		}

		private bool ShowReflexTestPage()
		{
			bool result = false;
			YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest panelSetLse = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest();
			YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest panelSetcccp = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetLse.PanelSetId, this.m_KRASStandardReflexTestOrder.OrderedOnId, true) == true)
			{
				result = true;
                string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLse.PanelSetId, this.m_KRASStandardReflexTestOrder.OrderedOnId, true).ReportNo;
				YellowstonePathology.UI.Test.LynchSyndromeEvaluationResultPath resultPath = new YellowstonePathology.UI.Test.LynchSyndromeEvaluationResultPath(reportNo,
					this.m_AccessionOrder, this.m_PageNavigator, this.m_Window, System.Windows.Visibility.Visible);

				resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
				resultPath.Back += new LynchSyndromeEvaluationResultPath.BackEventHandler(ResultPath_Back);
				resultPath.Start();
			}
            else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetcccp.PanelSetId, this.m_KRASStandardReflexTestOrder.OrderedOnId, true) == true)
			{
				result = true;
                string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetcccp.PanelSetId, this.m_KRASStandardReflexTestOrder.OrderedOnId, true).ReportNo;
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

		private void ResultPage_OrderBRAF(object sender, EventArgs e)
		{
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            this.StartReportOrderPath(brafV600EKTest);
		}

		private void StartReportOrderPath(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
		{
            YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest();
            //YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardReflexTest.PanelSetId);
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_KRASStandardReflexTestOrder.OrderedOnId);
			YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(panelSet, orderTarget, false);            

			YellowstonePathology.UI.Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_AccessionOrder, this.m_PageNavigator, PageNavigationModeEnum.Inline, this.m_Window);
			reportOrderPath.Finish += new Login.Receiving.ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
            reportOrderPath.Start(testOrderInfo);
		}

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}
	}
}
