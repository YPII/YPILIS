using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectSurgicalPathology : BillableObject
    {
        public BillableObjectSurgicalPathology(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) 
            : base(accessionOrder, reportNo)
        {
            
        }        

        public override void SetPanelSetOrderCPTCodes()
        {
            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
            billableObjectStains.SetPanelSetOrderCPTCodes();
            this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.UpdateCodeType();
        }        

        public override void PostTechnical(string billTo, string billBy)
        {
            base.PostManualEntriesTechnical(billTo, billBy);
			BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
			billableObjectStains.PostTechnical(billTo, billBy);
        }

        public override void PostProfessional(string billTo, string billBy)
        {
            base.PostManualEntriesProfessional(billTo, billBy);
            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
			billableObjectStains.PostProfessional(billTo, billBy);
        }

        public override void PostGlobal(string billTo, string billBy)
        {
            base.PostManualEntriesGlobal(billTo, billBy);
            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
			billableObjectStains.PostGlobal(billTo, billBy);            
        }		
    }
}
