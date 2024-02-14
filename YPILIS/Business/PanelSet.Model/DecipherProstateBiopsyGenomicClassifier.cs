using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.PanelSet.Model
{
    public class DecipherProstateBiopsyGenomicClassifier : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public DecipherProstateBiopsyGenomicClassifier()
        {
            this.m_PanelSetId = 444;
            this.m_PanelSetName = "Decipher Prostate Biopsy Genomic Classifier";
            this.m_CaseType = Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_IsBillable = false;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_NeverDistribute = true;
            this.m_AcceptOnFinal = true;
            this.m_HasNoOrderTarget = true;

            this.m_AddSurgicalAmendment = true;
            this.m_SurgicalAmendmentTemplate = $"Per request of Dr. [Provider] on {DateTime.Today.ToString("MM/dd/yyyy")}, slides were retrieved from archival storage and reviewed in order to select a block for Decipher Prostate Biopsy Genomic Classifier testing. Block [BlockLetter] was selected and forwarded to Veracyte Labs SD for testing.  Results will follow separately.";

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("VRCTLBSSD");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("VRCTLBSSD");

            YellowstonePathology.Business.Facility.Model.Facility facility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("VRCTLBSSD");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, "Gather materials and sendout to Vericyte Labs SD.", facility));

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88363", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
