using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Linq;

namespace YellowstonePathology.Business.Document
{
    public class CaseReportV2: YellowstonePathology.Business.Interface.ICaseDocument
    {
        public const string TestDevelopedComment = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  " +
            "It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  " +
            "This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical " +
            "Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";

        protected string m_TemplateName;
		protected XmlDocument m_ReportXml;
		protected XmlNamespaceManager m_NameSpaceManager;
		protected string m_SaveFileName;				

		protected YellowstonePathology.Business.Document.ReportSaveModeEnum m_ReportSaveMode;		
        protected Business.Test.AccessionOrder m_AccessionOrder;
        protected Business.Test.PanelSetOrder m_PanelSetOrder;

        protected YellowstonePathology.Business.Document.NativeDocumentFormatEnum m_NativeDocumentFormat;
		
        public CaseReportV2()
        {

        }
		
		public CaseReportV2(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
            this.m_ReportSaveMode = reportSaveMode;

            this.m_ReportXml = new XmlDocument();
            this.m_NativeDocumentFormat = NativeDocumentFormatEnum.Word;
            this.m_NameSpaceManager = new XmlNamespaceManager(m_ReportXml.NameTable);
            this.m_NameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");            			
		}

		public virtual YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser)
		{
			return YellowstonePathology.Business.Document.CaseDocument.DeleteCaseFiles(orderIdParser);            
        }

		public virtual void Render()
        {
            throw new NotImplementedException("Not Implemented Here");
        }

        public virtual void Publish()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsPDF(orderIdParser);
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_PanelSetOrder.ReportNo);
        }

        public YellowstonePathology.Business.Document.NativeDocumentFormatEnum NativeDocumentFormat
        {
            get { return this.m_NativeDocumentFormat; }
            set { this.m_NativeDocumentFormat = value; }
        }
		
        public void OpenTemplate()
        {
            this.m_ReportXml.Load(this.m_TemplateName);
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            switch (this.m_ReportSaveMode)
            {
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Draft:
					this.m_SaveFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + this.m_PanelSetOrder.ReportNo + ".draft.xml";
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Normal:
					this.m_SaveFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + this.m_PanelSetOrder.ReportNo + ".xml";                
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Test:
                    this.m_SaveFileName = @"c:\test.xml";
                    break;
            }            
        }

        public void WriteTestPerformedLocation()
        {
            
        }

        public void SetDemographics()
        {
            this.SetXmlNodeData("patient_name", this.m_AccessionOrder.PatientName);
            this.SetXmlNodeData("patient_birthdate", BaseData.GetShortDateString(this.m_AccessionOrder.PBirthdate));
            this.SetXmlNodeData("patient_age", this.m_AccessionOrder.PatientAccessionAge);
            this.SetXmlNodeData("physician_name", this.m_AccessionOrder.PhysicianName);
			this.SetXmlNodeData("accession_no", this.m_PanelSetOrder.ReportNo);

			this.SetXmlNodeData("collection_date", CaseReportV2.ReportDateTimeFormat(this.m_AccessionOrder.CollectionDate.Value));
			this.SetXmlNodeData("accession_date", CaseReportV2.ReportDateTimeFormat(this.m_AccessionOrder.AccessionTime.Value));

            this.SetXmlNodeData("page2_header_patient_name", this.m_AccessionOrder.PatientName);
			this.SetXmlNodeData("page2_header_accessionno", this.m_PanelSetOrder.ReportNo);
            this.SetXmlNodeData("location_performed", this.m_PanelSetOrder.GetLocationPerformedComment());
        }

        public void SetDemographicsV2()
        {
			string reportNumber = this.m_PanelSetOrder.ReportNo;
            this.ReplaceText("report_number", reportNumber);
			this.ReplaceText("accession_no", this.m_PanelSetOrder.MasterAccessionNo.ToString());
			this.ReplaceText("patient_name", this.m_AccessionOrder.PatientDisplayName);
            this.ReplaceText("patient_age", this.m_AccessionOrder.PatientAccessionAge + ", " + this.m_AccessionOrder.PSex);            

            if (string.IsNullOrEmpty(this.m_AccessionOrder.PCAN) == false)
            {
                this.ReplaceText("client_mrn_no", this.m_AccessionOrder.PCAN);
                this.ReplaceText("client_account_no", string.Empty);
            }
            else
            {
                this.ReplaceText("client_mrn_no", this.m_AccessionOrder.SvhMedicalRecord);
                this.ReplaceText("client_account_no", this.m_AccessionOrder.SvhAccount);
            }            

            if (this.m_AccessionOrder.AccessionDate.HasValue == true)
            {
                this.ReplaceText("received_date", this.m_AccessionOrder.AccessionDate.Value.ToShortDateString());
                this.ReplaceText("accession_date", CaseReportV2.ReportDateTimeFormat(this.m_AccessionOrder.AccessionTime.Value));
            }
			if (this.m_PanelSetOrder.FinalDate.HasValue == true)
            {				
				this.ReplaceText("report_date", this.m_PanelSetOrder.FinalDate.Value.ToShortDateString());
            }
			if (this.m_PanelSetOrder.FinalTime.HasValue == true)
            {
                this.ReplaceText("final_date", this.m_PanelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"));
				this.ReplaceText("report_time", this.m_PanelSetOrder.FinalTime.Value.ToShortTimeString());
				this.ReplaceText("final_time", this.m_PanelSetOrder.FinalTime.Value.ToShortTimeString());

                this.ReplaceText("signature_date", this.m_PanelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"));
            }
			if (this.m_AccessionOrder.CollectionDate.HasValue == true)
			{
                this.ReplaceText("collection_date", CaseReportV2.ReportDateTimeFormat(this.m_AccessionOrder.CollectionDate.Value));           
            }
            if (this.m_AccessionOrder.PBirthdate.HasValue == true) this.ReplaceText("patient_birthdate", this.m_AccessionOrder.PBirthdate.Value.ToShortDateString());            

            this.ReplaceText("physician_name", this.m_AccessionOrder.PhysicianName);
            this.ReplaceText("client_name", this.m_AccessionOrder.ClientName);
            this.ReplaceText("page2_header", this.m_AccessionOrder.PatientName + ", " + reportNumber);

            this.SetXmlNodeData("location_performed", this.m_PanelSetOrder.GetLocationPerformedComment());
        }       

        public void SetReportDistribution()
        {            
            XmlNode nodeP = m_ReportXml.SelectSingleNode("descendant::w:p[w:r/w:t='report_distribution']", this.m_NameSpaceManager);
            XmlNode nodeTc = m_ReportXml.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='report_distribution']", this.m_NameSpaceManager);
			string reportDistribution = "";

            if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.Count != 0)
            {
                foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in this.m_PanelSetOrder.TestOrderReportDistributionCollection)
                {
                    reportDistribution = testOrderReportDistribution.DistributionType + ": " + testOrderReportDistribution.ClientName + " - " + testOrderReportDistribution.PhysicianName;
                    XmlNode nodeAppend = nodeP.Clone();
                    nodeAppend.SelectSingleNode("//w:r/w:t", this.m_NameSpaceManager).InnerText = reportDistribution;
                    nodeTc.AppendChild(nodeAppend);
                }
                nodeTc.RemoveChild(nodeP);
            }
            else
            {
                this.ReplaceText("report_distribution", string.Empty);
            }
        }

        public void SetCaseHistory()
        {
            XmlNode nodeP = m_ReportXml.SelectSingleNode("descendant::w:p[w:r/w:t='other_ypii_cases']", this.m_NameSpaceManager);
            XmlNode nodeTc = m_ReportXml.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='other_ypii_cases']", this.m_NameSpaceManager);

            YellowstonePathology.Business.Patient.Model.PatientHistoryList patientHistoryList = new Patient.Model.PatientHistoryList();
            patientHistoryList.SetFillCommandByAccessionNo(this.m_PanelSetOrder.ReportNo);
            patientHistoryList.Fill();

			bool hashistory = false;
            if (patientHistoryList.Count > 1)
            {
                string caseHistory = "";
                foreach (Business.Patient.Model.PatientHistoryListItem item in patientHistoryList)
                {
					if (item.ReportNo != this.m_PanelSetOrder.ReportNo)
                    {
						caseHistory += item.ReportNo + ", ";
                    }
                }
				if (caseHistory.Length > 2)
				{
					hashistory = true;
					caseHistory = caseHistory.Substring(0, caseHistory.Length - 2);
					XmlNode nodeAppend = nodeP.Clone();
					nodeAppend.SelectSingleNode("//w:r/w:t", this.m_NameSpaceManager).InnerText = caseHistory;
					nodeTc.AppendChild(nodeAppend);
					nodeTc.RemoveChild(nodeP);
				}
            }
            
			if(!hashistory)
            {
                XmlNode nodeAppend = nodeP.Clone();
                nodeAppend.SelectSingleNode("//w:r/w:t", this.m_NameSpaceManager).InnerText = "None.";
                nodeTc.AppendChild(nodeAppend);
                nodeTc.RemoveChild(nodeP);
            }
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

            if (string.IsNullOrEmpty(replaceString) == true) return nodeList;

            replaceString = replaceString.Replace("&", "&amp;");
            replaceString = replaceString.Replace(">", "&gt;");
            replaceString = replaceString.Replace("<", "&lt;");
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
                
                for (int i=0; i<lines.Length; i++)
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

        public void SetXmlNodeData(string field, string data)
        {
            try
            {
                XmlNode node = m_ReportXml.SelectSingleNode("//w:t[.='" + field + "']", this.m_NameSpaceManager);
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

                Regex regex = new Regex("(\r\n)");            
                string[] lineSplit = regex.Split(data);                

                for (int i = 0; i < lineSplit.Length; i++)
                {
                    if (lineSplit[i] != "\r\n")
                    {
                        XmlNode childNodeClone = childNode.Clone();
                        XmlNode node = childNodeClone.SelectSingleNode("descendant::w:r[w:t='" + field + "']/w:t", this.m_NameSpaceManager);
                        node.InnerText = lineSplit[i];
                        parentNode.AppendChild(childNodeClone);
                    }
                }
                parentNode.RemoveChild(childNode);
            }
            catch (Exception)
            {

            }
        }

        public List<string> GetLines(string data)
        {
            List<string> lines = new List<string>();
     
            Regex regex = new Regex("(\r\n)");
            string[] lineSplit = regex.Split(data);

            for (int i = 0; i < lineSplit.Length; i++)
            {
                if (lineSplit[i] != "\r\n")
                {
                    lines.Add(lineSplit[i]);
                }
            }

            return lines;
        }       

        public void SetXMLNodeParagraphDataNode(XmlNode inputNode, string field, string data)
        {
			if (data != null)
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
        }

        public void DeleteRow(string field)
        {
            XmlNode parentNode = m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='" + field + "']", this.m_NameSpaceManager);
            XmlNode childNode = parentNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='" + field + "']", this.m_NameSpaceManager);
            parentNode.RemoveChild(childNode);
        }

        public void SaveReport()
        {            
            switch (this.m_ReportSaveMode)
            {
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Draft:                                        
                    this.m_ReportXml.Save(this.m_SaveFileName);
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Normal:                
                    this.m_ReportXml.Save(this.m_SaveFileName);
					YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
					CaseDocument.SaveXMLAsDoc(orderIdParser);
					CaseDocument.SaveDocAsXPS(orderIdParser);
                    break;
                case YellowstonePathology.Business.Document.ReportSaveModeEnum.Test:
                    this.m_ReportXml.Save(@"c:\Testing\Test.xml");
                    break;
            }            
        }        

		public static string ReportDateTimeFormat(DateTime date)
		{
            string result = string.Empty;
            if (date.Hour == 0 && date.Minute == 0)
            {
                result = date.ToString("MM/dd/yyy");
            }
            else
            {
                result = date.ToString("MM/dd/yyy HH:mm");
            }
            return result;            
		}        
    }
}
