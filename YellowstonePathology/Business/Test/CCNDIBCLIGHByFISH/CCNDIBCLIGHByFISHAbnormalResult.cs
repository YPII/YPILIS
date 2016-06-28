using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH
{
	public class CCNDIBCLIGHByFISHAbnormalResult : CCNDIBCLIGHByFISHResult
	{
		public CCNDIBCLIGHByFISHAbnormalResult()
		{
			this.m_Result = "Abnormal";
			this.m_Interpretation = "POSITIVE FOR ADDITIONAL COPIES OF THE CCND1(BCL1) GENE OR CHROMOSOME 11\r\n" +
				"Negative for t(11;14).\r\n" +
				"Normal result was seen in the 14(IgH) probe set.\r\n\r\n" +
				"Fluorescence in situ hybridization (FISH) analysis was performed using a dual color, dual fusion CCND1(BCL1)/IgH probe set used to detect " +
				"the (11;14) translocation most commonly associated with mantle cell lymphoma.  This interphase study revealed an abnormal signal pattern " +
				"(3-4R2G, 67.5%, negative less than 6%, borderline positive 6-15%) in the 11;14(CCND1/IGH) probe set.  This ABNORMAL signal pattern is " +
				"suggestive of gene duplication in the 11q13 locus or polysomy 11.  As such, this represents an ABNORMAL result.  The probe 14(IgH) " +
				"signals were within the normal reference range";
			this.m_ProbeSetDetail = "CCND1/IgH t(11;14): nuc ish(CCND1x3-4,IGHx2)[135/200]";
			this.m_References = "Perry AM, et al. Br J Haematol. 2013; 162(1):40-9.\r\n" +
				"http://atlasgeneticsoncology.org/";
		}
	}
}
