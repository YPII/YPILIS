using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class HER2AmplificationByISHResultPath : ResultPath
    {
        HER2AmplificationByISHResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder m_PanelSetOrder;

        public HER2AmplificationByISHResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new HER2AmplificationByISHResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
            this.m_ResultPage.Next += new HER2AmplificationByISHResultPage.NextEventHandler(ResultPage_Next);
            this.m_ResultPage.SpecimenDetail += new HER2AmplificationByISHResultPage.SpecimenDetailEventHandler(ResultPage_SpecimenDetail);
            this.m_ResultPage.OrderTest += ResultPage_OrderTest;

            this.RegisterCancelATest(this.m_ResultPage);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_OrderTest(object sender, CustomEventArgs.PanelSetReturnEventArgs e)
        {
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(e.PanelSet, orderTarget, false);

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
            if (this.ShowReflexTestPage() == false)
            {
                if (this.ShowAmendmentPage() == false)
                {
                    this.Finished();
                }
            }
        }

        private void ResultPage_SpecimenDetail(object sender, EventArgs e)
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            Login.SpecimenOrderDetailsPath specimenOrderDetailsPath = new Login.SpecimenOrderDetailsPath(specimenOrder, this.m_AccessionOrder, this.m_PageNavigator);
            specimenOrderDetailsPath.Finish += new Login.SpecimenOrderDetailsPath.FinishEventHandler(SpecimenOrderDetailsPath_Finish);
            specimenOrderDetailsPath.Start();
        }

        private void SpecimenOrderDetailsPath_Finish(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

        /*private bool ShowSummaryPage()
        {
            bool result = false;
            YellowstonePathology.Business.Test.PanelSetOrder summaryTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetHER2Summary();
            if (summaryTestOrder != null)
            {
                result = true;
                HER2AmplificationSummaryResultPath resultPath = new HER2AmplificationSummaryResultPath(summaryTestOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
                resultPath.Finish += SummaryResultPath_Finish;
                resultPath.Start();
            }

            return result;
        }

        private void SummaryResultPath_Finish(object sender, EventArgs e)
        {
            if (this.ShowReflexTestPage() == false)
            {
                if (this.ShowAmendmentPage() == false)
                {
                    base.Finished();
                }
            }
        }*/

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
            if (this.ShowAmendmentPage() == false)
            {
                base.Finished();
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
                    if (surgicalTestOrder.AmendmentCollection.HasAmendmentForReferenceReportNo(this.m_PanelSetOrder.ReportNo) == true)
                    {
                        result = true;
                        YellowstonePathology.Business.Amendment.Model.Amendment amendment = surgicalTestOrder.AmendmentCollection.GetAmendmentForReferenceReportNo(this.m_PanelSetOrder.ReportNo);
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
            if (this.ShowReflexTestPage() == false)
            {
                this.ShowResultPage();
            }
        }
    }
}
