using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.PlasmaCellMyelomaPrognosticFISHPanel
{
    public class PlasmaCellMyelomaPrognosticFISHPanelTest : YellowstonePathology.Business.PanelSet.Model.FISHTest
    {
        public PlasmaCellMyelomaPrognosticFISHPanelTest()
        {
            this.m_PanelSetId = 277;
            this.m_PanelSetName = "Plasma Cell Myeloma Prognostic FISH Panel (FISH)";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_IsBillable = true;
            this.m_HasNoOrderTarget = false;
            this.m_ExpectedDuration = TimeSpan.FromDays(8);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88374", null), 7);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);

            string taskDescription = "Collect material and send to Neo.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, taskDescription, neogenomicsIrvine));

            this.m_ProbeSetCount = 6;
        }
    }
}
