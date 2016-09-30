using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618
{
    public class HPV1618Genotype18PositiveResult : HPV1618Result
    {
        public HPV1618Genotype18PositiveResult()
        {
            this.m_ResultCode = "HPV1618G18PSTV";
            this.m_Result = HPV1618Result.PositiveResult;
        }
    }
}
