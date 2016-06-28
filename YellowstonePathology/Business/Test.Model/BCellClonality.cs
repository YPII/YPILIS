using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class BCellClonality : NoCptCodeTest
	{
		public BCellClonality()
		{
			this.m_TestId = 259;
			this.m_TestName = "B-Cell Clonality";
            this.m_TestAbbreviation = "B-Cell";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
