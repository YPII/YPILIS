using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.NeoTYPEOtherSolidTumorProfile
{
    public class NeoTYPEOtherSolidTumorProfileTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public NeoTYPEOtherSolidTumorProfileTest()
        {
            this.m_PanelSetId = 266;
            this.m_PanelSetName = "NeoTYPE Other Solid Tumor Profile  (FISH-Molecular)";
            this.m_Abbreviation = "NTOtherSolidTumor";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_AttemptOrderTargetLookup = true;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(12);

            string taskDescription = "Gather materials and send out to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = neogenomicsIrvine;
            this.m_ProfessionalComponentBillingFacility = neogenomicsIrvine;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81479", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode2 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88377", null), 2);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode2);

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode3 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88360", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode3);

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode4 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode4);

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode5 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81479", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode5);
        }
    }
}
