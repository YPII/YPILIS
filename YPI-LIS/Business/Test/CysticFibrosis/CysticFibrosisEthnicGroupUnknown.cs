using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisEthnicGroupUnknown : CysticFibrosisEthnicGroup
	{
        public CysticFibrosisEthnicGroupUnknown()
		{
            this.m_EthnicGroupId = "ETHCGRPUNKN";
			this.m_EthnicGroupName = "Unknown";            
            this.m_TemplateId = 2;
		}

        public override string GetInterpretation()
        {
			string result = "It is assumed that this individual has no personal or family history of cystic fibrosis (CF).  Although no common mutation was " +
				"detected, these results do not rule out the possibility that this individual is a carrier of a CFTR gene mutation that is not detected by this assay.";
			return result;
        }

        public override string GetResidualRiskStatement()
        {
            return null;
        }
	}
}
