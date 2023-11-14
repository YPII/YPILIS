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

        public override void SetPanelSetOrderCPTCodes()
        {
            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
            billableObjectStains.SetPanelSetOrderCPTCodes();
            this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.UpdateCodeType();
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

            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
            billableObjectStains.PostTechnical(billTo, billBy);
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

            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
            billableObjectStains.PostProfessional(billTo, billBy);
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

            BillableObjectStains billableObjectStains = new BillableObjectStains(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
            billableObjectStains.PostGlobal(billTo, billBy);            
        }

        public void Post88305(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            CptCode cpt88305 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", null);

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
                    panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                    panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
                }
            }            
        }

        public void PostG0416(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cptG0416 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0416", null);

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
                panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
            }            
        }
    }
}
