using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.AMLStandardByFish
{
	public class AMLStandardByFishNormalResult : AMLStandardByFishResult
	{
		public AMLStandardByFishNormalResult()
		{
			this.m_Result = "Normal";
			this.m_Interpretation = "Fluorescence in situ hybridization (FISH) analysis of at least 200 nuclei per probe was performed using an AML specific probe set to detect " +
				"abnormalities commonly associated with acute myeloid leukemia.  All FISH probe signals were within the normal reference range;  this " +
				"represents a NORMAL result.";
			this.m_ProbeSetDetail = "5q-/-5/+5 tricolor: The probe set for chromosome 5 shows a normal FISH signal pattern.\r\n" +
				"7q-/-7*: The probe set for chromosome 7 shows a normal FISH signal pattern.\r\n" +
				"+8/20q-/-20: The probe set for chromosome 8 shows a normal FISH signal pattern.\r\n" +
				"RUNX1/RUNX1T1 (ETO/AML1) t(8;21): A normal FISH signal pattern was observed. This is negative for the RUNX1/RUNX1T1 (AML1/ETO) fusion.\r\n" +
				"MLL (11q23)*: The MLL probe set shows a normal FISH signal pattern; there is no evidence of a MLL rearrangement.\r\n" +
				"PML/RARA t(15;17): A normal FISH signal pattern was observed. This represents a NORMAL result negative for the PML/RARA translocation.\r\n" +
				"CBFB (16q22): The CBFB probe set shows a normal FISH signal pattern, and is negative for the CBFB gene rearrangement.";
		}
	}
}
