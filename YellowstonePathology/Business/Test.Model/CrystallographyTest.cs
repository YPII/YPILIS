using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CrystalographyTest : Test
    {        

		public CrystalographyTest()
		{
			this.m_IsBillable = true;            
		}
		
		public CrystalographyTest(string testId, string testName)
            : base(testId, testName)
        {
            this.m_IsBillable = true;            
        }

        public CrystalographyTest(Stain.Model.Stain stain)
            : base(stain)
        {

        }

        public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
        {
            YellowstonePathology.Business.Billing.Model.CptCode result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("89060", null);
            if (isTechnicalOnly == true)
            {
                result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("89060", null);
            }
            return result;            
        }        

        public override string GetCodeableType(bool orderedAsDual)
        {
            return YellowstonePathology.Business.Billing.Model.CodeableType.CRYSTALLOGRAPHY;
        }       
    }
}
