using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618SolidTumor
{
    public class HPV1618SolidTumorIndication
    {
        public static string SquamousCellCarcinomaHeadAndNeck = "Squamous Cell Carcinoma of Head and Neck";
        public static string SquamousCellCarcinomaAnalRegion = "Squamous cell carcinoma of anal region";

        public static List<string> GetIndicationList()
        {
            List<string> result = new List<string>();
            result.Add(null);
            result.Add(SquamousCellCarcinomaHeadAndNeck);
            result.Add(SquamousCellCarcinomaAnalRegion);
            return result;
        }
        
    }
}
