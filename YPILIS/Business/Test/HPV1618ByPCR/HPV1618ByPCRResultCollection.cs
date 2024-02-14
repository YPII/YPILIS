using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
	public class HPV1618ByPCRResultCollection : List<HPV1618ByPCRResult>
	{
		public HPV1618ByPCRResultCollection()
        {

        }

        public HPV1618ByPCRResult GetResult(string resultCode)
        {
            HPV1618ByPCRResult result = null;
			foreach (HPV1618ByPCRResult hpv1618Result in this)
            {
				if (hpv1618Result.ResultCode == resultCode)
                {
					result = hpv1618Result;
                    break;
                }
            }
            return result;
        }

		public static HPV1618ByPCRResultCollection GetAllResults()
        {
            HPV1618ByPCRResultCollection result = new HPV1618ByPCRResultCollection();
            result.Add(new HPV1618NoResult());
			result.Add(new HPV1618BothNegativeResult());
			result.Add(new HPV1618BothPositiveResult());
			result.Add(new HPV1618IndeterminateResult());
			result.Add(new HPV16NegativeHPV18PositiveResult());
			result.Add(new HPV16PositiveHPV18NegativeResult());
			return result;
		}
	}
}
