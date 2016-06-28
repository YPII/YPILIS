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

        public ImmunoHistochemistryTest(int testId, string testName)
            : base(testId, testName)
        {
            this.m_IsBillable = true;
            this.m_HasGCode = true;
            this.m_HasCptCodeLevels = true;
        }

        public override YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
        {
            YellowstonePathology.Business.Billing.Model.CptCode result = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88342();
            if (isTechnicalOnly == true)
            {
                result = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88342TC();
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
                        result = new YellowstonePathology.Business.Billing.Model.GCodeDefinitions.CPTG0461();
                    }
                    else
                    {
                        result = new YellowstonePathology.Business.Billing.Model.GCodeDefinitions.CPTG0461TC();
                    }                    
                    break;
                case CptCodeLevelEnum.Subsequent:
                    if (isTechnicalOnly == true)
                    {
                        result = new YellowstonePathology.Business.Billing.Model.GCodeDefinitions.CPTG0462();
                    }
                    else
                    {
                        result = new YellowstonePathology.Business.Billing.Model.GCodeDefinitions.CPTG0462TC();
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
                        result = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88342TC();
                    }
                    else
                    {
                        result = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88342();
                    }
                    break;
                case CptCodeLevelEnum.Subsequent:
                    if (isTechnicalOnly == true)
                    {
                        result = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88343TC();
                    }
                    else
                    {
                        result = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88343();
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
                result = YellowstonePathology.Business.Billing.Model.CodeableType.IHCSINGLE;
            }
            else
            {
                result = YellowstonePathology.Business.Billing.Model.CodeableType.IHCDUAL;
            }
            return result;
        }
    }
}
