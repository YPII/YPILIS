using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfileTest
{
	public class NeoARRAYSNPCytogeneticProfileTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public NeoARRAYSNPCytogeneticProfileTest()
		{
            this.m_PanelSetId = 174;
            this.m_PanelSetName = "NeoARRAY SNP/Cytogenetic Profile";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Cytogenetics;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            
            this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile.NeoARRAYSNPCytogeneticProfileTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile.NeoARRAYSNPCytogeneticProfileWordDocument).AssemblyQualifiedName;
			//Changed by MS and TK;

            string taskDescription2 = "Collect (Peripheral blood: 2-5 mL in EDTA tube ONLY; " +
            "Bone marrow: 2 mL in EDTA tube ONLY; Fresh unfixed tissue in RPMI) and send to Neogenomics.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription2, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
		}
	}
}
