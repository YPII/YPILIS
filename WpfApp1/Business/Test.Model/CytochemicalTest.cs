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
		
		public CytochemicalTest(string testId, string testName)
            : base(testId, testName)
        {
            this.m_IsBillable = true;            
        }

        public CytochemicalTest(Stain.Model.Stain stain)
            : base(stain)
        {
        }

        public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
        {
            YellowstonePathology.Business.Billing.Model.CptCode result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88313", null);
            if (isTechnicalOnly == true)
            {
                result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88313", "TC");
            }
            return result;            
        }        

        public override string GetCodeableType(bool orderedAsDual)
        {
            return YellowstonePathology.Business.Billing.Model.CodeableType.CYTOCHMCL;
        }       
    }
}
