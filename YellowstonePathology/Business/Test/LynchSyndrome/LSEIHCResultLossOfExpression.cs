using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEIHCResultLossOfExpression : LSEIHCResult
	{
		public LSEIHCResultLossOfExpression()
		{
			this.m_Description = LSEIHCResult.LossDescription;
			this.m_IHCResultType = LSEIHCResult.Loss;
		}
	}
}
