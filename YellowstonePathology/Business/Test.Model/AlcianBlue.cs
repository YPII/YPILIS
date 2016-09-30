using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class AlcianBlue : CytochemicalTest
	{
		public AlcianBlue()
		{
			this.m_TestId = 51;
			this.m_TestName = "Alcian Blue";
            this.m_TestAbbreviation = "Alcian Blue";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
