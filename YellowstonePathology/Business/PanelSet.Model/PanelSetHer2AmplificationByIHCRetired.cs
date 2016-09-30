using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetHer2AmplificationByIHCRetired : PanelSet
	{
		public PanelSetHer2AmplificationByIHCRetired()
		{
			this.m_PanelSetId = 70;
			this.m_PanelSetName = "HER2 Amplification by IHC - Retired";
            this.m_CaseType = YellowstonePathology.Business.CaseType.IHC;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;


            this.m_AllowMultiplePerAccession = true;
            this.m_Active = false;

            string taskDescription = "Gather materials (Cut Slides: 3 cut slides. Cut sections at 3-4 microns, and place tissue at the center bottom of the slide" +
                "or Paraffin block: (Preferred for image analysis.) Formalin-fixed paraffin-embedded tissue.) and give to Molecular for send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

            string taskDescription2 = "Receive materials from histology and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription2, new Facility.Model.NeogenomicsIrvine()));            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
