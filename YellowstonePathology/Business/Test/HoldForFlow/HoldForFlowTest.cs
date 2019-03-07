using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HoldForFlow
{
	public class HoldForFlowTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public HoldForFlowTest()
		{
			this.m_PanelSetId = 211;
			this.m_PanelSetName = "Hold For Flow";
            this.m_Abbreviation = "Hold For Flow";
			this.m_CaseType = YellowstonePathology.Business.CaseType.FlowCytometry;
			this.m_HasTechnicalComponent = false;			
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;
            this.m_NeverDistribute = true;
            this.m_ExpectedDuration = new TimeSpan(2, 0, 0, 0);
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.HoldForFlow.HoldForFlowTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.HoldForFlow.HoldForFlowWordDocument).AssemblyQualifiedName;
            this.m_RequiresPathologistSignature = false;
            this.m_AcceptOnFinal = false;
			this.m_AllowMultiplePerAccession = true;                                   
            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
            this.m_ReportAsAdditionalTesting = false;
            this.m_IsBillable = false;
        }
	}
}
