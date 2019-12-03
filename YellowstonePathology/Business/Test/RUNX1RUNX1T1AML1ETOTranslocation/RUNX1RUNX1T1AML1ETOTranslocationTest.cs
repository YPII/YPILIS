using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.RUNX1RUNX1T1AML1ETOTranslocation
{
    public class RUNX1RUNX1T1AML1ETOTranslocationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public RUNX1RUNX1T1AML1ETOTranslocationTest()
        {
            this.m_PanelSetId = 231;
            this.m_PanelSetName = "RUNX1-RUNX1T1 (AML1-ETO) Translocation, t(8;21) (FISH)";
            this.m_Abbreviation = "RUNX1-RUNX1T1 (AML1-ETO) Translocation, t(8;21)";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.RUNX1RUNX1T1AML1ETOTranslocation.RUNX1RUNX1T1AML1ETOTranslocationTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.RUNX1RUNX1T1AML1ETOTranslocation.RUNX1RUNX1T1AML1ETOTranslocationWordDocument).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);

            string task1Description = "Cut H&E slide and give to pathologist to circle tumor for tech only. Give the paraffin block to Molecular so they can send to NEO.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, task1Description));

            string task2Description = "Collect slide from pathologist and paraffin block from histology and send to Neogenomics.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, task2Description, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;

            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());

            Business.Panel.Model.HAndEPanel handePanel = new Panel.Model.HAndEPanel();
            this.m_PanelCollection.Add(handePanel);
        }
    }
}
