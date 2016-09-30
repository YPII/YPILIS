using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class KRASStandardResultPath : ResultPath
	{
		KRASStandardResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder m_PanelSetOrder;

		public KRASStandardResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_ResultPage = new KRASStandardResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
			this.m_ResultPage.Next += new KRASStandardResultPage.NextEventHandler(ResultPage_Next);
			this.m_ResultPage.OrderBRAF += new KRASStandardResultPage.OrderBRAFEventHandler(ResultPage_OrderBRAF);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

		private void ResultPage_Next(object sender, EventArgs e)
        {
            if (this.ShowBRAFV600EKResultPage() == false)
            {
                if (this.ShowReflexTestPage() == false)
                {
                    this.Finished();
                }
            }
		}

        private bool ShowBRAFV600EKResultPage()
        {
            bool result = false;
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true))
            {
                result = true;
                string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true).ReportNo;
                YellowstonePathology.UI.Test.BRAFV600EKResultPath resultPath = new YellowstonePathology.UI.Test.BRAFV600EKResultPath(reportNo,
					this.m_AccessionOrder, this.m_PageNavigator, System.Windows.Visibility.Visible, this.m_Window);

                resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
				resultPath.Back += new BRAFV600EKResultPath.BackEventHandler(ResultPath_Back);
				resultPath.Start();
            }
            return result;
        }

		private bool ShowReflexTestPage()
		{
			bool result = false;
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
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardReflexTest.PanelSetId);
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
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
