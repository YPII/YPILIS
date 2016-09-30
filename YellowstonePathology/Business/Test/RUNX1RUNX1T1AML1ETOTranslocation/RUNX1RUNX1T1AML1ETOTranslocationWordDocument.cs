using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.RUNX1RUNX1T1AML1ETOTranslocation
{
    public class RUNX1RUNX1T1AML1ETOTranslocationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public RUNX1RUNX1T1AML1ETOTranslocationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\RUNX1RUNX1T1AML1ETOTranslocation.1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            Document.AmendmentSection amendmentSection = new Document.AmendmentSection();
            amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Test.RUNX1RUNX1T1AML1ETOTranslocation.RUNX1RUNX1T1AML1ETOTranslocationTestOrder testOrder = (YellowstonePathology.Business.Test.RUNX1RUNX1T1AML1ETOTranslocation.RUNX1RUNX1T1AML1ETOTranslocationTestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(testOrder.OrderedOnId);
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);

            string specimenDescription = specimenOrder.Description;
            if (aliquotOrder != null) specimenDescription += ", Block " + aliquotOrder.Label;
            this.ReplaceText("specimen_description", specimenDescription);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXMLNodeParagraphData("report_result", testOrder.Result);
            this.SetXMLNodeParagraphData("report_comment", testOrder.Comment);
            this.SetXMLNodeParagraphData("report_interpretation", testOrder.Interpretation);
            this.SetXMLNodeParagraphData("report_method", testOrder.Method);
            this.SetXMLNodeParagraphData("report_references", testOrder.ReportReferences);

            this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);
            this.ReplaceText("report_disclaimer", testOrder.ASR);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
