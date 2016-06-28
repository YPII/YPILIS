using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MPNFish
{
	public class MPNFishNormalResult : MPNFishResult
	{
		public MPNFishNormalResult()
		{
			this.m_Result = "Normal";
			this.m_Interpretation = "Fluorescence in situ hybridization (FISH) analysis was performed using a probe set to detect abnormalities commonly associated with " +
				"myeloproliferative neoplasms.  All the signals were within the normal reference range.  This represents a NORMAL result.";
			this.m_ProbeSetDetail = "PDGFRa (4q12): The PDGFRA probe set shows a normal FISH signal pattern within the normal reference range.\r\n" +
				"PDGFRb (5q33): The PDGFRb probe set shows a normal FISH signal pattern of 2F within the normal reference range\r\n" +
				"FGFR1 (8p11): The FGFR1 break-apart probe set shows a normal FISH signal pattern within the normal reference range.\r\n" +
				"BCR/ABL1/ASS1 t(9;22): A normal FISH signal pattern of 2R2G2A was observed.  This represents a NORMAL result that is negative for the BCR/ABL fusion.";
		}
	}
}
