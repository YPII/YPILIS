using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetHer2AmplificationByFishRetired1 : PanelSet
	{
		public PanelSetHer2AmplificationByFishRetired1()
		{
			this.m_PanelSetId = 11;
			this.m_PanelSetName = "HER2 Amplification by FISH - Retired(1)";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.RetiredTestDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = false;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.DoNotPublishReport).AssemblyQualifiedName;

            this.m_SurgicalAmendmentRequired = true;		            
			this.m_AllowMultiplePerAccession = true;            			
            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
