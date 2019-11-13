using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Document;

namespace YellowstonePathology.Business.Test.AdditionalTestingNotification
{
	public class AdditionalTestingNotificationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        private string m_SendToName;

        public AdditionalTestingNotificationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode, string sendToName) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
            this.m_SendToName = sendToName;
        }

        public override void Render()
		{
            string statement = "The following additional testing has been ordered: additional_tests, please hold the billing window open for this encounter. Additional charges will follow within 30-45 days.";
            base.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\AdditionalTestingNotification.5.xml";
			base.OpenTemplate();
			this.SetDemographicsV2();

            //this.SetXmlNodeData("test_name", this.m_PanelSetOrder.PanelSetName);
            //Business.Facility.Model.Facility facility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(this.m_PanelSetOrder.TechnicalComponentFacilityId);
            //this.SetXmlNodeData("facility_name", facility.FacilityName);

            statement = statement.Replace("additional_tests", this.m_AccessionOrder.PanelSetOrderCollection.GetAdditionalTestsDisplayString());
            this.SetXmlNodeData("test_statement", statement);
            if (string.IsNullOrEmpty(this.m_SendToName) == false)
            {
                this.ReplaceText("send_to", this.m_SendToName);
            }
            else
            {
                this.DeleteRow("send_to");
            }

            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            this.m_SaveFileName = Business.Document.CaseDocument.GetCaseFileNameXMLNotify(orderIdParser);
            this.m_ReportXml.Save(this.m_SaveFileName);
        }

        public override void Publish()
        {            
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);            
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.AdditionalTestingNotification, CaseDocumentFileTypeEnum.xml, CaseDocumentFileTypeEnum.doc);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.AdditionalTestingNotification, CaseDocumentFileTypeEnum.doc, CaseDocumentFileTypeEnum.xps);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.AdditionalTestingNotification, CaseDocumentFileTypeEnum.xps, CaseDocumentFileTypeEnum.tif);
        }
    }
}
