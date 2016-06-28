using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetFoundationOneGeneticTesting : PanelSet
	{
        public PanelSetFoundationOneGeneticTesting()
		{
			this.m_PanelSetId = 142;
			this.m_PanelSetName = "Foundation One Genetic Testing";            
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            
			this.m_AllowMultiplePerAccession = true;

            
			

            string taskDescription = "Gather materials and send to Foundation Medicine";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.FoundationMedicine();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.FoundationMedicine();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.FoundationMedicine();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.FoundationMedicine();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
