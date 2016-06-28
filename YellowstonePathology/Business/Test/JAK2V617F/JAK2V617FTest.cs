using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public JAK2V617FTest()
        {
			this.m_PanelSetId = 1;
			this.m_PanelSetName = "JAK2 V617F";
            this.m_Abbreviation = "J2V617F";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;            
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;

            this.m_ExpectedDuration = new TimeSpan(3, 0, 0, 0);

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_HasSplitCPTCode = true;            

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81270(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

			YellowstonePathology.Business.Task.Model.TaskParaffinCurlPreparation paraffinCurlPreparation = new YellowstonePathology.Business.Task.Model.TaskParaffinCurlPreparation();
            this.m_TaskCollection.Add(paraffinCurlPreparation);            
            
            string task2Description = "Gather materials and get to work.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, task2Description));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceJAK2());
        }
	}
}
