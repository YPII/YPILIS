using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace YellowstonePathology.Business.Reports
{
    public class Report
    {
        protected DateTime m_StartDate;
        protected DateTime m_EndDate;

        protected XmlDocument m_ReportXml;
        protected XmlNamespaceManager m_NameSpaceManager;

        protected string m_ReportTemplate;
        protected string m_ReportSaveFileName;

        string m_PrinterName;

        public Report()
        {

        }        

        public XmlNode FindXmlTableInDetail(string text)
        {
            XmlNode nodeTable = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='" + text + "']", this.m_NameSpaceManager);
            return nodeTable;
        }

        public XmlNode FindXmlTableRowInDetail(string text, XmlNode node)
        {
            XmlNode nodeRow = node.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='" + text + "']", this.m_NameSpaceManager);
            return nodeRow;
        }

        public void ReplaceTextInRowNode(XmlNode node, string field, string text)
        {
            node.SelectSingleNode("//w:r[w:t='" + field + "']/w:t", this.m_NameSpaceManager).InnerText = text;
        }

        public void SaveReport()
        {
			try
			{
				this.m_ReportXml.Save(this.m_ReportSaveFileName);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
        }

        public void OpenReport()
        {
            Process p1 = new Process();
            p1.StartInfo = new ProcessStartInfo("wordview.exe", this.m_ReportSaveFileName);
            p1.Start();
        }

        public void PrintReport(string printerName)
        {
            this.m_PrinterName = printerName;
            this.PrintReport();
        }

        public void PrintReport()
        {            
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;            

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;

            string activePrinter = oWord.ActivePrinter;
            if (string.IsNullOrEmpty(this.m_PrinterName) == false)
            {
                oWord.ActivePrinter = m_PrinterName;
            }

            Object oFile = this.m_ReportSaveFileName;
            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref oFile, ref oMissing, ref oTrue,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            doc.PageSetup.FirstPageTray = Microsoft.Office.Interop.Word.WdPaperTray.wdPrinterMiddleBin;

            doc.PrintOut(ref oFalse, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oFalse,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            oWord.ActivePrinter = activePrinter;
            oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
        }

		public void SaveReport(string reportDate)
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
			this.m_ReportXml.Save(fileName);
			this.m_ReportSaveFileName = fileName;
		}
	}
}
