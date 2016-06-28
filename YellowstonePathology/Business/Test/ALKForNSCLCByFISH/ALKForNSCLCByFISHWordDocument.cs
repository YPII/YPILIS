using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
	public class ALKForNSCLCByFISHWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public ALKForNSCLCByFISHWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{						
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ALKForNSCLCByFISH.2.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

            Document.AmendmentSection amendmentSection = new Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkForNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)this.m_PanelSetOrder;

			YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(alkForNSCLCByFISHTestOrder.OrderedOnId);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(alkForNSCLCByFISHTestOrder.OrderedOn, alkForNSCLCByFISHTestOrder.OrderedOnId);            

            string specimenDescription = specimenOrder.Description;
			if(aliquotOrder != null) specimenDescription += ", Block " + aliquotOrder.Label;
			this.ReplaceText("specimen_description", specimenDescription);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.SetXMLNodeParagraphData("report_result", alkForNSCLCByFISHTestOrder.Result);
			this.SetXMLNodeParagraphData("report_interpretation", alkForNSCLCByFISHTestOrder.Interpretation);
			this.SetXMLNodeParagraphData("report_references", alkForNSCLCByFISHTestOrder.References);
			this.SetXMLNodeParagraphData("reference_range", alkForNSCLCByFISHTestOrder.ReferenceRange);
			this.SetXMLNodeParagraphData("nuclei_scored", alkForNSCLCByFISHTestOrder.NucleiScored);
            this.SetXMLNodeParagraphData("tumor_nuclei_percentage", alkForNSCLCByFISHTestOrder.TumorNucleiPercentage);
			this.SetXMLNodeParagraphData("probeset_details", alkForNSCLCByFISHTestOrder.ProbeSetDetail);
			this.SetXMLNodeParagraphData("report_method", alkForNSCLCByFISHTestOrder.Method);

            this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);

            string locationPerformed = this.m_PanelSetOrder.GetLocationPerformedComment();
            this.ReplaceText("report_disclaimer", locationPerformed);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
