using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC34GTG12CDetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC34GTG12CDetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC34GTG12C";
            //this.m_Result = "KRAS DETECTED. c.34G>T (G12C) cysteine";
			this.m_ResultDescription = "c.34G>T  p.Gly12Cys (G12C)";
			//this.m_KRASResultDetail = "Mutation GGT>GTT in codon 12 (glycine>valine)";
			
		}
	}
}
