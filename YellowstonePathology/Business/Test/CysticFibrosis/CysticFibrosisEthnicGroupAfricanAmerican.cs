using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisEthnicGroupAfricanAmerican : CysticFibrosisEthnicGroup
	{
        public CysticFibrosisEthnicGroupAfricanAmerican()
		{
            this.m_EthnicGroupId = "AFRCNAMRCN";
            this.m_EthnicGroupName = "African American";
            this.m_BeforeTestString = "65%";
            this.m_DetectionRateString ="1 in 65";
            this.m_AfterNegativeTestString = "1 in 186";
            this.m_TemplateId = 1;
		}
	}
}
