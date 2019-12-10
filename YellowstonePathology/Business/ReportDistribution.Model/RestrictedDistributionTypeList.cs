using System;
using System.Collections.Generic;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class RestrictedDistributionTypeList : List<string>
    {
        public RestrictedDistributionTypeList()
        {
            this.Add(DistributionType.MTDOH);
            this.Add(DistributionType.WYDOH);
        }
    }
}
