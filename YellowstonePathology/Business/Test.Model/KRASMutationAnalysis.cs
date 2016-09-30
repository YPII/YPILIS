using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class KRASMutationAnalysis : NoCptCodeTest
	{
		public KRASMutationAnalysis()
		{
			this.m_TestId = 221;
			this.m_TestName = "KRAS Mutation Analysis";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
