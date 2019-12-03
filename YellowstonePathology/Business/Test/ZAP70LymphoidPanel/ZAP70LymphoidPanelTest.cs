using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ZAP70LymphoidPanel
{
	public class ZAP70LymphoidPanelTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public ZAP70LymphoidPanelTest()
		{
			this.m_PanelSetId = 143;
            this.m_PanelSetName = "ZAP-70 Lymphoid Panel (Flow Cytometry)";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FlowCytometry;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(1);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ZAP70LymphoidPanel.ZAP70LymphoidPanelTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ZAP70LymphoidPanel.ZAP70LymphoidPanelWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);

            string taskDescription = "Collect (Peripheral blood: 2-5 mL in sodium heparin tube, 2x5 mL in EDTA tube; " +
            "Bone marrow: 1-2 mL in sodium heparin tube or 2 mL in EDTA tube; Fresh unfixed tissue in RPMI) and send to Neogenomics.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
		}
	}
}
