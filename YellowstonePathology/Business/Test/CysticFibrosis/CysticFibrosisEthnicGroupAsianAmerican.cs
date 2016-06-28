using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisEthnicGroupAsianAmerican : CysticFibrosisEthnicGroup
	{
        public CysticFibrosisEthnicGroupAsianAmerican()
		{
            this.m_EthnicGroupId = "ASNAMRCN";
            this.m_EthnicGroupName = "Asian American";
            this.m_TemplateId = 1;
            this.m_BeforeTestString = "49%";
            this.m_DetectionRateString = "1 in 94";
            this.m_AfterNegativeTestString = "1 in 184";            
		}
	}
}
