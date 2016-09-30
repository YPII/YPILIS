using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MYD88MutationAnalysis
{
    public class MYD88MutationAnalysisTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public MYD88MutationAnalysisTest()
		{
			this.m_PanelSetId = 178;
            this.m_PanelSetName = "MYD88 Mutation Analysis";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.MYD88MutationAnalysis.MYD88MutationAnalysisTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.MYD88MutationAnalysis.MYD88MutationAnalysisWordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;
            //Changed by MS and TK;
            this.m_EpicDistributionIsImplemented = true;

            string taskDescription1 = "Cut H&E slide and give to pathologist to circle tumor for tech only. Give the paraffin block to Flow so they can send to NEO.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription1));

            string taskDescription2 = "Collect slide from pathologist and paraffin block from histology, or collect (Peripheral blood: 2-5 mL in EDTA tube ONLY; " +
            "Bone marrow: 2 mL in EDTA tube ONLY; Fresh unfixed tissue in RPMI) and send to Neogenomics.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription2, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81479(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
		}
	}
}
