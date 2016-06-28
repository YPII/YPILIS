using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PASWithDiastase : CytochemicalTest
	{
		public PASWithDiastase()
		{
			this.m_TestId = 140;
			this.m_TestName = "PAS with diastase";
            this.m_TestAbbreviation = "PAS with diastase";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
