using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReviewForAdditionalTesting
{
	public class ReviewForAdditionalTestingWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public ReviewForAdditionalTestingWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			ReviewForAdditionalTestingTestOrder reviewForAdditionalTestingTestOrder = (YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ReviewForAdditionalTesting.1.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.ReplaceText("report_comment", reviewForAdditionalTestingTestOrder.Comment);

			this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
