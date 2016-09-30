using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColorectalResult10 : LSEResult
	{
		public LSEColorectalResult10()
		{
			this.m_MLH1Result = LSEResultEnum.Positive;
			this.m_MSH2Result = LSEResultEnum.Positive;
			this.m_MSH6Result = LSEResultEnum.Negative;
			this.m_PMS2Result = LSEResultEnum.Positive;
			this.m_BrafResult = LSEResultEnum.NotApplicable;
			this.m_MethResult = LSEResultEnum.NotApplicable;
            this.m_BRAFIsIndicated = false;

			this.m_Interpretation = "Loss of nuclear expression of MSH6 mismatch repair protein.";
			this.m_Comment = "This staining pattern is highly suggestive of Lynch Syndrome and is associated with germline MSH6 or MSH2 mutations.  Recommend genetic counseling and further evaluation.";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
