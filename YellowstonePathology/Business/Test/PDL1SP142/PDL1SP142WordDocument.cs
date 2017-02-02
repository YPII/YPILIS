using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PDL1SP142
{
    public class PDL1SP142WordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public PDL1SP142WordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {            
            PDL1SP142TestOrder testOrder = (PDL1SP142TestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\PDL1SP142.1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("stain_percent", testOrder.StainPercent);
            this.ReplaceText("report_comment", testOrder.Comment);
            this.ReplaceText("report_interpretation", testOrder.Interpretation);
            this.ReplaceText("report_method", testOrder.Method);
            this.ReplaceText("report_references", testOrder.ReportReferences);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);            

            if (this.m_PanelSetOrder.ReferenceLabFinalDate.HasValue == true)
            {
                this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
                this.ReplaceText("report_time", YellowstonePathology.Business.Helper.DateTimeExtensions.ShortTimeStringFromNullable(this.m_PanelSetOrder.ReferenceLabFinalDate));
                this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);
            }
            else
            {
                this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
                this.ReplaceText("report_time", YellowstonePathology.Business.Helper.DateTimeExtensions.ShortTimeStringFromNullable(this.m_PanelSetOrder.FinalDate));
                this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);
            }

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
