using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FactorVLeiden
{
	public class FactorVLeidenTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public FactorVLeidenTest()
		{
			this.m_PanelSetId = 32;
			this.m_PanelSetName = "Factor V Leiden";
            this.m_Abbreviation = "FV";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;            
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.FactorVLeiden.FactorVLeidenTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.FactorVLeiden.FactorVLeidenWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;

            string taskDescription = "Gather materials and send to St. V's Healthcare for testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.StVincentHealthcare();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.StVincentHealthcare();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_HasSplitCPTCode = true;

            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81241 cpt81241 = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81241();
            cpt81241.Modifier = "26";
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(cpt81241, 1);            
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
        }
	}
}
