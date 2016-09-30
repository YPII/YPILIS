using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColorectalResult6 : LSEResult
	{
		public LSEColorectalResult6()
		{
			this.m_MLH1Result = LSEResultEnum.Negative;
			this.m_MSH2Result = LSEResultEnum.Positive;
			this.m_MSH6Result = LSEResultEnum.Positive;
			this.m_PMS2Result = LSEResultEnum.Positive;
			this.m_BrafResult = LSEResultEnum.Positive;
			this.m_MethResult = LSEResultEnum.NotApplicable;
            this.m_BRAFIsIndicated = true;

			this.m_Interpretation = "Loss of nuclear expression of MLH1 mismatch repair protein.";
			this.m_Comment = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";
            this.m_Method = IHCBRAFMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
