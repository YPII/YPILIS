using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC37GTG13CDetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC37GTG13CDetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC37GTG13C";
            //this.m_Result = "KRAS DETECTED. c.37G>T (G13C) cysteine";
			this.m_ResultDescription = "c.37G>T  p.Gly13Cys (G13C)";
			//this.m_KRASResultDetail = "Mutation GGC>GCG in codon 13 (glycine>arginine)";			
		}
	}
}
