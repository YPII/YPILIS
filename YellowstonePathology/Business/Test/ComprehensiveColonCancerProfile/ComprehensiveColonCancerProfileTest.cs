using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile
{
	public class ComprehensiveColonCancerProfileTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public ComprehensiveColonCancerProfileTest()
		{
			this.m_PanelSetId = 112;
            this.m_PanelSetName = "Comprehensive Colon Cancer Profile";
			this.m_CaseType = YellowstonePathology.Business.CaseType.ALLCaseTypes;
			this.m_HasTechnicalComponent = false;
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
			this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
			this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfile).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileWordDocument).AssemblyQualifiedName;
			
			this.m_ReflexTestingComment = string.Empty;
			this.m_EnforceOrderTarget = false;
			this.m_RequiresPathologistSignature = true;
			this.m_AcceptOnFinal = true;
			this.m_IsReflexPanel = true;
			this.m_AllowMultiplePerAccession = true;
            this.m_IsBillable = false;
            this.m_NeverDistribute = true;
            this.m_EpicDistributionIsImplemented = true;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());
		}
	}
}
