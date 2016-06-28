using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class KRASBRAFReflex : NoCptCodeTest
	{
		public KRASBRAFReflex()
		{
			this.m_TestId = 260;
			this.m_TestName = "KRAS BRAF Reflex";
            this.m_TestAbbreviation = "KRAS BRAF Reflex";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
