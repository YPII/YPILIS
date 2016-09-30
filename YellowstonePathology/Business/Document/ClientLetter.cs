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
            string templateName = @"\\CFileServer\documents\ReportTemplates\XmlTemplates\ClientMissingInfoFax.xml";            

            XmlDocument xmlDocument = new XmlDocument();
            XmlNamespaceManager nameSpaceManager = new XmlNamespaceManager(m_ReportXml.NameTable);
            nameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");            

            xmlDocument.Load(templateName);
            this.m_ReportXml = xmlDocument;

            this.ReplaceText("client_name", client.ClientName);
            this.ReplaceText("fax_number", YellowstonePathology.Business.Domain.PhoneNumber.Format(client.Fax));
            this.ReplaceText("current_date", DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            this.ReplaceText("patient_name", patientName);

            this.SetXMLNodeParagraphData("letter_body", letterBody);

            string xmlDocumentFileName = YellowstonePathology.Properties.Settings.Default.ClientMissingInformationLetterFileName.ToUpper().Replace("DOC", "XML"); 
            xmlDocument.Save(xmlDocumentFileName);

            YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsDocFromFileName(xmlDocumentFileName);            
        }
	}
}
