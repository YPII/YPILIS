using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LynchSyndromeEvaluationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        private List<int> m_PanelSetIDList;

		public LynchSyndromeEvaluationTest()
		{
            this.m_PanelSetIDList = new List<int>();
            this.m_PanelSetIDList.Add(13); //Surgical
            this.m_PanelSetIDList.Add(102); //IHC
            this.m_PanelSetIDList.Add(18); //BRAF
            this.m_PanelSetIDList.Add(144); //MLH1

            this.m_PanelSetId = 106;
			this.m_PanelSetName = "Mismatch Repair (MMR)/Lynch Syndrome Evaluation";
			this.m_CaseType = Business.CaseType.ReflexTesting;
			this.m_HasTechnicalComponent = true;
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
			this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
			this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationWordDocument).AssemblyQualifiedName;
			
			this.m_ReflexTestingComment = string.Empty;
			this.m_EnforceOrderTarget = false;
			this.m_RequiresPathologistSignature = true;
			this.m_AcceptOnFinal = true;
			this.m_IsReflexPanel = true;
			this.m_AllowMultiplePerAccession = true;
            this.m_IsBillable = false;
            this.m_ExpectedDuration = TimeSpan.FromDays(12);

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            string taskDescription = "Perform IHC testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

			this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
			this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

			this.m_ProfessionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
			this.m_ProfessionalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());
		}

        public List<int> PanelSetIDList
        {
            get { return this.m_PanelSetIDList; }
        }
	}
}
