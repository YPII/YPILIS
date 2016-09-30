using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ParaffinCurls : NoCptCodeTest
	{
        public ParaffinCurls()
		{
			this.m_TestId = 183;
			this.m_TestName = "Paraffin Curls for Molecular";
            this.m_TestAbbreviation = "Curls";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
