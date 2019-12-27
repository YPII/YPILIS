using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysisPreliminary
{
    public class EGFRMutationAnalysisPreliminaryTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public EGFRMutationAnalysisPreliminaryTest()
        {
            this.m_PanelSetId = 303;
            this.m_PanelSetName = "EGFR Mutation Analysis Preliminary";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Technical;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;
            this.m_IsBillable = false;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_NeverDistribute = true;
            this.m_AcceptOnFinal = true;
            this.m_HasNoOrderTarget = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
