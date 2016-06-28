using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
    public class ROS1ByFISHWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public ROS1ByFISHWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ROS1ByFISH.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

            Document.AmendmentSection amendmentSection = new Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(ros1ByFISHTestOrder.OrderedOnId);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(ros1ByFISHTestOrder.OrderedOn, ros1ByFISHTestOrder.OrderedOnId);

			string specimenDescription = specimenOrder.Description;
			if(aliquotOrder != null) specimenDescription += ", Block " + aliquotOrder.Label;
			this.ReplaceText("specimen_description", specimenDescription);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXMLNodeParagraphData("report_result", ros1ByFISHTestOrder.Result);
            this.SetXMLNodeParagraphData("report_interpretation", ros1ByFISHTestOrder.Interpretation);
            this.SetXMLNodeParagraphData("report_references", ros1ByFISHTestOrder.References);
            this.SetXMLNodeParagraphData("reference_range", ros1ByFISHTestOrder.ReferenceRange);
            this.SetXMLNodeParagraphData("nuclei_scored", ros1ByFISHTestOrder.NucleiScored);
            this.ReplaceText("probeset_details", ros1ByFISHTestOrder.ProbeSetDetail);
            this.SetXMLNodeParagraphData("report_method", ros1ByFISHTestOrder.Method);
            this.SetXMLNodeParagraphData("tumor_nuclei_percentage", ros1ByFISHTestOrder.TumorNucleiPercentage);

            this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);            
            this.ReplaceText("report_disclaimer", ros1ByFISHTestOrder.ReportDisclaimer);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
