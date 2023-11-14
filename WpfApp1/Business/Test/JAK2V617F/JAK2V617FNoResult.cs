using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FNoResult : JAK2V617FResult
	{		
		public JAK2V617FNoResult()
		{
            this.m_ResultCode = null;
            this.m_Result = null;
            this.m_Interpretation = null;
            this.m_Comment = null;
		}
	}
}
