using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.API2MALT1ByPCR
{
    public class API2MALT1ByPCRWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public API2MALT1ByPCRWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            API2MALT1ByPCRTestOrder testOrder = (API2MALT1ByPCRTestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\API2MALT1ByPCR.1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            string result = testOrder.Result;
            if (string.IsNullOrEmpty(testOrder.ResultDescription) == false) result = testOrder.ResultDescription;
            this.ReplaceText("report_result", result);
            this.SetXMLNodeParagraphData("report_interpretation", testOrder.Interpretation);
            this.SetXMLNodeParagraphData("probe_set_detail", testOrder.ProbeSetDetail);
            this.ReplaceText("nuclei_scored", testOrder.NucleiScored);
            this.SetXMLNodeParagraphData("report_references", testOrder.ReportReferences);

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
