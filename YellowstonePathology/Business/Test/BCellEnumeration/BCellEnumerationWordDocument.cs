using System;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.BCellEnumeration
{

	public class BCellEnumerationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public BCellEnumerationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			BCellEnumerationTestOrder testOrder = (BCellEnumerationTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\BCellEnumeration.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.ReplaceText("wbc_count", testOrder.WBC.ToString());
			this.ReplaceText("lymphocyte_percentage", testOrder.LymphocytePercentage.ToString().StringAsPercent());			
			this.ReplaceText("cd19_bcell_positive_percent", testOrder.CD19BCellPositivePercent.ToString().StringAsPercent());			
			this.ReplaceText("cd20_bcell_positive_percent", testOrder.CD20BCellPositivePercent.ToString().StringAsPercent());
			this.ReplaceText("cd19_absolute_count", testOrder.CD19AbsoluteCount.ToString());
			this.ReplaceText("cd20_absolute_count", testOrder.CD20AbsoluteCount.ToString());

			this.ReplaceText("report_method", testOrder.Method);
			this.ReplaceText("report_references", testOrder.References);
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
