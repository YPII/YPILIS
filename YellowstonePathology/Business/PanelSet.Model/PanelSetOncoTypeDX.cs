using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetOncoTypeDX : PanelSet
	{
        public PanelSetOncoTypeDX()
		{
			this.m_PanelSetId = 101;
			this.m_PanelSetName = "OncoType DX";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;            
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_ExpectedDuration = new TimeSpan(10, 0, 0, 0);            			
            
			this.m_AllowMultiplePerAccession = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            string taskDescription = "Gather materials and send to Genomic Health.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, taskDescription, new Facility.Model.GenomicHealth()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.GenomicHealth();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.GenomicHealth();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.GenomicHealth();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.GenomicHealth();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());

            Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88363(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);
		}
	}
}
