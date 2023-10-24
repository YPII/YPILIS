using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC38GCG13ADetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC38GCG13ADetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC38GCG13A";
            //this.m_Result = "KRAS DETECTED. c.38G>C (G13A) alanine";
			this.m_ResultDescription = "c.38G>C  p.Gly13Ala (G13A)";
			//this.m_KRASResultDetail = "Mutation GGC>GCC in codon 13 (glycine>alanine)";			
		}
	}
}
