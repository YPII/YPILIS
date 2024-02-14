using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public BRAFMutationAnalysisWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder panelSetOrder = (YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)this.m_PanelSetOrder;
            base.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\BRAFMutationAnalysis.2.xml";
            base.OpenTemplate();

            this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
            this.ReplaceText("report_time", YellowstonePathology.Business.BaseData.GetShortTimeString(this.m_PanelSetOrder.ReferenceLabFinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            string brafResult = panelSetOrder.Result;

            this.SetXmlNodeData("report_result", brafResult);
            this.SetXmlNodeData("final_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));

            this.ReplaceText("report_title", this.m_PanelSetOrder.PanelSetName);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            if (amendmentCollection.Count == 0)
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

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
