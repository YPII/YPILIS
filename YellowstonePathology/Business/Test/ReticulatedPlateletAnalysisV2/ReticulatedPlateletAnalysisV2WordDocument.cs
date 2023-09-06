using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.ReticulatedPlateletAnalysisV2
{
    class ReticulatedPlateletAnalysisV2WordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public ReticulatedPlateletAnalysisV2WordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
        }

        public override void Render()
        {
            this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\ReticulatedPlateletAnalysisV2.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            ReticulatedPlateletAnalysisV2TestOrder testOrder = (ReticulatedPlateletAnalysisV2TestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(testOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
            amendment.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.ReplaceText("date_time_collected", collectionDateTimeString);

            this.ReplaceText("test_result", testOrder.Result);
            this.ReplaceText("result_reference", testOrder.ResultReference);

            this.ReplaceText("report_method", testOrder.Method);
            this.ReplaceText("asr_comment", testOrder.ASRComment);

            this.SaveReport();
        }
    }
}
