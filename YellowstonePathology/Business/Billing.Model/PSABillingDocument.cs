using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Billing.Model
{
	public class PSABillingDocument : XElement
	{
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private string m_ReportNo;
		private string m_ProviderNPI;

        public PSABillingDocument(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
			: base("Case")
		{
            this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
		}		

		public virtual void Build()
		{			
			this.m_ProviderNPI = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId).Npi;
			this.SetAccessionNode();
			this.SetCptCodeNodes();			
            this.SetICD10CodeNodes();
		}		

		protected void SetAccessionNode()
		{
			string assignedTo = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(this.m_PanelSetOrder.AssignedToId).DisplayName;

            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetAllFacilities();
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.m_PanelSetOrder.PanelSetId);

            string professionalComponentFacilityName = string.Empty;
            string professionalComponentFacilityCLIA = string.Empty;
            string technicalComponentFacilityName = string.Empty;
            string technicalComponentFacilityCLIA = string.Empty;

            if (panelSet.HasProfessionalComponent == true)
            {
				YellowstonePathology.Business.Facility.Model.Facility professionalComponentFacility = facilityCollection.GetByFacilityId(this.m_PanelSetOrder.ProfessionalComponentFacilityId);
                professionalComponentFacilityName = professionalComponentFacility.FacilityName;
                professionalComponentFacilityCLIA = professionalComponentFacility.CLIALicense.LicenseNumber;
            }

            if (panelSet.HasTechnicalComponent == true)
            {
				YellowstonePathology.Business.Facility.Model.Facility technicalComponentFacility = facilityCollection.GetByFacilityId(this.m_PanelSetOrder.TechnicalComponentFacilityId);
                technicalComponentFacilityName = technicalComponentFacility.FacilityName;
                technicalComponentFacilityCLIA = technicalComponentFacility.CLIALicense.LicenseNumber;
            }                       

			this.Add(new XElement("MasterAccessionNo", this.m_AccessionOrder.MasterAccessionNo),
				new XElement("PanelSetName", this.m_PanelSetOrder.PanelSetName),
				new XElement("ReportNo", this.m_ReportNo),
                new XElement("PrimaryInsurance", this.m_AccessionOrder.PrimaryInsurance),
                new XElement("SecondaryInsurance", this.m_AccessionOrder.SecondaryInsurance),
                new XElement("FeeSchedule", this.m_AccessionOrder.FeeSchedule),
				new XElement("PatientType", this.m_AccessionOrder.PatientType),
				new XElement("PatientFirstName", this.m_AccessionOrder.PFirstName),
				new XElement("PatientLastName", this.m_AccessionOrder.PLastName),
				new XElement("PatientMiddleInitial", this.m_AccessionOrder.PMiddleInitial),
				new XElement("PatientSuffix", this.m_AccessionOrder.PSuffix),
				new XElement("PatientRace", this.m_AccessionOrder.PRace),
				new XElement("PatientGender", this.m_AccessionOrder.PSex),
				new XElement("PatientBirthdate", Helper.DateTimeExtensions.DateStringFromNullable(this.m_AccessionOrder.PBirthdate)),
				new XElement("PatientMaritalStatus", this.m_AccessionOrder.PMaritalStatus),
				new XElement("PatientPhoneNumberBusiness", this.m_AccessionOrder.PPhoneNumberBusiness),
				new XElement("PatientPhoneNumberHome", this.m_AccessionOrder.PPhoneNumberHome),
				new XElement("PatientAddress1", this.m_AccessionOrder.PAddress1),
				new XElement("PatientAddress2", this.m_AccessionOrder.PAddress2),
				new XElement("PatientCity", this.m_AccessionOrder.PCity),                
				new XElement("PatientState", this.m_AccessionOrder.PState),
				new XElement("PatientZip", this.m_AccessionOrder.PZipCode),
				new XElement("PatientInsurancePlan1", this.m_AccessionOrder.InsurancePlan1),
				new XElement("PatientInsurancePlan2", this.m_AccessionOrder.InsurancePlan2),
				new XElement("ClientId", this.m_AccessionOrder.ClientId),
				new XElement("ClientName", this.m_AccessionOrder.ClientName),
				new XElement("ProviderName", this.m_AccessionOrder.PhysicianName),
				new XElement("ProviderNPI", this.m_ProviderNPI),
                new XElement("AssignedTo", assignedTo),
				new XElement("DateOfService", Helper.DateTimeExtensions.DateStringFromNullable(this.m_AccessionOrder.CollectionDate)),
				new XElement("FinalDate", Helper.DateTimeExtensions.DateStringFromNullable(this.m_PanelSetOrder.FinalDate)),
                new XElement("TechnicalComponentFacilityCLIA", technicalComponentFacilityCLIA),
                new XElement("TechnicalComponentFacilityName", technicalComponentFacilityName),
                new XElement("ProfessionalComponentFacilityCLIA", professionalComponentFacilityCLIA),
                new XElement("ProfessionalComponentFacilityName", professionalComponentFacilityName));				
		}

		private void SetCptCodeNodes()
		{
			XElement cptListElement = new XElement("CPTCodes");
            YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();

			foreach(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection)
			{
                YellowstonePathology.Business.Billing.Model.CptCode cptCode = cptCodeCollection.GetCptCode(panelSetOrderCPTCodeBill.CPTCode);
                if (panelSetOrderCPTCodeBill.BillTo == "Patient" && panelSetOrderCPTCodeBill.BillBy != "CLNT" 
                    || cptCode is YellowstonePathology.Business.Billing.Model.PQRSCode == true)
                {
                    XElement cptElement = new XElement("CPTCode",
                        new XElement("Code", panelSetOrderCPTCodeBill.CPTCode),
                        new XElement("Quantity", panelSetOrderCPTCodeBill.Quantity.ToString()),
                        new XElement("Modifier", panelSetOrderCPTCodeBill.Modifier),                        
                        new XElement("PostDate", panelSetOrderCPTCodeBill.PostDate),
                        new XElement("BillTo", panelSetOrderCPTCodeBill.BillTo));
                    cptListElement.Add(cptElement);
                }
			}
			this.Add(cptListElement);
		}		

        protected void SetICD10CodeNodes()
        {
            XElement icd10ListElement = new XElement("ICD10Codes");
            foreach (YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd10BillingCode in this.m_AccessionOrder.ICD9BillingCodeCollection)
            {
                XElement icd10Element = new XElement("ICD10Code",
                    new XElement("Code", icd10BillingCode.ICD10Code),
                    new XElement("Quantity", icd10BillingCode.Quantity.ToString()));
                icd10ListElement.Add(icd10Element);
            }
            this.Add(icd10ListElement);
        }        		
	}
}
