using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisNullResult : CysticFibrosisResult
	{
        public CysticFibrosisNullResult()
		{
            this.m_ResultCode = null;
            this.m_Result = null;
            this.m_MutationsDetected = null;
            this.m_Interpretation = null;
		}		
	}
}
