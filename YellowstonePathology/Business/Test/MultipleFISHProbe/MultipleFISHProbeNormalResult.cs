using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MultipleFISHProbe
{
	public class MultipleFISHProbeNormalResult : MultipleFISHProbeResult
	{
		public MultipleFISHProbeNormalResult()
		{
			this.m_Result = "Normal";
			this.m_Interpretation = "Fluorescence in situ hybridization (FISH) analysis was performed using a multiple myeloma specific set of FISH probes.  All the probe " +
				"signals were within the normal reference range.  This represents a NORMAL result indicating the absence of deletions of translocations " +
				"involving FGFR3, IgH and MAF.  CD138+ and/or CD38+ magnetic cell sorting was utilized to enrich for plasma cells in the sample prior to " +
				"fluorescence in situ hybridization (FISH) analysis.  FISH analysis of at least 100 interphase nuclei was then performed using a commercially " +
				"available FISH probe set specific for FGFR3 (4p16.3), IgH (14q32), and MAF (16q23) loci.  All FISH probe results in this panel were within " +
				"their normal reference ranges.";
			this.m_ProbeSetDetail = "FGFR3/IgH t(4;14): A normal FISH signal pattern of 2R2G was observed. This is a NORMAL result negative for FGFR3/IgH fusion.\r\n" +
				"CCND1/IgH t(11;14): All probe signals were within the normal reference range. This represents a NORMAL result.\r\n" +
				"IgH/MAF t(14;16): Normal signal pattern 2R2G was observed. This is a NORMAL result negative for IgH/MAF fusion.";
		}
	}
}
