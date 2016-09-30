using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
	public class EGFRMutationAnalysisTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
        public EGFRMutationAnalysisTest()
		{
			this.m_PanelSetId = 60;
			this.m_PanelSetName = "EGFR Mutation Analysis";
            this.m_Abbreviation = "EGFR";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;			
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisWordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(5, 0, 0, 0);
            //Checked by MS and TK;            
            this.m_EpicDistributionIsImplemented = true;

            string taskDescription = "Cut curls and an after H&E. Also, cut 2 unstained slides and an after H&E for possible ALK. Give materials to molecular.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

            string task2Description = "Receive curls from Histology and perform testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, task2Description));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            
            this.m_HasSplitCPTCode = true;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81235(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());

            EGFRMutationAnalysisPanel egfrMutationAnalysisPanel = new EGFRMutationAnalysisPanel();
            this.m_PanelCollection.Add(egfrMutationAnalysisPanel);
        }
    }
}
