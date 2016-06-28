using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEIHCResultIntactExpression : LSEIHCResult
	{
		public LSEIHCResultIntactExpression()
		{
			this.m_Description = "Intact nuclear expression";
			this.m_LSEResult = LSEResultEnum.Positive;
		}
	}
}
