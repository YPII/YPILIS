using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class FineNeedleAspirate : NoCptCodeTest
	{
		public FineNeedleAspirate()
		{
			this.m_TestId = 204;
			this.m_TestName = "Fine Needle Aspirate";
            this.m_TestAbbreviation = "FNA";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
