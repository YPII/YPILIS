using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisEthnicGroupHispanicAmerican : CysticFibrosisEthnicGroup
	{
        public CysticFibrosisEthnicGroupHispanicAmerican()
		{
            this.m_EthnicGroupId = "HSPAMRCN";
			this.m_EthnicGroupName = "Hispanic American";
            this.m_BeforeTestString = "72%";
            this.m_DetectionRateString = "1 in 46";
            this.m_AfterNegativeTestString = "1 in 164";
            this.m_TemplateId = 1;            
		}
	}
}
