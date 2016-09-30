using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class TrichomonasResultPath : ResultPath
    {
        private TrichomonasResultPage m_ResultPage;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder m_TestOrder;        

        public TrichomonasResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_TestOrder = (YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);            
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new TrichomonasResultPage(this.m_TestOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_ResultPage.Next += new TrichomonasResultPage.NextEventHandler(TrichomonasResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

		private void TrichomonasResultPage_Next(object sender, EventArgs e)
        {
			if (this.ShowWomensHealthProfilePagePage() == false)
			{
				this.Finished();
			}
		}

		private bool ShowWomensHealthProfilePagePage()
		{
			bool result = false;
			if (this.m_AccessionOrder.PanelSetOrderCollection.HasWomensHealthProfileOrder() == true)
			{
                YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new Business.Test.WomensHealthProfile.WomensHealthProfileTest();
                YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
                YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);

				YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(this.m_AccessionOrder.ClientOrderId, this.m_Window);
				WomensHealthProfilePage womensHealthProfilePage = new WomensHealthProfilePage(womensHealthProfileTestOrder, this.m_AccessionOrder, clientOrder, System.Windows.Visibility.Visible);
				womensHealthProfilePage.Finished += new WomensHealthProfilePage.FinishedEventHandler(WomensHealthProfilePage_Finished);
                womensHealthProfilePage.Back += new WomensHealthProfilePage.BackEventHandler(WomensHealthProfilePage_Back);
                this.m_PageNavigator.Navigate(womensHealthProfilePage);
				result = true;
			}
			return result;
		}

		private void WomensHealthProfilePage_Finished(object sender, EventArgs e)
		{
			this.Finished();
		}

        private void WomensHealthProfilePage_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }
    }
}
