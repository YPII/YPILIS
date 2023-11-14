using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	class CalreticulinMutationAnalysisResultPath : ResultPath
    {
		CalreticulinMutationAnalysisResultPage m_CalreticulinMutationAnalysisResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder m_ReportOrderCalreticulinMutationAnalysis;

		public CalreticulinMutationAnalysisResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_ReportOrderCalreticulinMutationAnalysis = (YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_CalreticulinMutationAnalysisResultPage = new CalreticulinMutationAnalysisResultPage(this.m_ReportOrderCalreticulinMutationAnalysis, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_CalreticulinMutationAnalysisResultPage.Next += new CalreticulinMutationAnalysisResultPage.NextEventHandler(CalreticulinMutationAnalysisResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_CalreticulinMutationAnalysisResultPage);
        }

		private void CalreticulinMutationAnalysisResultPage_Next(object sender, EventArgs e)
        {
			this.Finished();
        }        
	}
}
