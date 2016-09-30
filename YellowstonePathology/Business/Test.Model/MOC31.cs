using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class MOC31 : ImmunoHistochemistryTest
	{
		public MOC31()
		{
			this.m_TestId = 215;
			this.m_TestName = "MOC 31";
            this.m_TestAbbreviation = "MOC 31";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
