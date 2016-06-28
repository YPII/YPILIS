using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class TissueSmear : NoCptCodeTest
	{
		public TissueSmear()
		{
			this.m_TestId = 47;
			this.m_TestName = "Tissue Smear";
            this.m_TestAbbreviation = "Tissue Smear";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
