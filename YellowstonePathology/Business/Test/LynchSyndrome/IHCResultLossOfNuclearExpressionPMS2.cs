using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class IHCResultLossOfNuclearExpressionPMS2 : IHCResult
    {
        public IHCResultLossOfNuclearExpressionPMS2()
        {
            this.m_ResultCode = "LNCHIHC05";
			this.m_MLH1Result = new LSEIHCResultIntactExpression();
			this.m_MSH2Result = new LSEIHCResultIntactExpression();
			this.m_MSH6Result = new LSEIHCResultIntactExpression();
			this.m_PMS2Result = new LSEIHCResultLossOfExpression();            
        }        
    }
}
