using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCellClonalityByPCR
{
	public class BCellClonalityByPCRNegativeResult : BCellClonalityByPCRResult
	{
		public BCellClonalityByPCRNegativeResult()
		{
			this.m_Result = "Negative for a monoclonal B-cell population.";
			this.m_ResultCode = "BCLLCLNLTYPCRNGTV";
			this.m_Interpretation = "PCR based assays can be effectively used to detect a clonal B-cell population.  The antigen receptor genes of the " +
				"immunoglobulin heavy and light chains undergo rearrangements during B-cell maturation.  Each B-cell has a single productive rearrangement " +
				"of the variable and joining regions.  When the DNA from this region is amplified in a polyclonal population, there is a bell-shaped " +
				"(Gaussian distribution) of amplicon products.  In a clonal population of cells there will be one or two prominent amplified products.  " +
				"Since the antigen receptor genes are polymorphic, multiple sets of primers targeting conserved framework regions are necessary.  The " +
				"analysis of the PCR products is NEGATIVE for a monoclonal B-cell population." + Environment.NewLine + Environment.NewLine +
				"The assay is subject to interference by degradation of DNA or inhibition of PCR by heparin or other agents.  The assay cannot reliably " +
				"detect less than 1 positive cell per 100 normal cells.  PCR based testing does not identify 100% of clonal cell populations; therefore " +
				"repeat testing by Southern blot may be advisable to rule out clonality.";
		}
	}
}
