using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class IHCResultLossOfNuclearExpressionMSH2 : IHCResult
    {
		public IHCResultLossOfNuclearExpressionMSH2()
        {
            this.m_ResultCode = "LNCHIHC07";
			this.m_MLH1Result = new LSEIHCResultIntactExpression();
			this.m_MSH2Result = new LSEIHCResultLossOfExpression();
			this.m_MSH6Result = new LSEIHCResultIntactExpression();
			this.m_PMS2Result = new LSEIHCResultIntactExpression();            
        }
	}
}
