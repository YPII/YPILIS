using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Vimentin : ImmunoHistochemistryTest
	{
		public Vimentin()
		{
			this.m_TestId = 164;
			this.m_TestName = "Vimentin";
            this.m_TestAbbreviation = "Vimentin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
