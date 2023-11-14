using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618
{
	public class HPV1618ResultCollection : List<HPV1618Result>
	{
		public HPV1618ResultCollection()
        {

        }

        public HPV1618Result GetResult(string resultCode)
        {
			HPV1618Result result = null;
			foreach (HPV1618Result hpv1618Result in this)
            {
				if (hpv1618Result.ResultCode == resultCode)
                {
					result = hpv1618Result;
                    break;
                }
            }
            return result;
        }

		/*public static HPV1618ResultCollection GetAllResults()
        {
			HPV1618ResultCollection result = new HPV1618ResultCollection();
            result.Add(new HPV1618NoResult());
			result.Add(new HPV1618BothNegativeResult());
			result.Add(new HPV1618BothPositiveResult());
			result.Add(new HPV1618IndeterminateResult());
			result.Add(new HPV16NegativeHPV18PositiveResult());
			result.Add(new HPV16PositiveHPV18NegativeResult());
			return result;
		}*/

        public static HPV1618ResultCollection GetGenotype16Results()
        {
            HPV1618ResultCollection result = new HPV1618ResultCollection();
            result.Add(new HPV1618NoResult());
            result.Add(new HPV1618Genotype16NegativeResult());
            result.Add(new HPV1618Genotype16PositiveResult());
            result.Add(new HPV1618InvalidResult());
            return result;
        }

        public static HPV1618ResultCollection GetGenotype18Results()
        {
            HPV1618ResultCollection result = new HPV1618ResultCollection();
            result.Add(new HPV1618NoResult());
            result.Add(new HPV1618Genotype18NegativeResult());
            result.Add(new HPV1618Genotype18PositiveResult());
            result.Add(new HPV1618InvalidResult());
            return result;
        }
    }
}
