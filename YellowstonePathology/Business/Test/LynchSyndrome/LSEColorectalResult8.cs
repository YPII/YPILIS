using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColorectalResult8 : LSEResult
	{
		public LSEColorectalResult8()
		{
			this.m_MLH1Result = LSEResultEnum.Negative;
			this.m_MSH2Result = LSEResultEnum.Positive;
			this.m_MSH6Result = LSEResultEnum.Positive;
			this.m_PMS2Result = LSEResultEnum.Positive;
			this.m_BrafResult = LSEResultEnum.Negative;
			this.m_MethResult = LSEResultEnum.Negative;
            this.m_BRAFIsIndicated = true;

			this.m_Interpretation = "Loss of nuclear expression of MLH1 mismatch repair protein.";
			this.m_Comment = "This staining pattern is highly suggestive of Lynch Syndrome and is associated with germline MLH1 mutations.  Recommend genetic counseling and further evaluation.";
            this.m_Method = IHCBRAFMLHMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
