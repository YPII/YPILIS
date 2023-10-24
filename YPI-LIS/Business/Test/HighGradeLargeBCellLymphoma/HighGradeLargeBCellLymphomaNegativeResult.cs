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

			this.m_Interpretation = "Normal results were observed for the BCL6, MYC, and t(14;18) probe sets." + Environment.NewLine + Environment.NewLine +
                "Fluorescence in situ hybridization (FISH)analysis was performed using probes specific for rearrangements involving BCL6, MYC, and t(14; 18), which are " +
                "reported in high - grade / large B - cell lymphomas.All signals were within the normal reference range. This finding represents a NORMAL result indicating " +
                "the absence of gene rearrangements involving BCL2, BCL6, MYC, and IGH.";

            this.m_ProbeSetDetail = "IgH/BCL2 t(14;18): Normal signal pattern 2R2G was observed. This is a NORMAL result negative for IgH/BCL2 fusion." + Environment.NewLine +
                "MYC(8q24): Normal signal pattern 2F was observed. This is negative for MYC gene rearrangement." + Environment.NewLine +
                "BCL6(3q27): The probe shows a normal 2F(yellow) signal pattern.This represents a NORMAL result negative for BCL6 gene rearrangement.";
        }
    }
}
