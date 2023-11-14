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
            this.m_Interpretation = "inv(3)/t(3;3):      Not Detected" + Environment.NewLine +
                "Del(5q):            Not Detected" + Environment.NewLine +
                "Monosomy 5:         Not Detected" + Environment.NewLine +
                "Del(7q):            Not Detected" + Environment.NewLine +
                "Monosomy 7:         Not Detected" + Environment.NewLine +
                "Trisomy 8:          Not Detected" + Environment.NewLine +
                "Del(17p) TP53:      Not Detected" + Environment.NewLine +
                "Trisomy 19:         Not Detected" + Environment.NewLine +
                "Tetrasomy 19:       Not Detected" + Environment.NewLine +
                "Del(20q):           Not Detected" + Environment.NewLine +
                "ETV6 Rearrangement: Not Detected" + Environment.NewLine +
                "KMT2A(MLL) Rearrangement: Not Detected" + Environment.NewLine + Environment.NewLine +
                "Fluorescence in situ hybridization (FISH)analysis was performed using a specific set of probes for myelodysplastic syndrome.  " +
                "Counts for all probe signals were within the normal reference range.This finding represents a NORMAL result.";
			this.m_ProbeSetDetail = "+19: Chromosome 19 showed a normal 2R2G signal pattern within the normal reference range.  " +
                "This represents a NORMAL result." + Environment.NewLine +
                "+ 8 / 20q -/ -20: The probe sets for chromosomes 8 and 20 show a normal FISH signal pattern within normal limits." + Environment.NewLine +
                "5q -/ -5 / +5 tricolor: The probe set for chromosome 5 shows a normal FISH signal pattern within the normal reference range." + Environment.NewLine +
                "7q -/ -7 tri: The probe set for chromosome 7 shows a normal FISH signal pattern within the normal reference range." + Environment.NewLine +
                "p53(17p13.1) / NF1(17q11): The probe set 17 shows a normal 2R2G signal pattern.This represents a NORMAL result." + Environment.NewLine +
                "ETV6(12p13): Chromosome 12 showed a normal 2F signal pattern within the normal reference range.This represents a NORMAL result." + Environment.NewLine +
                "KMT2A(MLL)(11q23) *: The KMT2A(MLL) probe set shows a normal FISH signal pattern within the normal reference range." + Environment.NewLine +
                "RPN1 / MECOM(3q): Chromosome 3 showed a normal 2R2G signal pattern within the normal reference range.This represents a NORMAL result.";
		}
	}
}
