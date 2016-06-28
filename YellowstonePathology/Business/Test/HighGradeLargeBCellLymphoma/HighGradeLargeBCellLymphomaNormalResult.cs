using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma
{
	public class HighGradeLargeBCellLymphomaNormalResult : HighGradeLargeBCellLymphomaResult
	{
		public HighGradeLargeBCellLymphomaNormalResult()
		{
			this.m_Result = "NORMAL";

			this.m_Interpretation = "Normal results were observed for the BCL6, MYC, and t(14;18) probe sets.  Fluorescence in situ hybridization " +
				"(FISH) analysis was performed using probes specific for rearrangements involving BCL6, MYC, and IgH/BCL2 [t(14;18)], which are " +
				"reported in high-grade/large B-cell lymphomas.  All signals were within the normal reference range.  This finding represents a " +
				"NORMAL result indicating the absence of gene rearrangements involving BCL2, BCL6, MYC, and IGH.";

			this.m_ProbeSetDetail = "BCL6 (3q27): The probe shows a normal 2F signal pattern.  This represents a NORMAL result, negative for a BCL6 gene rearrangement.\r\n" +
				"IgH/BCL2 t(14;18): The probe shows a normal 2R2G signal pattern. This represents a NORMAL result, negative for an IgH/BCL2 gene rearrangement.\r\n" +
				"MYC (8q24): Normal signal pattern 2F was observed. This is negative for MYC gene rearrangement.";
		}
	}
}
