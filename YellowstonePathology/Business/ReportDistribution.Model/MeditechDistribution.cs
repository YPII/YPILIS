using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MeditechDistribution
    {
        public const string DistributionType = "Meditech";

        public const string WestParkHospitalIPAddress = "165.136.181.151";
        //public const string ProductionFolderPath = @"\\" + WestParkHospitalIPAddress + @"\PathologyLive\";
        public const string ProductionFolderPath = @"\\YPIIInterface1\ChannelData\Outgoing\WestParkHospital";
        public const string UserName = @"WPHD\YPII";
        public const string Password = @"yellowstone";

        public MeditechDistribution()            
        {

        }

        public DistributionResult Distribute(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            DistributionResult result = new DistributionResult();
            result.IsComplete = true;                        

			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			string tifDocumentPath = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);
			string pdfDocumentPath = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser);

            System.IO.FileStream fileStream = new System.IO.FileStream(tifDocumentPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            System.Windows.Media.Imaging.TiffBitmapDecoder tiffBitmapDecoder = new System.Windows.Media.Imaging.TiffBitmapDecoder(fileStream, System.Windows.Media.Imaging.BitmapCreateOptions.PreservePixelFormat, System.Windows.Media.Imaging.BitmapCacheOption.Default);
            int pageCount = tiffBitmapDecoder.Frames.Count;

            YellowstonePathology.Business.ReportDistribution.Model.MeditechFileDefinition meditechFileDefinition =
                    new Business.ReportDistribution.Model.MeditechFileDefinition(pageCount, accessionOrder.SvhAccount, accessionOrder.SvhMedicalRecord, accessionOrder.MasterAccessionNo,
                        reportNo, accessionOrder.PBirthdate.Value, accessionOrder.PSex, accessionOrder.AccessionDate.Value);

            string fileName = meditechFileDefinition.GetFileName() + ".pdf";
            string saveToFileName = System.IO.Path.Combine(ProductionFolderPath, fileName);
            System.IO.File.Copy(pdfDocumentPath, saveToFileName, true);
            
            return result;
        }        
    }
}
