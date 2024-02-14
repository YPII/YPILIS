using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
	public class BillableObjectStains : BillableObject
    {
        protected StainSpecimenCollection m_StainSpecimenCollection;

        protected YellowstonePathology.Business.Test.Model.TestCollection m_IhcTestCollection;
        protected YellowstonePathology.Business.Test.Model.TestCollection m_GradedTestCollection;
        protected YellowstonePathology.Business.Test.Model.TestCollection m_CytochemicalTestCollection;
        protected YellowstonePathology.Business.Test.Model.TestCollection m_CytochemicalForMicroorganismsTestCollection;

		public BillableObjectStains(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) 
            : base(accessionOrder, reportNo)
        {
            this.m_StainSpecimenCollection = StainSpecimenCollection.GetCollection(accessionOrder, reportNo);

            this.m_IhcTestCollection = Business.Test.Model.TestCollectionInstance.GetIHCTests();
            this.m_GradedTestCollection = Business.Test.Model.TestCollectionInstance.GetGradedTests();
            this.m_CytochemicalTestCollection = Business.Test.Model.TestCollectionInstance.GetCytochemicalTests();
            this.m_CytochemicalForMicroorganismsTestCollection = Business.Test.Model.TestCollectionInstance.GetCytochemicalForMicroorganismsTests();            
        }                       

        public override void PostGlobal(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				this.Post88360(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
                this.Post88364(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
                this.Post88365(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
                this.Post88313(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
				this.Post88312(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
				this.Post88342(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
				this.Post88341(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
                this.Post88344(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
                this.Post89060(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
                //this.Post3395F();
			}
        }

        public override void PostProfessional(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				this.Post88360(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
                this.Post88364(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
                this.Post88365(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
                this.Post88313(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
				this.Post88312(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
				this.Post88342(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
				this.Post88341(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
                this.Post88344(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
                this.Post89060(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
                //this.Post3395F();
			}
        }

        public override void PostTechnical(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				this.Post88360(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
                this.Post88364(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
                this.Post88365(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
                this.Post88313(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
				this.Post88312(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
                this.PostWrightsStain(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
                this.Post88342(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
				this.Post88341(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
                this.Post88344(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
                this.Post89060(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
                //this.Post3395F();
			}
        }        

        public void Post88360(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {            
            YellowstonePathology.Business.Billing.Model.CptCode cpt88360 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88360", null);
            string modifier = cpt88360.GetModifier(billingComponent);
            int cpt88360Count = this.m_StainSpecimenCollection.GetBillable88360Count() - this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88360.Code, modifier);
            if (cpt88360Count > 0)
            {                
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;                    
                panelSetOrderCPTCodeBill.BillTo = billTo;
                panelSetOrderCPTCodeBill.BillBy = billBy;
                panelSetOrderCPTCodeBill.CPTCode = cpt88360.Code;
                panelSetOrderCPTCodeBill.CodeType = cpt88360.CodeType.ToString();
                panelSetOrderCPTCodeBill.Modifier = modifier;
                panelSetOrderCPTCodeBill.Quantity = cpt88360Count;
                panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);             
            }
        }

        public void Post88313(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cpt88313 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88313", null);
            string modifier = cpt88313.GetModifier(billingComponent);
            int billableCount = this.m_StainSpecimenCollection.GetBillable88313Count();
            int billedCount = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88313.Code, modifier);
            int cpt88313Count = billableCount - billedCount;
            if (cpt88313Count > 0)
            {                
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCodeBill.BillTo = billTo;
                panelSetOrderCPTCodeBill.BillBy = billBy;
                panelSetOrderCPTCodeBill.CPTCode = cpt88313.Code;
                panelSetOrderCPTCodeBill.CodeType = cpt88313.CodeType.ToString();
                panelSetOrderCPTCodeBill.Modifier = modifier;
                panelSetOrderCPTCodeBill.Quantity = cpt88313Count;
                panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);                
            }
        }

        public void Post88312(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cpt88312 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88312", null);
            string modifier = cpt88312.GetModifier(billingComponent);
            int cpt88312Count = this.m_StainSpecimenCollection.GetBillable88312Count() - this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88312.Code, modifier);
            if (cpt88312Count > 0)
            {             
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCodeBill.BillTo = billTo;
                panelSetOrderCPTCodeBill.BillBy = billBy;
                panelSetOrderCPTCodeBill.CPTCode = cpt88312.Code;
                panelSetOrderCPTCodeBill.CodeType = cpt88312.CodeType.ToString();
                panelSetOrderCPTCodeBill.Modifier = modifier;
                panelSetOrderCPTCodeBill.Quantity = cpt88312Count;
                panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);             
            }
        }

        public void PostWrightsStain(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cpt88312 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88312", null);
            string modifier = cpt88312.GetModifier(billingComponent);
            int cpt88312Count = this.m_StainSpecimenCollection.GetBillableWrightsStainCount() - this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88312.Code, modifier);
            if (cpt88312Count > 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCodeBill.BillTo = billTo;
                panelSetOrderCPTCodeBill.BillBy = billBy;
                panelSetOrderCPTCodeBill.CPTCode = cpt88312.Code;
                panelSetOrderCPTCodeBill.CodeType = cpt88312.CodeType.ToString();
                panelSetOrderCPTCodeBill.Modifier = modifier;
                panelSetOrderCPTCodeBill.Quantity = cpt88312Count;
                panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
            }
        }

        public void Post89060(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cpt89060 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("89060", null);
            string modifier = cpt89060.GetModifier(billingComponent);
            int cpt889060Count = this.m_StainSpecimenCollection.GetBillable89060Count() - this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt89060.Code, modifier);
            if (cpt889060Count > 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCodeBill.BillTo = billTo;
                panelSetOrderCPTCodeBill.BillBy = billBy;
                panelSetOrderCPTCodeBill.CPTCode = cpt89060.Code;
                panelSetOrderCPTCodeBill.CodeType = cpt89060.CodeType.ToString();
                panelSetOrderCPTCodeBill.Modifier = modifier;
                panelSetOrderCPTCodeBill.Quantity = cpt889060Count;
                panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
            }
        }

        public void Post88344(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cpt88344 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88344", null);
            string modifier = cpt88344.GetModifier(billingComponent);
            int cpt88344Count = this.m_StainSpecimenCollection.GetBillable88344Count() - this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88344.Code, modifier);

            if (cpt88344Count > 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCodeBill.BillTo = billTo;
                panelSetOrderCPTCodeBill.BillBy = billBy;
                panelSetOrderCPTCodeBill.CPTCode = cpt88344.Code;
                panelSetOrderCPTCodeBill.CodeType = cpt88344.CodeType.ToString();
                panelSetOrderCPTCodeBill.Modifier = modifier;
                panelSetOrderCPTCodeBill.Quantity = cpt88344Count;
                panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
            }
        }

        public void Post88342(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cpt88342 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null);
            string modifier = cpt88342.GetModifier(billingComponent);
            int billableCount = this.m_StainSpecimenCollection.GetBillable88342Count();
            int billedCount = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88342.Code, modifier);
            int cpt88342Count = billableCount - billedCount;
            
            if (cpt88342Count > 0)
            {                                
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCodeBill.BillTo = billTo;
                panelSetOrderCPTCodeBill.BillBy = billBy;
                panelSetOrderCPTCodeBill.CPTCode = cpt88342.Code;
                panelSetOrderCPTCodeBill.CodeType = cpt88342.CodeType.ToString();
                panelSetOrderCPTCodeBill.Modifier = modifier;
                panelSetOrderCPTCodeBill.Quantity = cpt88342Count;
                panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);             
            }
        }

        public void Post88341(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cpt88341 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88341", null);
            string modifier = cpt88341.GetModifier(billingComponent);
            int billableCount = this.m_StainSpecimenCollection.GetBillable88341Count();
            int billedCount = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88341.Code, modifier);
            int cpt88341Count = billableCount - billedCount;

            if (cpt88341Count > 0)
            {                
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCodeBill.BillTo = billTo;
                panelSetOrderCPTCodeBill.BillBy = billBy;
                panelSetOrderCPTCodeBill.CPTCode = cpt88341.Code;
                panelSetOrderCPTCodeBill.CodeType = cpt88341.CodeType.ToString();
                panelSetOrderCPTCodeBill.Modifier = modifier;
                panelSetOrderCPTCodeBill.Quantity = cpt88341Count;
                panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);             
            }
        }

        public void Post3395F()
        {
            if (this.m_StainSpecimenCollection.Requires3395F() == true)
            {
                if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists("3395F") == false)
                {
                    YellowstonePathology.Business.Billing.Model.PQRSCode pqrs3395F = (PQRSCode)Store.AppDataStore.Instance.CPTCodeCollection.GetClone("3395F", null);
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                    panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                    panelSetOrderCPTCodeBill.BillTo = null;
                    panelSetOrderCPTCodeBill.BillBy = null;
                    panelSetOrderCPTCodeBill.CPTCode = pqrs3395F.Code;
                    panelSetOrderCPTCodeBill.CodeType = pqrs3395F.CodeType.ToString();
                    panelSetOrderCPTCodeBill.Modifier = null;
                    panelSetOrderCPTCodeBill.Quantity = 1;
                    panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                    panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
                }
            }
        }

		public override void SetPanelSetOrderCPTCodes()
        {
			if (this.IsOkToSet() == true )
			{
                this.SetERPRPQRSCodes();

				foreach (StainSpecimen stainSpecimen in this.m_StainSpecimenCollection)
				{
					foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in stainSpecimen.TestOrderCollection)
					{						
                        if(testOrder.NoCharge == false)
                        {
                            YellowstonePathology.Business.Test.Model.Test test = Business.Test.Model.TestCollectionInstance.GetClone(testOrder.TestId);
                            if (test.IsBillable == true)
                            {
                                if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.SystemGeneratedReferenceIdExists(testOrder.TestOrderId) == false)
                                {
                                    string codeableType = test.GetCodeableType(testOrder.OrderedAsDual);
                                    YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                                    panelSetOrderCPTCode.Quantity = 1;
                                    panelSetOrderCPTCode.CodeableType = codeableType;
                                    panelSetOrderCPTCode.CodeableDescription = "Specimen " + stainSpecimen.SpecimenOrder.SpecimenNumber + ": " + testOrder.TestName;
                                    panelSetOrderCPTCode.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
                                    panelSetOrderCPTCode.SpecimenOrderId = stainSpecimen.SpecimenOrder.SpecimenOrderId;
                                    panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                                    panelSetOrderCPTCode.ReferenceId = testOrder.TestOrderId;
                                    panelSetOrderCPTCode.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                                    panelSetOrderCPTCode.Account = this.m_AccessionOrder.SvhAccount;

                                    if (this.m_PanelSetOrder.PanelSetId != 31 && this.m_PanelSetOrder.PanelSetId != 201 && this.m_PanelSetOrder.PanelSetId != 289 && this.m_PanelSetOrder.PanelSetId != 203) //Not technical only
                                    {
                                        YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_PanelSetOrder;
                                        if (panelSetOrderSurgical.SurgicalSpecimenCollection.HasStainResult(testOrder.TestOrderId) == true)
                                        {
                                            YellowstonePathology.Business.SpecialStain.StainResultItem stainResult = panelSetOrderSurgical.SurgicalSpecimenCollection.GetStainResult(testOrder.TestOrderId);
                                            YellowstonePathology.Business.Billing.Model.CptCode cptCode = null;

                                            if (stainResult.IsGraded == true)
                                            {
                                                cptCode = test.GetGradedCptCode(false);
                                                panelSetOrderCPTCode.CPTCode = cptCode.Code;
                                                panelSetOrderCPTCode.CodeType = cptCode.CodeType.ToString();
                                            }
                                            else
                                            {
                                                cptCode = test.GetCptCode(false);
                                                panelSetOrderCPTCode.CPTCode = cptCode.Code;
                                                panelSetOrderCPTCode.CodeType = cptCode.CodeType.ToString();
                                            }
                                        }
                                        else
                                        {
                                            panelSetOrderCPTCode.CPTCode = test.GetCptCode(false).Code;
                                        }
                                    }
                                    else //Is technical only
                                    {
                                        YellowstonePathology.Business.Billing.Model.CptCode cptCode = test.GetCptCode(true);
                                        panelSetOrderCPTCode.CPTCode = cptCode.Code;
                                        panelSetOrderCPTCode.CodeType = cptCode.CodeType.ToString();
                                    }

                                    this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
                                }
                            }
                        }						
					}
				}
			}       
        }

        private void SetERPRPQRSCodes()
        {
            int count88360 = this.m_StainSpecimenCollection.GetBillable88360Count();
            if (count88360 > 0)
            {
                if (this.m_AccessionOrder.PrimaryInsurance == "Medicare")
                {
                    YellowstonePathology.Business.Billing.Model.PQRSCode pqrs3395 = (PQRSCode)Store.AppDataStore.Instance.CPTCodeCollection.GetClone("3395F", null);
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);                    
                    panelSetOrderCPTCode.Quantity = 1;
                    panelSetOrderCPTCode.CPTCode = pqrs3395.Code;
                    panelSetOrderCPTCode.CodeType = pqrs3395.CodeType.ToString();
                    panelSetOrderCPTCode.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;                    
                    panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                    panelSetOrderCPTCode.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                    panelSetOrderCPTCode.Account = this.m_AccessionOrder.SvhAccount;
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
                }
            }
        }

        public void Post88365(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cpt88365 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88365", null);
            string modifier = cpt88365.GetModifier(billingComponent);            

            int cpt88365Count = this.m_StainSpecimenCollection.GetBillable88365Count() - this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88365.Code, modifier);
            if (cpt88365Count > 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCodeBill.BillTo = billTo;
                panelSetOrderCPTCodeBill.BillBy = billBy;
                panelSetOrderCPTCodeBill.CPTCode = cpt88365.Code;
                panelSetOrderCPTCodeBill.CodeType = cpt88365.CodeType.ToString();
                panelSetOrderCPTCodeBill.Modifier = modifier;
                panelSetOrderCPTCodeBill.Quantity = cpt88365Count;
                panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
            }
        }

        public void Post88364(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cpt88364 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88364", null);
            string modifier = cpt88364.GetModifier(billingComponent);

            int cpt88364Count = this.m_StainSpecimenCollection.GetBillable88364Count() - this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88364.Code, modifier);
            if (cpt88364Count > 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCodeBill.BillTo = billTo;
                panelSetOrderCPTCodeBill.BillBy = billBy;
                panelSetOrderCPTCodeBill.CPTCode = cpt88364.Code;
                panelSetOrderCPTCodeBill.CodeType = cpt88364.CodeType.ToString();
                panelSetOrderCPTCodeBill.Modifier = modifier;
                panelSetOrderCPTCodeBill.Quantity = cpt88364Count;
                panelSetOrderCPTCodeBill.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                panelSetOrderCPTCodeBill.Account = this.m_AccessionOrder.SvhAccount;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
            }
        }
    }
}
