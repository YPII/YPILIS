using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NoCptCodeTest : Test
    {
		public NoCptCodeTest()
		{
			this.m_IsBillable = false;
			this.m_HasGCode = false;
			this.m_HasCptCodeLevels = false;
		}

		public NoCptCodeTest(int testId, string testName)
            : base(testId, testName)
        {
            this.m_IsBillable = false;
            this.m_HasGCode = false;
            this.m_HasCptCodeLevels = false;
        }
    }
}
