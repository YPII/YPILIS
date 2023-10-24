using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Xps.Serialization;
using System.Windows.Xps.Packaging;
using System.IO.Packaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using YellowstonePathology.Business.Document;

namespace YellowstonePathology.Business.Helper
{
    public class FileConversionHelper
    {
        public static void CreateXPSFromPNGFiles(string sourceImagesPath, string xpsFIlePath)
        {           
            var bitmaps = new List<System.Drawing.Bitmap>();
            foreach (var file in Directory.EnumerateFiles(sourceImagesPath, "*.png"))
            {
                bitmaps.Add(new System.Drawing.Bitmap(file));
            }

            FixedDocument doc = new FixedDocument();
            foreach (var bitmap in bitmaps)
            {
                ImageSource imageSource;
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Position = 0;
                    imageSource = BitmapFrame.Create(stream,
                        BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                }
                var page = new FixedPage();
                page.Children.Add(new System.Windows.Controls.Image { Source = imageSource });
                doc.Pages.Add(new PageContent { Child = page });
            }

            var xps = new XpsDocument(xpsFIlePath, FileAccess.Write, System.IO.Packaging.CompressionOption.Fast);
            var writer = XpsDocument.CreateXpsDocumentWriter(xps);
            writer.Write(doc);
            xps.Close();            
        }

        public static void SaveFixedDocumentToXPS(FixedDocument document, string filePath)
        {
            var xps = new XpsDocument(filePath, FileAccess.Write, CompressionOption.Maximum);
            var writer = XpsDocument.CreateXpsDocumentWriter(xps);
            writer.Write(document);
            xps.Close();
        }

        public static void ConvertDocumentTo(YellowstonePathology.Business.OrderIdParser orderIdParser, CaseDocumentTypeEnum caseDocumentType,
            CaseDocumentFileTypeEnum fromType, CaseDocumentFileTypeEnum toType)
        {            
            string filePath = Business.Document.CaseDocumentPath.GetPath(orderIdParser) + orderIdParser.ReportNo;

            switch (caseDocumentType)
            {
                case CaseDocumentTypeEnum.CaseReport:
                    //do nothing
                    break;
                case CaseDocumentTypeEnum.AdditionalTestingNotification:
                    filePath = filePath + ".notify";
                    break;
                case CaseDocumentTypeEnum.PreauthorizationRequest:
                    filePath = filePath + ".preauth";
                    break;
            }

            if (fromType == CaseDocumentFileTypeEnum.xml && toType == CaseDocumentFileTypeEnum.doc)
            {
                ConvertXMLToDoc(filePath + ".xml", filePath + ".doc");
            }
            else if (fromType == CaseDocumentFileTypeEnum.doc && toType == CaseDocumentFileTypeEnum.xps)
            {
                ConvertDocToXPS(filePath + ".doc", filePath + ".xps");
            }
            else if (fromType == CaseDocumentFileTypeEnum.xps && toType == CaseDocumentFileTypeEnum.tif)
            {
                ConvertXPSToTIF(filePath + ".xps", filePath + ".tif");
            }
            else if (fromType == CaseDocumentFileTypeEnum.xml && toType == CaseDocumentFileTypeEnum.pdf)
            {
                ConvertXMLToPDF(filePath + ".xml", filePath + ".pdf");
            }
            else if(fromType == CaseDocumentFileTypeEnum.doc && toType == CaseDocumentFileTypeEnum.xml)
            {
                ConvertDocToXML(filePath + ".doc", filePath + ".xml");
            }
        }

        public static void ConvertXMLToPDF(object xmlFilename, object pdfFileName)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;
            oWord.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;

            try
            {
                File.Delete(pdfFileName.ToString());
            }
            catch (Exception)
            {                
                oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            }

            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref xmlFilename, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object oFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

            doc.SaveAs(ref pdfFileName, ref oFmt, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            
            CaseDocument.ReleaseComObject(oFmt);
            CaseDocument.ReleaseComObject(doc);
            oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            CaseDocument.ReleaseComObject(oWord);
        }

        public static void ConvertXMLToDoc(object xmlFileName, object docFileName)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;
            oWord.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;

            if (File.Exists(docFileName.ToString()))
            {              
                try
                {
                    File.Delete(docFileName.ToString());
                }
                catch (Exception)
                {
                    oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
                }            
            }

            Object fileFormat = "wdFormatDocument";

            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref xmlFileName, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object oFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;

            doc.SaveAs(ref docFileName, ref oFmt, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            //CaseDocument.ReleaseComObject(oFmt);
            //CaseDocument.ReleaseComObject(doc);
            oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            //CaseDocument.ReleaseComObject(oWord);
        }

        public static void ConvertDocToXPS(object docFileName, object xpsFileName)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;
            oWord.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;

            string currentPrinter = oWord.ActivePrinter;
            oWord.ActivePrinter = "Microsoft XPS Document Writer";           

            Object fileFormat = "wdFormatDocument";

            if (System.IO.File.Exists(docFileName.ToString()) == true)
            {
                Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref docFileName, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                object oOutputFile = xpsFileName;
                doc.PrintOut(ref oFalse, ref oFalse, ref oMissing, ref oOutputFile, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oTrue, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);                
            }

            oWord.ActivePrinter = currentPrinter;
            oWord.Quit(ref oFalse, ref oMissing, ref oMissing);            
        }

        public static bool ConvertXPSToPDF(string xpsFileName, string pdfFileName)
        {
            bool result = true;
			
            string gxpsFilePath = "C:\\Program Files\\Yellowstone Pathology Institute\\gxps.exe";
            string arguments =  " -sDEVICE=pdfwrite -sOutputFile=" + xpsFileName + " -dNOPAUSE " + pdfFileName;

            Process process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = gxpsFilePath;
            processStartInfo.Arguments = arguments;

            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit();

            return result;
        }       

        static public void ConvertXPSToTIF(string xpsFileName, string tifFileName)
        {			
            if (File.Exists(xpsFileName) == true)
            {                
                XpsDocument xpsDoc = new XpsDocument(xpsFileName, System.IO.FileAccess.Read);                
                FixedDocumentSequence docSeq = xpsDoc.GetFixedDocumentSequence();
                int pages = docSeq.DocumentPaginator.PageCount;

                TiffBitmapEncoder encoder = new TiffBitmapEncoder();                
				encoder.Compression = TiffCompressOption.Default;				
			    encoder.Compression = TiffCompressOption.Ccitt4;				    
                
                for (int pageNum = 0; pageNum < pages; pageNum++)
                {
                    DocumentPage docPage = docSeq.DocumentPaginator.GetPage(pageNum);
                    RenderTargetBitmap renderTarget =
						new RenderTargetBitmap((int)(docPage.Size.Width * 300 / 96),
												(int)(docPage.Size.Height * 300 / 96),
                                                300d, 
                                                300d,
                                                System.Windows.Media.PixelFormats.Default);

                    renderTarget.Render(docPage.Visual);
                    encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                }

                FileStream pageOutStream = new FileStream(tifFileName, FileMode.Create, FileAccess.Write);
                encoder.Save(pageOutStream);
                pageOutStream.Close();

                xpsDoc.Close();
            }
        }

		public static void SaveXpsAsMultiPageTif(string reportNo, object visual)
		{
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			string outputFileName = Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);
			FrameworkElement frameworkElement = (FrameworkElement)visual;

			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)frameworkElement.ActualWidth,
									  (int)frameworkElement.ActualHeight, 96d, 96d, PixelFormats.Default);
			renderTargetBitmap.Render(frameworkElement);

			TiffBitmapEncoder tiffBitmapEncoder = new TiffBitmapEncoder();
			tiffBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

			using (Stream stream = File.Create(outputFileName))
			{
				tiffBitmapEncoder.Save(stream);
			}
		}        

        public static void SaveFixedDocumentAsTiff(FixedDocument document, string outputFileName)
		{
			int pages = document.DocumentPaginator.PageCount;

			TiffBitmapEncoder encoder = new TiffBitmapEncoder();
            encoder.Compression = TiffCompressOption.Ccitt4;

			for (int pageNum = 0; pageNum < pages; pageNum++)
			{
				DocumentPage docPage = document.DocumentPaginator.GetPage(pageNum);

				RenderTargetBitmap renderTarget =
					new RenderTargetBitmap((int)(docPage.Size.Width * 300 / 96),
											(int)(docPage.Size.Height * 300 / 96),
											300d,
											300d,
											System.Windows.Media.PixelFormats.Default);

				renderTarget.Render(docPage.Visual);
				encoder.Frames.Add(BitmapFrame.Create(renderTarget));
			}

			FileStream outputFileStream = new FileStream(outputFileName, FileMode.Create);
			encoder.Save(outputFileStream);
			outputFileStream.Close();
		}

        public static void ConvertDocToXML(object docFileName, object xmlFileName)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;
            oWord.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;

            try
            {
                File.Delete(xmlFileName.ToString());
            }
            catch (Exception)
            {
                oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            }

            Object fileFormat = "wdFormatXMLDocument";

            Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref docFileName, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object oFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;

            doc.SaveAs(ref xmlFileName, ref oFmt, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            CaseDocument.ReleaseComObject(oFmt);
            CaseDocument.ReleaseComObject(doc);
            oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
            CaseDocument.ReleaseComObject(oWord);
        }

        public static void publishCaseDocs(string filePath, string reportNo)
        {            
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;
            oWord.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;

            string currentPrinter = oWord.ActivePrinter;
            oWord.ActivePrinter = "Microsoft XPS Document Writer";

            Object fileFormat = "wdFormatDocument";

            object xmlFile = $"{filePath}.xml";
            if (System.IO.File.Exists(xmlFile.ToString()) == true)
            {
                Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Open(ref xmlFile, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                object oOutputFileXps = $"{filePath}.xps";
                doc.PrintOut(ref oFalse, ref oFalse, ref oMissing, ref oOutputFileXps, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oTrue, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                object oFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
                object oOutputFilePdf = $"{filePath}.pdf";
                doc.SaveAs(ref oOutputFilePdf, ref oFmt, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                string outputFileTif = $"{filePath}.tif";
                FileConversionHelper.ConvertXPSToTIF(oOutputFileXps.ToString(), outputFileTif);
            }

            oWord.ActivePrinter = currentPrinter;
            oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
        }
    }
}
