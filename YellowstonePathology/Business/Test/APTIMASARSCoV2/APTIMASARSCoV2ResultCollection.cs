using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.APTIMASARSCoV2
{
    public class APTIMASARSCoV2ResultCollection : List<APTIMASARSCoV2Result>
    {
        public APTIMASARSCoV2ResultCollection()
        {

        }                

        public static APTIMASARSCoV2ResultCollection GetAllResults()
        {
            APTIMASARSCoV2ResultCollection result = new APTIMASARSCoV2ResultCollection();
            result.Add(new APTIMASARSCoV2NegativeResult());
            result.Add(new APTIMASARSCoV2PositiveResult());
            result.Add(new APTIMASARSCoV2InvalidResult());

            //result.Add(new APTIMASARSCoV2DetectedResult());
            //result.Add(new APTIMASARSCoV2NotDetectedResult());
            return result;
        }
    }
}
