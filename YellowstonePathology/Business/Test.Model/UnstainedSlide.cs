using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class UnstainedSlide : NoCptCodeTest
	{
        public UnstainedSlide()
		{
            this.m_TestId = 352;
			this.m_TestName = "Unstained Slide";
            this.m_TestAbbreviation = "Unstained";            
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
