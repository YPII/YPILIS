﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public HER2AmplificationByISHTest()
		{
			this.m_PanelSetId = 46;
			this.m_PanelSetName = "HER2 Amplification by D-ISH";
			this.m_CaseType = Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;
            
			this.m_SurgicalAmendmentRequired = true;
			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHWordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(4);

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            
            this.m_HasSplitCPTCode = false;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88377", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            string task1Description = "Cut 2 unstained slides and an after H&E. Give to molecular.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, task1Description));

            string task2Description = "Recieve materials from histology and run test.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, task2Description));

            string task3Description = "Please check to make sure the Fixation is entered correctly for this case.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, task3Description));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceHERAMP());

            HER2AmplificationByISHPanel her2AmplificationByISHPanel = new HER2AmplificationByISHPanel();
            this.m_PanelCollection.Add(her2AmplificationByISHPanel);
        }
    }
}
