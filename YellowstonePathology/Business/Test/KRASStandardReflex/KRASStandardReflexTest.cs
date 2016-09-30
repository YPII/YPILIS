using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandardReflex
{
	public class KRASStandardReflexTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public KRASStandardReflexTest()
		{
			this.m_PanelSetId = 30;
			this.m_PanelSetName = "KRAS Standard Reflex";
            this.m_Abbreviation = "KRASSR";
			this.m_CaseType = YellowstonePathology.Business.CaseType.ReflexTesting;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;			
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
            this.m_Active = true;
            
			this.m_SurgicalAmendmentRequired = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
			this.m_AcceptOnFinal = true;
            this.m_ExpectedDuration = new TimeSpan(120, 0, 0);
            this.m_IsBillable = false;
            this.m_EpicDistributionIsImplemented = true;

            string taskDescription = "Give block to Molecular for sendout.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

            string task2Description = "Receive block from Histology and send to Neo for testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, task2Description, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceKRASBRAF());
		}
	}
}
