using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

namespace YellowstonePathology.Business.Document.Old
{
    public class BaseReport
    {                
        public ArrayList m_SqlStatements;
        public ArrayList m_TableNames;
        public DataSet m_ReportData;     
        public XmlDocument m_ReportXml;
        public XmlNamespaceManager m_NameSpaceManager;
        public Document.Old.DataClasses.BaseData m_Data;
        public string m_SaveFileName;

        protected Business.Test.AccessionOrder m_AccessionOrder;
        protected Business.Test.PanelSetOrder m_PanelSetOrder;
        protected YellowstonePathology.Business.Document.ReportSaveModeEnum m_ReportSaveMode;        

        public BaseReport(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
            this.m_ReportSaveMode = reportSaveMode;

            this.m_SqlStatements = new ArrayList();
            this.m_TableNames = new ArrayList();
            this.m_Data = new YellowstonePathology.Business.Document.Old.DataClasses.BaseData();
            this.m_ReportXml = new XmlDocument();
            this.m_NameSpaceManager = new XmlNamespaceManager(m_ReportXml.NameTable);
            this.m_NameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
        }

        public void OpenTemplate(string templateName)
        {            
            this.m_SaveFileName = Common.getCasePath(this.m_PanelSetOrder.ReportNo) + this.m_PanelSetOrder.ReportNo + ".xml";            
            this.m_ReportXml.Load(templateName);            
        }

        public void GetDataSet()
        {
            this.m_TableNames.Add("tblTestOrderReportDistribution");
            this.m_SqlStatements.Add("select * from tblTestOrderReportDistribution where ReportNo = '" + this.m_PanelSetOrder.ReportNo + "'");
            this.m_ReportData = this.m_Data.GetDataSetFromSqlStatementsWithHistory(this.m_SqlStatements, this.m_TableNames, this.m_PanelSetOrder.ReportNo);
        }

        public void SetDemographics(string tableName)
        {
            string patientName = this.m_ReportData.Tables[tableName].Rows[0]["plastname"].ToString() + ", " + 
                this.m_ReportData.Tables[tableName].Rows[0]["pfirstname"].ToString() + " " +
                this.m_ReportData.Tables[tableName].Rows[0]["pmiddleinitial"].ToString();

            string birthdate = Common.getShortDateStringFromDate(this.m_ReportData.Tables[tableName].Rows[0]["pbirthdate"].ToString());
            string accessionDate = Common.getShortDateStringFromDate(this.m_ReportData.Tables[tableName].Rows[0]["accessiondate"].ToString());
            string genderString = this.m_ReportData.Tables[0].Rows[0]["psex"].ToString();
            string patientAge = Common.getPatientAgeString(accessionDate, birthdate) + ", " + genderString;            
            string physicianName;

            if (tableName == "tblCytology" | tableName == "tblSurgical")
            {
                physicianName = this.m_ReportData.Tables[tableName].Rows[0]["physicianname"].ToString();
                accessionDate += " - " + DateTime.Parse(this.m_ReportData.Tables[tableName].Rows[0]["AccessionTime"].ToString()).ToString("HH:MM");
            }
            else
            {
                physicianName = this.m_ReportData.Tables[tableName].Rows[0]["physicianname"].ToString();
            }
            string collectionDate = Common.getShortDateStringFromDate(this.m_ReportData.Tables[tableName].Rows[0]["collectiondate"].ToString());            

            this.SetXmlNodeData("patient_name", patientName);
            this.SetXmlNodeData("patient_birthdate", birthdate);
            this.SetXmlNodeData("patient_age", patientAge);
            this.SetXmlNodeData("physician_name", physicianName);
            this.SetXmlNodeData("accession_no", this.m_PanelSetOrder.ReportNo);

            this.SetXmlNodeData("collection_date", collectionDate);            
            this.SetXmlNodeData("accession_date", accessionDate);

            this.SetXmlNodeData("page2_header_patient_name", patientName);
            this.SetXmlNodeData("page2_header_accessionno", this.m_PanelSetOrder.ReportNo);
        }

        public void SetDemographicsV2(string tableName)
        {
            string patientName = this.m_ReportData.Tables[tableName].Rows[0]["plastname"].ToString() + ", " +
                this.m_ReportData.Tables[tableName].Rows[0]["pfirstname"].ToString() + " " +
                this.m_ReportData.Tables[tableName].Rows[0]["pmiddleinitial"].ToString();

            string birthdate = Common.getShortDateStringFromDate(this.m_ReportData.Tables[tableName].Rows[0]["pbirthdate"].ToString());
            string accessionDate = Common.getShortDateTimeString(this.m_ReportData.Tables[tableName].Rows[0]["accessiontime"].ToString());            
            string finalDate = Common.getShortDateStringFromDate(this.m_ReportData.Tables[tableName].Rows[0]["finaldate"].ToString());
            string finalTime = Common.getShortTimeStringFromTime(this.m_ReportData.Tables[tableName].Rows[0]["finaltime"].ToString());
            string genderString = this.m_ReportData.Tables[0].Rows[0]["psex"].ToString();
            string patientAge = Common.getPatientAgeString(accessionDate, birthdate) + " " + genderString;
            string physicianName = string.Empty;
            string clientName = string.Empty;

            if (tableName == "tblCytology" | tableName == "tblSurgical")
            {
                physicianName = this.m_ReportData.Tables[tableName].Rows[0]["physicianname"].ToString();
                clientName = this.m_ReportData.Tables[tableName].Rows[0]["clientname"].ToString();
                accessionDate += " - " + DateTime.Parse(this.m_ReportData.Tables[tableName].Rows[0]["AccessionTime"].ToString()).ToString("HH:MM");
            }
            else
            {
                physicianName = this.m_ReportData.Tables[tableName].Rows[0]["physicianname"].ToString();
                clientName = this.m_ReportData.Tables[tableName].Rows[0]["clientname"].ToString();
            }

            Nullable<DateTime> collectionTime = null;
            string xxx = this.m_ReportData.Tables[tableName].Rows[0]["collectiontime"].ToString();
            if (string.IsNullOrEmpty(this.m_ReportData.Tables[tableName].Rows[0]["collectiontime"].ToString()) == false)
            {
                collectionTime = DateTime.Parse(this.m_ReportData.Tables[tableName].Rows[0]["collectiontime"].ToString());
            }

            Nullable<DateTime> collectionDate = null;
            if (string.IsNullOrEmpty(this.m_ReportData.Tables[tableName].Rows[0]["collectiondate"].ToString()) == false)
            {
                collectionDate = DateTime.Parse(this.m_ReportData.Tables[tableName].Rows[0]["collectiondate"].ToString());
            }

            this.ReplaceText("report_number", this.m_PanelSetOrder.ReportNo);
            this.ReplaceText("accession_no", this.m_AccessionOrder.MasterAccessionNo);
            this.ReplaceText("patient_name", patientName);
            this.ReplaceText("patient_age", patientAge);

            this.ReplaceText("received_date", accessionDate);
            this.ReplaceText("accession_date", accessionDate);

            this.ReplaceText("final_date", finalDate);
            this.ReplaceText("report_date", finalDate);

            this.ReplaceText("report_time", finalTime);
            this.ReplaceText("final_time", Common.getShortDateTimeString(this.m_ReportData.Tables[tableName].Rows[0]["finaltime"].ToString()));            

            if (collectionTime.HasValue == true)
            {
                this.ReplaceText("collection_date", collectionTime.Value.ToString("MM/dd/yyyy HH:mm"));
            }
            else
            {
                this.ReplaceText("collection_date", collectionDate.Value.ToString("MM/dd/yyyy"));
            }

            this.ReplaceText("patient_birthdate", birthdate);

            this.ReplaceText("physician_name", physicianName);
            this.ReplaceText("client_name", clientName);           			

            if (string.IsNullOrEmpty(this.m_ReportData.Tables[tableName].Rows[0]["pcan"].ToString()) == false)
            {
                this.ReplaceText("client_mrn_no", this.m_ReportData.Tables[tableName].Rows[0]["pcan"].ToString());
                this.ReplaceText("client_account_no", string.Empty);
            }
            else
            {
                this.ReplaceText("client_mrn_no", this.m_ReportData.Tables[tableName].Rows[0]["SvhMedicalRecord"].ToString());
                this.ReplaceText("client_account_no", this.m_ReportData.Tables[tableName].Rows[0]["SvhAccount"].ToString());
            }      

            string page2Header = patientName + ", " + this.m_PanelSetOrder.ReportNo;
            this.ReplaceText("page2_header", page2Header);
        }

        public void ReplaceText(string searchString, string replaceString)
        {
            XmlNodeList nodeList = this.m_ReportXml.SelectNodes("descendant::w:t", this.m_NameSpaceManager);
            foreach (XmlNode node in nodeList)
            {
                string text = node.InnerXml;
                if (text.Contains(searchString) == true)
                {
                    XmlNode parentNode = node.ParentNode;
                    List<XmlNode> nodesToAdd = this.GetNodesToInsert(node, searchString, replaceString);
                    XmlNode nodeToAddAfter = node;
                    foreach (XmlNode addNode in nodesToAdd)
                    {
                        parentNode.InsertAfter(addNode, nodeToAddAfter);
                        nodeToAddAfter = addNode;
                    }
                    parentNode.RemoveChild(node);
                }
            }
        }

        public List<XmlNode> GetNodesToInsert(XmlNode replaceNode, string searchString, string replaceString)
        {
            List<XmlNode> nodeList = new List<XmlNode>();
            string[] lines = Regex.Split(replaceString, "\r\n");

            if (lines.Length == 1)
            {
                XmlNode tNode = this.m_ReportXml.CreateElement("w", "t", "http://schemas.microsoft.com/office/word/2003/wordml");
                tNode.InnerXml = replaceNode.InnerXml.Replace(searchString, replaceString);
                nodeList.Add(tNode);
            }
            else
            {
                string[] preservedText = Regex.Split(replaceNode.InnerXml, searchString);
                if (preservedText.Length >= 1)
                {
                    XmlNode startNode = this.m_ReportXml.CreateElement("w", "t", "http://schemas.microsoft.com/office/word/2003/wordml");
                    startNode.InnerXml = preservedText[0];
                    nodeList.Add(startNode);
                }

                for (int i = 0; i < lines.Length; i++)
                {
                    XmlNode lineNode = this.m_ReportXml.CreateElement("w", "t", "http://schemas.microsoft.com/office/word/2003/wordml");
                    lineNode.InnerXml = lines[i];
                    nodeList.Add(lineNode);

                    if (i != lines.Length - 1)
                    {
                        XmlNode breakNode = this.m_ReportXml.CreateElement("w", "br", "http://schemas.microsoft.com/office/word/2003/wordml");
                        nodeList.Add(breakNode);
                    }
                }

                if (preservedText.Length == 2)
                {
                    XmlNode endNode = this.m_ReportXml.CreateElement("w", "t", "http://schemas.microsoft.com/office/word/2003/wordml");
                    endNode.InnerXml = preservedText[1];
                    nodeList.Add(endNode);
                }
            }
            return nodeList;
        }

        public void SetReportDistribution()
        {
            if (this.m_ReportData.Tables["tblTestOrderReportDistribution"].Rows.Count != 0)
            {
                XmlNode nodeP = m_ReportXml.SelectSingleNode("descendant::w:p[w:r/w:t='report_distribution']", this.m_NameSpaceManager);
                XmlNode nodeTc = m_ReportXml.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='report_distribution']", this.m_NameSpaceManager);

                foreach (DataRow dr in this.m_ReportData.Tables["tblTestOrderReportDistribution"].Rows)
                {
                    string reportDistribution = dr["DistributionType"]  + ": " + dr["clientname"].ToString() + " - " + dr["physicianname"].ToString();
                    XmlNode nodeAppend = nodeP.Clone();
                    nodeAppend.SelectSingleNode("//w:r/w:t", this.m_NameSpaceManager).InnerText = reportDistribution;                
                    nodeTc.AppendChild(nodeAppend);
                }
                nodeTc.RemoveChild(nodeP);
            }
        }

        public void SetCaseHistory()
        {
            XmlNode nodeP = m_ReportXml.SelectSingleNode("descendant::w:p[w:r/w:t='other_ypii_cases']", this.m_NameSpaceManager);
            XmlNode nodeTc = m_ReportXml.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='other_ypii_cases']", this.m_NameSpaceManager);
            
            if (this.m_ReportData.Tables["tblCaseHistory"] != null && this.m_ReportData.Tables["tblCaseHistory"].Rows.Count > 1)
            {
                string caseHistory = "";
                foreach (DataRow dr in this.m_ReportData.Tables["tblCaseHistory"].Rows)
                {                    
                    if (dr["ReportNo"].ToString() != this.m_PanelSetOrder.ReportNo)
                    {
                        caseHistory += dr["ReportNo"].ToString() + ", ";                                        
                    }
                }
                caseHistory = caseHistory.Substring(0, caseHistory.Length - 2);
                XmlNode nodeAppend = nodeP.Clone();                        
                nodeAppend.SelectSingleNode("//w:r/w:t", this.m_NameSpaceManager).InnerText = caseHistory;
                nodeTc.AppendChild(nodeAppend);
                nodeTc.RemoveChild(nodeP);                
            }
            else
            {
                XmlNode nodeAppend = nodeP.Clone();
                nodeAppend.SelectSingleNode("//w:r/w:t", this.m_NameSpaceManager).InnerText = "None.";
                nodeTc.AppendChild(nodeAppend);
                nodeTc.RemoveChild(nodeP);
            }
        }

        public void SetXmlNodeData(string field, string data)
        {
            try
            {
                XmlNode node = m_ReportXml.SelectSingleNode("descendant::w:r[w:t='" + field + "']/w:t", this.m_NameSpaceManager);
                node.InnerText = data;
            }
            catch (NullReferenceException)
            {

            }
        }

        public void SetXMLNodeParagraphData(string field, string data)
        {
            try
            {
                XmlNode parentNode = m_ReportXml.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='" + field + "']", this.m_NameSpaceManager);
                XmlNode childNode = parentNode.SelectSingleNode("descendant::w:p[w:r/w:t='" + field + "']", this.m_NameSpaceManager);
            

                string paragraphs = data;
                string[] lineSplit = paragraphs.Split('\n');

                for (int i = 0; i < lineSplit.Length; i++)
                {
                    XmlNode childNodeClone = childNode.Clone();
                    XmlNode node = childNodeClone.SelectSingleNode("descendant::w:r[w:t='" + field + "']/w:t", this.m_NameSpaceManager);
                    node.InnerText = lineSplit[i];
                    parentNode.AppendChild(childNodeClone);
                }
                parentNode.RemoveChild(childNode);
            }
            catch (Exception)
            {
                
            }
        }

        public void SetXMLNodeParagraphDataNode(XmlNode inputNode, string field, string data)
        {
            XmlNode parentNode = inputNode.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='" + field + "']", this.m_NameSpaceManager); ;
            XmlNode childNode = parentNode.SelectSingleNode("descendant::w:p[w:r/w:t='" + field + "']", this.m_NameSpaceManager);

            string paragraphs = data;
            string[] lineSplit = paragraphs.Split('\n');

            for (int i = 0; i < lineSplit.Length; i++)
            {
                XmlNode childNodeClone = childNode.Clone();
                XmlNode node = childNodeClone.SelectSingleNode("descendant::w:r[w:t='" + field + "']/w:t", this.m_NameSpaceManager);
                node.InnerText = lineSplit[i];
                parentNode.AppendChild(childNodeClone);
            }
            parentNode.RemoveChild(childNode);
        }

        public void DeleteRow(string field)
        {
            XmlNode parentNode = m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='" + field + "']", this.m_NameSpaceManager);
            XmlNode childNode = parentNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='" + field + "']", this.m_NameSpaceManager);
            parentNode.RemoveChild(childNode);
        }

        public void SaveReport()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            switch (this.m_ReportSaveMode)
            {
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Test:
                    this.m_ReportXml.Save(@"c:\testing\test.xml");                
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Draft:
					this.m_SaveFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + this.m_PanelSetOrder.ReportNo + ".draft.xml";
                    this.m_ReportXml.Save(this.m_SaveFileName);
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Normal:
                    this.m_ReportXml.Save(this.m_SaveFileName);
					CaseDocument.SaveXMLAsDoc(orderIdParser);
					CaseDocument.SaveDocAsXPS(orderIdParser);
					break;
            }            
        }        
    }
}
