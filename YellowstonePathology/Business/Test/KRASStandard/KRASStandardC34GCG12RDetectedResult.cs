using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC34GCG12RDetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC34GCG12RDetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC34GCG12R";
            //this.m_Result = "KRAS DETECTED. c.34G>C (G12R) arginine";
			this.m_ResultDescription = "c.34G>C  p.Gly12Arg (G12R)";
			//this.m_KRASResultDetail = "Mutation GGT>GAT in codon 12 (glycine>aspartate)";			
		}
	}
}
