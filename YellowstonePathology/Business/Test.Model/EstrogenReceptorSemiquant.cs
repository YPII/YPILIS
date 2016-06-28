using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class EstrogenReceptorSemiquant : GradedTest
	{
		public EstrogenReceptorSemiquant()
		{
			this.m_TestId = 99;
			this.m_TestName = "Estrogen Receptor, Semi-quantitative";
            this.m_TestAbbreviation = "ER";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
