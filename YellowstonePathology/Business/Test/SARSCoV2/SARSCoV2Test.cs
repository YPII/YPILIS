using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.SARSCoV2
{
	public class SARSCoV2Test : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public SARSCoV2Test()
		{
			this.m_PanelSetId = 400;
			this.m_PanelSetName = "SARS-CoV-2";
            this.m_Abbreviation = "SARS-CoV-2";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;           

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.SARSCoV2.SARSCoV2TestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.SARSCoV2.SARSCoV2WordDocument).AssemblyQualifiedName;
            this.m_RequiresPathologistSignature = false;
            this.m_AcceptOnFinal = true;
			this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);            

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");            

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("U0003", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceSARSCOV2());            
        }
	}
}
