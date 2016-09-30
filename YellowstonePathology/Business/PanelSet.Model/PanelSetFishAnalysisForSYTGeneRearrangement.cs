using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetFishAnalysisForSYTGeneRearrangement : PanelSet
	{
        public PanelSetFishAnalysisForSYTGeneRearrangement()
		{
			this.m_PanelSetId = 127;
            this.m_PanelSetName = "Fish Analysis For SYT Gene Rearrangement";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;


            this.m_AllowMultiplePerAccession = true;

            string taskDescription = "Gather materials and send to Emory U.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, new Facility.Model.EmoryUniversity()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.EmoryUniversity();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.EmoryUniversity();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.EmoryUniversity();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.EmoryUniversity();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
