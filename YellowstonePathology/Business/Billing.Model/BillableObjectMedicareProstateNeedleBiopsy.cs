using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectMedicareProstateNeedleBiopsy : BillableObject
    {
        public BillableObjectMedicareProstateNeedleBiopsy(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) 
            : base(accessionOrder, reportNo)
        {
            
        }        
        
        public override void PostTechnical(string billTo, string billBy)
        {
            if (billTo == "Client")
            {
                this.Post88305(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
            }
            else
            {
                this.PostG0416(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
            }            
        }

        public override void PostProfessional(string billTo, string billBy)
        {
            if (billTo == "Client")
            {
                this.Post88305(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
            }
            else
            {
                this.PostG0416(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
            }  
        }

        public override void PostGlobal(string billTo, string billBy)
        {
            if (billTo == "Client")
            {
                this.Post88305(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
            }
            else
            {
                this.PostG0416(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
            }  
        }

        public void Post88305(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88305 cpt88305 = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88305();
            int cpt88305Count = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetCodeQuantity(cpt88305.Code);

            if (cpt88305Count > 0)
            {
                string modifier = cpt88305.GetModifier(billingComponent);
                if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(cpt88305.Code, modifier) == false)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                    panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                    panelSetOrderCPTCodeBill.BillTo = billTo;
                    panelSetOrderCPTCodeBill.BillBy = billBy;
                    panelSetOrderCPTCodeBill.CPTCode = cpt88305.Code;
                    panelSetOrderCPTCodeBill.CodeType = cpt88305.CodeType.ToString();
                    panelSetOrderCPTCodeBill.Modifier = modifier;
                    panelSetOrderCPTCodeBill.Quantity = cpt88305Count;
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
                }
            }
        }

        public void PostG0416(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.GCodeDefinitions.CPTG0416 cptG0416 = new YellowstonePathology.Business.Billing.Model.GCodeDefinitions.CPTG0416();

            string modifier = cptG0416.GetModifier(billingComponent);
            if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(cptG0416.Code, modifier) == false)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCodeBill.BillTo = billTo;
                panelSetOrderCPTCodeBill.BillBy = billBy;
                panelSetOrderCPTCodeBill.CPTCode = cptG0416.Code;
                panelSetOrderCPTCodeBill.CodeType = cptG0416.CodeType.ToString();
                panelSetOrderCPTCodeBill.Modifier = modifier;
                panelSetOrderCPTCodeBill.Quantity = 1;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
            }            
        }
    }
}
