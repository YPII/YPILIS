using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MDSExtendedByFish
{
	public class MDSExtendedByFishNormalResult : MDSExtendedByFishResult
	{
		public MDSExtendedByFishNormalResult()
		{
			this.m_Result = "Normal";
			this.m_Interpretation = "Fluorescence in situ hybridization (FISH) analysis was performed using a myelodysplastic syndrome specific set of FISH probes.  All the " +
				"probe signals were within the normal reference range.  This represents a NORMAL result.";
			this.m_ProbeSetDetail = "RPN1/MECOM (3q): Chromosome 3 showed a normal 2R2G signal pattern within the normal reference range.  This represents a NORMAL result.\r\n" +
				"5q-/-5/+5 tricolor: The probe set for chromosome 5 shows a normal FISH signal pattern within the normal reference range.\r\n" +
				"7q-/-7*: The probe set for chromosome 7 shows a normal FISH signal pattern within the normal reference range.\r\n" +
				"+8/20q-/-20: The probe sets for chromosomes 8 and 20 show a normal FISH signal pattern within normal limits.\r\n" +
				"MLL (11q23)*: The MLL probe set shows a normal FISH signal pattern within the normal reference range.\r\n" +
				"ETV6 (12p13): Chromosome 12 showed a normal 2F signal pattern within the normal reference range.  This represents a NORMAL result.\r\n" +
				"17p- (TP53/CEN 17): The probe set 17 shows a normal 2R2G signal pattern.  This represents a NORMAL result.\r\n" +
				"+19: Chromosome 19 showed a normal 2R2G signal pattern within the normal reference range.  This represents a NORMAL result.";
		}
	}
}
