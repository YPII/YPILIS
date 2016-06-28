using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class AlphaNaphthylAcetateEsterase : CytochemicalTest
	{
		public AlphaNaphthylAcetateEsterase()
		{
			this.m_TestId = 189;
			this.m_TestName = "Alpha-naphthyl acetate esterase";
            this.m_TestAbbreviation = "ANAE";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
