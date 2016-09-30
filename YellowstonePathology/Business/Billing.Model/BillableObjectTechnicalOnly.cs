using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectTechnicalOnly : BillableObject
    {
        public BillableObjectTechnicalOnly(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) 
            : base(accessionOrder, reportNo)
        {

        }        

        public override void SetPanelSetOrderCPTCodes()
        {
			BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
			billableObjectStains.SetPanelSetOrderCPTCodes();
        }

		public override void PostTechnical(string billTo, string billBy)
		{
			BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
			billableObjectStains.PostTechnical(billTo, billBy);
		}

		public override void PostProfessional(string billTo, string billBy)
		{
			//Do nothing
		}

		public override void PostGlobal(string billTo, string billBy)
		{
			// Do nothing
		}

		public override void PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent)
		{
			BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
			billableObjectStains.PostClientGCodes(billingComponent);
		}
	}
}
