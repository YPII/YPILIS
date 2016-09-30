using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class IHCResultLossOfNuclearExpressionMLH1 : IHCResult
    {
		public IHCResultLossOfNuclearExpressionMLH1()
        {
            this.m_ResultCode = "LNCHIHC06";
			this.m_MLH1Result = new LSEIHCResultLossOfExpression();
			this.m_MSH2Result = new LSEIHCResultIntactExpression();
			this.m_MSH6Result = new LSEIHCResultIntactExpression();
			this.m_PMS2Result = new LSEIHCResultIntactExpression();            
        }
	}
}
