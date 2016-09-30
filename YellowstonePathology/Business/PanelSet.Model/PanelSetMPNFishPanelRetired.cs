using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetMPNFishPanelRetired : PanelSet
	{
        public PanelSetMPNFishPanelRetired()
		{
			this.m_PanelSetId = 105;
			this.m_PanelSetName = "MPN FISH Panel - Retired";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();                        			            
			this.m_AllowMultiplePerAccession = true;
            this.m_Active = false;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            string taskDescription = "Gather materials (Bone Marrow Aspirate: 1-2 mL sodium heparin tube. EDTA tube is acceptable. " +
                "Peripheral Blood: 2-5 mL sodium heparin tube. EDTA tube is acceptable." +
                "Fresh, Unfixed Tissue: Tissue in RPMI. Fluids: Equal parts RPMI to specimen volume) and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, new Facility.Model.NeogenomicsIrvine()));            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
