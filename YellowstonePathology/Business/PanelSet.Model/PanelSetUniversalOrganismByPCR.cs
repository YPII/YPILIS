using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
    public class PanelSetUniversalOrganismByPCR : PanelSet
	{
		public PanelSetUniversalOrganismByPCR()
		{
			this.m_PanelSetId = 80;
			this.m_PanelSetName = "Universal Organism By PCR";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;            
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
			
			
            
			this.m_AllowMultiplePerAccession = true;

            string task1Description = "Gather materials and take to transcription for send out to University of Washington.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, task1Description));

            string task2Description = "Receive materials from Histo and send out to University of Washington.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, task2Description));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.UniversityOfWashington();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.UniversityOfWashington();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.UniversityOfWashington();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.UniversityOfWashington();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
