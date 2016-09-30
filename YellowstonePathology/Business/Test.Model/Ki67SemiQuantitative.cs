using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Ki67SemiQuantitative : GradedTest
	{
		public Ki67SemiQuantitative()
        {
            this.m_TestId = 116;
			this.m_TestName = "Ki-67, Semi-quantitative";
            this.m_TestAbbreviation = "Ki-67 Semi";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
