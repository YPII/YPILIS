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

namespace YellowstonePathology.Business.Helper
{
    public class FileConversionHelper
    {
        public static void SaveDocAsXPS(string fileName)
        {
            Microsoft.Office.Interop.Word.Application oWord;
            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;

            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = false;

            string currentPrinter = oWord.ActivePrinter;
            oWord.ActivePrinter = "Microsoft XPS Document Writer";

            Object docFileName = fileName;
            Object xpsFileName = fileName.Replace(".doc", ".xps");

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

        public static bool ConvertXpsDocumentToPdf(string reportNo)
        {
            bool result = true;

			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			string inputFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameXPS(orderIdParser);
			string outputFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser);
            string gxpsFilePath = "C:\\Program Files\\Yellowstone Pathology Institute\\gxps.exe";
            string arguments =  " -sDEVICE=pdfwrite -sOutputFile=" + outputFileName + " -dNOPAUSE " + inputFileName;

            Process process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = gxpsFilePath;
            processStartInfo.Arguments = arguments;

            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit();

            return result;
        }       

        static public void SaveXpsReportToTiff(string reportNo)
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			string inputFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameXPS(orderIdParser);
			string outputFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);

            if (File.Exists(inputFileName) == true)
            {                
                XpsDocument xpsDoc = new XpsDocument(inputFileName, System.IO.FileAccess.Read);                
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

                FileStream pageOutStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write);
                encoder.Save(pageOutStream);
                pageOutStream.Close();

                xpsDoc.Close();
            }
        }

		public static void SaveXpsAsMultiPageTif(string reportNo, object visual)
		{
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			string outputFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);
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
											300d, // WPF (Avalon) units are 96dpi based
											300d,
											System.Windows.Media.PixelFormats.Default);

				renderTarget.Render(docPage.Visual);
				encoder.Frames.Add(BitmapFrame.Create(renderTarget));
			}

			FileStream outputFileStream = new FileStream(outputFileName, FileMode.Create);
			encoder.Save(outputFileStream);
			outputFileStream.Close();
		}
	}
}
