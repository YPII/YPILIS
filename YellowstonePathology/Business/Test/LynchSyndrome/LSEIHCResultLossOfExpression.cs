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
			this.m_Description = "Loss of nuclear expression";
			this.m_LSEResult = LSEResultEnum.Negative;
		}
	}
}
