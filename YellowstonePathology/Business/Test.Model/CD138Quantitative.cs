using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD138Quantitative : GradedTest
	{
        public CD138Quantitative()
		{
			this.m_TestId = 273;
			this.m_TestName = "CD138 Quantitative";
            this.m_TestAbbreviation = "CD138 Quant";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
