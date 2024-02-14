using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Document;

namespace YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization
{
	public class ExtractAndHoldForPreauthorizationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public ExtractAndHoldForPreauthorizationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{            
			base.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\PreauthorizationNotification.5.xml";
			base.OpenTemplate();
			this.SetDemographicsV2();			

            Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationTestOrder testOrder = (Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationTestOrder)this.m_PanelSetOrder;
            Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
            Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(testOrder.TestId.Value);

            this.SetXmlNodeData("additional_testing", panelSet.PanelSetName);
            this.SetXmlNodeData("cpt_codes", panelSet.PanelSetCptCodeCollection.GetCommaSeparatedString());
            this.SetXmlNodeData("report_comment", testOrder.Comment);

            if (this.m_AccessionOrder.PrimaryInsurance == "Medicare")
            {
                this.SetXmlNodeData("additional_testing_message", "The following additional testing needs an ABN");                
            }
            else
            {
                this.SetXmlNodeData("additional_testing_message", "The following additional testing needs preauthorization:");
            }            

            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            this.m_SaveFileName = Business.Document.CaseDocument.GetCaseFileNameXMLPreAuth(orderIdParser);
            this.m_ReportXml.Save(this.m_SaveFileName);            
		}        

        public override void Publish()
        {
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.PreauthorizationRequest, CaseDocumentFileTypeEnum.xml, CaseDocumentFileTypeEnum.doc);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.PreauthorizationRequest, CaseDocumentFileTypeEnum.doc, CaseDocumentFileTypeEnum.xps);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.PreauthorizationRequest, CaseDocumentFileTypeEnum.xps, CaseDocumentFileTypeEnum.tif);
        }
    }
}
