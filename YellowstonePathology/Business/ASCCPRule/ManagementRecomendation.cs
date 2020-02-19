using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class ManagementRecomendation
    {
        public const string BASECOMMENT = "Based on the patient’s age and test results, the ASCCP Clinical Management Guidelines recommend *RECOMMENDATION*.  Deviation from recommendations is anticipated, as there may be extenuating clinical circumstances and all treatment and screening decisions should be made between the patient and clinical provider.";

        public static string GetRepeatTwoToFour()
        {
            return BASECOMMENT.Replace("*RECOMMENDATION*", "repeating cyotlogy in 2-4 months");
        }

        public static string GetColposcopyOrRepeatTwoToFour()
        {
            return BASECOMMENT.Replace("*RECOMMENDATION*", "colposcopy or repeating cyotlogy in 2-4 months");
        }

        public static string GetRoutineScreening()
        {
            return BASECOMMENT.Replace("*RECOMMENDATION*", "routine screening");
        }

        public static string GetColposcopy()
        {
            return BASECOMMENT.Replace("*RECOMMENDATION*", "colposcopy");
        }

        public static string GetRepeatIn1Year()
        {
            return BASECOMMENT.Replace("*RECOMMENDATION*", "repeat cytology in 1 year");
        }
    }
}
