using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.InvasiveBreastPanel
{
	public class InvasiveBreastPanelTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public InvasiveBreastPanelTest()
		{
			this.m_PanelSetId = 125;
			this.m_PanelSetName = "Invasive Breast Panel";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Surgical;
			this.m_HasTechnicalComponent = true;
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
			this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
			this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel).AssemblyQualifiedName;
			
			this.m_ReflexTestingComment = string.Empty;
			this.m_EnforceOrderTarget = false;
			this.m_RequiresPathologistSignature = true;
			this.m_AcceptOnFinal = true;
			this.m_IsReflexPanel = true;
			this.m_AllowMultiplePerAccession = true;
            this.m_IsBillable = false;
            this.m_NeverDistribute = true;
            this.m_ExpectedDuration = new TimeSpan(14, 0, 0, 0);

			YellowstonePathology.Business.Task.Model.TaskUnstainedSlideWithAfterSlidePreparation unstainedSlidePreparation = new YellowstonePathology.Business.Task.Model.TaskUnstainedSlideWithAfterSlidePreparation();
			YellowstonePathology.Business.Task.Model.TaskPerformInhouseMolecularTesting taskPerformInhouseMolecularTesting = new YellowstonePathology.Business.Task.Model.TaskPerformInhouseMolecularTesting(this.m_PanelSetName);

            string taskDescription = "Prepare 2 unstained slides with 1 H&E after slide for D-ISH and deliver to Molecular. Prepare ER and PR IHC and deliver to pathologist.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

            string taskDescription2 = "Receive material from Histology and perform D-ISH testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription2));

			this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
			this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

			this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
			this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());
		}
	}
}
