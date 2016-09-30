using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model.ReflexTesting
{
	public class JAK2ReflexAnalysis : PanelSet
	{
		public JAK2ReflexAnalysis()
		{
			this.m_PanelSetId = 114;
			this.m_PanelSetName = "JAK2 Reflex Analysis";
			this.m_CaseType = YellowstonePathology.Business.CaseType.ALLCaseTypes;
			this.m_HasTechnicalComponent = false;
			this.m_HasProfessionalComponent = false;
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
            this.m_ExpectedDuration = new TimeSpan(14, 0, 0, 0);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());
		}
	}
}
