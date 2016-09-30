using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public ErPrSemiQuantitativeTest()
		{
			this.m_PanelSetId = 50;
			this.m_PanelSetName = "Estrogen/Progesterone Receptor, Semi-Quantitative";
			this.m_CaseType = YellowstonePathology.Business.CaseType.IHC;
            this.m_HasTechnicalComponent = true;
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeWordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = false;
            this.m_EpicDistributionIsImplemented = true;

            string taskDescription = "Gather materials and perform testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88360(), 2);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceYPI());

            ERPRSemiQuantitativePanel erprSemiQuantitativePanel = new ERPRSemiQuantitativePanel();
            this.m_PanelCollection.Add(erprSemiQuantitativePanel);
        }
    }
}
