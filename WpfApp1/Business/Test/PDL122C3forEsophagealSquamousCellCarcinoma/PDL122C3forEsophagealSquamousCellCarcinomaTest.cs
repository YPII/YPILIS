using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.PDL122C3forEsophagealSquamousCellCarcinoma
{
    public class PDL122C3forEsophagealSquamousCellCarcinomaTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public PDL122C3forEsophagealSquamousCellCarcinomaTest()
        {
            this.m_PanelSetId = 365;
            this.m_PanelSetName = "PD-L1 (22C3) for Esophageal Squamous Cell Carcinoma";
            this.m_Abbreviation = "PD-L1 (22C3)EsphglSquamCellCarcinoma";
            this.m_CaseType = Business.CaseType.IHC;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(2);

            this.m_SurgicalAmendmentRequired = true;
            this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PDL122C3forEsophagealSquamousCellCarcinoma.PDL122C3forEsophagealSquamousCellCarcinomaTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.PDL122C3forEsophagealSquamousCellCarcinoma.PDL122C3EsophSquamCellCarcinomaWordDocument).AssemblyQualifiedName;

            string taskDescription = "Collect paraffin block from Histology and send to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, neogenomicsIrvine));

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88360", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_ProfessionalComponentFacility = neogenomicsIrvine;
            this.m_ProfessionalComponentBillingFacility = neogenomicsIrvine;

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = neogenomicsIrvine;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
        }
    }
}
