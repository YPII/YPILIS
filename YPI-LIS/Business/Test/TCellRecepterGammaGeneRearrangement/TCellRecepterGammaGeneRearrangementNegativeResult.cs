using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.TCellRecepterGammaGeneRearrangement
{
	public class TCellRecepterGammaGeneRearrangementNegativeResult : TCellRecepterGammaGeneRearrangementResult
    {
		public TCellRecepterGammaGeneRearrangementNegativeResult()
		{
			this.m_Result = "Negative";
			this.m_Interpretation = "Polymerase chain reaction (PCR) assays are routinely used for the identification of clonal T-cell populations. Clonal T cell populations are " +
				"highly suggestive of T cell malignancies and are useful in the diagnosis, staging or monitoring of T-cell lymphoproliferative diseases.  Rarely, " +
				"reactive conditions can also show clonal T-cell populations using PCR.  In addition, a negative result does not entirely exclude the presence " +
				"of a clonal T-cell receptor gene rearrangement in all cases.  Up to 20% of T-cell lymphoproliferative disorders can be negative by PCR " +
				"evaluation.  So, results must be interpreted within the context of clinical, morphologic and immunophenotypic findings.";
		}
	}
}
