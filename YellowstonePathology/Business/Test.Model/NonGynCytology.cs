using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NonGynCytology : NoCptCodeTest
	{
		public NonGynCytology()
		{
			this.m_TestId = 206;
			this.m_TestName = "Non-Gyn Cytology";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
