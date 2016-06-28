using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CytochemicalTest : Test
    {
		public CytochemicalTest()
		{
			this.m_IsBillable = true;            
		}
		
		public CytochemicalTest(int testId, string testName)
            : base(testId, testName)
        {
            this.m_IsBillable = true;            
        }

        public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
        {
            YellowstonePathology.Business.Billing.Model.CptCode result = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88313();
            if (isTechnicalOnly == true)
            {
                result = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88313TC();
            }
            return result;            
        }

        public override string GetCodeableType(bool orderedAsDual)
        {
            return YellowstonePathology.Business.Billing.Model.CodeableType.CYTOCHMCL;
        }
    }
}
