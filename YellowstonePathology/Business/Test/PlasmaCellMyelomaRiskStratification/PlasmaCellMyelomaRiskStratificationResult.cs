using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification
{
	public class PlasmaCellMyelomaRiskStratificationResult
	{

		protected string m_References = "1. Boyd KD, Ross FM, Chiecchio L, et al; NCRI Haematology Oncology Studies Group. A novel prognostic model in myeloma based on " +
			"cosegregating adverse FISH lesions and the ISS: analysis of patients treated in the MRC Myeloma IX trial. Leukemia. 2012;26(2):349-55.\r\n" +
			"2. Fonseca R, Van Wier SA, Chng WJ, et al. Prognostic value of chromosome 1q21 gain by fluorescent in situ hybridization and increase " +
			"CKS1B expression in myeloma. Leukemia. 2006;20(11):2034-40.\r\n" +
			"3. Chng WJ, Dispenzieri A, et al; International Myeloma Working Group. IMWG consensus on risk stratification in multiple myeloma. " +
			"Leukemia. 2014;28(2):269-77.\r\n" +
			"4. Atlas of Genetics and Cytogenetics in Oncology and Hematology http://atlasgeneticsoncology.org/";

		protected string m_TestDeveloped = "All controls were within expected ranges.\r\n" +
			"NeoGenomics Laboratories FISH test uses either FDA cleared and/or analyte specific reagent (ASR) probes.  This test was developed and its performance " +
			"characteristics determined by NeoGenomics Laboratories in Irvine CA. It has not been cleared or approved by the U.S. Food and Drug Administration (FDA).  " +
			"The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes and should not be regarded as " +
			"investigational or for research.  This laboratory is regulated under CLIA '88 as qualified to perform high complexity testing.Interphase FISH does not " +
			"include examination of the entire chromosomal complement. Clinically significant anomalies detectable by routine banded cytogenetic analysis may still " +
			"be present. Consider reflex banded cytogenetic analysis.";

		public PlasmaCellMyelomaRiskStratificationResult()
		{
		}

		public void SetResults(PlasmaCellMyelomaRiskStratificationTestOrder testOrder)
		{
			testOrder.References = this.m_References;

			StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine(testOrder.GetLocationPerformedComment());
			disclaimer.AppendLine();
			disclaimer.AppendLine(m_TestDeveloped);
			testOrder.ReportDisclaimer = disclaimer.ToString();
		}
	}
}
