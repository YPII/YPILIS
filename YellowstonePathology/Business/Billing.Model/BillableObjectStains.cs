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

            this.m_IhcTestCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetIHCTests();
            this.m_GradedTestCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetGradedTests();
            this.m_CytochemicalTestCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetCytochemicalTests();
            this.m_CytochemicalForMicroorganismsTestCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetCytochemicalForMicroorganismsTests();            
        }                       

        public override void PostGlobal(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				this.Post88360(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
				this.Post88313(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
				this.Post88312(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
				this.Post88342(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);
				this.Post88341(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global, billTo, billBy);                
                this.Post3395F();
			}
        }

        public override void PostProfessional(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				this.Post88360(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
				this.Post88313(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
				this.Post88312(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
				this.Post88342(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);
				this.Post88341(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional, billTo, billBy);                
                this.Post3395F();
			}
        }

        public override void PostTechnical(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				this.Post88360(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
				this.Post88313(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
				this.Post88312(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
				this.Post88342(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);
				this.Post88341(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, billTo, billBy);                
                this.Post3395F();
			}
        }        

        public void Post88360(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {            
            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88360 cpt88360 = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88360();
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
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);             
            }
        }

        public void Post88313(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88313 cpt88313 = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88313();
            string modifier = cpt88313.GetModifier(billingComponent);
            int cpt88313Count = this.m_StainSpecimenCollection.GetBillable88313Count() - this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88313.Code, modifier);
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
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);                
            }
        }

        public void Post88312(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88312 cpt88312 = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88312();
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
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);             
            }
        }

        public void Post88342(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88342 cpt88342 = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88342();
            string modifier = cpt88342.GetModifier(billingComponent);
            int cpt88342Count = this.m_StainSpecimenCollection.GetBillable88342Count() - this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88342.Code, modifier);
            
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
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);             
            }
        }

        public void Post88341(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy)
        {
            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88341 cpt88341 = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88341();
            string modifier = cpt88341.GetModifier(billingComponent);
            int cpt88341Count = this.m_StainSpecimenCollection.GetBillable88341Count() - this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetBilledCount(cpt88341.Code, modifier);            

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
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);             
            }
        }

        public void Post3395F()
        {
            if (this.m_StainSpecimenCollection.Requires3395F() == true)
            {
                if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists("3395F") == false)
                {
                    YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3395F pqrs3395F = new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3395F();
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                    panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
                    panelSetOrderCPTCodeBill.BillTo = null;
                    panelSetOrderCPTCodeBill.BillBy = null;
                    panelSetOrderCPTCodeBill.CPTCode = pqrs3395F.Code;
                    panelSetOrderCPTCodeBill.CodeType = pqrs3395F.CodeType.ToString();
                    panelSetOrderCPTCodeBill.Modifier = null;
                    panelSetOrderCPTCodeBill.Quantity = 1;
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
                }
            }
        }

		public override void SetPanelSetOrderCPTCodes()
        {
			if (this.IsOkToSet() == true)
			{
                this.SetERPRPQRSCodes();

				YellowstonePathology.Business.Test.Model.TestCollection testCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetAllTests();
				foreach (StainSpecimen stainSpecimen in this.m_StainSpecimenCollection)
				{
					foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in stainSpecimen.TestOrderCollection)
					{						
						YellowstonePathology.Business.Test.Model.Test test = testCollection.GetTest(testOrder.TestId);
						if (test.IsBillable == true)
						{
							if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.SystemGeneratedReferenceIdExists(testOrder.TestOrderId) == false)
							{
								string codeableType = test.GetCodeableType(testOrder.OrderedAsDual);
								YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
								panelSetOrderCPTCode.Quantity = 1;								
								panelSetOrderCPTCode.CodeableType = codeableType;
								panelSetOrderCPTCode.CodeableDescription = "Specimen " + stainSpecimen.SpecimenOrder.SpecimenNumber + ": " + testOrder.TestName;
								panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
								panelSetOrderCPTCode.SpecimenOrderId = stainSpecimen.SpecimenOrder.SpecimenOrderId;
								panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
								panelSetOrderCPTCode.ReferenceId = testOrder.TestOrderId;


                                if (this.m_PanelSetOrder.PanelSetId != 31 &&  this.m_PanelSetOrder.PanelSetId != 201) //Not technical only
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

        private void SetERPRPQRSCodes()
        {
            int count88360 = this.m_StainSpecimenCollection.GetBillable88360Count();
            if (count88360 > 0)
            {
                if (this.m_AccessionOrder.PrimaryInsurance == "Medicare")
                {
                    YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3395F pqrs3395 = new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3395F();
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);                    
                    panelSetOrderCPTCode.Quantity = 1;
                    panelSetOrderCPTCode.CPTCode = pqrs3395.Code;
                    panelSetOrderCPTCode.CodeType = pqrs3395.CodeType.ToString();
                    panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;                    
                    panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
                }
            }
        }                                         
    }
}
