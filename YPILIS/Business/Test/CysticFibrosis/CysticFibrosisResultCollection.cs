using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public CysticFibrosisResultCollection()
		{

		}

        public CysticFibrosisResult GetResult(string resultCode)
        {
            CysticFibrosisResult result = null;
            foreach (CysticFibrosisResult cysticFibrosisResult in this)
            {
                if (cysticFibrosisResult.ResultCode == resultCode)
                {
                    result = cysticFibrosisResult;
                }
            }
            return result;
        }

		public static CysticFibrosisResultCollection GetAllResults()
		{
			CysticFibrosisResultCollection result = new CysticFibrosisResultCollection();
			result.Add(new CysticFibrosisNotDetectedResult());            
			result.Add(new CysticFibrosisDetectedResult());
            result.Add(new CysticFibrosisNullResult());
			return result;
		}
	}
}
