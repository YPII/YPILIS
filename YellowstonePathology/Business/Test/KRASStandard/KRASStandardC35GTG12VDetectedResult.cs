using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardC35GTG12VDetectedResult : KRASStandardDetectedResult
	{
		public KRASStandardC35GTG12VDetectedResult()
		{
			//this.m_ResultCode = "KRSSTDDTCTDC35GTG12V";
            //this.m_Result = "KRAS DETECTED. c.35G>T (G12V) valine";
			this.m_ResultDescription = "c.35G>T  p.Gly12Val (G12V)";
			//this.m_KRASResultDetail = "Mutation GGT>AGT in codon 12 (glycine>serine)";			
		}
	}
}
