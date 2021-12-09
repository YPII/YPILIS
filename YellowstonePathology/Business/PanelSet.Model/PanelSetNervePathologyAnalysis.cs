using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetNervePathologyAnalysis : PanelSet
	{
        public PanelSetNervePathologyAnalysis()
		{
			this.m_PanelSetId = 98;
			this.m_PanelSetName = "Nerve Pathology Analysis";
            this.m_CaseType = Business.CaseType.Surgical;
			this.m_HasTechnicalComponent = true;            
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;

this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            string taskDescription = "Gather materials and send out to Therapath.";

            YellowstonePathology.Business.Facility.Model.Facility therapath = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("THRPTH");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, taskDescription, therapath));

            this.m_TechnicalComponentFacility = therapath;
            this.m_ProfessionalComponentFacility = therapath;

            this.m_TechnicalComponentBillingFacility = therapath;
            this.m_ProfessionalComponentBillingFacility = therapath;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
