using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
    public class PanelSetCytogeneticAnalysisRetired : PanelSet
	{
		public PanelSetCytogeneticAnalysisRetired()
		{
			this.m_PanelSetId = 54;
			this.m_PanelSetName = "Cytogenetic Analysis - Retired";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Cytogenetics;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = false;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;

            string taskDescription = "Gather materials (Peripheral blood: 2-5 mL in sodium heparin tube or Bone marrow: 1-2 mL in sodium heparin tube) and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, new Facility.Model.NeogenomicsIrvine()));            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
