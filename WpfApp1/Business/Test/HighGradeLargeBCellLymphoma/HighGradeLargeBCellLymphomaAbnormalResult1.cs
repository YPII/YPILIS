using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma
{
	public class HighGradeLargeBCellLymphomaAbnormalResult1 : HighGradeLargeBCellLymphomaResult
	{
		public HighGradeLargeBCellLymphomaAbnormalResult1()
		{
			this.m_Result = "ABNORMAL";

			this.m_Interpretation = "POSITIVE FOR BCL6 gene rearrangement. Normal results are seen for the MYC and t(14;18) probes. Fluorescence in " +
				"situ hybridization (FISH) analysis was performed using probes specific for rearrangements involving BCL6, MYC, and t(14;18) which " +
				"are reported in highgrade/large B-cell lymphomas. This interphase FISH study revealed a break apart signal pattern (1R1G1F, 81%, " +
				"normal </= 3.1%) in the BCL6 probe set. The remaining probe sets were within normal reference ranges. This is an ABNORMAL result " +
				"indicative of a BCL6 gene rearrangement.\r\n\r\n" +
				"Rearrangements of the BCL6 locus are commonly associated with B-cell lymphoma/leukemia.\r\n" +
				"Therefore, while this study is abnormal, NO EVIDENCE OF A \"DOUBLE/TRIPLE HIT\" LYMPHOMA was identified. The high risk \"double/triple\" " +
				"hit lymphomas are characterized by a MYC translocation combined with a BCL6 and/or BCL2 translocation.";

			this.m_ProbeSetDetail = "BCL6 (3q27): The break apart signal pattern 1R1G1F was observed.  This represents an ABNORMAL result indicative " +
				"of BCL6 gene rearrangement with an unidentified chromosomal locus.\r\n" +
				"IgH/BCL2 t(14;18): Interphase FISH was performed with a dual color dual fusion IgH/BCL2 probe set (Abbott Molecular, Des Plaines, IL )" +
				"used to detect the (14;18) translocation most commonly associated with follicular lymphoma. At least 200 interphase nuclei were scored " +
				"and probe signals were within normal reference ranges. This represents a NORMAL result.\r\n" +
				"MYC (8q24): Normal signal pattern 2F was observed. This is negative for MYC gene rearrangement.";
		}
	}
}
