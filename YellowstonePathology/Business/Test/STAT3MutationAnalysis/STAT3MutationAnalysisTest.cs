using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.STAT3MutationAnalysis
{
    public class STAT3MutationAnalysisTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public STAT3MutationAnalysisTest()
        {
            this.m_PanelSetId = 261;
            this.m_PanelSetName = "STAT3 Mutation Analysis (Molecular)";
            this.m_Abbreviation = "STAT3 Mutation Analysis";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;

            this.m_AllowMultiplePerAccession = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            string taskDescription = "Collect materials and send to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81405", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);
        }
    }
}
