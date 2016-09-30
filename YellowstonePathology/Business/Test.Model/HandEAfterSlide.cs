using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class HandEAfterSlide : NoCptCodeTest
	{
		public HandEAfterSlide()
		{
			this.m_TestId = 348;
			this.m_TestName = "H&E After Slide";
            this.m_TestAbbreviation = "H&E After";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
