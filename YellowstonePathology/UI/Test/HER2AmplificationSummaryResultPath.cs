using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class HER2AmplificationSummaryResultPath : ResultPath
    {
        HER2AmplificationByISHResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder m_PanelSetOrder;

        public HER2AmplificationSummaryResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new HER2AmplificationByISHResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
            this.m_ResultPage.Next += ResultPage_Next;
            this.m_ResultPage.SpecimenDetail += ResultPage_SpecimenDetail;
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
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
    }
}
