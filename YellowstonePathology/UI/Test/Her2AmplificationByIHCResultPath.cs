using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class Her2AmplificationByIHCResultPath : ResultPath
	{
		Her2AmplificationByIHCResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC m_PanelSetOrder;

		public Her2AmplificationByIHCResultPath(string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new Her2AmplificationByIHCResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_ResultPage.Next += new Her2AmplificationByIHCResultPage.NextEventHandler(ResultPage_Next);
            this.m_ResultPage.OrderTest += ResultPage_OrderTest;
			this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultPage_OrderTest(object sender, CustomEventArgs.PanelSetReturnEventArgs e)
        {
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(e.PanelSet, orderTarget, !e.PanelSet.NeverDistribute);

            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
            this.m_AccessionOrder.TakeATrip(orderTestOrderVisitor);

            if (testOrderInfo.PanelSet.TaskCollection.Count != 0)
            {
                YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = this.m_AccessionOrder.CreateTask(testOrderInfo);
                this.m_AccessionOrder.TaskOrderCollection.Add(taskOrder);
            }

            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
		{
            if (this.ShowRecountPage() == false)
            {
                if (this.ShowHER2AmplificationSummaryResultPage() == false)
                {
                    if (this.ShowAmendmentPage() == false)
                    {
                        this.Finished();
                    }
                }
            }
        }

        private bool ShowAmendmentPage()
        {
            bool result = false;
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                if (this.m_PanelSetOrder.Result != YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationResultEnum.Equivocal.ToString())
                {
                    YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                    YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(surgicalTestOrder.ReportNo);
                    if (amendmentCollection.HasAmendmentForReferenceReportNo(this.m_PanelSetOrder.ReportNo) == true)
                    {
                        result = true;
                        YellowstonePathology.Business.Amendment.Model.Amendment amendment = amendmentCollection.GetAmendmentForReferenceReportNo(this.m_PanelSetOrder.ReportNo);
                        AmendmentPage amendmentPage = new AmendmentPage(this.m_AccessionOrder, amendment, this.m_SystemIdentity);
                        amendmentPage.Back += AmendmentPage_Back;
                        amendmentPage.Finish += AmendmentPage_Finish;
                        this.m_PageNavigator.Navigate(amendmentPage);
                    }
                }
            }
            return result;
        }

        private void AmendmentPage_Finish(object sender, EventArgs e)
        {
            base.Finished();
        }

        private void AmendmentPage_Back(object sender, EventArgs e)
        {            
            this.ShowResultPage();            
        }

        private bool ShowRecountPage()
        {
            bool result = false;

            YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest her2AmplificationRecountTest = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(her2AmplificationRecountTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                result = true;
                YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder her2AmplificationRecountTestOrder = (Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(her2AmplificationRecountTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                HER2AmplificationRecountResultPath recountPath = new HER2AmplificationRecountResultPath(her2AmplificationRecountTestOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
                recountPath.Finish += HER2AmplificationRecountResultPath_Finish;
                recountPath.Start();
            }

            return result;
        }

        private void HER2AmplificationRecountResultPath_Finish(object sender, EventArgs e)
        {
            this.Finished();
        }

        private bool ShowHER2AmplificationSummaryResultPage()
        {
            bool result = false;

            YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTest test = new Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTest();
            YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTestOrder summaryTestOrder = (Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(test.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
            if (summaryTestOrder != null)
            {
                result = true;
                HER2AmplificationSummaryResultPage her2AmplificationSummaryResultPage = new HER2AmplificationSummaryResultPage(summaryTestOrder, this.m_AccessionOrder, Business.User.SystemIdentity.Instance);
                her2AmplificationSummaryResultPage.Next += HER2AmplificationSummaryResultPage_Next;
                this.m_PageNavigator.Navigate(her2AmplificationSummaryResultPage);
            }
            return result;
        }

        private void HER2AmplificationSummaryResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
    }
}
