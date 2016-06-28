using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PeerReview
{
	public class PeerReviewTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public PeerReviewTest()
        {
			this.m_PanelSetId = 197;
			this.m_PanelSetName = "Peer Review";
            this.m_Abbreviation = "PEERRVW";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Surgical;
			this.m_HasTechnicalComponent = false;            
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterQ();
            this.m_Active = true;
            
			this.m_AllowMultiplePerAccession = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder).AssemblyQualifiedName;
			
            this.m_ExpectedDuration = new TimeSpan(3, 0, 0, 0);
            this.m_NeverDistribute = true;
            this.m_IsBillable = false;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
        }
	}
}
