using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class FGFR1ResultPath : ResultPath
    {
        FGFR1ResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.FGFR1.FGFR1TestOrder m_TestOrder;

        public FGFR1ResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window) 
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_TestOrder = (YellowstonePathology.Business.Test.FGFR1.FGFR1TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new FGFR1ResultPage(this.m_TestOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new FGFR1ResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
    }
}
