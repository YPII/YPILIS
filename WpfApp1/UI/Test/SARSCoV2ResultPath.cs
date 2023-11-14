using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class SARSCoV2ResultPath : ResultPath
	{
		private SARSCoV2ResultPage m_SARSCoV2ResultPage;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
		private YellowstonePathology.Business.Test.SARSCoV2.SARSCoV2TestOrder m_SARSCoV2TestOrder;

        public SARSCoV2ResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,            
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;            
            this.m_SARSCoV2TestOrder = (YellowstonePathology.Business.Test.SARSCoV2.SARSCoV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_SARSCoV2ResultPage = new SARSCoV2ResultPage(this.m_SARSCoV2TestOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
			this.m_SARSCoV2ResultPage.Next += new SARSCoV2ResultPage.NextEventHandler(SARSCoV2ResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_SARSCoV2ResultPage);
		}

		private void SARSCoV2ResultPage_Next(object sender, EventArgs e)
		{
			this.Finished();
		}		
    }
}
