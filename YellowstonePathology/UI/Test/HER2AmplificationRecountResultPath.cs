using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class HER2AmplificationRecountResultPath : ResultPath
    {
        HER2AmplificationRecountResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder m_PanelSetOrder;

        public HER2AmplificationRecountResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new HER2AmplificationRecountResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new HER2AmplificationRecountResultPage.NextEventHandler(ResultPage_Next);
            this.m_ResultPage.OrderTest += ResultPage_OrderTest;
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
            if(this.ShowHER2AmplificationSummaryResultPage() == false)
            {
                this.Finished();
            }
        }

        private bool ShowHER2AmplificationSummaryResultPage()
        {
            bool result = false;

            YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest test = new Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest();
            YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTestOrder summaryTestOrder = (Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(test.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
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

