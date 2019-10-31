using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest
{
    public class AuthorizationForVerbalTestRequestTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public AuthorizationForVerbalTestRequestTest()
        {
            this.m_PanelSetId = 363;
            this.m_PanelSetName = "Authorization For Verbal Test Request";
            this.m_Abbreviation = "AUTHVBLREQ";
            this.m_CaseType = YellowstonePathology.Business.CaseType.ALLCaseTypes;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterI();
            this.m_Active = true;
            this.m_ReportAsAdditionalTesting = false;
            this.m_ExpectedDuration = TimeSpan.FromDays(1);
            this.m_NeverDistribute = true;
            this.m_IsBillable = false;
            this.m_HasNoOrderTarget = true;
            this.m_AllowMultiplePerAccession = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest.AuthorizationForVerbalTestRequestTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
        }
    }
}
