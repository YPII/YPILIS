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
            int billedCount = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetCodeQuantity("AUTOPSYBLOCK");            
            YellowstonePathology.Business.Billing.Model.CptCode autopsyBlock = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("AUTOPSYBLOCK", null);

            if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists(autopsyBlock.Code, blockCount) == false)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCode.Quantity = blockCount - billedCount;
                panelSetOrderCPTCode.CPTCode = autopsyBlock.Code;
                panelSetOrderCPTCode.Modifier = CptCodeModifier.TechnicalComponent;
                panelSetOrderCPTCode.CodeableDescription = "Autopsy Block";
                panelSetOrderCPTCode.CodeableType = "BillableTest";
                panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
                panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCode.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCode.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
            }

            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
            billableObjectStains.SetPanelSetOrderCPTCodes();

            this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.UpdateCodeType();
        }

        public override void PostTechnical(string billTo, string billBy)
        {
            int blockCount = this.m_AccessionOrder.SpecimenOrderCollection.GetBlockCount();
            int billedCount = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount("AUTOPSYBLOCK", "TC");

            YellowstonePathology.Business.Billing.Model.CptCode autopsyBlock = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("AUTOPSYBLOCK", null);
            if(billedCount < blockCount)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                item.FromPanelSetOrderCPTCode(this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection[0]);
                item.BillTo = billTo;
                item.BillBy = billBy;
                item.Quantity = blockCount - billedCount;
                item.Modifier = "TC";
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
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
