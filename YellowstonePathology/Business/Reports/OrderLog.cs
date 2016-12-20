using System;
using System.Xml;
using System.IO;

namespace YellowstonePathology.Business.Reports
{
	public class LabOrdersLog : Report
	{
		string ReportBaseFileName = @"\\CFileServer\Documents\Reports\Templates\ReportBase.xml";
		public XmlDocument ReportBaseXml;
		protected XmlNamespaceManager NameSpaceManagerBase;

		public LabOrdersLog()
		{

		}

		public void CreateReport(DateTime reportDate)
		{
			YellowstonePathology.Business.Domain.OrderLogCollection orderLogCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetOrderLogCollectionByReportDate(reportDate);

			this.m_ReportTemplate = @"\\CFileServer\documents\Reports\Templates\LabOrdersLog.xml";

			this.m_ReportXml = new XmlDocument();
			this.m_ReportXml.Load(this.m_ReportTemplate);

			this.m_NameSpaceManager = new XmlNamespaceManager(this.m_ReportXml.NameTable);
			this.m_NameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
			this.m_NameSpaceManager.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");
			this.m_ReportSaveFileName = @"\\CFileServer\documents\Reports\Lab\LabOrdersLog\YEAR\MONTH\LabOrdersLog.FILEDATE.v1.xml";

			this.ReportBaseXml = new XmlDocument();
			this.ReportBaseXml.Load(ReportBaseFileName);

			this.NameSpaceManagerBase = new XmlNamespaceManager(ReportBaseXml.NameTable);
			this.NameSpaceManagerBase.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
			this.NameSpaceManagerBase.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");

			string reportTitle = "Lab Orders Daily Log - " + reportDate.ToLongDateString();
			ReportBaseXml.SelectSingleNode("//w:r[w:t='report_title_first_page']/w:t", this.NameSpaceManagerBase).InnerText = reportTitle;
			ReportBaseXml.SelectSingleNode("//w:r[w:t='report_title_second_page']/w:t", this.NameSpaceManagerBase).InnerText = reportTitle;

			XmlNode nodeFirstPage = ReportBaseXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_header_first_page']", this.NameSpaceManagerBase);
			XmlNode nodeSecondPage = ReportBaseXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_header_second_page']", this.NameSpaceManagerBase);

			XmlNode nodeHeader = m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_header']", this.m_NameSpaceManager);
			XmlNode nodeRowToDelete = nodeHeader.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='report_header']", this.m_NameSpaceManager);
			nodeHeader.RemoveChild(nodeRowToDelete);

			nodeFirstPage.InnerXml = nodeHeader.InnerXml;

			nodeSecondPage.InnerXml = nodeHeader.InnerXml;

			XmlNode nodeTable = this.FindXmlTableInDetail("accession_no");
			XmlNode nodeTemplateR1 = this.FindXmlTableRowInDetail("accession_no", nodeTable);

			foreach (YellowstonePathology.Business.Domain.OrderLogItem orderLogItem in orderLogCollection)
			{
				string reportNo = orderLogItem.ReportNo;

				DateTime dateTime = (DateTime)orderLogItem.OrderTime.Value;
				string orderDate = dateTime.ToString("M/d/yyyy h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
				string orderedBy = orderLogItem.Initials;
				string procedure = orderLogItem.TestName;
				string block = orderLogItem.Description;
				string comment = orderLogItem.ProcedureComment;

				XmlNode nodeNewR1 = nodeTemplateR1.Clone();
				this.ReplaceTextInRowNode(nodeNewR1, "accession_no", reportNo);
				this.ReplaceTextInRowNode(nodeNewR1, "order_date", orderDate);
				this.ReplaceTextInRowNode(nodeNewR1, "by", orderedBy);
				this.ReplaceTextInRowNode(nodeNewR1, "procedure", procedure);
				this.ReplaceTextInRowNode(nodeNewR1, "block", block);
				this.ReplaceTextInRowNode(nodeNewR1, "comment", comment);

				nodeTable.AppendChild(nodeNewR1);
			}

			nodeTable.RemoveChild(nodeTemplateR1);
			this.SetReportBody(nodeTable);
			this.SaveTheReport(reportDate.ToString());
		}

		public void SetReportBody(XmlNode nodeNew)
		{
			XmlNode nodeOld = ReportBaseXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_body']", this.m_NameSpaceManager);
			XmlNode nodeOldParent = nodeOld.ParentNode;

			XmlNode nodeImported = ReportBaseXml.ImportNode(nodeNew, true);

			nodeOldParent.AppendChild(nodeImported);
			nodeOldParent.RemoveChild(nodeOld);
		}

		public void SaveTheReport(string reportDate)
		{
			string month = DateTime.Parse(reportDate).ToString("MMMM");
			string year = DateTime.Parse(reportDate).ToString("yyyy");
			string fileName = this.m_ReportSaveFileName.Replace("YEAR", year);
			fileName = fileName.Replace("MONTH", month);
			fileName = fileName.Replace("FILEDATE", DateTime.Parse(reportDate).ToString("MM.dd.yy"));
			this.m_ReportSaveFileName = fileName;

			while (true)
			{
				if (File.Exists(fileName) == true)
				{
					DateTime dtReportDate = DateTime.Parse(reportDate);
					DateTime dtNow = DateTime.Parse(DateTime.Now.ToShortDateString());
					TimeSpan timeSpan = new TimeSpan(dtNow.Ticks - dtReportDate.Ticks);
					if (timeSpan.TotalDays >= 4)
					{
						int vLocation = fileName.IndexOf(".v");
						int originalVersion = Convert.ToInt32(fileName.Substring(vLocation + 2, 1));
						int newVersion = originalVersion + 1;
						fileName = fileName.Replace(".v" + originalVersion.ToString(), ".v" + newVersion.ToString());
					}
					else
					{
						break;
					}
				}
				else
				{
					break;
				}
			}
			this.ReportBaseXml.Save(fileName);
			this.m_ReportSaveFileName = fileName;
		}
	}
}
