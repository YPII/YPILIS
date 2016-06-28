using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisEthnicGroupAshkenaziJewish : CysticFibrosisEthnicGroup
	{
        public CysticFibrosisEthnicGroupAshkenaziJewish()
		{
            this.m_EthnicGroupId = "ASHKJWSH";
            this.m_EthnicGroupName = "Ashkenazi Jewish";
            this.m_BeforeTestString = "94%";
            this.m_DetectionRateString = "1 in 24";
            this.m_AfterNegativeTestString = "1 in 400";
            this.m_TemplateId = 1;            
		}
	}
}
