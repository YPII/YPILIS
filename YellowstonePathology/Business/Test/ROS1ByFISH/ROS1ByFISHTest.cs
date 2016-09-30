using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
	public class ROS1ByFISHTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public ROS1ByFISHTest()
		{
			this.m_PanelSetId = 204;
			this.m_PanelSetName = "ROS1 By FISH";
            this.m_Abbreviation = "ROS1";
			this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = true;            
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHWordDocument).AssemblyQualifiedName;            
			this.m_AllowMultiplePerAccession = true;
            //Changed by MS and TK;
            this.m_EpicDistributionIsImplemented = true;

            string taskDescription1 = "Cut H&E slide and give to pathologist to circle tumor for tech only. Give the paraffin block to Molecular so they can send to NEO.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription1));

            string taskDescription2 = "Collect slide from pathologist and paraffin block from histology and send to Neogenomics.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription2, new Facility.Model.NeogenomicsIrvine())); 

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
		}
	}
}
