using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Xps.Packaging;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Document
{
	public partial class CaseDocument : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		private string m_DocumentId;
		private string m_MasterAccessionNo;
		private string m_ClientOrderId;
		private bool m_Received;
		private bool m_Verified;

		private string m_CaseDocumentType;
		private string m_FileName;
		private string m_FilePath;
		private string m_FullFileName;
		private bool m_Split = false;
		private bool m_Delete = false;

        public CaseDocument()
        {

        }

		[PersistentPrimaryKeyProperty(false)]
		public string DocumentId
		{
			get { return this.m_DocumentId; }
			set
			{
				if (this.m_DocumentId != value)
				{
					this.m_DocumentId = value;
					this.NotifyPropertyChanged("DocumentId");
				}
			}
		}

		[PersistentProperty()]
		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set
			{
				if (this.m_MasterAccessionNo != value)
				{
					this.m_MasterAccessionNo = value;
					this.NotifyPropertyChanged("MasterAccessionNo");
				}
			}
		}

		[PersistentProperty()]
		public string ClientOrderId
		{
			get { return this.m_ClientOrderId; }
			set
			{
				if (this.m_ClientOrderId != value)
				{
					this.m_ClientOrderId = value;
					this.NotifyPropertyChanged("ClientOrderId");
				}
			}
		}

		[PersistentProperty()]
		public bool Received
		{
			get { return this.m_Received; }
			set
			{
				if (this.m_Received != value)
				{
					this.m_Received = value;
					this.NotifyPropertyChanged("Received");
				}
			}
		}

		[PersistentProperty()]
		public bool Verified
		{
			get { return this.m_Verified; }
			set
			{
				if (this.m_Verified != value)
				{
					this.m_Verified = value;
					this.NotifyPropertyChanged("Verified");
				}
			}
		}

		public bool Split
        {
            get { return this.m_Split; }
            set { this.m_Split = value; }
        }

        public bool Delete
        {
            get { return this.m_Delete; }
            set { this.m_Delete = value;}
        }

        public string FileName
        {
            get { return this.m_FileName; }
            set { this.m_FileName = value; }
        }

        public string FilePath
        {
            get { return this.m_FilePath; }
            set { this.m_FilePath = value; }
        }

        public string FullFileName
        {
            get { return this.m_FullFileName; }
            set { this.m_FullFileName = value; }
        }

		public string Extension
		{
			get
			{
				string result = string.Empty;
				string[] dotSplit = FileName.Split('.');
				if (dotSplit.Length != 0)
				{
					result = dotSplit[dotSplit.Length - 1];
				}
				return result;
			}
		}

		public string CaseDocumentType
		{
			get { return this.m_CaseDocumentType; }
			set { this.m_CaseDocumentType = value; }
		}

        public void SetFileName(string fullFileName)
        {
            string [] slashSplit = fullFileName.Split('\\');
            this.m_FullFileName = fullFileName;
            this.m_FileName = slashSplit[slashSplit.Length - 1];
            this.m_FilePath = fullFileName.Replace(this.m_FileName, "");            
        }

        public virtual void Close()
        {

        }

        public bool IsRequisition()
        {
            bool result = false;
            if (this.m_FullFileName.ToUpper().Contains("REQ") == true) result = true;
            return result;
        }

		public virtual void Show(System.Windows.Controls.ContentControl contentControl, object writer)
		{

		}

		public static void PrintWordDoc(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {            
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;
            Object oCopies = 1;

            oWord = new Microsoft.Office.Interop.Word.Application();
            string currentPrinter = oWord.ActivePrinter;            
            oWord.Visible = false;

			Object oFile = CaseDocument.GetCaseFileNameDoc(orderIdParser);
            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref oFile, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            doc.PrintOut(ref oFalse, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oCopies, ref oMissing, ref oMissing, ref oFalse,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

			CaseDocument.ReleaseComObject(doc);
			oWord.ActivePrinter = currentPrinter;
            oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
			CaseDocument.ReleaseComObject(oWord);
        }

        public static void OpenTiff(string fileName)
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo(fileName);
            p.StartInfo = info;            
            p.Start();            
        }

        public static void OpenPDF(string fileName)
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo(fileName);
            p.StartInfo = info;
            p.Start();            
        }

        public static void OpenWordDocumentWithWordViewer(string fileName)
        {
            string newFile = string.Empty;
            if (File.Exists(fileName) == true)
            {
                //string[] dotsplit = fileName.Split('.');
                //string extenstion = dotsplit[dotsplit.Length - 1];
				//newFile = @"\\cfileserver\Documents\Distribution\OpenedWordViewerFiles\" + Guid.NewGuid() + "." + extenstion;
                //File.Copy(fileName, newFile);

                Process p1 = new Process();
                p1.StartInfo = new ProcessStartInfo("wordview.exe", fileName);
                p1.Start();                                
                p1.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("The file does not exist. " + newFile);
            }
        }

		public static string GetDraftDocumentFilePath(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string path = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
			path = path + orderIdParser.ReportNo + ".DRAFT.XML";
            return path;
        }

		public static string GetDraftDocumentFilePathDOCX(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string path = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
			path = path + orderIdParser.ReportNo + ".DRAFT.DOCX";
            return path;
        }

        public static void SaveXMLAsDocFromFileName(string fileName)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;

            Object xmlFileName = fileName;
            Object docFileName = fileName.ToUpper().Replace("XML", "DOC");

            try
            {
                File.Delete(docFileName.ToString());
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show("This file is locked and cannot be published at this time: " + reportNo);
                oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            }

            Object fileFormat = "wdFormatDocument";

            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref xmlFileName, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object oFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;

            doc.SaveAs(ref docFileName, ref oFmt, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);            

			CaseDocument.ReleaseComObject(oFmt);
			CaseDocument.ReleaseComObject(doc);
			oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
			CaseDocument.ReleaseComObject(oWord);
		}

		public static void SaveXMLAsDoc(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;

			Object xmlFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".xml";
			Object docFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".doc";
            
            try
            {
                File.Delete(docFileName.ToString());
            }
            catch (Exception)
            {                
                oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            }

            Object fileFormat = "wdFormatDocument";

            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref xmlFileName, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object oFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;            

            doc.SaveAs(ref docFileName, ref oFmt, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);            

			CaseDocument.ReleaseComObject(oFmt);
			CaseDocument.ReleaseComObject(doc);
			oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
			CaseDocument.ReleaseComObject(oWord);
		}

		public static void SaveXMLAsPDF(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;

			Object xmlFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".xml";
			Object docFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".pdf";

            try
            {
                File.Delete(docFileName.ToString());
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show("This file is locked and cannot be published at this time: " + reportNo);
                oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            }

            //Object fileFormat = "wdFormatPDF";

            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref xmlFileName, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object oFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

            doc.SaveAs(ref docFileName, ref oFmt, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);            

			CaseDocument.ReleaseComObject(oFmt);
			CaseDocument.ReleaseComObject(doc);
			oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
			CaseDocument.ReleaseComObject(oWord);
		}

		public static void SaveDOCXAsDoc(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;

			Object xmlFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".docx";
			Object docFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".doc";

            try
            {
                File.Delete(docFileName.ToString());
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show("This file is locked and cannot be published at this time: " + reportNo);
                oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            }

            Object fileFormat = "wdFormatDocument";

            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref xmlFileName, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object oFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;

            doc.SaveAs(ref docFileName, ref oFmt, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);            

			CaseDocument.ReleaseComObject(oFmt);
			CaseDocument.ReleaseComObject(doc);
			oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
			CaseDocument.ReleaseComObject(oWord);
		}

		public static void SaveDocAsXPS(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;

            string currentPrinter = oWord.ActivePrinter;
            oWord.ActivePrinter = "Microsoft XPS Document Writer";

			Object docFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".doc";
			Object xpsFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".xps";

            Object fileFormat = "wdFormatDocument";
			            
            if (System.IO.File.Exists(docFileName.ToString()) == true)
            {

                Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref docFileName, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                object oOutputFile = xpsFileName;
                doc.PrintOut(ref oFalse, ref oFalse, ref oMissing, ref oOutputFile, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oTrue, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

				CaseDocument.ReleaseComObject(doc);
			}
            
			oWord.ActivePrinter = currentPrinter;
            oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
			CaseDocument.ReleaseComObject(oWord);                			
        }

		public static int WriteTiffPages(YellowstonePathology.Business.OrderIdParser orderIdParser, long reportDistributionLogId)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = true;

            string currentPrinter = oWord.ActivePrinter;
            oWord.ActivePrinter = "Microsoft Office Document Image Writer";

			Object docFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".doc";
            Object fileFormat = "wdFormatDocument";

            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref docFileName, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object oOutputFile;
            object oPages;
            object oRange = Microsoft.Office.Interop.Word.WdPrintOutRange.wdPrintRangeOfPages;

            int pages = doc.ComputeStatistics(Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages, ref oTrue);
            for (int i = 1; i <= pages; i++)
            {
                oPages = i.ToString();
				oOutputFile = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + @"Centricity\" + reportDistributionLogId.ToString() + "." + i.ToString().PadLeft(3, '0');
                doc.PrintOut(ref oFalse, ref oFalse, ref oRange, ref oOutputFile, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oPages, ref oMissing,
                    ref oTrue, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);                
            }

            oWord.ActivePrinter = currentPrinter;            

			CaseDocument.ReleaseComObject(oRange);
			CaseDocument.ReleaseComObject(doc);
			oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
			CaseDocument.ReleaseComObject(oWord);

            return pages;
        }

		public static string GetCaseDocumentFullPath(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string filePath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
            string[] files = Directory.GetFiles(filePath);
            string fullFileName = string.Empty;

            foreach (string file in files)
            {
                string[] slashSplit = file.Split('\\');
                if (slashSplit.Length > 2)
                {
                    string fileName = slashSplit[slashSplit.Length - 1];
                    string [] dotSplit = fileName.Split('.');

					if (orderIdParser.IsLegacyReportNo == true)
					{
						if (dotSplit[0].ToUpper() == orderIdParser.ReportNo.ToUpper())
						{
							if (dotSplit[1].ToUpper() == "DOC" | dotSplit[1].ToUpper() == "DOCX")
							{
								fullFileName = file;
								break;
							}
						}
					}
					else if (dotSplit.Length > 2)
					{
						string tempName = dotSplit[0] + "." + dotSplit[1];
						if (tempName.ToUpper() == orderIdParser.ReportNo.ToUpper())
						{
							if (dotSplit[2].ToUpper() == "DOC" | dotSplit[2].ToUpper() == "DOCX")
							{
								fullFileName = file;
								break;
							}
						}
					}
                }                
            }            
            return fullFileName;
        }

		public static string GetCaseFileNameDoc(YellowstonePathology.Business.OrderIdParser orderIdParser)
		{
			string fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".doc";
			return fileName;
		}

		public static string GetCaseFileNameXml(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".xml";
            return fileName;
        }

		public static string GetPublishedReport(YellowstonePathology.Business.OrderIdParser orderIdParser, bool isDOCX)
        {
            string fileName = string.Empty;
            if (isDOCX == true)
            {
				fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".docx";
            }
            else
            {
				fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".doc";
            }
            
            return fileName;
        }

		public static string GetCaseFileNameDOCX(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".docx";
            return fileName;
        }

		public static string GetCaseFileNameXPS(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".xps";
            return fileName;
        }

		public static string GetCaseFileNamePDF(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".pdf";
            return fileName;
        }

		public static string GetCaseFileNameTif(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".tif";
            return fileName;
        }

		public static string GetCaseFileNamePatientTif(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".Patient.tif";
            return fileName;
        }

		public static string GetFirstRequisitionFileName(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".REQ.1.TIF";
            return fileName;
        }

		public static string GetFlowHistogramFileName(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo + ".HST.1.JPG";
            return fileName;
        }

		private static void ReleaseComObject(object o)
		{
			try
			{
				System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
			}
			catch { }
			finally
			{
				o = null;
			}
		}

		public static YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            methodResult.Success = true;

			string xmlFile = GetCaseFileNameXml(orderIdParser);
			string docFile = GetCaseFileNameDoc(orderIdParser);
			string xpsFile = GetCaseFileNameXPS(orderIdParser);
			string tifFile = GetCaseFileNameTif(orderIdParser);
			string pdfFile = GetCaseFileNamePDF(orderIdParser);

            try
            {
                if (System.IO.File.Exists(xmlFile) == true) System.IO.File.Delete(xmlFile);
                if (System.IO.File.Exists(docFile) == true) System.IO.File.Delete(docFile);
                if (System.IO.File.Exists(xpsFile) == true) System.IO.File.Delete(xpsFile);
                if (System.IO.File.Exists(tifFile) == true) System.IO.File.Delete(tifFile);
                if (System.IO.File.Exists(pdfFile) == true) System.IO.File.Delete(pdfFile);
            }
            catch (Exception e)
            {
                methodResult.Success = false;
                methodResult.Message = e.Message;
            }

            return methodResult;
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
