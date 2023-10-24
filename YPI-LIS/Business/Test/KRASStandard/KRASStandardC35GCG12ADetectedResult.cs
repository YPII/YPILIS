using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC35GCG12ADetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC35GCG12ADetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC35GCG12A";
            //this.m_Result = "KRAS DETECTED. c.35G>C (G12A) alanine";
			this.m_ResultDescription = "c.35G>C  p.Gly12Ala (G12A)";
			//this.m_KRASResultDetail = "Mutation GGT>TGT in codon 12 (glycine>cysteine)";			
		}
	}
}
