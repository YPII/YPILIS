using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Document;

namespace YellowstonePathology.Business.Test.MissingInformation
{
	public class MissingInformationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        private Business.Test.MissingInformation.MissingInformationTestOrder m_MissingInformationTestOrder;

        public MissingInformationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.MissingInformation.MissingInformationTestOrder missingInformationTestOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, missingInformationTestOrder, reportSaveMode)
        {
            this.m_MissingInformationTestOrder = missingInformationTestOrder;
        }

        public override void Render()
		{                        
			base.m_TemplateName = @"\\fileserver\documents\ReportTemplates\XmlTemplates\MissingInformation.1.xml";
            base.OpenTemplate();
			this.SetDemographicsV2();

            this.ReplaceText("letter_body", this.m_MissingInformationTestOrder.LetterBody);
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            this.m_SaveFileName = Business.Document.CaseDocument.GetCaseFileNameXml(orderIdParser);
            this.m_ReportXml.Save(this.m_SaveFileName);            
		}        

        public override void Publish()
        {
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.CaseReport, CaseDocumentFileTypeEnum.xml, CaseDocumentFileTypeEnum.doc);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.CaseReport, CaseDocumentFileTypeEnum.doc, CaseDocumentFileTypeEnum.xps);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.CaseReport, CaseDocumentFileTypeEnum.xps, CaseDocumentFileTypeEnum.tif);
        }
    }
}
