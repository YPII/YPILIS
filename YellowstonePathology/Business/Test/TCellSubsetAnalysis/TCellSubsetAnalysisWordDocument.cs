/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/5/2016
 * Time: 10:42 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.TCellSubsetAnalysis
{
	/// <summary>
	/// Description of TCellSubsetAnalysisWordDocument.
	/// </summary>
	public class TCellSubsetAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public TCellSubsetAnalysisWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			TCellSubsetAnalysisTestOrder testOrder = (TCellSubsetAnalysisTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\TCellSubsetAnalysis.2.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.ReplaceText("report_cd3_percent", testOrder.CD3Percent.ToString().StringAsPercent());
			this.ReplaceText("report_cd4_percent", testOrder.CD4Percent.ToString().StringAsPercent());
			this.ReplaceText("report_cd8_percent", testOrder.CD8Percent.ToString().StringAsPercent());
            string value = string.Empty;
			if(testOrder.CD4CD8Ratio.HasValue) value = Math.Round(testOrder.CD4CD8Ratio.Value, 2).ToString();
			this.ReplaceText("report_cd4cd8_ratio", value);
            this.ReplaceText("report_reference_range", testOrder.ReferenceRange);
            this.ReplaceText("report_interpretation", testOrder.Interpretation);
            this.ReplaceText("report_method", testOrder.Method);
			this.ReplaceText("report_references", testOrder.References);
            this.ReplaceText("report_disclosure", testOrder.Disclosure);
            this.ReplaceText("asr_comment", testOrder.ASRComment);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			base.ReplaceText("specimen_description", specimenOrder.Description);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

			this.SaveReport();
		}
        
		public override void Publish()
		{
			base.Publish();
		}
	}
}
