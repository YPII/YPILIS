using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class OilRedO : CytochemicalTest
	{
		public OilRedO()
		{
			this.m_TestId = 131;
			this.m_TestName = "Oil Red O";
            this.m_TestAbbreviation = "Oil Red O";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
