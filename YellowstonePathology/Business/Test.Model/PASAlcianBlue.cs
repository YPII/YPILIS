using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PASAlcianBlue : CytochemicalTest
	{
		public PASAlcianBlue()
		{
			this.m_TestId = 138;
            this.m_TestName = "PAS alcian blue";
			this.m_TestAbbreviation = "PAS alcian blue";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
