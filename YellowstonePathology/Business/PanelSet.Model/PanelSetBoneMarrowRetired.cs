using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetBoneMarrowRetired : PanelSet
	{
		public PanelSetBoneMarrowRetired()
		{
			this.m_PanelSetId = 49;
			this.m_PanelSetName = "Bone Marrow";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Surgical;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;			
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.RetiredTestDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterS();
            this.m_Active = true;            			            
			this.m_AllowMultiplePerAccession = true;

            string taskDescription = "Flow to do bone marrow.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceYPI());
		}
	}
}
