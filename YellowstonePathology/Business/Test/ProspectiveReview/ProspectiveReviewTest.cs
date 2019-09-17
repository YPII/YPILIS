using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ProspectiveReview
{
	public class ProspectiveReviewTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public ProspectiveReviewTest()
        {
			this.m_PanelSetId = 197;
			this.m_PanelSetName = "Prospective Review";
            this.m_Abbreviation = "PEERRVW";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Surgical;
			this.m_HasTechnicalComponent = false;            
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterQ();
            this.m_Active = true;
            this.m_ReportAsAdditionalTesting = false;

            this.m_AllowMultiplePerAccession = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.NothingToPublishReport).AssemblyQualifiedName;

            this.m_ExpectedDuration = TimeSpan.FromDays(3);
            this.m_NeverDistribute = true;
            this.m_IsBillable = false;

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
        }
	}
}
