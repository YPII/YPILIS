using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public HER2AmplificationByISHTest()
		{
			this.m_PanelSetId = 46;
			this.m_PanelSetName = "HER2 Amplification by D-ISH";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;
            
			this.m_SurgicalAmendmentRequired = true;
			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(4, 0, 0, 0);
            this.m_EpicDistributionIsImplemented = true;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            
            this.m_HasSplitCPTCode = false;

            this.m_CMMCDistributionIsImplemented = true;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88377(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            string taskDescription = "Cut 2 unstained slides and an after H&E. Give to molecular.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

            string task2Description = "Recieve materials from histology and run test.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, task2Description));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());

            HER2AmplificationByISHPanel her2AmplificationByISHPanel = new HER2AmplificationByISHPanel();
            this.m_PanelCollection.Add(her2AmplificationByISHPanel);
        }
    }
}
