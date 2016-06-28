using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.TestCancelled
{
	public class TestCancelledTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public TestCancelledTest()
		{
			this.m_PanelSetId = 66;
			this.m_PanelSetName = "Test Cancelled";
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_HasTechnicalComponent = false;                        
            this.m_HasProfessionalComponent = false;
            this.m_CaseType = YellowstonePathology.Business.CaseType.TestCancelled;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            this.m_IsBillable = false;
            this.m_EpicDistributionIsImplemented = true;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
