using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Thyroglobulin : ImmunoHistochemistryTest
	{
		public Thyroglobulin()
		{
			this.m_TestId = 158;
			this.m_TestName = "Thyroglobulin";
            this.m_TestAbbreviation = "Thyroglobulin";            
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
