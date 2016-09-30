using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class PrintDistribution 
    {
        public const string DistributionType = "Mail";

        public PrintDistribution()        
        {

        }

        public DistributionResult Distribute(string reportNo)
        {
            DistributionResult distributionResult = new DistributionResult();

            string printDistributionFolderPath = @"\\cfileserver\Documents\Distribution\Print\";
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			string caseDocumentFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameXPS(orderIdParser);

            string fileName = System.IO.Path.GetFileName(caseDocumentFileName);
            string newFileName = System.IO.Path.Combine(printDistributionFolderPath, fileName);

            if (System.IO.File.Exists(newFileName) == false)
            {                
                System.IO.File.Copy(caseDocumentFileName, newFileName);
            }

            distributionResult.IsComplete = true;
            return distributionResult;
        }
    }
}
