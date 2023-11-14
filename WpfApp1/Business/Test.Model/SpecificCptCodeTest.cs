using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class SpecificCptCodeTest : Test
    {
		public SpecificCptCodeTest()
		{
			this.m_IsBillable = false;
			this.m_HasGCode = false;
			this.m_HasCptCodeLevels = false;
		}

		public SpecificCptCodeTest(string testId, string testName)
            : base(testId, testName)
        {
            this.m_IsBillable = false;
            this.m_HasGCode = false;
            this.m_HasCptCodeLevels = false;
        }

        public virtual YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
        {
            //Not implemented here.
            return null;
        }
    }
}
