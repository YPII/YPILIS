using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CellBlock : NoCptCodeTest
	{
		public CellBlock()
		{
			this.m_TestId = 195;
			this.m_TestName = "Cell Block";
            this.m_TestAbbreviation = "Cell Block";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
