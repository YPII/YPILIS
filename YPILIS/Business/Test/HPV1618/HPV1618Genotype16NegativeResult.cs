using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618
{
    public class HPV1618Genotype16NegativeResult : HPV1618Result
    {
        public HPV1618Genotype16NegativeResult()
        {
            this.m_ResultCode = "HPV1618G16NGTV";
            this.m_Result = HPV1618Result.NegativeResult;
        }
    }
}
