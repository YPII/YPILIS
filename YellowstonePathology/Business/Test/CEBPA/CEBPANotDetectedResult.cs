using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CEBPA
{
	public class CEBPANotDetectedResult : CEBPAResult
	{
		public CEBPANotDetectedResult()
		{
			this.m_Result = "Not Detected";
			this.m_SNPResult = "GG";
			this.m_Interpretation = "CCAAT/enhancer-binding protein alpha (CEBPA) mutations have been reported in patients with acute myeloid leukemia (AML).  The mutation " +
				"has been shown to be associated with good outcome in the cytogenetic normal or intermediate-risk subgroup, particularly in patients with " +
				"double mutations. Multiple single nucleotide polymorphisms (SNPs) have been reported in CEBPA gene.  The 6 bp duplication (p." +
				"H191_P192 dup) is a common polymorphism.  However, the SNP rs34529039 TT genotype (690g>t, T230 silent) has been reported to be " +
				"associated with shorter event-free survival (EFS) and shorter time-to-relapse.  This test is to determine the prognosis of newly diagnosed " +
				"AML with normal cytogenetics.";
			this.m_Method = "The CEBPA mutation analysis is performed by both fragment analysis and Sanger sequencing methods.  Patient DNA is extracted; two " +
				"overlapping PCR products covering the entire CEBPA coding region are amplified.  The products were sequenced in both directions, point " +
				"mutations and SNP are identified by SeqScape using Genebank ID#HSU34070 as reference.  Fragment length analysis is performed to " +
				"further determine very low levels of heterozygous insertions/deletions which may missed by sequencing.  All mutations, heterozygous indels " +
				"and SNP rs34529039 will be reported. This assay has a sensitivity of 10-15% for detecting point mutations and a sensitivity of 5% for " +
				"detecting heterozygous insertion/deletions in the wild-type background.  Various factors including quantity and quality of nucleic acid, sample " +
				"preparation and sample age can affect assay performance.";
			this.m_References = "1. Vera Grossmann et.al. Strategy for Robust Detection of Insertions, Deletions, and point Mutations in CEBPA, a GC-Rich content Gene, " +
				"Using Next-Generation Deep-Sequencing Technology. The Journal of Molecular Diagnostics, Vol.13, No.2, March 2011. P129-136.\r\n" +
				"2. Mahmoud Aljurf et.al. CEBPA Single Nucleotide polymorphism (SNP) rs34529039 as an Independent Adverse Prognostic Factor in Acute " +
				"Myeloid Leukemia Treated with Allogeneic Hematopoietic Stem Cell Transplantation. ASH 2011 Poster #1465";
		}
	}
}
