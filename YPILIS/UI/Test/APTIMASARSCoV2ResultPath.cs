using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class APTIMASARSCoV2ResultPath : ResultPath
	{
		private APTIMASARSCoV2ResultPage m_SARSCoV2ResultPage;
		private Business.Test.AccessionOrder m_AccessionOrder;        
		private Business.Test.APTIMASARSCoV2.APTIMASARSCoV2TestOrder m_SARSCoV2TestOrder;

        public APTIMASARSCoV2ResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,            
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;            
            this.m_SARSCoV2TestOrder = (Business.Test.APTIMASARSCoV2.APTIMASARSCoV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_SARSCoV2ResultPage = new APTIMASARSCoV2ResultPage(this.m_SARSCoV2TestOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
			this.m_SARSCoV2ResultPage.Next += new APTIMASARSCoV2ResultPage.NextEventHandler(SARSCoV2ResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_SARSCoV2ResultPage);
		}

		private void SARSCoV2ResultPage_Next(object sender, EventArgs e)
		{
			this.Finished();
		}		
    }
}
