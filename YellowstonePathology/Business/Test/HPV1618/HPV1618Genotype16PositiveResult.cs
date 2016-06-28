using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618
{
    public class HPV1618Genotype16PositiveResult : HPV1618Result
    {
        public HPV1618Genotype16PositiveResult()
        {
            this.m_ResultCode = "HPV1618G16PSTV";
            this.m_Result = HPV1618Result.PositiveResult;
        }
    }
}
