using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.Electrophoresis
{
    public class ElectrophoresisTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public ElectrophoresisTest()
        {
            this.m_PanelSetId = 377;
            this.m_PanelSetName = "Electrophoresis";
            this.m_Abbreviation = "Electrophoresis";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Surgical;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_AttemptOrderTargetLookup = true;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_NeverDistribute = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(6);

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            YellowstonePathology.Business.Facility.Model.Facility svh = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("STVNCNT");
            YellowstonePathology.Business.Facility.Model.Facility ypi = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_TechnicalComponentFacility = svh;
            this.m_TechnicalComponentBillingFacility = svh;

            this.m_ProfessionalComponentFacility = ypi;
            this.m_ProfessionalComponentBillingFacility = ypi;            
        }
    }
}
