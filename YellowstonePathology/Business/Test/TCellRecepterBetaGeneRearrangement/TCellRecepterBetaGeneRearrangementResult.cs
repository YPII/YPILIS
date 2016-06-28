using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement
{
	public class TCellRecepterBetaGeneRearrangementResult
    {
		public static string Method = "Patient DNA was isolated, purified and subjected to PCR amplification using oligonucleotide consensus primers specific for the T-cell receptor " +
			"gamma gene variable and joining regions.  PCR products are separated by capillary gel electrophoresis and detected by fluorescence.  " +
			"Typically PCR can detect clonal populations present in the 5-10% range. Various factors including quantity and quality of nucleic acid, " +
			"sample preparation and sample age can affect assay performance.";
		public static string References = "1. vanDongen J, et.al. Design and standardization of PCR primers and protocols for detection of clonal immunoglobulin and T-cell receptor " +
			"gene recombinations in suspect lymphoproliferations: Report of the BIOMED-2 Concerted Action BMH4-CT98-3936. Leukemia 2003;17:2257-2317.";

		protected string m_Result;
		protected string m_Interpretation;

		public TCellRecepterBetaGeneRearrangementResult()
		{
		}

		public void SetResults(TCellRecepterBetaGeneRearrangementTestOrder tCellRecepterBetaGeneRearrangementTestOrder)
		{
            tCellRecepterBetaGeneRearrangementTestOrder.Result = this.m_Result;
            tCellRecepterBetaGeneRearrangementTestOrder.Interpretation = this.m_Interpretation;
            tCellRecepterBetaGeneRearrangementTestOrder.Method = TCellRecepterBetaGeneRearrangementResult.Method;
            tCellRecepterBetaGeneRearrangementTestOrder.References = TCellRecepterBetaGeneRearrangementResult.References;
		}
	}
}
