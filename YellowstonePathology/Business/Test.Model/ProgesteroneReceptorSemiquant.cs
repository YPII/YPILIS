using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ProgesteroneReceptorSemiquant : GradedTest
	{
		public ProgesteroneReceptorSemiquant()
		{
			this.m_TestId = 145;
			this.m_TestName = "Progesterone Receptor, Semi-quantitative";
            this.m_TestAbbreviation = "PR";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
