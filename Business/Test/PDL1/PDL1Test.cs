using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PDL1
{
	public class PDL1Test : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public PDL1Test()
		{
			this.m_PanelSetId = 215;
            this.m_PanelSetName = "PD-L1";
            this.m_CaseType = YellowstonePathology.Business.CaseType.IHC;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            //this.m_ResultDocumentSource = PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_NeverDistribute = true;

			this.m_AllowMultiplePerAccession = true;

            string taskDescription = "Gather materials and send to Neogenomics.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription));

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88342(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
		}
	}
}
