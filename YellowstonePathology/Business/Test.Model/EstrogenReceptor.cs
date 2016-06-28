using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class EstrogenReceptor : ImmunoHistochemistryTest
	{
		public EstrogenReceptor()
		{
			this.m_TestId = 98;
			this.m_TestName = "Estrogen Receptor";
            this.m_TestAbbreviation = "Estrogen Receptor";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
