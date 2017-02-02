using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectTechnicalOnlyAutopsy : BillableObject
    {
        public BillableObjectTechnicalOnlyAutopsy(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) 
            : base(accessionOrder, reportNo)
        {

        }        

        public override void SetPanelSetOrderCPTCodes()
        {        
            int blockCount = this.m_AccessionOrder.SpecimenOrderCollection.GetBlockCount();
            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.AutopsyBlock autopsyBlock = new Business.Billing.Model.CptCodeDefinition.AutopsyBlock();
            if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists(autopsyBlock.Code, blockCount) == false)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCode.Quantity = blockCount;
                panelSetOrderCPTCode.CPTCode = autopsyBlock.Code;
                panelSetOrderCPTCode.Modifier = autopsyBlock.Modifier;
                panelSetOrderCPTCode.CodeableDescription = "Autopsy Block";
                panelSetOrderCPTCode.CodeableType = "BillableTest";
                panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;                
                panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
            }

            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
            billableObjectStains.SetPanelSetOrderCPTCodes();

            this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.UpdateCodeType();
        }

		public override void PostTechnical(string billTo, string billBy)
		{
            int blockCount = this.m_AccessionOrder.SpecimenOrderCollection.GetBlockCount();
            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.AutopsyBlock autopsyBlock = new Business.Billing.Model.CptCodeDefinition.AutopsyBlock();
            foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection)
            {                              
                if(panelSetOrderCPTCode.CPTCode == autopsyBlock.Code)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                    item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
                    item.BillTo = billTo;
                    item.BillBy = billBy;
                    item.Quantity = blockCount;
                    item.Modifier = autopsyBlock.Modifier;
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
                }                 
            }

            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
            billableObjectStains.PostTechnical(billTo, billBy);

            this.m_PanelSetOrder.TechnicalComponentBillingFacilityId = "YPIBLGS";
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
            // Do nothing
        }
    }
}
