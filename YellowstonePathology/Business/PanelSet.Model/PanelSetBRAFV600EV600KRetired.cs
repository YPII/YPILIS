using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetBRAFV600EV600KRetired : PanelSet
	{
        public PanelSetBRAFV600EV600KRetired()
		{
			this.m_PanelSetId = 123;
            this.m_PanelSetName = "BRAF V600E/K Retired";            
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;
            this.m_Active = false;

            
			

            string taskDescription = "Gather materials (FFPE solid tumor tissue: Paraffin block is preferred. " +
                "Alternatively, send 1 H&E slide plus 5-10 unstained slides cut at 5 or more microns.) and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, new Facility.Model.NeogenomicsIrvine())); 
            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceBRAFMANAL());
		}
	}
}
