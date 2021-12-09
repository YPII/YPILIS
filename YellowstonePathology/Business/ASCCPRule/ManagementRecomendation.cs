using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class ManagementRecomendation
    {
        public const string BASECOMMENT = "Based on the patient's age and test results, the 2012 ASCCP Clinical Management Guidelines recommend *RECOMMENDATION*.  Deviation from recommendations is anticipated, as there may be extenuating clinical circumstances and all treatment and screening decisions should be made between the patient and clinical provider.";

        public static string GetRepeatTwoToFour()
        {
            return BASECOMMENT.Replace("*RECOMMENDATION*", "repeating cytology in 2-4 months");
        }

        public static string GetColposcopyOrRepeatTwoToFour()
        {
            return BASECOMMENT.Replace("*RECOMMENDATION*", "colposcopy or repeating cytology in 2-4 months");
        }

        public static string GetRoutineScreening()
        {
            return BASECOMMENT.Replace("*RECOMMENDATION*", "routine screening");
        }

        public static string GetColposcopy()
        {
            return BASECOMMENT.Replace("*RECOMMENDATION*", "colposcopy");
        }        

        public static string GetRepeatInOneYear(string orderType)
        {
            if(orderType == "10")
            {
                return BASECOMMENT.Replace("*RECOMMENDATION*", "repeat cytology in one year");
            }
            else if(orderType == "11")
            {
                return BASECOMMENT.Replace("*RECOMMENDATION*", "repeat cotesting in one year");
            }
            else
            {
                throw new Exception("Order Type Not Handled: " + orderType);
            }
        }
    }
}
