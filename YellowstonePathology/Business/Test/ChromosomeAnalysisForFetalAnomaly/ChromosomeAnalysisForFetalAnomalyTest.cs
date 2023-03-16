using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly
{
	public class ChromosomeAnalysisForFetalAnomalyTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public ChromosomeAnalysisForFetalAnomalyTest()
		{
			this.m_PanelSetId = 169;
            this.m_PanelSetName = "Chromosome Analysis For Fetal Anomaly";
			this.m_CaseType = Business.CaseType.Cytogenetics;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(17);
            this.m_IsBillable = false;
            this.m_NeverDistribute = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyWordDocument).AssemblyQualifiedName;

            this.m_SurgicalAmendmentRequired = true;

            this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            YellowstonePathology.Business.Facility.Model.Facility facility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("STVNCNT");
            string taskDescription = "Collect fresh tissue in RPMI and send out to SCL Health.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription, facility));

            this.m_TechnicalComponentFacility = facility;
            this.m_TechnicalComponentBillingFacility = facility;

            this.m_ProfessionalComponentFacility = facility;
            this.m_ProfessionalComponentBillingFacility = facility;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", null), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode2 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88233", null), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode3 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88261", null), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode4 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88267", null), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode5 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88280", null), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode6 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88291", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode2);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode3);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode4);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode5);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode6);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
