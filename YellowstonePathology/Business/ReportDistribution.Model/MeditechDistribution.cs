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
        public const string ProductionFolderPath = @"\\YPIIInterface2\ChannelData\Outgoing\WestParkHospital";
        public const string UserName = @"WPHD\YPII";
        public const string Password = @"yellowstone";

        public MeditechDistribution()            
        {

        }

        public DistributionResult Distribute(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            DistributionResult result = new DistributionResult();
            Business.HL7View.WPH.WPHResultView resultView = new HL7View.WPH.WPHResultView(reportNo, accessionOrder, false);
            YellowstonePathology.Business.Rules.MethodResult hl7Result = new Rules.MethodResult();
            resultView.Send(hl7Result);
            result.IsComplete = hl7Result.Success;
            result.Message = hl7Result.Message;

            return result;
        }

        public YellowstonePathology.Business.Rules.MethodResult DistributeHL7(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            Business.HL7View.WPH.WPHResultView resultView = new HL7View.WPH.WPHResultView(reportNo, accessionOrder, false);
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            resultView.Send(result);
            return result;
        }
    }
}
