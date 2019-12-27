using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Helper
{
    public static class DistributionTypeMap
    {
        private static Dictionary<string, string> DistributionToResultTypeMap;
        static DistributionTypeMap()
        {
            DistributionTypeMap.DistributionToResultTypeMap = new Dictionary<string, string>();
            DistributionTypeMap.DistributionToResultTypeMap.Add(ReportDistribution.Model.DistributionType.EPIC, Test.ResultType.EPIC);
            DistributionTypeMap.DistributionToResultTypeMap.Add(ReportDistribution.Model.DistributionType.EPICANDFAX, Test.ResultType.EPIC);
            DistributionTypeMap.DistributionToResultTypeMap.Add(ReportDistribution.Model.DistributionType.MEDITECH, Test.ResultType.WPH);
            DistributionTypeMap.DistributionToResultTypeMap.Add(ReportDistribution.Model.DistributionType.ATHENA, Test.ResultType.CMMC);
            DistributionTypeMap.DistributionToResultTypeMap.Add(ReportDistribution.Model.DistributionType.ECW, Test.ResultType.ECW);
            DistributionTypeMap.DistributionToResultTypeMap.Add(ReportDistribution.Model.DistributionType.MTDOH, Test.ResultType.MDOH);
            DistributionTypeMap.DistributionToResultTypeMap.Add(ReportDistribution.Model.DistributionType.WYDOH, Test.ResultType.WDOH);
        }
        public static string ResultTypeFromDistributionType(string distributionType)
        {
            string result;
            if(DistributionTypeMap.DistributionToResultTypeMap.TryGetValue(distributionType, out result) == false)
            {
                result = string.Empty;
            }
            return result;
        }
    }
}
