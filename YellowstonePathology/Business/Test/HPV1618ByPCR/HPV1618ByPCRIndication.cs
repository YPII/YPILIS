using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
    public class HPV1618ByPCRIndication
    {
        public static string SquamousCellCarcinoma = "Squamous Cell Carcinoma of Head and Neck";        

        public static List<string> GetIndicationList()
        {
            List<string> result = new List<string>();
            result.Add(null);
            result.Add(SquamousCellCarcinoma);            
            return result;
        }
        
    }
}
