using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MDSByFish
{
	public class MDSByFishNormalResult : MDSByFishResult
	{
		public MDSByFishNormalResult()
		{
			this.m_Result = "Normal";
			this.m_Interpretation = "Fluorescence in situ hybridization (FISH) analysis was performed using a myelodysplastic syndrome specific set of FISH probes.  All the " +
				"probe signals were within the normal reference range.  This represents a NORMAL result.";
			this.m_ProbeSetDetail = "5q-/-5/+5 tricolor: The probe set for chromosome 5 shows a normal FISH signal pattern within the normal reference range.\r\n" +
				"7q-/-7*: The probe set for chromosome 7 shows a normal FISH signal pattern within the normal reference range.\r\n" +
				"+8/20q-/-20: The probe sets for chromosomes 8 and 20 show a normal FISH signal pattern within normal limits.\r\n" +
				"MLL (11q23)*: The MLL probe set shows a normal FISH signal pattern within the normal reference range.";
		}
	}
}
