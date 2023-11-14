using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetAnatomicPathologyConsultation : PanelSet
	{
        public PanelSetAnatomicPathologyConsultation()
		{
			this.m_PanelSetId = 110;
			this.m_PanelSetName = "Anatomic Pathology Consultation";            
            this.m_CaseType = Business.CaseType.Surgical;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            string taskDescription = "Gather materials and send out to consulting pathologist.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            YellowstonePathology.Business.Facility.Model.Facility universityOfMichigan = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("UOMHS");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = universityOfMichigan;
            this.m_ProfessionalComponentFacility = universityOfMichigan;

            this.m_TechnicalComponentBillingFacility = universityOfMichigan;
            this.m_ProfessionalComponentBillingFacility = universityOfMichigan;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
