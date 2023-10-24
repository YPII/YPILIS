using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class RulesAcceptPeerReviewResults : BaseRules
	{
        private static RulesAcceptPeerReviewResults m_Instance;
		private YellowstonePathology.Business.Test.PanelOrder m_PanelOrderItemPeerReview;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemUser m_OrderingUser;

		private RulesAcceptPeerReviewResults()
			: base(typeof(RulesAcceptPeerReviewResults)) { }

		public static RulesAcceptPeerReviewResults Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new RulesAcceptPeerReviewResults();
				}
				return m_Instance;
			}
		}

		public YellowstonePathology.Business.Test.PanelOrder PanelOrderItemPeerReview
		{
			get { return this.m_PanelOrderItemPeerReview; }
			set { this.m_PanelOrderItemPeerReview = value; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
			set { this.m_AccessionOrder = value; }
		}

		public void SetPanelSetOrderItem()
		{
			this.m_PanelSetOrderItem = this.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelOrderItemPeerReview.ReportNo);
		}

		public YellowstonePathology.Business.User.SystemUser OrderingUser
		{
			get { return this.m_OrderingUser; }
			set { this.m_OrderingUser = value; }
		}

		private bool UserIsReviewer()
		{			
			if (this.m_OrderingUser.UserId == m_PanelOrderItemPeerReview.OrderedById)
			{
				return true;
			}		
			return false;
		}

		private bool HasResult()
		{
			if (m_PanelOrderItemPeerReview.TestOrderCollection[0].Result == "Disagree" ||
				m_PanelOrderItemPeerReview.TestOrderCollection[0].Result == "Agree")
			{
				return true;
			}
			return false;
		}

		private void SetResultValues()
		{
			m_PanelOrderItemPeerReview.AcceptedById = OrderingUser.UserId;
			m_PanelOrderItemPeerReview.AcceptedDate = DateTime.Today;
			m_PanelOrderItemPeerReview.AcceptedTime = DateTime.Now;
		}

		private void UnSetResultValues()
		{
			m_PanelOrderItemPeerReview.AcceptedById = 0;
			m_PanelOrderItemPeerReview.AcceptedDate = null;
			m_PanelOrderItemPeerReview.AcceptedTime = null;
		}
	}
}
