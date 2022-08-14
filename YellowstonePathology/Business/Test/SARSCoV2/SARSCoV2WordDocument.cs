using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.SARSCoV2
{
	public class SARSCoV2WordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public SARSCoV2WordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			YellowstonePathology.Business.Test.SARSCoV2.SARSCoV2TestOrder panelSetOrder = (YellowstonePathology.Business.Test.SARSCoV2.SARSCoV2TestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\SARSCoV2.3.xml";
			base.OpenTemplate();

			base.SetDemographicsV2();

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetByAliquotOrderId(this.m_PanelSetOrder.OrderedOnId);
			string description = specimenOrder.Description;
            base.ReplaceText("specimen_description", description);            

			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

			string comment = "Questions about your test result or clinical condition should be directed to your personal physician or healthcare provider.  For local quarantine and return-to-work guidelines, refer to https://covid.riverstonehealth.org/.";
			if (string.IsNullOrEmpty(panelSetOrder.Comment) == false) comment = panelSetOrder.Comment;
			this.SetXmlNodeData("report_comment", comment);


			string result = null;
			if (panelSetOrder.Result == "DETECTED")
			{
				result = "POSITIVE (DETECTED)";
				base.ReplaceText("test_result_p", result);
				base.ReplaceText("test_result_n", string.Empty);
			}

			if (panelSetOrder.Result == "NOT DETECTED")
			{
				result = "Negative (Not Detected)";
				base.ReplaceText("test_result_n", result);
				base.ReplaceText("test_result_p", string.Empty);
			}

			if (panelSetOrder.Result == "Invalid")
			{
				result = "Invalid";
				base.ReplaceText("test_result_n", result);
				base.ReplaceText("test_result_p", string.Empty);
			}

			base.ReplaceText("report_method", panelSetOrder.Method);
			base.ReplaceText("report_result", panelSetOrder.Result);                                    
            base.ReplaceText("report_references", panelSetOrder.ReportReferences);
			base.ReplaceText("asr_comment", panelSetOrder.ASRComment);
			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));			

			this.SetReportDistribution();
			this.SetCaseHistory();

			this.m_ReportXml.Save(this.m_SaveFileName);
		}

		public override void Publish()
		{
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string filePath = Business.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo;
			Business.Helper.FileConversionHelper.publishCaseDocs(filePath, this.m_PanelSetOrder.ReportNo);			
		}
	}
}
