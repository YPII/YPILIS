using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetFishAnalysisForSYTGeneRearrangement : FISHTest
	{
        public PanelSetFishAnalysisForSYTGeneRearrangement()
		{
			this.m_PanelSetId = 127;
            this.m_PanelSetName = "Fish Analysis For SYT Gene Rearrangement";
            this.m_CaseType = Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;

this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            YellowstonePathology.Business.Facility.Model.Facility facility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("EMRYNVSTY");
            string taskDescription = "Gather materials and send to Emory U.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, facility));

            this.m_TechnicalComponentFacility = facility;
            this.m_ProfessionalComponentFacility = facility;

            this.m_TechnicalComponentBillingFacility = facility;
            this.m_ProfessionalComponentBillingFacility = facility;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());

            this.m_ProbeSetCount = 0;
		}
	}
}
