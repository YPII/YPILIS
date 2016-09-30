using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Desmin : ImmunoHistochemistryTest
	{
		public Desmin()
		{
			this.m_TestId = 92;
			this.m_TestName = "Desmin";
            this.m_TestAbbreviation = "Desmin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
