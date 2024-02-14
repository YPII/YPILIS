using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Billing.Model
{
	public class APSBillingDocument
	{
		private JObject m_JObject;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private string m_ReportNo;
		private string m_ProviderNPI;

        public APSBillingDocument(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
		}	
		
		public JObject Document
		{
			get { return this.m_JObject; }
		}

		public virtual void Build()
		{			
			this.m_ProviderNPI = Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId).Npi;
			this.SetAccessionNode();
			this.SetCptCodeNodes();			
            this.SetICD10CodeNodes();
			this.SetADT();
		}		

		private void SetADT()
		{								
			Business.HL7View.ADTMessages adtMessages = Business.Gateway.AccessionOrderGateway.GetADTMessagesByPatientNameDOB(this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PBirthdate.Value);
			List<HL7View.IN1> uniqueIN1Segments = adtMessages.GetUniqueIN1Segments();			

			JArray in1Array = new JArray();
			foreach (HL7View.IN1 in1 in uniqueIN1Segments)
			{
				in1Array.Add(in1.ToJson());
			}
			
			if(in1Array.Count > 0)
			{
				this.m_JObject.Add(new JProperty("insurance", in1Array));
			}			

			HL7View.GT1 gt1Segment = adtMessages.GetFirstGT1Segment();
					
			if(gt1Segment != null)
			{								
				this.m_JObject.Add(new JProperty("guarantor",
				new JObject(new JProperty("guarantorLastName", gt1Segment.GuarantorLastName),
				new JProperty("guarntorFirstName", gt1Segment.GuarantorFirstName),
				new JProperty("guarntorMiddleName", gt1Segment.GuarantorMiddleName),				
				new JProperty("guarantorAddressLine1", gt1Segment.GuarantorAddressLine1),
				new JProperty("guarantorAddressLine2", gt1Segment.GuarantorAddressLine2),
				new JProperty("guarantorCity", gt1Segment.GuarantorCity),
				new JProperty("guarantorState", gt1Segment.GuarantorState),
				new JProperty("guarantorZip", gt1Segment.GuarantorZip)
				)));				
			}			
		}

		protected void SetAccessionNode()
		{
			string assignedTo = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(this.m_PanelSetOrder.AssignedToId).DisplayName;

            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.m_PanelSetOrder.PanelSetId);

            string professionalComponentFacilityName = string.Empty;
            string professionalComponentFacilityCLIA = string.Empty;
            string technicalComponentFacilityName = string.Empty;
            string technicalComponentFacilityCLIA = string.Empty;

            if (panelSet.HasProfessionalComponent == true)
            {
				YellowstonePathology.Business.Facility.Model.Facility professionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(this.m_PanelSetOrder.ProfessionalComponentFacilityId);
                professionalComponentFacilityName = professionalComponentFacility.FacilityName;
                professionalComponentFacilityCLIA = professionalComponentFacility.CLIALicenseNumber;
            }

            if (panelSet.HasTechnicalComponent == true)
            {
				YellowstonePathology.Business.Facility.Model.Facility technicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(this.m_PanelSetOrder.TechnicalComponentFacilityId);
                technicalComponentFacilityName = technicalComponentFacility.FacilityName;
                technicalComponentFacilityCLIA = technicalComponentFacility.CLIALicenseNumber;
            }			
			
			this.m_JObject = new JObject(new JProperty("masterAccessionNo", this.m_AccessionOrder.MasterAccessionNo),
				new JProperty("panelSetName", this.m_PanelSetOrder.PanelSetName),
				new JProperty("reportNo", this.m_ReportNo),
                new JProperty("primaryInsurance", this.m_AccessionOrder.PrimaryInsurance),
                new JProperty("secondaryInsurance", this.m_AccessionOrder.SecondaryInsurance),
                new JProperty("feeSchedule", this.m_AccessionOrder.FeeSchedule),
				new JProperty("patientType", this.m_AccessionOrder.PatientType),
				new JProperty("patientFirstName", this.m_AccessionOrder.PFirstName),
				new JProperty("patientLastName", this.m_AccessionOrder.PLastName),
				new JProperty("patientMiddleInitial", this.m_AccessionOrder.PMiddleInitial),
				new JProperty("patientSuffix", this.m_AccessionOrder.PSuffix),
				new JProperty("patientRace", this.m_AccessionOrder.PRace),
				new JProperty("patientGender", this.m_AccessionOrder.PSex),
				new JProperty("patientBirthdate", Helper.DateTimeExtensions.DateStringFromNullable(this.m_AccessionOrder.PBirthdate)),
				new JProperty("patientMaritalStatus", this.m_AccessionOrder.PMaritalStatus),
				new JProperty("patientPhoneNumberBusiness", this.m_AccessionOrder.PPhoneNumberBusiness),
				new JProperty("patientPhoneNumberHome", this.m_AccessionOrder.PPhoneNumberHome),
				new JProperty("patientAddress1", this.m_AccessionOrder.PAddress1),
				new JProperty("patientAddress2", this.m_AccessionOrder.PAddress2),
				new JProperty("patientCity", this.m_AccessionOrder.PCity),                
				new JProperty("patientState", this.m_AccessionOrder.PState),
				new JProperty("patientZip", this.m_AccessionOrder.PZipCode),
                new JProperty("patientSsn", this.m_AccessionOrder.PSSN),
                new JProperty("patientInsurancePlan1", this.m_AccessionOrder.InsurancePlan1),
				new JProperty("patientInsurancePlan2", this.m_AccessionOrder.InsurancePlan2),
				new JProperty("clientId", this.m_AccessionOrder.ClientId),
				new JProperty("clientName", this.m_AccessionOrder.ClientName),
				new JProperty("providerName", this.m_AccessionOrder.PhysicianName),
				new JProperty("providerNPI", this.m_ProviderNPI),
                new JProperty("assignedTo", assignedTo),
				new JProperty("dateOfService", Helper.DateTimeExtensions.DateStringFromNullable(this.m_AccessionOrder.CollectionDate)),
				new JProperty("finalDate", Helper.DateTimeExtensions.DateStringFromNullable(this.m_PanelSetOrder.FinalDate)),
                new JProperty("technicalComponentFacilityCLIA", technicalComponentFacilityCLIA),
                new JProperty("technicalComponentFacilityName", technicalComponentFacilityName),
                new JProperty("professionalComponentFacilityCLIA", professionalComponentFacilityCLIA),
                new JProperty("professionalComponentFacilityName", professionalComponentFacilityName),
                new JProperty("additionalInformation", this.m_AccessionOrder.AdditionalInformation),                
                new JProperty("dateOfDeath", Helper.DateTimeExtensions.DateStringFromNullable(this.m_AccessionOrder.DateOfDeath)),
                new JProperty("dateOfInjury", Helper.DateTimeExtensions.DateStringFromNullable(this.m_AccessionOrder.DateOfInjury)),
                new JProperty("wcAuthorizationId", this.m_AccessionOrder.WCAuthorizationId),
                new JProperty("vcAuthorizationId", this.m_AccessionOrder.VAAuthorizationId),
                new JProperty("ordered14DaysPostDischarge", this.m_PanelSetOrder.Ordered14DaysPostDischarge),
                new JProperty("VAAuthorizationStart", Helper.DateTimeExtensions.DateStringFromNullable(this.m_AccessionOrder.VAAuthorizationStart)),
                new JProperty("VAAuthorizationEnd", Helper.DateTimeExtensions.DateStringFromNullable(this.m_AccessionOrder.VAAuthorizationEnd)),                
                new JProperty("patientPaymentInstructions", this.m_AccessionOrder.PatientPaymentInstructions));

			Business.HL7View.ADTMessages adtMessages = Business.Gateway.AccessionOrderGateway.GetADTMessagesByPatientNameDOB(this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PBirthdate.Value);
		}

		private void SetCptCodeNodes()
		{
			JArray cptCodeArray = new JArray();
			JProperty cptCodesElement = new JProperty("cptCodes", cptCodeArray);

			foreach(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection)
			{
                YellowstonePathology.Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(panelSetOrderCPTCodeBill.CPTCode, panelSetOrderCPTCodeBill.Modifier);
                if (panelSetOrderCPTCodeBill.BillTo == "Patient" && panelSetOrderCPTCodeBill.BillBy != "CLNT" 
                    || cptCode is YellowstonePathology.Business.Billing.Model.PQRSCode == true)
                {
                    JObject cptElement = new JObject(
                        new JProperty("code", panelSetOrderCPTCodeBill.CPTCode),
                        new JProperty("quantity", panelSetOrderCPTCodeBill.Quantity.ToString()),
                        new JProperty("modifier", panelSetOrderCPTCodeBill.Modifier),                        
                        new JProperty("postDate", panelSetOrderCPTCodeBill.PostDate),
                        new JProperty("billTo", panelSetOrderCPTCodeBill.BillTo));
					cptCodeArray.Add(cptElement);
                }
			}
			this.m_JObject.Add(cptCodesElement);
		}		

        protected void SetICD10CodeNodes()
        {
			JArray icdCodeArray = new JArray();
            JProperty icdCodeElement = new JProperty("icd10Codes", icdCodeArray);
            foreach (YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd10BillingCode in this.m_AccessionOrder.ICD9BillingCodeCollection)
            {
                JObject icd10Element = new JObject(
                    new JProperty("code", icd10BillingCode.ICD10Code),
                    new JProperty("quantity", icd10BillingCode.Quantity.ToString()));
				icdCodeArray.Add(icd10Element);
            }
            this.m_JObject.Add(icdCodeElement);
        }        		
	}
}
