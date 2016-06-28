using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YellowstonePathology.Business
{
	public class Requisition : TifDocument
	{        
        public Requisition()
        {

        }

		public static string GetNextFileName(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
            string fileName = null;
            if (orderIdParser.IsLegacyReportNo == true)
            {
				fileName = System.IO.Path.Combine(YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser), orderIdParser.ReportNo + ".REQ.ZZZ.TIF");
            }
            else
            {
				fileName = System.IO.Path.Combine(YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser), orderIdParser.MasterAccessionNo + ".REQ.ZZZ.TIF");
            }

            fileName = fileName.ToUpper();            

            int x = 1;
            while (true)
            {
                string fileNameTemp = fileName.Replace("ZZZ", x.ToString());
                if (File.Exists(fileNameTemp) == false)
                {
                    fileName = fileNameTemp;
                    break;
                }
                else
                {
                    x += 1;
                }
            }
            return fileName;
        }        

        public static void Save(string fileName, List<System.Windows.Controls.Image> imageList)
        {            
            TiffBitmapEncoder encoder = new TiffBitmapEncoder();
            encoder.Compression = TiffCompressOption.Ccitt4;
            foreach (System.Windows.Controls.Image image in imageList)
            {
                BitmapSource bitmapSource = (BitmapSource)image.Source;
				BitmapFrame bitmapFrame = BitmapFrame.Create(bitmapSource);
				if (bitmapFrame != null)
				{
					encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
				}
            }
			if (encoder.Frames.Count > 0)
			{
                FileStream fileStream = new System.IO.FileStream(fileName, FileMode.Create);
				encoder.Save(fileStream);
				fileStream.Close();
			}
			else
			{
				throw new Exception("No frames in the scanned file encoder.");
			}            
        }

        public static void CopyToReportNo(int specimenLogId, string reportNo)
        {         
            string source = Requisition.GetSpecimenLogFileName(specimenLogId);
            string destination = Requisition.GetAccessionedFileName(reportNo);            
            File.Copy(source, destination, true);                        
        }

        public static string GetSpecimenLogFileName(int specimenLogId)
        {            
            string result = string.Empty;            
            result = YellowstonePathology.Properties.Settings.Default.SpecimenLogScannedDocumentFilePath + specimenLogId.ToString() + @".tif";            
            return result;
        }

        public static string GetAccessionedFileName(string reportNo)
        {
            string result = string.Empty;
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			result = YellowstonePathology.Business.Document.CaseDocument.GetFirstRequisitionFileName(orderIdParser);            
            return result;
        }        

        public void WriteIdentificationHeader(string fileName, string masterAccession, string reportNo)
        {            
            Image image = Image.FromFile(fileName);
            int pageCount = image.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
            List<Bitmap> bitmapList = new List<Bitmap>();

            for (int i = 0; i < pageCount; i++)
            {
                image.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, i);
                MemoryStream byteStream = new MemoryStream();
                image.Save(byteStream, System.Drawing.Imaging.ImageFormat.Bmp);

                Bitmap newBitmap = new Bitmap(Image.FromStream(byteStream));
                System.Drawing.Graphics graphicImage = System.Drawing.Graphics.FromImage(newBitmap);
                graphicImage.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                Rectangle rectangle = new Rectangle(5, 5, 325, 100);
                graphicImage.FillRectangle(System.Drawing.Brushes.White, rectangle);
                graphicImage.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Black, 1), rectangle);
                graphicImage.DrawString("MAN" + masterAccession, new System.Drawing.Font("OCRAMCE", 20, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.PointF(30, 25));
                graphicImage.DrawString("RPT" + reportNo, new System.Drawing.Font("OCRAMCE", 20, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.PointF(30, 60));
                bitmapList.Add(newBitmap);
            }

            image.Dispose();
            
            YellowstonePathology.Business.TifDocument tifDocument = new TifDocument();
            tifDocument.FileName = fileName;
            tifDocument.Create(bitmapList);            
        }        
	}
}
