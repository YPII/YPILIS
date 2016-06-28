using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma
{
	public class HighGradeLargeBCellLymphomaNegativeResult : HighGradeLargeBCellLymphomaResult
	{
		public HighGradeLargeBCellLymphomaNegativeResult()
		{
			this.m_Result = "NEGATIVE";

			this.m_Interpretation = "Fluorescence in situ hybridization (FISH) analysis was performed using probes specific for rearrangements involving " +
				"BCL6, MYC, and t (14;18), which are reported in high-grade/large B-cell lymphomas.  Increased signals were identified in all probe sets, " +
				"suggesting polysomy.  While this represents an abnormal result, there is no presence of gene rearrangements involving BCL2, BCL6, MYC, " +
				"and IGH, and the results are considered NEGATIVE.";

			this.m_ProbeSetDetail = "IgH/BCL2 t(14;18): A subpopulation of cells contains increased signals indicating polysomy. There is no evidence of " +
				"a IGH/BCL2 translocation.\r\n" +
				"MYC (8q24): A subpopulation of cells contains increased signals indicating polysomy. There is no evidence of a MYC translocation.\r\n" +
				"BCL6 (3q27): A subpopulation of cells contains increased signals indicating polysomy. There is no evidence of a BCL6 translocation.";
		}
	}
}
