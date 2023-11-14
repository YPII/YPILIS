using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisEthnicGroupEuropeanCaucasian : CysticFibrosisEthnicGroup
	{
		public CysticFibrosisEthnicGroupEuropeanCaucasian()
		{
            this.m_EthnicGroupId = "EUCCSN";
            this.m_EthnicGroupName = "European Caucasian";
            this.m_BeforeTestString = "88%";
            this.m_DetectionRateString = "1 in 25";
            this.m_AfterNegativeTestString = "1 in 208";
            this.m_TemplateId = 1;            
		}
	}
}
