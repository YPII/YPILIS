using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	public class BRAFV600EKDetectedLynchSyndromeResult : BRAFV600EKDetectedResult
	{
		public BRAFV600EKDetectedLynchSyndromeResult()
		{
			YellowstonePathology.Business.Test.IndicationLynchSymdrome indication = new IndicationLynchSymdrome();
			this.m_Indication = indication.IndicationCode;
			this.m_IndicationComment = indication.Description;
			this.m_Interpretation = "BRAF mutation V600E detected.";
			this.m_Method = YellowstonePathology.Business.Test.BRAFV600EK.BRAFResult.Method;
			this.m_Comment = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";
			this.m_References = YellowstonePathology.Business.Test.LynchSyndrome.LSEResult.LSEColonReferences;
		}
	}
}
