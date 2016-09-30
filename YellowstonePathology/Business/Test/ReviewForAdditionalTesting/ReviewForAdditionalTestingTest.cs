using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReviewForAdditionalTesting
{
	public class ReviewForAdditionalTestingTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public ReviewForAdditionalTestingTest()
		{
			this.m_PanelSetId = 203;
			this.m_PanelSetName = "Review For Additional Testing";
			this.m_CaseType = YellowstonePathology.Business.CaseType.ALLCaseTypes;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;            
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;
            this.m_IsBillable = false;
            this.m_NeverDistribute = true;
            this.m_HasNoOrderTarget = false;
            this.m_ExpectedDuration = new TimeSpan(1, 0, 0, 0);
            this.m_IsClientAccessioned = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
		}
	}
}
