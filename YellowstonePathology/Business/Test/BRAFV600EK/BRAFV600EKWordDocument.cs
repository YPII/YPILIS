using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	public class BRAFV600EKWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public BRAFV600EKWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{            
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrder = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_PanelSetOrder;
			base.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\BRAFV600EK.2.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			string brafResult = panelSetOrder.Result;

			this.SetXmlNodeData("report_result", brafResult);
			this.SetXmlNodeData("final_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			if (m_PanelSetOrder.AmendmentCollection.Count == 0)
			{
				this.SetXmlNodeData("test_result_header", "Test Result");
			}
			else // If an amendment exists show as corrected
			{
				this.SetXmlNodeData("test_result_header", "Corrected Test Result");
			}

			if (string.IsNullOrEmpty(panelSetOrder.Comment) == false) this.ReplaceText("result_comment", panelSetOrder.Comment);
			else this.DeleteRow("result_comment");
			this.ReplaceText("report_interpretation", panelSetOrder.Interpretation);
			this.ReplaceText("report_indication_comment", panelSetOrder.IndicationComment);
			this.ReplaceText("tumor_nuclei_percent", panelSetOrder.TumorNucleiPercentage);
			this.ReplaceText("report_method", panelSetOrder.Method);
			this.ReplaceText("report_reference", panelSetOrder.ReportReferences);
            this.ReplaceText("report_disclaimer", panelSetOrder.ReportDisclaimer);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			this.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));

			this.SetXmlNodeData("pathologist_signature", m_PanelSetOrder.Signature);

			this.SaveReport();
		}		

		public override void Publish()
        {
            base.Publish();
        }
	}
}
