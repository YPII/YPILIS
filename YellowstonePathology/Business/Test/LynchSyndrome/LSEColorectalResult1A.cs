using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColorectalResult1A : LSEResult
	{

		public LSEColorectalResult1A()
		{
			this.m_MLH1Result = LSEResultEnum.Intact;
			this.m_MSH2Result = LSEResultEnum.Loss;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Intact;
			this.m_BrafResult = LSEResultEnum.NotApplicable;
			this.m_MethResult = LSEResultEnum.NotApplicable;
            this.m_BRAFIsIndicated = false;

			this.m_Interpretation = "Loss of nuclear expression of MSH2 mismatch repair protein.";
            this.m_Comment = "This staining pattern is extremely rare and may be due to an MSH2 gene mutation.  Recommend genetic counseling and further evaluation to exclude Lynch Syndrome. ";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
