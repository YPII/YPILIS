using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetHer2AmplificationByFishRetired2 : PanelSet
	{
		public PanelSetHer2AmplificationByFishRetired2()
		{
			this.m_PanelSetId = 12;
			this.m_PanelSetName = "HER2 Amplification by FISH - Retired(2)";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = false;
            
			this.m_SurgicalAmendmentRequired = true;
			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.Retired.PanelSetOrderHer2AmplificationByFishRetired2).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;           			
            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
