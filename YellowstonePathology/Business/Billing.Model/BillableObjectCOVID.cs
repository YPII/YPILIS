using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectCOVID : BillableObject
    {        
        public BillableObjectCOVID(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
            : base(accessionOrder, reportNo)
        {
            
        }

        public override YellowstonePathology.Business.Rules.MethodResult Set()
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            if (this.m_PanelSetOrder.HoldBilling == false)
            {                
                this.HandleICD10Codes();         
                this.SetBillingType();                
                this.SetPanelSetOrderCPTCodes();
                this.SetMedicareAddonCode();
                this.SetCOVIDFacilityCode();
                
                methodResult.Success = true;
            }
            else
            {
                methodResult.Success = false;
                methodResult.Message = "This case cannot be set because it is on hold.";
            }
            return methodResult;
        }        

        public void HandleICD10Codes()
        {
            if(this.m_AccessionOrder.ICD9BillingCodeCollection.CodeExists(this.m_AccessionOrder.ICD10Code) == false)
            {
                string specimenOrderId = this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenOrderId;
                string reportNo = this.m_PanelSetOrder.ReportNo;
                YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9BillingCode = this.m_AccessionOrder.ICD9BillingCodeCollection.GetNextItem(reportNo,
                    this.m_AccessionOrder.MasterAccessionNo, specimenOrderId, this.m_AccessionOrder.ICD10Code, 1);
                this.m_AccessionOrder.ICD9BillingCodeCollection.Add(icd9BillingCode);
            }            
        }

        public void SetMedicareAddonCode()
        {
            if(this.m_AccessionOrder.CollectionDate >= DateTime.Parse("01/01/2021"))
            {                
                if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists("U0005", 1) == false)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                    panelSetOrderCPTCode.Quantity = 1;
                    panelSetOrderCPTCode.CPTCode = "U0005";
                    panelSetOrderCPTCode.Modifier = null;
                    panelSetOrderCPTCode.CodeableDescription = "Medicare addon code for COVID.";
                    panelSetOrderCPTCode.CodeableType = "BillableTest";
                    panelSetOrderCPTCode.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
                    panelSetOrderCPTCode.SpecimenOrderId = this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenOrderId;
                    panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                    panelSetOrderCPTCode.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                    panelSetOrderCPTCode.Account = this.m_AccessionOrder.SvhAccount;
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
                }                
            }            
        }

        public void SetCOVIDFacilityCode()
        {
            if (this.m_AccessionOrder.CollectionFacilityId == "YPICOVID")
            {                
                if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists("99211", 1) == false)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                    panelSetOrderCPTCode.Quantity = 1;
                    panelSetOrderCPTCode.CPTCode = "99211";
                    panelSetOrderCPTCode.Modifier = null;
                    panelSetOrderCPTCode.CodeableDescription = "COVID Facaility addon code.";
                    panelSetOrderCPTCode.CodeableType = "BillableTest";
                    panelSetOrderCPTCode.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
                    panelSetOrderCPTCode.SpecimenOrderId = this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenOrderId;
                    panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                    panelSetOrderCPTCode.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                    panelSetOrderCPTCode.Account = this.m_AccessionOrder.SvhAccount;
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
                }
            }
        }

        public override void SetBillingType()
        {
            Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_AccessionOrder.ClientId);
            if(string.IsNullOrEmpty(client.COVIDBillingType) == false)
            {
                this.m_PanelSetOrder.BillingType = client.COVIDBillingType;
            }
            else
            {
                throw new Exception($"The COVID Billing Type is not set for this client: {client.ClientName}");
            }                        
            
            if(string.IsNullOrEmpty(client.COVIDTravelBillingType) == false)
            {
                Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(this.m_AccessionOrder.MasterAccessionNo);
                if(clientOrderCollection.Count > 0)
                {
                    if(string.IsNullOrEmpty(clientOrderCollection[0].AsymptomaticType) == false && clientOrderCollection[0].AsymptomaticType.Contains("Travel") == true)
                    {
                        this.m_PanelSetOrder.BillingType = client.COVIDTravelBillingType;
                    }
                }
            }

            if (this.m_AccessionOrder.PaymentType == "Insurance")
            {
                this.m_PanelSetOrder.BillingType = "Patient";
            }
        }

        public override void PostTechnical(string billTo, string billBy)
        {            
            if(this.m_AccessionOrder.UseBillingAgent == true && this.m_PanelSetOrder.NoCharge == false)
            {
                this.PostU0003(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, this.m_PanelSetOrder.BillingType, billBy);
                this.PostU0005(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, this.m_PanelSetOrder.BillingType, billBy);
                this.Post99211(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, this.m_PanelSetOrder.BillingType, billBy);
            }            
        }

        public override void PostProfessional(string billTo, string billBy)
        {
            //Do Nothing
        }

        public override void PostGlobal(string billTo, string billBy)
        {
            //Do Nothing
        }

        public void PostU0003(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            if(this.m_AccessionOrder.UseBillingAgent == true && this.m_PanelSetOrder.NoCharge == false)
            {
                CptCode u0003 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("U0003", null);

                string modifier = string.Empty;
                if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(u0003.Code, modifier) == false)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                    panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                    panelSetOrderCPTCodeBill.BillTo = billTo;
                    panelSetOrderCPTCodeBill.BillBy = billBy;
                    panelSetOrderCPTCodeBill.CPTCode = u0003.Code;
                    panelSetOrderCPTCodeBill.CodeType = u0003.CodeType.ToString();
                    panelSetOrderCPTCodeBill.Modifier = modifier;
                    panelSetOrderCPTCodeBill.Quantity = 1;
                    panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                    panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                    panelSetOrderCPTCodeBill.PostDate = DateTime.Today;
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
                }
            }            
        }

        public void PostU0005(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            if(this.m_AccessionOrder.UseBillingAgent == true && this.m_PanelSetOrder.NoCharge == false)
            {
                CptCode u0005 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("U0005", null);

                string modifier = string.Empty;
                if (this.m_AccessionOrder.CollectionDate >= DateTime.Parse("01/01/2021"))
                {
                    if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(u0005.Code, modifier) == false)
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                        panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                        panelSetOrderCPTCodeBill.BillTo = billTo;
                        panelSetOrderCPTCodeBill.BillBy = billBy;
                        panelSetOrderCPTCodeBill.CPTCode = u0005.Code;
                        panelSetOrderCPTCodeBill.CodeType = u0005.CodeType.ToString();
                        panelSetOrderCPTCodeBill.Modifier = modifier;
                        panelSetOrderCPTCodeBill.Quantity = 1;
                        panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                        panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                        panelSetOrderCPTCodeBill.PostDate = DateTime.Today;
                        this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
                    }
                }
            }            
        }

        public void Post99211(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            if(this.m_AccessionOrder.UseBillingAgent == true && this.m_PanelSetOrder.NoCharge == false)
            {
                CptCode cpt99211 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("99211", null);

                string modifier = string.Empty;
                if (this.m_AccessionOrder.CollectionFacilityId == "YPICOVID")
                {
                    if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(cpt99211.Code, modifier) == false)
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                        panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                        panelSetOrderCPTCodeBill.BillTo = billTo;
                        panelSetOrderCPTCodeBill.BillBy = billBy;
                        panelSetOrderCPTCodeBill.CPTCode = cpt99211.Code;
                        panelSetOrderCPTCodeBill.CodeType = cpt99211.CodeType.ToString();
                        panelSetOrderCPTCodeBill.Modifier = modifier;
                        panelSetOrderCPTCodeBill.Quantity = 1;
                        panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                        panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                        panelSetOrderCPTCodeBill.PostDate = DateTime.Today;
                        this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
                    }
                }
            }            
        }

        public void PostG0416(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            //DO nothing.
        }
    }
}
