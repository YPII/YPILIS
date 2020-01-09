using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.ChimerismStudyByDNAMarkers
{
    public class ChimerismStudyByDNAMarkersTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public ChimerismStudyByDNAMarkersTest()
        {
            this.m_PanelSetId = 302;
            this.m_PanelSetName = "Chimerism Study By DNA Markers";
            this.m_Abbreviation = "Chimerism Study By DNA Markers";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_IsBillable = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(8);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;

this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            string taskDescription = "Gather materials and send to Children’s Hospital Colorado Laboratory.";

            YellowstonePathology.Business.Facility.Model.Facility childrensFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("CHHOSCO");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, childrensFacility));

            this.m_TechnicalComponentFacility = childrensFacility;
            this.m_ProfessionalComponentFacility = childrensFacility;

            this.m_TechnicalComponentBillingFacility = childrensFacility;
            this.m_ProfessionalComponentBillingFacility = childrensFacility;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81267", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
        }
}
}
