using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetDNAContent : PanelSet
	{
        public PanelSetDNAContent()
		{
			this.m_PanelSetId = 85;
			this.m_PanelSetName = "DNA Content / Cell Cycle Analysis";
            this.m_CaseType = YellowstonePathology.Business.CaseType.DNA;
			this.m_HasTechnicalComponent = true;            
            this.HasProfessionalComponent = true;
			this.ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.Active = true;			
            
			this.m_AllowMultiplePerAccession = true;

            string taskDescription = "Gather materials and send to ARUP.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.ARUP();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.ARUP();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.ARUP();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.ARUP();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
