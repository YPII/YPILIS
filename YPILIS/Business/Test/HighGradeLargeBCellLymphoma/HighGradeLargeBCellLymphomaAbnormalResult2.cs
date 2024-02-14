using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma
{
	public class HighGradeLargeBCellLymphomaAbnormalResult2 : HighGradeLargeBCellLymphomaResult
	{
		public HighGradeLargeBCellLymphomaAbnormalResult2()
		{
			this.m_Result = "ABNORMAL - Gains of BCL6, MYC, IgH/BCL2 - Negative for MYC translocation";

			this.m_Interpretation = "POSITIVE FOR ADDITIONAL COPIES OF THE BCL6 GENE, MYC gene and the IgH/BCL2 genes.  Fluorescence in situ hybridization " +
				"(FISH) analysis was performed using a NHL specific probe set to detect abnormalities commonly associated with large B-cell lymphoma.  " +
				"The abnormal signal pattern (>2F) was observed with the BCL6 break apart probe in 78% of the analyzed nuclei.  The normal reference " +
				"range is <10% and as such, this represents an ABNORMAL result indicative of a gains or extra copies of the BCL6 gene region on " +
				"chromosome 3 or extra copies of chromosome 3. Increased copies of the other genes were also identified.\r\n\r\n" +
				"Therefore, while this study is ABNORMAL, NO EVIDENCE OF A \"DOUBLE/TRIPLE HIT\" LYMPHOMA was identified. The high risk " +
				"\"double/triple\" hit lymphomas are characterized by a MYC translocation combined with a BCL6 and/or BCL2 translocation.";

			this.m_ProbeSetDetail = "BCL6 (3q27): The probe shows a 3F (yellow) signal pattern. This represents an abnormal result indicating gains of the BCL6 gene. " +
				"However it is negative for the BCL6 gene rearrangement.\r\n" +
				"IgH/BCL2 t(14;18): Interphase FISH was performed with a dual color, dual fusion IgH/BCL2 probe set (Abbott Molecular, Des Plaines, IL) " +
				"used to detect the (14;18) translocation most commonly associated with follicular lymphoma. At least 200 interphase nuclei were scored " +
				"and gains of IgH and BCL2 were identified. However, no translocation was identified.\r\n" +
				"MYC (8q24): Normal signal pattern 2F was observed as well as >=3F. This is negative for MYC gene rearrangement.";
		}
	}
}
