using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
	public class MPNExtendedReflexWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public MPNExtendedReflexWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexResult mpnExtendedReflexResult = new Test.MPNExtendedReflex.MPNExtendedReflexResult(this.m_AccessionOrder);
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\MPNExtendedReflex.3.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("panelset_name", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.PanelSetName);
            this.ReplaceText("jak2v617_result", mpnExtendedReflexResult.JAK2V617FResult.Result);
            this.ReplaceText("calr_result", mpnExtendedReflexResult.CALRResult.Result);
            this.ReplaceText("mpl_result", mpnExtendedReflexResult.MPLResult.Result);
            this.ReplaceText("report_comment", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Comment);

            base.ReplaceText("specimen_description", mpnExtendedReflexResult.SpecimenOrder.Description);
            this.ReplaceText("report_interpretation", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Interpretation);
            this.ReplaceText("report_method", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Method);
            this.ReplaceText("report_reference", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.ReportReferences);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(mpnExtendedReflexResult.SpecimenOrder.CollectionDate, mpnExtendedReflexResult.SpecimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Signature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
