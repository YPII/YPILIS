using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC37GCG13RDetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC37GCG13RDetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC37GCG13R";
            //this.m_Result = "KRAS DETECTED. c.37G>C (G13R) arginine";
			this.m_ResultDescription = "'c.37G>C  p.Gly13Arg (G13R)";
			//this.m_KRASResultDetail = "Mutation GGC>AGC in codon 13 (glycine>serine)";			
		}
	}
}
