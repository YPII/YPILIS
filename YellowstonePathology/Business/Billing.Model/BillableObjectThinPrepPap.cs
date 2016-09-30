using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectThinPrepPap : BillableObject
    {        
        public BillableObjectThinPrepPap(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
            : base(accessionOrder, reportNo)
        {
            
        }       		

        public override void SetPanelSetOrderCPTCodes()
        {
			if (this.IsOkToSet() == true)
			{
				this.SetPrimaryScreeningCode();
				this.SetPhysicianInterpretationCode();
				this.SetICD9Codes();
			}
        }

		public override void PostGlobal(string billTo, string billby)
		{
			if (this.IsOkToPost() == true)
			{
				YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
				foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection)
				{
					YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetCptCode(panelSetOrderCPTCode.CPTCode);
					if (cptCode.IsBillable == true)
					{
						bool okToPost = true;
						if (cptCode.HasProfessionalComponent == true) okToPost = this.CanPostProfessionalCode();
						if (okToPost == true)
						{
							YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
							item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
							item.BillTo = billTo;
							item.BillBy = billby;
							this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
						}
					}
				}
			}
		}

		public override void PostTechnical(string billTo, string billBy)
		{
			if (this.IsOkToPost() == true)
			{
				YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
				foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection)
				{
					YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetCptCode(panelSetOrderCPTCode.CPTCode);
					if (cptCode.IsBillable == true)
					{
						if (cptCode.HasTechnicalComponent == true)
						{
							YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill bill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
							bill.ClientId = this.m_AccessionOrder.ClientId;
							bill.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
							bill.BillTo = billTo;
							bill.BillBy = billBy;
							if (bill.Modifier == null) bill.Modifier = cptCode.GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical);
							this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(bill);
						}
					}
				}
			}
		}

		public override void PostProfessional(string billTo, string billby)
		{
			if (this.IsOkToPost() == true)
			{
				if (this.CanPostProfessionalCode() == true)
				{
					YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();

					foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection)
					{
						YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetCptCode(panelSetOrderCPTCode.CPTCode);
						if (cptCode.IsBillable == true)
						{
							if (cptCode.HasProfessionalComponent == true)
							{
								YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
								item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
								item.BillTo = billTo;
								item.BillBy = billby;
								item.Modifier = cptCode.GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional);
								this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);
							}
						}
					}
				}
			}
		}

		public override void PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent)
		{
			// do nothing
		}

        private void SetICD9Codes()
        {
			string resultCode = ((YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_PanelSetOrder).ResultCode;
            string icd9Code = string.Empty;
            string icd10Code = string.Empty;

            if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeReactive(resultCode) == true)
            {
                icd9Code = "795.09";
                icd10Code = "R87.820";
            }
            else if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisASCUS(resultCode) == true)
            {
                icd9Code = "795.01";
                icd10Code = "R87.610";
            }
            else if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisASCH(resultCode) == true)
            {
                icd9Code = "795.02";
                icd10Code = "R87.611";
            }
            else if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisLSIL(resultCode) == true)
            {
                icd9Code = "795.03";
                icd10Code = "R87.612";
            }
            else if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisHGSIL(resultCode) == true)
            {
                icd9Code = "795.04";
                icd10Code = "R87.613";
            }
            else if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisAGUS(resultCode) == true)
            {
                icd9Code = "795.00";
                icd10Code = "R87.619";
            }            

            if (string.IsNullOrEmpty(icd9Code) == false)
            {
                if (this.m_AccessionOrder.ICD9BillingCodeCollection.CodeExists(icd9Code) == false)
                {
                    YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9BillingCode = this.m_AccessionOrder.ICD9BillingCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo,
                        this.m_AccessionOrder.MasterAccessionNo, this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenOrderId, icd9Code, icd10Code, 1);
                    icd9BillingCode.DesignatedFor = "Signing Physician";
                    icd9BillingCode.Source = "Cytology";
                    this.m_AccessionOrder.ICD9BillingCodeCollection.Add(icd9BillingCode);
                }
            }
        }

        private void SetPrimaryScreeningCode()
        {
			if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists("88175", 1) == false &&
				this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists("88142", 1) == false)
			{
				YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_PanelSetOrder;
				YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytology = panelSetOrderCytology.GetPrimaryScreening();
				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);

                YellowstonePathology.Business.Billing.Model.CptCode cptCode = null;
				
				if (panelOrderCytology.ImagerError == false)
				{
                    cptCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88175();                    
				}
				else
				{
                    cptCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88142();
                    cptCode.Modifier = "52";					
				}

				YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
				panelSetOrderCPTCode.Quantity = 1;
				panelSetOrderCPTCode.CPTCode = cptCode.Code;
                panelSetOrderCPTCode.CodeType = cptCode.CodeType.ToString();
				panelSetOrderCPTCode.Modifier = cptCode.Modifier;
				panelSetOrderCPTCode.CodeableDescription = "Primary Screening (" + panelOrderCytology.ScreenedByName + ")";
				panelSetOrderCPTCode.CodeableType = "CytologyPrimaryScreening";
				panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
				panelSetOrderCPTCode.SpecimenOrderId = specimenOrder.SpecimenOrderId;
				panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
				this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
			}
        }        

        private void SetPhysicianInterpretationCode()
        {
            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88141 cptCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88141();
			if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists(cptCode.Code, 1) == false)
			{
				YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytology = ((YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_PanelSetOrder).GetPhysicianInterp();
				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);

				if (panelOrderCytology != null)
				{
					YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
					panelSetOrderCPTCode.Quantity = 1;
                    panelSetOrderCPTCode.CPTCode = cptCode.Code;
                    panelSetOrderCPTCode.CodeType = cptCode.CodeType.ToString();
					panelSetOrderCPTCode.Modifier = null;
					panelSetOrderCPTCode.CodeableDescription = "Physician Interpretation (" + panelOrderCytology.ScreenedByName + ")";
					panelSetOrderCPTCode.CodeableType = "CytologyPrimaryScreening";
					panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;
					panelSetOrderCPTCode.SpecimenOrderId = specimenOrder.SpecimenOrderId;
					panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
					this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
				}
			}
        }

		protected bool CanPostProfessionalCode()
		{
			bool result = false;
			YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytology = ((YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_PanelSetOrder).GetPhysicianInterp();
			if (panelOrderCytology != null && panelOrderCytology.NoCharge == false) result = true;

			return result;
		}
    }
}
