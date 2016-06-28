using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class DiffQuik : NoCptCodeTest
	{
		public DiffQuik()
		{
			this.m_TestId = 279;
			this.m_TestName = "Diff-Quik";
            this.m_TestAbbreviation = "Diff-Quik";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
