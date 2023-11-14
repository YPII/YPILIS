using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.SARSCoV2
{
    public class SARSCoV2ResultCollection : List<SARSCoV2Result>
    {
        public SARSCoV2ResultCollection()
        {

        }                

        public static SARSCoV2ResultCollection GetAllResults()
        {
            SARSCoV2ResultCollection result = new SARSCoV2ResultCollection();            
            result.Add(new SARSCoV2DetectedResult());
            result.Add(new SARSCoV2NotDetectedResult());
            result.Add(new SARSCoV2InvalidResult());
            return result;
        }
    }
}
