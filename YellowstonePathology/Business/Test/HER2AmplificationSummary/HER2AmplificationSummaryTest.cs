using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationSummaryTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public HER2AmplificationSummaryTest()
        {
            this.m_PanelSetId = 313;
            this.m_PanelSetName = "HER2 Amplification Summary";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
            this.m_Active = true;
            this.IsBillable = false;
            this.NeverDistribute = false;
            this.m_SurgicalAmendmentRequired = false;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryWordDocument).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(4, 0, 0, 0);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
