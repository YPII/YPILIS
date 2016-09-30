using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetBCRABLByPCRRetired : PanelSet
	{
        public PanelSetBCRABLByPCRRetired()
		{
			this.m_PanelSetId = 109;
			this.m_PanelSetName = "BCR-ABL By PCR - Retired";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;


            this.m_AllowMultiplePerAccession = true;
            this.m_Active = false;

            string taskDescription = "Gather materials (Peripheral blood: 2-5 mL in sodium heparin tube and 2x5 mL in EDTA tube or " + 
                "Bone marrow: 1-2 mL in sodium heparin tube and 2 mL in EDTA tube) and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, new Facility.Model.NeogenomicsIrvine()));

            Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81206(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);

            Business.Billing.Model.PanelSetCptCode panelSetCptCode2 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81207(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode2);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
		}
	}
}
