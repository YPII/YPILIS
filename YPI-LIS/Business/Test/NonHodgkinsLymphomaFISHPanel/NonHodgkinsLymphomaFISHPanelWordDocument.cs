using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel
{
	public class NonHodgkinsLymphomaFISHPanelWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public NonHodgkinsLymphomaFISHPanelWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{
			this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\NonHodgkinsLymphomaFISHPanel.3.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

            this.ReplaceText("report_title", this.m_PanelSetOrder.PanelSetName);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelTestOrder nonHodgkinsLymphomaFISHPanelTestOrder = (YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelTestOrder)this.m_PanelSetOrder;

			YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(nonHodgkinsLymphomaFISHPanelTestOrder.OrderedOnId);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(nonHodgkinsLymphomaFISHPanelTestOrder.OrderedOn, nonHodgkinsLymphomaFISHPanelTestOrder.OrderedOnId);

			string specimenDescription = specimenOrder.Description;
			if (aliquotOrder != null) specimenDescription += ", Block " + aliquotOrder.Label;
			this.ReplaceText("specimen_description", specimenDescription);

			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.SetXMLNodeParagraphData("report_result", nonHodgkinsLymphomaFISHPanelTestOrder.Result);
			this.SetXMLNodeParagraphData("report_interpretation", nonHodgkinsLymphomaFISHPanelTestOrder.Interpretation);
			this.SetXMLNodeParagraphData("nuclei_scored", nonHodgkinsLymphomaFISHPanelTestOrder.NucleiScored);
			this.SetXMLNodeParagraphData("probeset_details", nonHodgkinsLymphomaFISHPanelTestOrder.ProbeSetDetail);
			this.SetXMLNodeParagraphData("report_reference", nonHodgkinsLymphomaFISHPanelTestOrder.ReportReferences);
			this.ReplaceText("report_disclaimer", nonHodgkinsLymphomaFISHPanelTestOrder.ReportDisclaimer);

			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
