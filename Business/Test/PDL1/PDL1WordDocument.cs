using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PDL1
{
    public class PDL1WordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
        {
            this.m_ReportNo = reportNo;
            this.m_ReportSaveEnum = reportSaveEnum;
            this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            PDL1TestOrder testOrder = (PDL1TestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\PDL1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("report_result", testOrder.Result);
            this.ReplaceText("report_interpretation", testOrder.Interpretation);
            this.ReplaceText("report_method", testOrder.Method);
            this.ReplaceText("report_references", testOrder.References);
            //this.ReplaceText("test_developed_comment", testOrder.TestDevelopment);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("report_time", YellowstonePathology.Business.Helper.DateTimeExtensions.ShortTimeStringFromNullable(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
