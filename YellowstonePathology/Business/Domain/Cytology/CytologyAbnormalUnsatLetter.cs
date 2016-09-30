using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace YellowstonePathology.Reports.Cytology
{
    public class CytologyAbnormalUnsatLetter
    {
        string m_ReportTemplate;
        string m_ReportSaveFileName;

        DateTime m_StartDate;
        DateTime m_EndDate;
        YellowstonePathology.Business.Reports.Cytology.CytologyUnsatLetters m_ReportData;

        public XmlDocument ReportXml;
        public XmlNamespaceManager NameSpaceManager;

        string m_ReportFolderPath = @"\\CFileServer\Documents\Reports\Cytology\CytologyAbnormalUnsatLetter";        

        public CytologyAbnormalUnsatLetter(DateTime startDate, DateTime endDate)
        {            
            this.m_StartDate = startDate;
            this.m_EndDate = endDate;
            this.m_ReportTemplate = @"\\CFileServer\Documents\Reports\Templates\CytologyAbnormalUnsatLetter.xml";
            this.m_ReportData = new YellowstonePathology.Business.Reports.Cytology.CytologyUnsatLetters();
            this.m_ReportData.FillByDate(startDate, endDate);            
        }

        public void FaxReports()
        {            
            string path = @"\\CFileServer\documents\Reports\Cytology\CytologyAbnormalUnsatLetter\";
            foreach (YellowstonePathology.Business.Reports.Cytology.CytologyUnsatLetterItem item in this.m_ReportData)
            {
				YellowstonePathology.Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(item.ClientId);		
                string fileName = path + item.PhysicianClientId.ToString() + ".doc";
                YellowstonePathology.Business.ReportDistribution.Model.FaxSubmission.Submit(client.Fax, client.LongDistance, "Cytology Unsat Letters", fileName);                
            }            
        }


        public void CreateReports()
        {            
            this.ClearFolder();
            foreach(YellowstonePathology.Business.Reports.Cytology.CytologyUnsatLetterItem item in this.m_ReportData)
            {
                this.ReportXml = new XmlDocument();
                this.ReportXml.Load(this.m_ReportTemplate);

                this.NameSpaceManager = new XmlNamespaceManager(ReportXml.NameTable);
                this.NameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
                this.NameSpaceManager.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");

                this.m_ReportSaveFileName = @"\\CFileServer\Documents\Reports\Cytology\CytologyAbnormalUnsatLetter\" + item.PhysicianClientId.ToString() + ".xml";

                string openingStatement = "Below is a list of your patient(s) who have had an abnormal or unsatisfactory Pap Test between report_start_date and report_end_date.";

                XmlNode nodeTable = this.FindXmlTableInDetail("letter_date");
                XmlNode nodeLetterDate = this.FindXmlTableRowInDetail("letter_date", nodeTable);
                XmlNode nodePhysicianName = this.FindXmlTableRowInDetail("physician_name", nodeTable);
                XmlNode nodeClientName = this.FindXmlTableRowInDetail("client_name", nodeTable);
                XmlNode nodeClientAddress = this.FindXmlTableRowInDetail("client_address", nodeTable);
                XmlNode nodeClientCityStateZip = this.FindXmlTableRowInDetail("client_city_state_zip", nodeTable);
                XmlNode nodeOpeningStatement = this.FindXmlTableRowInDetail("opening_statement", nodeTable);

                this.ReplaceTextInRowNode(nodeLetterDate, "letter_date", DateTime.Today.ToLongDateString());
                this.ReplaceTextInRowNode(nodePhysicianName, "physician_name", item.PhysicianName);
                this.ReplaceTextInRowNode(nodeClientName, "client_name", item.ClientName);
                this.ReplaceTextInRowNode(nodeClientAddress, "client_address", item.Address);
                this.ReplaceTextInRowNode(nodeClientCityStateZip, "client_city_state_zip", item.CityStateZip);                

                string newOpeningStatement = openingStatement.Replace("report_start_date", this.m_StartDate.ToShortDateString());
                newOpeningStatement = newOpeningStatement.Replace("report_end_date", this.m_EndDate.ToShortDateString());
                this.ReplaceTextInRowNode(nodeOpeningStatement, "opening_statement", newOpeningStatement);                

                XmlNode nodeTemplateR1 = this.FindXmlTableRowInDetail("patient_name", nodeTable);
                foreach (YellowstonePathology.Business.Reports.Cytology.CytologyUnsatLetterDetailItem detailItem in item.DetailItems)
                {
                    XmlNode nodeNewR1 = nodeTemplateR1.Clone();                    
                    this.ReplaceTextInRowNode(nodeNewR1, "patient_name", detailItem.PatientName);
                    this.ReplaceTextInRowNode(nodeNewR1, "report_no", detailItem.ReportNo);
                    this.ReplaceTextInRowNode(nodeNewR1, "birth_date", detailItem.PBirthdate.Value.ToShortDateString());
                    this.ReplaceTextInRowNode(nodeNewR1, "collection_date", detailItem.CollectionDate.Value.ToShortDateString());
                    this.ReplaceTextInRowNode(nodeNewR1, "screening_impression", detailItem.ScreeningImpression);
                    nodeTable.InsertAfter(nodeNewR1, nodeTemplateR1);
                }
                nodeTable.RemoveChild(nodeTemplateR1);
                this.SaveReport();

				YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsDocFromFileName(this.m_ReportSaveFileName);
                //YellowstonePathology.Business.Document.Old.Routines.CommonRoutines.SaveXMLDocAsWordDoc(this.m_ReportSaveFileName);                
            }          
        }

        public void PrintReports()
        {
            string[] files = Directory.GetFiles(this.m_ReportFolderPath);
            foreach (string file in files)
            {
                //YellowstonePathology.Routines.CommonRoutines.PrintWordDoc(file);
            }
        }

        public void PrintEnvelopes()
        {
            /*
            foreach (YellowstonePathology.Business.Reports.Cytology.CytologyUnsatLetterItem item in this.m_ReportData)
            {
                YellowstonePathology.Client.Envelope envelope = new YellowstonePathology.Client.Envelope();
                envelope.PrintEnvelope(item.ClientName, item.Address, item.CityStateZip);                
            }
            */
        }

        public void ClearFolder()
        {
            string[] files = Directory.GetFiles(this.m_ReportFolderPath);
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }

        public XmlNode FindXmlTableInDetail(string text)
        {
            XmlNode nodeTable = ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='" + text + "']", this.NameSpaceManager);
            return nodeTable;
        }

        public XmlNode FindXmlTableRowInDetail(string text, XmlNode node)
        {
            XmlNode nodeRow = node.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='" + text + "']", this.NameSpaceManager);
            return nodeRow;
        }

        public void ReplaceTextInRowNode(XmlNode node, string field, string text)
        {
            node.SelectSingleNode("//w:r[w:t='" + field + "']/w:t", this.NameSpaceManager).InnerText = text;
        }

        public void SaveReport()
        {
            this.ReportXml.Save(this.m_ReportSaveFileName);
        }
    }
}
