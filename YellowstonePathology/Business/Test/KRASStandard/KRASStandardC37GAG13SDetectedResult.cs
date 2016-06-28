using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC37GAG13SDetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC37GAG13SDetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC37GAG13S";
            //this.m_Result = "KRAS DETECTED. c.37G>A (G13S) serine";
			this.m_ResultDescription = "c.37G>A  p.Gly13Ser (G13S)";
			//this.m_KRASResultDetail = "Mutation GGC>TGC in codon 13 (glycine>cysteine)";			
		}
	}
}
