using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class ReportSummary : PanelSet
	{        
        protected string m_ReportTemplatePath;

        public ReportSummary()
		{
			this.m_PanelSetId = 78;
			this.m_PanelSetName = "Summary Report";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Summary;
            this.m_HasTechnicalComponent = false;            
            this.m_HasProfessionalComponent = false;            
            this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
			this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
			this.m_Active = true;			                        

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());
		}        

        public string ReportTemplatePath
        {
            get { return this.m_ReportTemplatePath; }
            set { this.m_ReportTemplatePath = value; }
        }
	}
}
