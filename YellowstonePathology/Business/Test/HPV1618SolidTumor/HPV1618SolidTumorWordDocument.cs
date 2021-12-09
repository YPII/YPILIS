using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618SolidTumor
{
	public class HPV1618SolidTumorWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public HPV1618SolidTumorWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder = (YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HPV1618GenotypingSolidTumor.3.xml";
			base.OpenTemplate();

			base.SetDemographicsV2();

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetByAliquotOrderId(this.m_PanelSetOrder.OrderedOnId);
            string description = specimenOrder.Description + " - Block " + aliquotOrder.GetDescription();
            base.ReplaceText("specimen_description", description);            

			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

            base.ReplaceText("hpvdna_result", panelSetOrder.HPVDNAResult);
            if(panelSetOrder.HPV6Result == PanelSetOrder.NotPerformedResult) base.DeleteRow("hpv6_result");
            else base.ReplaceText("hpv6_result", panelSetOrder.HPV6Result);

            if (panelSetOrder.HPV16Result == PanelSetOrder.NotPerformedResult) base.DeleteRow("hpv16_result");
            else base.ReplaceText("hpv16_result", panelSetOrder.HPV16Result);

            if (panelSetOrder.HPV18Result == PanelSetOrder.NotPerformedResult) base.DeleteRow("hpv18_result");
            else base.ReplaceText("hpv18_result", panelSetOrder.HPV18Result);

            if (panelSetOrder.HPV31Result == PanelSetOrder.NotPerformedResult) base.DeleteRow("hpv31_result");
            else base.ReplaceText("hpv31_result", panelSetOrder.HPV31Result);

            if (panelSetOrder.HPV33Result == PanelSetOrder.NotPerformedResult) base.DeleteRow("hpv33_result");
            else base.ReplaceText("hpv33_result", panelSetOrder.HPV33Result);

            if (panelSetOrder.HPV45Result == PanelSetOrder.NotPerformedResult) base.DeleteRow("hpv45_result");
            else base.ReplaceText("hpv45_result", panelSetOrder.HPV45Result);

            if (panelSetOrder.HPV58Result == PanelSetOrder.NotPerformedResult) base.DeleteRow("hpv58_result");
            else base.ReplaceText("hpv58_result", panelSetOrder.HPV58Result);

            if (panelSetOrder.Indication == Business.Test.HPV1618SolidTumor.HPV1618SolidTumorIndication.SquamousCellCarcinomaHeadAndNeck)
            {
                base.ReplaceText("report_interpretation_header", "Interpretation");
                base.ReplaceText("report_interpretation", panelSetOrder.Interpretation);
				base.ReplaceText("pathologist_signature", panelSetOrder.Signature);

                if (panelSetOrder.FinalTime.HasValue == true)
                {
                    string esignedHeader = "*** E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm") + "***";
                    base.ReplaceText("esigned_header", esignedHeader);
                }
            }
            else
            {
                base.DeleteRow("report_interpretation_header");
                base.DeleteRow("report_interpretation");
                base.DeleteRow("pathologist_signature");
                base.DeleteRow("esigned_header");
            }

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                base.ReplaceText("report_comment", panelSetOrder.Comment);
            }
            else
            {
                base.DeleteRow("report_comment");
            }
            
            base.ReplaceText("report_method", panelSetOrder.Method);
            base.ReplaceText("report_references", panelSetOrder.ReportReferences);
			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
			this.SetXmlNodeData("pathologist_signature", this.m_PanelSetOrder.Signature);

			this.SetReportDistribution();
			this.SetCaseHistory();

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
