using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class LynchSyndromeEvaluationResultPath : ResultPath
	{
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private LynchSyndromeEvaluationResultPage m_LynchSyndromeEvaluationResultPage;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation m_PanelSetOrderLynchSyndromeEvaluation;

        private System.Windows.Visibility m_BackButtonVisibility;

		public LynchSyndromeEvaluationResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window,
            System.Windows.Visibility backButtonVisibility)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderLynchSyndromeEvaluation = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            this.m_BackButtonVisibility = backButtonVisibility;
		}

        protected override void ShowResultPage()
        {
			this.m_LynchSyndromeEvaluationResultPage = new LynchSyndromeEvaluationResultPage(this.m_PanelSetOrderLynchSyndromeEvaluation, this.m_AccessionOrder, this.m_SystemIdentity, this.m_BackButtonVisibility);
			this.m_LynchSyndromeEvaluationResultPage.Next += new LynchSyndromeEvaluationResultPage.NextEventHandler(LynchSyndromeEvaluationResultPage_Next);
            this.m_LynchSyndromeEvaluationResultPage.Back += new LynchSyndromeEvaluationResultPage.BackEventHandler(LynchSyndromeEvaluationResultPage_Back);			
			this.m_LynchSyndromeEvaluationResultPage.OrderBraf += new LynchSyndromeEvaluationResultPage.OrderBrafEventHandler(LynchSyndromeEvaluationResultPage_OrderBraf);
			this.m_LynchSyndromeEvaluationResultPage.OrderMLH1MethylationAnalysis += new LynchSyndromeEvaluationResultPage.OrderMLH1MethylationAnalysisEventHandler(LynchSyndromeEvaluationResultPage_OrderMLH1MethylationAnalysis);
            this.m_LynchSyndromeEvaluationResultPage.OrderColonCancerProfile += new LynchSyndromeEvaluationResultPage.OrderColonCancerProfileEventHandler(LynchSyndromeEvaluationResultPage_OrderColonCancerProfile);
			this.m_PageNavigator.Navigate(this.m_LynchSyndromeEvaluationResultPage);
        }

        private void LynchSyndromeEvaluationResultPage_OrderColonCancerProfile(object sender, EventArgs e)
        {
			YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest comprehensiveColonCancerProfileTest = new Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();
            this.StartReportOrderPath(comprehensiveColonCancerProfileTest);
        }

        private void LynchSyndromeEvaluationResultPage_Back(object sender, EventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }		

		private void LynchSyndromeEvaluationResultPage_OrderBraf(object sender, EventArgs e)
		{
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            this.StartReportOrderPath(brafV600EKTest);
			this.m_PanelSetOrderLynchSyndromeEvaluation.BRAFIsIndicated = true;            
		}

		private void LynchSyndromeEvaluationResultPage_OrderMLH1MethylationAnalysis(object sender, EventArgs e)
		{
			YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
			this.StartReportOrderPath(panelSetMLH1);
		}		

		private void LynchSyndromeEvaluationResultPage_Next(object sender, EventArgs e)
        {
			if(this.ShowComprehensiveColonCancerProfilePage() == false) this.Finished();
        }

		private bool ShowComprehensiveColonCancerProfilePage()
		{
			bool result = false;
			YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest psComprehensiveColonCancerProfile = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(psComprehensiveColonCancerProfile.PanelSetId, this.m_PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
			{
				YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfile comprehensiveColonCancerProfile = (YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfile)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(psComprehensiveColonCancerProfile.PanelSetId, this.m_PanelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
				ComprehensiveColonCancerProfilePage comprehensiveColonCancerProfilePage = new ComprehensiveColonCancerProfilePage(comprehensiveColonCancerProfile, this.m_AccessionOrder, this.m_SystemIdentity, System.Windows.Visibility.Visible);
				comprehensiveColonCancerProfilePage.Next += new ComprehensiveColonCancerProfilePage.NextEventHandler(ComprehensiveColonCancerProfilePage_Next);
                comprehensiveColonCancerProfilePage.Back += new ComprehensiveColonCancerProfilePage.BackEventHandler(ComprehensiveColonCancerProfilePage_Back);
				this.m_PageNavigator.Navigate(comprehensiveColonCancerProfilePage);
				result = true;
			}
			return result;
		}

        private void ComprehensiveColonCancerProfilePage_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

		private void ComprehensiveColonCancerProfilePage_Next(object sender, EventArgs e)
		{
            this.Finished();
		}

		private void StartReportOrderPath(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
		{
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrderLynchSyndromeEvaluation.OrderedOnId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(panelSet, orderTarget, false);            
			YellowstonePathology.UI.Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_AccessionOrder, this.m_PageNavigator, PageNavigationModeEnum.Inline, this.m_Window);
			reportOrderPath.Finish += new Login.Receiving.ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
            reportOrderPath.Start(testOrderInfo);
		}

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
			this.m_PageNavigator.Navigate(this.m_LynchSyndromeEvaluationResultPage);
		}
	}
}
