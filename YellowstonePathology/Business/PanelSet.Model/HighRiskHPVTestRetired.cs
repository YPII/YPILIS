using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class HighRiskHPVTestRetired: YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public HighRiskHPVTestRetired()
		{
			this.m_PanelSetId = 10;
			this.m_PanelSetName = "High Risk HPV - Retired";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.RetiredTestDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = false;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.DoNotPublishReport).AssemblyQualifiedName;


            this.m_AllowMultiplePerAccession = true;

            string taskDescription = "This is not to be ordered.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskInternalDelivery(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription));

			this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
			this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            
            this.m_HasSplitCPTCode = true;

            Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81220(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceCFYPI());
		}
	}
}
