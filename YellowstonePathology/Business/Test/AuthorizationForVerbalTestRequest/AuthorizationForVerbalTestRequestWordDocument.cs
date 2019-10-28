using System;
using YellowstonePathology.Business.Document;

namespace YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest
{
    public class AuthorizationForVerbalTestRequestWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        private PanelSetOrder m_PanelSetOrderToAuthorize;

        public AuthorizationForVerbalTestRequestWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode, PanelSetOrder panelSetOrderToAuthorize) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
            this.m_PanelSetOrderToAuthorize = panelSetOrderToAuthorize;
        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\AuthorizationForVerbalTestRequest.xml";
            base.OpenTemplate();

            AuthorizationForVerbalTestRequestTestOrder testOrder = (AuthorizationForVerbalTestRequestTestOrder)this.m_PanelSetOrder;

            string request = "Please perform " + this.m_PanelSetOrderToAuthorize.PanelSetName + " testing on the specimen for the patient described above.";
            base.ReplaceText("pso_request", request);
            base.ReplaceText("order_date", this.m_PanelSetOrderToAuthorize.OrderDate.Value.ToShortDateString());
            base.ReplaceText("pat_name", this.m_AccessionOrder.PatientDisplayName);
            base.ReplaceText("pso_report_no", this.m_PanelSetOrderToAuthorize.ReportNo);
            if (string.IsNullOrEmpty(this.m_AccessionOrder.PAddress1) == false)
            {
                string address = this.m_AccessionOrder.PAddress1;
                if (string.IsNullOrEmpty(this.m_AccessionOrder.PAddress2) == false) address += ", " + this.m_AccessionOrder.PAddress2;
                base.ReplaceText("pat_address", address);
            }
            else
            {
                base.ReplaceText("pat_address", "_____________________________");
            }

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PCity) == false) base.ReplaceText("pat_city", this.m_AccessionOrder.PCity);
            else base.ReplaceText("pat_city", "______________");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PState) == false) base.ReplaceText("pat_state", this.m_AccessionOrder.PState);
            else base.ReplaceText("pat_state", "______");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PZipCode) == false) base.ReplaceText("pat_zip", this.m_AccessionOrder.PZipCode);
            else base.ReplaceText("pat_zip", "________");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PSSN) == false) base.ReplaceText("pat_ssn", this.m_AccessionOrder.PSSN);
            else base.ReplaceText("pat_ssn", "________");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PSex) == false) base.ReplaceText("p_g", this.m_AccessionOrder.PSex);
            else base.ReplaceText("p_g", "(M) (F)");

            if (this.m_AccessionOrder.PBirthdate.HasValue == true) base.ReplaceText("pat_dob", this.m_AccessionOrder.PBirthdate.Value.ToShortDateString());
            else base.ReplaceText("pat_dob", "____________");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PrimaryInsurance) == false && this.m_AccessionOrder.PrimaryInsurance != "Not Selected")
                base.ReplaceText("primary_insurance", this.m_AccessionOrder.PrimaryInsurance);
            else base.ReplaceText("primary_insurance", "______________________");

            //if (string.IsNullOrEmpty(this.m_AccessionOrder.) == false) base.ReplaceText("prim_insu_address", this.m_AccessionOrder.);
            //else
             base.ReplaceText("prim_insu_address", "____________________________");

            //if (string.IsNullOrEmpty(this.m_AccessionOrder.) == false) base.ReplaceText("subscriber_name", this.m_AccessionOrder.);
            //else 
            base.ReplaceText("subscriber_name", "_____________________________");

            //if (string.IsNullOrEmpty(this.m_AccessionOrder.) == false) base.ReplaceText("subscriber_dob", this.m_AccessionOrder.);
            //else 
            base.ReplaceText("subscriber_dob", "_______________________________");

            if (string.IsNullOrEmpty(this.m_AccessionOrder.InsurancePlan1) == false) base.ReplaceText("ins_id", this.m_AccessionOrder.InsurancePlan1);
            else base.ReplaceText("ins_id", "____________________________________");

            //if (string.IsNullOrEmpty(this.m_AccessionOrder.I) == false) base.ReplaceText("ins_group", this.m_AccessionOrder.PSex);
            //else 
            base.ReplaceText("ins_group", "__________________________________");

            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            this.m_SaveFileName = Business.Document.CaseDocument.GetCaseFileNameXMLRequestForAuth(orderIdParser);
            this.m_ReportXml.Save(this.m_SaveFileName);
        }

        public override void Publish()
        {
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.AuthorizationForVerbalRequest, CaseDocumentFileTypeEnum.xml, CaseDocumentFileTypeEnum.doc);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.AuthorizationForVerbalRequest, CaseDocumentFileTypeEnum.doc, CaseDocumentFileTypeEnum.xps);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.AuthorizationForVerbalRequest, CaseDocumentFileTypeEnum.xps, CaseDocumentFileTypeEnum.tif);
        }
    }
}
