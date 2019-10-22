using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.GastricAdenocarcinoma
{
    public class GastricAdenocarcinomaTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public GastricAdenocarcinomaTest()
        {
            this.m_PanelSetId = 364;
            this.m_PanelSetName = "Gastric Adenocarcinoma Panel";
            this.m_Abbreviation = "Gastric Adenocarcinoma";
            this.m_CaseType = YellowstonePathology.Business.CaseType.ALLCaseTypes;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterQ();
            this.m_Active = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(1);
            this.m_IsBillable = false;
            this.m_NeverDistribute = true;

            this.m_OrderInitialTestsOnly = true;

            this.m_AllowMultiplePerAccession = true;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.GastricAdenocarcinoma.GastricAdenocarcinomaTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.DoNotPublishReport).AssemblyQualifiedName;

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
