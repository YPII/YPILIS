using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	class PanelSetMLH1Retired : PanelSet
	{
		public PanelSetMLH1Retired()
		{
			this.m_PanelSetId = 64;
            this.m_PanelSetName = "MLH1 Methylation Analysis - Retired";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;            
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();            					
			this.m_AllowMultiplePerAccession = true;
            this.m_Active = false;

            string task1Description = "Gather materials (FFPE solid tumor tissue: Paraffin block is preferred. " +
                "Alternatively, send 1 H&E slide plus 5-10 unstained slides cut at 5 or more microns.Take materials to transcription for send out to Neo";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, task1Description));

            string task3Description = "Receive materials from Histo and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, task3Description));            

			YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics taskSendBlockToNeogenomics = new YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics();
            this.m_TaskCollection.Add(taskSendBlockToNeogenomics);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
