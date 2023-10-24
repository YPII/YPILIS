using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.AnalCytology
{
	public class AnalCytologyTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public AnalCytologyTest()
		{
			this.m_PanelSetId = 433;
			this.m_PanelSetName = "Anal Cytology";
            this.m_Abbreviation = "Anal Cytology";
			this.m_CaseType = Business.CaseType.Cytology;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterA();
            this.m_Active = true;
            
			this.m_AllowMultiplePerAccession = true;
            this.m_PanelSetOrderClassName = typeof(AnalCytologyTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(AnalCytologyWordDocument).AssemblyQualifiedName;
        
            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88112", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceCYTO());
		}
	}
}
