using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NonGynCytologyStain : NoCptCodeTest
	{
		public NonGynCytologyStain()
		{
			this.m_TestId = 219;
			this.m_TestName = "Non-Gyn Cytology Stain";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
