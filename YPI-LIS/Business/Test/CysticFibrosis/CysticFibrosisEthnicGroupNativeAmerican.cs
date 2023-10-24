using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisEthnicGroupNativeAmerican : CysticFibrosisEthnicGroup
	{
        public CysticFibrosisEthnicGroupNativeAmerican()
		{
            this.m_EthnicGroupId = "NTVAMRCN";
            this.m_EthnicGroupName = "Native American";                       
            this.m_TemplateId = 1;            
		}

        public override string GetInterpretation()
        {
            string result = "The average Native American risk of being a carrier of CF is not well defined.  However, the Native American risk for being " +
                "a carrier of CF is approximately the rate for Asian American ethnicity, which is 1 in 94 before testing. The CF detection rate for people with " +
                "Native American ethnicity is approximately 49%.  Therefore the residual risk is approximately 1 in 180.";
            return result;
        }

        public override string GetResidualRiskStatement()
        {
            string result = "The residual risk of being a carrier of CF is 1 in 180.";
            return result;
        }
	}
}
