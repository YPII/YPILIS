using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class Distribution
    {
        protected string m_DistributionType;                

        protected Distribution(string distributionType)
        {            
            this.m_DistributionType = distributionType;
        }		

        public virtual YellowstonePathology.Business.Rules.MethodResult Distribute(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            throw new Exception("Not implemented in base.");
        }

        public virtual YellowstonePathology.Business.Rules.MethodResult IsOkToSend(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            methodResult.Success = true;
            return methodResult;
        }
    }
}
