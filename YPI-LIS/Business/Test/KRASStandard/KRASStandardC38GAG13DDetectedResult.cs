using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC38GAG13DDetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC38GAG13DDetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC38GAG13D";
            //this.m_Result = "KRAS DETECTED. c.38G>A (G13D) aspartate";
			this.m_ResultDescription = "c.38G>A  p.Gly13Asp (G13D)";
			//this.m_KRASResultDetail = "Mutation GGC>GAC in codon 13 (glycine>aspartate)";			
		}
	}
}
