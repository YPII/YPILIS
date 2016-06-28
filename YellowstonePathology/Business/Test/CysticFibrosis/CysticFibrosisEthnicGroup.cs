using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisEthnicGroup
	{        
        protected string m_EthnicGroupId;
        protected string m_EthnicGroupName;        
        protected int m_TemplateId;
        protected string m_BeforeTestString;
        protected string m_DetectionRateString;
        protected string m_AfterNegativeTestString;

		public CysticFibrosisEthnicGroup()
		{
                                   
		}

        public virtual string GetInterpretation()
        {            
            string result  = "It is assumed that this individual has no personal or family history of cystic fibrosis (CF). The average " + this.m_EthnicGroupName + " risk " +
                "of being a carrier of CF is " + this.m_DetectionRateString + ". The detection rate for people with " + this.m_EthnicGroupName + " " +
                "ethnicity is " + this.m_BeforeTestString + ". The test results are negative for CF mutations. Although no common mutation was detected, these results do not rule out the possibility that this individual is a " +
                "carrier of a CFTR gene mutation that is not detected by this assay. Therefore, the residual risk is now " + this.m_AfterNegativeTestString + ".";
            return result;
        }

        public virtual string GetResidualRiskStatement()
        {
            string result = "The residual risk of being a carrier of CF is " + this.m_AfterNegativeTestString + ".";                        
            return result;
        }

        public string EthnicGroupId
        {
            get { return this.m_EthnicGroupId; }
        }

        public string EthnicGroupName
        {
            get { return this.m_EthnicGroupName; }
        }

                
        public int TemplateId
        {
            get { return this.m_TemplateId; }
        }
	}
}
