using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetJAK2Exon1214Retired : PanelSet
	{
        public PanelSetJAK2Exon1214Retired()
		{
			this.m_PanelSetId = 107;
			this.m_PanelSetName = "JAK2 Exon 12-14 - Retired";            
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();            
            
			this.m_AllowMultiplePerAccession = true;
            this.m_Active = false;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            string taskDescription = "Gather materials (Peripheral blood: 5 mL in EDTA tube or Bone marrow: 2 mL in EDTA tube) and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, new Facility.Model.NeogenomicsIrvine()));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
		}
	}
}
