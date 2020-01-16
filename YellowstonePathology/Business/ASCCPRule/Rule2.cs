using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class Rule2 : BaseRule
    {        
        public Rule2()
        {
            this.m_Description = "Cytology ASCUS";
            /*
            if (woman.ScreeningImpression.ResultCode == "03")
            {                
                this.m_IsMatch = true;
                if (this.m_Woman.Age >= 25)
                {
                    //this.m_HPVStatus = "HPV Is Required.";
                    //this.m_HPV1618Status = "HPV Genotypes is Not Recommended.";
                    woman.ManagementRecomendation = "";
                }
                else if (this.m_Woman.Age < 25)
                {
                    //this.m_HPVStatus = "HPV Is Not Recommended.";
                    //this.m_HPV1618Status = "HPV Genotypes is not recommended";
                    woman.ManagementRecomendation = "Repeat Cytology in 12 months.";
                }                
            }
            */
        }        
    }
}
