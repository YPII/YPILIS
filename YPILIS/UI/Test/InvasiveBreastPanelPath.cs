using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class InvasiveBreastPanelPath : ResultPath
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
		private string m_ReportNo;

		private InvasiveBreastPanelPage m_InvasiveBreastPanelPage;
		private YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel m_InvasiveBreastPanel;

		public InvasiveBreastPanelPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window) : base(pageNavigator, window)
		{
			this.m_ReportNo = reportNo;
			this.m_AccessionOrder = accessionOrder;

			this.m_InvasiveBreastPanel = (YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
        }

        protected override void ShowResultPage()
		{
            this.m_InvasiveBreastPanelPage = new InvasiveBreastPanelPage(this.m_InvasiveBreastPanel, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_InvasiveBreastPanelPage.Next += new InvasiveBreastPanelPage.NextEventHandler(InvasiveBreastPanelPage_Next);
            this.m_InvasiveBreastPanelPage.OrderHER2byFISH += new InvasiveBreastPanelPage.OrderHER2byFISHEventHandler(InvasiveBreastPanelPage_OrderHER2byFISH);
			this.m_PageNavigator.Navigate(this.m_InvasiveBreastPanelPage);
		}

		private void InvasiveBreastPanelPage_Next(object sender, EventArgs e)
		{
            this.Finished();
		}

		private void InvasiveBreastPanelPage_OrderHER2byFISH(object sender, EventArgs e)
		{
			YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishTest panelSet = new Business.Test.Her2AmplificationByFish.Her2AmplificationByFishTest();
			this.StartReportOrderPath(panelSet);
		}

		private void StartReportOrderPath(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
		{            
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_InvasiveBreastPanel.OrderedOnId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(panelSet, orderTarget, true);

			YellowstonePathology.UI.Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_AccessionOrder, this.m_PageNavigator, PageNavigationModeEnum.Inline, this.m_Window);
			reportOrderPath.Finish += new Login.Receiving.ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
            reportOrderPath.Start(testOrderInfo);
		}

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(this.m_AccessionOrder, true);
			this.m_PageNavigator.Navigate(this.m_InvasiveBreastPanelPage);
		}
	}
}
