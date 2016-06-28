using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC38GTG13VDetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC38GTG13VDetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC38GTG13V";
            //this.m_Result = "KRAS DETECTED. c.38G>T (G13V) valine";
			this.m_ResultDescription = "c.38G>T  p.Gly13Val (G13V)";
			//this.m_KRASResultDetail = "Mutation GGC>GTC in codon 13 (glycine>valine)";			
		}
	}
}
