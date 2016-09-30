using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Lysozyme : ImmunoHistochemistryTest
	{
		public Lysozyme()
		{
			this.m_TestId = 117;
			this.m_TestName = "Lysozyme";
            this.m_TestAbbreviation = "Lysozyme";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
