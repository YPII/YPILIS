using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCellClonalityByPCR
{
	public class BCellClonalityByPCRIndeterminateResult : BCellClonalityByPCRResult
	{
		public BCellClonalityByPCRIndeterminateResult()
		{
			this.m_Result = "Indeterminate";
			this.m_ResultCode = "BCLLCLNLTYPCRNDTRMNT";
			this.m_Interpretation = "PCR based assays can be effectively used to detect a clonal B-cell population.  The antigen receptor genes of " +
				"the immunoglobulin heavy and light chains undergo rearrangements during B-cell maturation.  Each B-cell has a single productive " +
				"rearrangement of the variable and joining regions.  When the DNA from this region is amplified in a polyclonal population, there " +
				"is a bell-shaped (Gaussian distribution) of amplicon products.  In a clonal population of cells there will be one or two prominent " +
				"amplified products.  Since the antigen receptor genes are polymorphic, multiple sets of primers targeting conserved framework regions " +
				"are necessary.  The analysis of the PCR products is INDETERMINATE for a monoclonal B-cell population." + Environment.NewLine + Environment.NewLine +
				"A result cannot be reported on this specimen because there was DNA of insufficient quantity or quality for analysis.";
		}
	}
}
