using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2Exon1214
{
	public class JAK2Exon1214NotDetectedResult : YellowstonePathology.Business.Test.TestResult
    {
        public JAK2Exon1214NotDetectedResult()
        {
            this.m_Result = "Not Detected";
            this.m_ResultCode = "JAK2X1214NTDTCTD";
        }
    }
}
