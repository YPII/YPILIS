using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NeisseriaGonorrhoeae : Test
	{
        public NeisseriaGonorrhoeae()
		{
			this.m_TestId = 25;
            this.m_TestName = "Neisseria gonorrhoeae";
            this.m_NeedsAcknowledgement = false;
		}
	}
}
