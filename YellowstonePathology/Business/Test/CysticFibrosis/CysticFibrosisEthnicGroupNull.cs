using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisEthnicGroupNull : CysticFibrosisEthnicGroup
	{
        public CysticFibrosisEthnicGroupNull()
		{
            this.m_EthnicGroupId = null;
            this.m_EthnicGroupName = null;            
            this.m_TemplateId = 0;
		}
	}
}
