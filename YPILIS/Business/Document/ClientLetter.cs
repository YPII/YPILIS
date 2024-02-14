using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Document
{
	public class ClientLetter : CaseReportV2
	{
        public ClientLetter()
        {

        }

		public void Create(string patientName, YellowstonePathology.Business.Client.Model.Client client, string letterBody)
        {            
            this.m_TemplateName = @"\\CFileServer\documents\ReportTemplates\XmlTemplates\ClientMissingInfoFax.xml";            
            this.m_ReportXml = new XmlDocument();
            this.m_NameSpaceManager = new XmlNamespaceManager(this.m_ReportXml.NameTable);
            this.m_NameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");            

            this.m_ReportXml.Load(m_TemplateName);            

            this.ReplaceText("client_name", client.ClientName);
            this.ReplaceText("fax_number", YellowstonePathology.Business.Helper.PhoneNumberHelper.FormatWithDashes(client.Fax));
            this.ReplaceText("current_date", DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            this.ReplaceText("patient_name", patientName);

            this.SetXMLNodeParagraphData("letter_body", letterBody);

            string xmlDocumentFileName = YellowstonePathology.Properties.Settings.Default.ClientMissingInformationLetterFileName.ToUpper().Replace("DOC", "XML"); 
            this.m_ReportXml.Save(xmlDocumentFileName);

            //YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsDocFromFileName(xmlDocumentFileName);
            Business.Helper.FileConversionHelper.ConvertXMLToDoc(xmlDocumentFileName, xmlDocumentFileName.Replace(".xml", ".doc"));
        }
	}
}
