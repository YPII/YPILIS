using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class IgKappaIgLambdaDualStain : DualStain
	{
		public IgKappaIgLambdaDualStain()
		{
            this.m_TestId = "IGKIGL";
			this.m_TestName = "Ig Kappa/Ig Lambda";
			this.m_FirstTest = new IgKappa();
			this.m_SecondTest = new IgLambda();

			this.m_DepricatedFirstTestId = 231;
			this.m_DepricatedSecondTestId = 232;
		}
	}
}
