using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class CancelATestPath : ResultPath
    {
        public delegate void BackEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e);
        public event BackEventHandler Back;

        private TestCancelledResultPage m_ResultPage;
        private YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs m_CancelATestEventArgs;

        public CancelATestPath(YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs cancelATestEventArgs, 
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_CancelATestEventArgs = cancelATestEventArgs;
		}

        protected override void ShowResultPage()
		{
            CancelTestWarningPage cancelTestWarningPage = new CancelTestWarningPage(this.m_CancelATestEventArgs);
            cancelTestWarningPage.Back += new CancelTestWarningPage.BackEventHandler(CancelTestWarningPage_Back);
            cancelTestWarningPage.CancelTest += new CancelTestWarningPage.CancelTestEventHandler(CancelTestWarningPage_CancelTest);
            this.m_PageNavigator.Navigate(cancelTestWarningPage);
		}

        private void CancelTestWarningPage_CancelTest(object sender, CustomEventArgs.CancelTestEventArgs e)
        {
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = e.AccessionOrder.SpecimenOrderCollection.GetOrderTarget(e.PanelSetOrder.OrderedOnId);
            Business.Test.TestCancelled.TestCancelledTest cancelledTest = new Business.Test.TestCancelled.TestCancelledTest();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(cancelledTest, orderTarget, true);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);

            e.AccessionOrder.PanelSetOrderCollection.Remove(e.PanelSetOrder);

            e.AccessionOrder.TakeATrip(orderTestOrderVisitor);
            Business.Test.TestCancelled.TestCancelledTestOrder testCancelledTestOrder = (Business.Test.TestCancelled.TestCancelledTestOrder)orderTestOrderVisitor.PanelSetOrder;
            testCancelledTestOrder.CancelledTestId = e.PanelSetOrder.PanelSetId;
            testCancelledTestOrder.CancelledTestName = e.PanelSetOrder.PanelSetName;
            testCancelledTestOrder.Distribute = false;
            testCancelledTestOrder.NoCharge = true;
            testCancelledTestOrder.Comment = e.ReasonForCancelation;

            this.ShowTestCancelledResultPage(testCancelledTestOrder, e.AccessionOrder);
        }

        private void CancelTestWarningPage_Back(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e)
        {
            this.Back(this, e);
        }

        private void ShowTestCancelledResultPage(YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder testCancelledTestOrder, 
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{            
            this.m_ResultPage = new TestCancelledResultPage(testCancelledTestOrder, accessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new TestCancelledResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultPage_Next(object sender, EventArgs e)
        {             
            this.Finished();
        }        
	}
}
