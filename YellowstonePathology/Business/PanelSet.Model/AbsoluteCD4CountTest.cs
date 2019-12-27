using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.AbsoluteCD4Count
{
	public class AbsoluteCD4CountTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public AbsoluteCD4CountTest()
		{
			this.m_PanelSetId = 53;
			this.m_PanelSetName = "Absolute CD4 Count";
            this.m_CaseType = YellowstonePathology.Business.CaseType.IHC;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            YellowstonePathology.Business.Facility.Model.Facility facility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            string taskDescription = "Send materials to ARUP for testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, facility));

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("ARUPSPD");
            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("ARUPSPD");

            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS"); //YellowstonePathologyInstituteBillings
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS"); //YellowstonePathologistBillings

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
		}
	}
}
