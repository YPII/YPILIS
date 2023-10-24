using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Autopsy
{
	public class AutopsyTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public AutopsyTest()
		{
			this.m_PanelSetId = 35;
			this.m_PanelSetName = "Autopsy";
			this.m_CaseType = Business.CaseType.Autopsy;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterA();
            this.m_Active = true;            
			
			this.m_AllowMultiplePerAccession = false;

this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.Autopsy.AutopsyTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;            

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceAUTOPSY());
		}
	}
}
