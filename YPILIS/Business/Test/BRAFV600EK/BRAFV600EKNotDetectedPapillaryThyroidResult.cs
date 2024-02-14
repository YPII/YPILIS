using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	public class BRAFV600EKNotDetectedPapillaryThyroidResult : BRAFV600EKNotDetectedResult
	{		
		public BRAFV600EKNotDetectedPapillaryThyroidResult()
		{
            YellowstonePathology.Business.Test.IndicationPapillaryThyroid indication = new IndicationPapillaryThyroid();
            this.m_Indication = indication.IndicationCode;
            this.m_IndicationComment = indication.Description;
		    this.m_Comment = "False negatives may occur if the sample does not contain a sufficient quantity of tumor DNA.  If this test was performed on a " +
			    "small/scant sample and subsequent surgical excision confirms the presence of papillary thyroid carcinoma, retesting on the surgical specimen is recommended.";		    
		    this.m_Interpretation = "Papillary thyroid carcinoma (PTC) is one of the most common endocrine malignancies and accounts for " +
			    "approximately 80% of all thyroid cancers.  The BRAF gene, a member of the RAS-RAF-MEK-MAPK pathway that drives tumor growth and progression, has been found to play " +
			    "a pivotal role in PTC oncogenesis.  Recent studies have shown that approximately 40% of PTC's harbor a point mutation in codon 600 of the BRAF gene, resulting in " +
			    "the substitution of glutamic acid for valine in the encoded protein.  This alteration results in constitutive activation of the BRAF protein kinase, and thus, the " +
			    "RAS-RAF-MEK-MAPK pathway.  Testing for the BRAF V600E/K mutation in thyroid samples is useful both for diagnosis and for risk stratification in order to guide " +
			    "clinical operative decision making.  Among all thyroid neoplasms, studies have shown that BRAF V600E/K mutations occur almost exclusively in PTC.  This " +
			    "characteristic is useful in the diagnosis of lesions that are suspicious for, but not diagnostic of, PTC on histology or fine needle aspirates.  Furthermore, PTC's " +
			    "that harbor a BRAF V600E/K mutation exhibit more aggressive clinicopathologic features including extrathyroidal extension, lymph node metastasis, high-risk " +
			    "histology, advanced disease stage, and greater risk for persistence and recurrence.  Knowledge of the patient's BRAF gene status may therefore confer important " +
			    "prognostic information and allow for more individualized treatment of patients with PTC.  A 107-base product indicative of a BRAF V600E/K mutation was NOT detected " +
			    "by high resolution capillary electrophoresis.";
            this.m_References = BRAFV600EKResult.PapillaryThyroidReference;
		}
	}
}
