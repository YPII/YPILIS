using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV
{
    public class HPVResultCollection : List<HPVResult>
    {
        public HPVResultCollection()
        {

        }                

        public static HPVResultCollection GetAllResults()
        {
            HPVResultCollection result = new HPVResultCollection();            
            result.Add(new HPVNegativeResult());
            result.Add(new HPVPositiveResult());
            result.Add(new HPVInvalidResult());
            result.Add(new HPVNoResult());
            return result;
        }
    }
}
