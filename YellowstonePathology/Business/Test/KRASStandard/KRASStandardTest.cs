using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public KRASStandardTest()
		{
			this.m_PanelSetId = 27;
			this.m_PanelSetName = "KRAS Standard Mutation Analysis";
            this.m_Abbreviation = "KRAS";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;
            
			this.m_SurgicalAmendmentRequired = true;
			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.KRASStandard.KRASStandardWordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(120, 0, 0);
            this.m_EpicDistributionIsImplemented = true;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_HasSplitCPTCode = true;

            string taskDescription = "Give block to Molecular for sendout.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

            string task2Description = "Receive block from Histology and send to Neo for testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, task2Description, new Facility.Model.NeogenomicsIrvine()));

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81275(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceKRAS());

            KRASStandardPanel krasStandardPanel = new KRASStandardPanel();
            this.m_PanelCollection.Add(krasStandardPanel);
        }
    }
}
