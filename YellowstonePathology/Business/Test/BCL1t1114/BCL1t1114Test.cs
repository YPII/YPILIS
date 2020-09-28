using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCL1t1114
{
	public class BCL1t1114Test : YellowstonePathology.Business.PanelSet.Model.FISHTest
	{
        public BCL1t1114Test()
		{
			this.m_PanelSetId = 100;
			this.m_PanelSetName = "BCL1/IgH t(11;14)";
			this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = false;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.BCL1t1114.BCL1t1114TestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.BCL1t1114.BCL1t1114WordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);

            string task1Description = "Cut H&E slide and give to pathologist to circle tumor for tech only. Give the paraffin block to Flow so they can send to NEO.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, task1Description));

            string task2Description = "Collect slide from pathologist and paraffin block from histology, or collect (Peripheral blood: 2-5 mL in sodium heparin tube, 2x5 mL in EDTA tube(PCR is EDTA ONLY); " +
            "Bone marrow: 1-2 mL in sodium heparin tube or 2 mL in EDTA tube(PCR is EDTA ONLY); Fresh unfixed tissue in RPMI) and send to Neogenomics.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, task2Description, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81401", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());

            Business.Panel.Model.HAndEPanel handePanel = new Panel.Model.HAndEPanel();
            this.m_PanelCollection.Add(handePanel);

            this.m_ProbeSetCount = 1;
        }
    }
}
