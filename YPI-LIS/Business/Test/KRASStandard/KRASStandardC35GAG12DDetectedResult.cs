using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC35GAG12DDetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC35GAG12DDetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC35GAG12D";
            //this.m_Result = "KRAS DETECTED. c.35G>A (G12D) aspartate";
			this.m_ResultDescription = "c.35G>A  p.Gly12Asp (G12D)";
			//this.m_KRASResultDetail = "Mutation GGT>CGT in codon 12 (glycine>arginine)";
			
		}
	}
}
