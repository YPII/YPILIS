using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCellClonalityByPCR
{
	public class BCellClonalityByPCRWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public BCellClonalityByPCRWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{            
			BCellClonalityByPCRTestOrder testOrder = (BCellClonalityByPCRTestOrder)this.m_PanelSetOrder;
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\BCellClonality.8.xml";

			this.OpenTemplate();
			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			if (this.m_PanelSetOrder.Final)
			{				
				this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);
			}

			this.SetXmlNodeData("report_result", testOrder.Result);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
			this.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			if (string.IsNullOrEmpty(testOrder.Comment) == true)
			{
				this.DeleteRow("result_comment");
			}
			else
			{
				this.ReplaceText("result_comment", testOrder.Comment);
			}
			this.ReplaceText("report_interpretation", testOrder.Interpretation);
			this.ReplaceText("tumor_nuclei_percent", testOrder.TumorNucleiPercent);
			this.ReplaceText("report_method", testOrder.Method);
			this.ReplaceText("report_references", testOrder.ReportReferences);
			this.ReplaceText("asr_comment", testOrder.ASRComment);

			YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
			amendment.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.SaveReport();
		}

        public override void Publish()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsPDF(orderIdParser);
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_PanelSetOrder.ReportNo);
        }		
	}
}
