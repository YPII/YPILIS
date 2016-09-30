using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class IHCResultLossOfNuclearExpressionMSH2MSH6 : IHCResult
    {        
        public IHCResultLossOfNuclearExpressionMSH2MSH6()
        {
            this.m_ResultCode = "LNCHIHC03";
			this.m_MLH1Result = new LSEIHCResultIntactExpression();
			this.m_MSH2Result = new LSEIHCResultLossOfExpression();
			this.m_MSH6Result = new LSEIHCResultLossOfExpression();
			this.m_PMS2Result = new LSEIHCResultIntactExpression();            
        }        
    }
}
