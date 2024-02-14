using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class RetrospectiveResultPath : ResultPath
    {
		RetrospectiveReviewResultPage m_RectrospectiveReviewResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrder m_RetrospectiveReviewTestOrder;

        public RetrospectiveResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window) 
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_RetrospectiveReviewTestOrder = (YellowstonePathology.Business.Test.RetrospectiveReview.RetrospectiveReviewTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
            this.m_RectrospectiveReviewResultPage = new RetrospectiveReviewResultPage(this.m_RetrospectiveReviewTestOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_RectrospectiveReviewResultPage.Next += new RetrospectiveReviewResultPage.NextEventHandler(RetrospectiveReviewResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_RectrospectiveReviewResultPage);
        }

        private void RetrospectiveReviewResultPage_Next(object sender, EventArgs e)
        {            
			this.Finished();			
        }                
	}
}
