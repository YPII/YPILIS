using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class GradedTest : Test
	{
		public GradedTest()
		{
			this.m_IsBillable = true;
			this.m_HasGCode = false;
			this.m_HasCptCodeLevels = false;            
		}

		public GradedTest(int testId, string testName)
            : base(testId, testName)
        {
            this.m_IsBillable = true;
            this.m_HasGCode = false;
            this.m_HasCptCodeLevels = false;
        }

        public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
        {
            return new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88360();
        }

        public override string GetCodeableType(bool orderedAsDual)
        {
            return YellowstonePathology.Business.Billing.Model.CodeableType.QUANTITATIVE;
        }
	}
}
