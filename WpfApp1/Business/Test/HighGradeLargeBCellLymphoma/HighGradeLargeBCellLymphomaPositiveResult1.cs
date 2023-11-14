using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma
{
	public class HighGradeLargeBCellLymphomaPositiveResult1 : HighGradeLargeBCellLymphomaResult
	{
		public HighGradeLargeBCellLymphomaPositiveResult1()
		{
			this.m_Result = "Positive for BCL6 rearrangement and gains of MYC; negative for MYC translocation";

			this.m_Interpretation = "POSITIVE FOR BCL6 gene rearrangement. Normal results are seen for the t(14;18) probes. Fluorescence in situ hybridization (FISH) " + 
				"analysis was performed using probes specific for rearrangements involving BCL6, MYC, and t(14;18) which are reported in high-grade/large " +
				"B-cell lymphomas. This interphase FISH study revealed a break apart signal pattern (1R1G1F, 12%, normal < /= 5%) in the BCL6 probe set. " +
				"This is an ABNORMAL result indicative of a BCL6 gene rearrangement.\r\n" +
				"Rearrangements of the BCL6 locus are commonly associated with B-cell lymphoma/leukemia.\r\n\r\n" +
				"POSITIVE FOR ADDITIONAL COPIES OF THE MYC GENE OR CHROMOSOME 8. The abnormal signal pattern (>2F) was observed with " +
				"the MYC break apart probe in 18% of the analyzed nuclei. The normal reference range is <10% and as such, this represents an " +
				"ABNORMAL result indicative of a gains or extra copies of the MYC gene region on chromosome 8 or extra copies of chromosome 8.\r\n\r\n" +
				"Therefore, while this study is ABNORMAL, NO EVIDENCE OF A \"DOUBLE/TRIPLE HIT\" LYMPHOMA was identified. The high risk " +
				"\"double/triple\" hit lymphomas are characterized by a MYC translocation combined with a BCL6 and/or BCL2 translocation.";

			this.m_ProbeSetDetail = "BCL6 (3q27): The break apart signal pattern 1R1G1F was observed. This represents an ABNORMAL result indicative of BCL6 gene " +
				"rearrangement with an unidentified chromosomal locus.\r\n" +
				"IgH/BCL2 t(14;18): Interphase FISH was performed with a dual color, dual fusion IgH/BCL2 probe set (Abbott Molecular, Des Plaines, IL) " +
				"used to detect the (14;18) translocation most commonly associated with follicular lymphoma. at least 200 interphase nuclei were scored and " +
				"all probe signals were within the normal reference range. This represents a NORMAL result.\r\n" +
				"MYC (8q24): Normal signal pattern 2F was observed in 81% of cells. There was a gain of MYC in 18% of cells. This is negative for MYC " +
				"gene rearrangement.";
		}
	}
}
