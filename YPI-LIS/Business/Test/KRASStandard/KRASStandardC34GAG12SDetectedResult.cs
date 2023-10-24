using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC34GAG12SDetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC34GAG12SDetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC34GAG12S";
            //this.m_Result = "c.35G>A, p.Gly12Asp (G12D) mutation detected";
			this.m_ResultDescription = "c.34G>A  p.Gly12Ser (G12S)";
			//this.m_KRASResultDetail = "Mutation GGT>GCT in codon 12 (glycine>alanine)";			
		}
	}
}














