using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NaphtholASDChloroacetateEsterase : CytochemicalTest
	{
		public NaphtholASDChloroacetateEsterase()
		{
			this.m_TestId = 192;
			this.m_TestName = "Naphthol AS-D chloroacetate esterase";
            this.m_TestAbbreviation = "Naphthol AS-D chloroacetate esterase";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
