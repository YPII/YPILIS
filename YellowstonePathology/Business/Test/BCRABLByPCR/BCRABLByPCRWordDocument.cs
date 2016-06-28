using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCRABLByPCR
{
	public class BCRABLByPCRWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public BCRABLByPCRWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			BCRABLByPCRTestOrder testOrder = (BCRABLByPCRTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\BCRABLByPCR.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);
			
			if (string.IsNullOrEmpty(testOrder.DetectedLogReduction) == false)
			{
                this.ReplaceText("log_reduction", testOrder.DetectedLogReduction);
            }
            else
            {
                this.DeleteRow("log_reduction");
            }

			this.ReplaceText("report_result", testOrder.Result);
			this.ReplaceText("fusion_transcript_type", testOrder.FusionTranscriptType);
            this.ReplaceText("percent_bcrabl", testOrder.PercentBCRABL);
            this.ReplaceText("report_interpretation", testOrder.Interpretation);
			this.ReplaceText("report_method", testOrder.Method);
			this.ReplaceText("report_references", testOrder.References);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			base.ReplaceText("specimen_description", specimenOrder.Description);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
