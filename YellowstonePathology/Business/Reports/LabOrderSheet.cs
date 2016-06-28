using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Linq;

namespace YellowstonePathology.Business.Reports
{
	public class LabOrderSheet : Report
	{
		string ReportBaseFileName = @"\\CFileServer\Documents\Reports\Templates\ReportBase.xml";
		public XmlDocument ReportBaseXml;
		protected XmlNamespaceManager NameSpaceManagerBase;

		public LabOrderSheet()
		{

		}

		public void CreateReport(string panelOrderIds, DateTime acknowledgeDate, DateTime acknowledgeTime)
		{
			Domain.XElementFromSql xElementFromSql = new YellowstonePathology.Business.Domain.XElementFromSql();
			if (panelOrderIds.Length > 0)
			{
				xElementFromSql = YellowstonePathology.Business.Gateway.XmlGateway.GetXmlOrdersToAcknowledge(panelOrderIds);
				int seq = 1;
				foreach (XElement accession in xElementFromSql.Document.Elements("AccessionOrder"))
				{
					PrintForAccession(accession, seq.ToString(), acknowledgeDate, acknowledgeTime);
					seq++;
				}
			}
		}

		private void PrintForAccession(XElement accessionOrderElement, string seq, DateTime acknowledgeDate, DateTime acknowledgeTime)
		{
			this.m_ReportTemplate = @"\\CFileServer\documents\Reports\Templates\LabOrders.xml";
			this.m_ReportSaveFileName = @"\\CFileServer\documents\Reports\Lab\LabOrders" + seq + ".xml";

			this.m_ReportXml = new XmlDocument();
			this.m_ReportXml.Load(this.m_ReportTemplate);

			this.m_NameSpaceManager = new XmlNamespaceManager(this.m_ReportXml.NameTable);
			this.m_NameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
			this.m_NameSpaceManager.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");

			this.ReportBaseXml = new XmlDocument();
			this.ReportBaseXml.Load(ReportBaseFileName);

			this.NameSpaceManagerBase = new XmlNamespaceManager(ReportBaseXml.NameTable);
			this.NameSpaceManagerBase.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
			this.NameSpaceManagerBase.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");

			string reportTitle = "Yellowstone Pathology Institute, Inc. - Lab Order Sheet";

			XmlNode nodeHeaderTable = this.FindXmlTableInDetail("report_header");
			XmlNode nodeReportHeader = this.FindXmlTableRowInDetail("report_header", nodeHeaderTable);
			this.ReplaceTextInRowNode(nodeReportHeader, "report_header", reportTitle);

			XmlNode nodeTable = this.FindXmlTableInDetail("accession");
			XmlNode nodeTemplateR1 = this.FindXmlTableRowInDetail("accession", nodeTable);

            bool firstElement = true;
			foreach(XElement panelOrderElement in accessionOrderElement.Element("PanelOrders").Elements("PanelOrder"))
			{
				string reportNo = string.Empty;
				string pathologist = string.Empty;
				string comment = string.Empty;
				string stain = string.Empty;
				string block = string.Empty;                
				string ackTime = string.Empty;
				string ackDate = string.Empty;

				if(firstElement)
				{
					reportNo = accessionOrderElement.Element("ReportNo").Value; 
					pathologist = panelOrderElement.Element("Initials").Value;
                    firstElement = false;
					comment = panelOrderElement.Element("Comment").Value;

					XmlNode nodeNewR1 = nodeTemplateR1.Clone();
					this.SetNodeText(nodeNewR1, reportNo, pathologist, comment, block, ackTime, ackDate);
					nodeTable.AppendChild(nodeNewR1);
					reportNo = string.Empty;
					pathologist = string.Empty;
				}
				else
				{
					if(panelOrderElement.Element("Comment").Value.Length > 0)
					{
						comment = panelOrderElement.Element("Comment").Value;
						XmlNode nodeCommentR1 = nodeTemplateR1.Clone();
						this.SetNodeText(nodeCommentR1, reportNo, pathologist, comment, block, ackTime, ackDate);
						nodeTable.AppendChild(nodeCommentR1);
					}
				}

				ackTime = acknowledgeTime.ToShortTimeString();
				ackDate = acknowledgeDate.ToShortDateString();

                if (YellowstonePathology.Document.XMLHelper.ElementExists(panelOrderElement, "TestOrders") == true)
                {
                    foreach (XElement testOrderElement in panelOrderElement.Element("TestOrders").Elements("TestOrder"))
                    {
                        string testComment = testOrderElement.Element("Comment").Value.Trim();
                        stain = testOrderElement.Element("TestName").Value;
                        if (testComment.Length > 0)
                        {
                            stain += " - " + testComment;
                        }
                        block = testOrderElement.Element("Description").Value;

                        XmlNode nodeTestR1 = nodeTemplateR1.Clone();
                        this.SetNodeText(nodeTestR1, reportNo, pathologist, stain, block, ackTime, ackDate);
                        nodeTable.AppendChild(nodeTestR1);
                    }
                }
			}

			nodeTable.RemoveChild(nodeTemplateR1);
			this.SetReportBody(nodeTable);
			this.m_ReportXml.Save(m_ReportSaveFileName);
			this.PrintReport(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.SpecialStainAcknowledgementPrinter);
		}

		public void SetReportBody(XmlNode nodeNew)
		{
			XmlNode nodeOld = ReportBaseXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_body']", this.m_NameSpaceManager);
			XmlNode nodeOldParent = nodeOld.ParentNode;

			XmlNode nodeImported = ReportBaseXml.ImportNode(nodeNew, true);

			nodeOldParent.AppendChild(nodeImported);
			nodeOldParent.RemoveChild(nodeOld);
		}

		private void SetNodeText(XmlNode nodeToSet, string accession, string pathologist, string stain, string block, string ackTime, string ackDate)
		{
			this.ReplaceTextInRowNode(nodeToSet, "accession", accession);
			this.ReplaceTextInRowNode(nodeToSet, "pathologist", pathologist);
			this.ReplaceTextInRowNode(nodeToSet, "stain", stain);
			this.ReplaceTextInRowNode(nodeToSet, "block", block);            
			this.ReplaceTextInRowNode(nodeToSet, "ack_time", ackTime);
			this.ReplaceTextInRowNode(nodeToSet, "ack_date", ackDate);
		}
	}
}
