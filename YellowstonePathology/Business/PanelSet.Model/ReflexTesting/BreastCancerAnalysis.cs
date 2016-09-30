using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model.ReflexTesting
{
	public class BreastCancerAnalysis : PanelSet
	{
		public BreastCancerAnalysis()
		{
			this.m_PanelSetId = 111;
			this.m_PanelSetName = "Breast Cancer Analysis";
			this.m_CaseType = YellowstonePathology.Business.CaseType.ALLCaseTypes;
			this.m_HasTechnicalComponent = true;
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.YPIDatabase;
			this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
			this.m_Active = true;			
			
			
			this.m_ReflexTestingComment = string.Empty;
			this.m_EnforceOrderTarget = false;
			this.m_RequiresPathologistSignature = true;
			this.m_AcceptOnFinal = true;
			this.m_IsReflexPanel = true;
			this.m_AllowMultiplePerAccession = true;
            this.m_IsBillable = false;
            this.m_NeverDistribute = true;            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());
		}
	}
}
