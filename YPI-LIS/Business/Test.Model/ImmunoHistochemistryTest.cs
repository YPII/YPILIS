using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ImmunoHistochemistryTest : Test
    {                
        public ImmunoHistochemistryTest()            
        {
            this.m_IsBillable = true;
            this.m_HasGCode = true;
            this.m_HasCptCodeLevels = true;            
        }

        public ImmunoHistochemistryTest(string testId, string testName)
            : base(testId, testName)
        {
            this.m_IsBillable = true;
            this.m_HasGCode = true;
            this.m_HasCptCodeLevels = true;
        }

        public ImmunoHistochemistryTest(Stain.Model.Stain stain)
            : base(stain)
        {
        }

        public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
        {            
            YellowstonePathology.Business.Billing.Model.CptCode result = null;
            if (this.IsDualOrder == true)
            {
                result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88344", null);
                if (isTechnicalOnly == true)
                {                    
                    result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88344", "TC");
                }                
            }
            else
            {
                result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null);
                if (isTechnicalOnly == true)
                {
                    result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", "TC");
                }
            }                                        
            return result;  
        }

        public override YellowstonePathology.Business.Billing.Model.CptCode GetGCode(CptCodeLevelEnum cptCodeLevel, bool isTechnicalOnly)
        {
            YellowstonePathology.Business.Billing.Model.CptCode result = null;
            switch (cptCodeLevel)
            {
                case CptCodeLevelEnum.Initial:
                    if (isTechnicalOnly == true)
                    {
                        result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0461", null);
                    }
                    else
                    {
                        result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0461", "TC");
                    }
                    break;
                case CptCodeLevelEnum.Subsequent:
                    if (isTechnicalOnly == true)
                    {
                        result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0462", null);
                    }
                    else
                    {
                        result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0462", "TC");
                    }
                    break;
            }            
            return result;
        }

        public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(CptCodeLevelEnum cptCodeLevel, bool isTechnicalOnly)
        {
            YellowstonePathology.Business.Billing.Model.CptCode result = null;
            switch (cptCodeLevel)
            {
                case CptCodeLevelEnum.Initial:
                    if (isTechnicalOnly == true)
                    {
                        result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", "TC");
                    }
                    else
                    {
                        result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null);
                    }
                    break;
                case CptCodeLevelEnum.Subsequent:
                    if (isTechnicalOnly == true)
                    {
                        result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88343", "TC");
                    }
                    else
                    {
                        result = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88343", null);
                    }
                    break;
            }            

            return result;
        }

        public override string GetCodeableType(bool orderedAsDual)
        {
            string result = null;
            if (orderedAsDual == false)
            {
                result = Business.Billing.Model.CodeableType.IHCSINGLE;
            }
            else
            {
                result = Business.Billing.Model.CodeableType.IHCDUAL;
            }
            return result;
        }
    }
}
