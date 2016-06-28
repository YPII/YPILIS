using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class P504sRacemase : ImmunoHistochemistryTest
	{
		public P504sRacemase()
        {
            this.m_TestId = 133;
			this.m_TestName = "P504s racemase";
            this.m_TestAbbreviation = "P504s racemase";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
