using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	public class BRAFV600EKResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{        
		public BRAFV600EKResult GetResult(string resultCode)
		{
			BRAFV600EKResult result = new BRAFV600EKResult();
			foreach (BRAFV600EKResult brafResult in this)
			{
				if (brafResult.ResultCode == resultCode)
				{
					result = brafResult;
					break;
				}
			}
			return result;
		}

        public BRAFV600EKResult GetResult(string resultCode, string indication)
        {
            BRAFV600EKResult result = new BRAFV600EKResult();
            foreach (BRAFV600EKResult brafResult in this)
            {
				if (brafResult is BRAFV600EKIndeterminateResult && brafResult.ResultCode == resultCode)
				{
					result = brafResult;
					break;
				}
				if (brafResult is BRAFV600EKInsufficientResult && brafResult.ResultCode == resultCode)
				{
					result = brafResult;
					break;
				} 
                if (brafResult.ResultCode == resultCode && brafResult.Indication == indication)
                {
                    result = brafResult;
                    break;
                }
            }
            return result;
        }

		public static BRAFV600EKResultCollection GetUniqueResultChoices()
		{
			BRAFV600EKResultCollection result = new BRAFV600EKResultCollection();
			result.Add(new BRAFV600EKDetectedResult());
			result.Add(new BRAFV600EKNotDetectedResult());
			result.Add(new BRAFV600EKIndeterminateResult());
			result.Add(new BRAFV600EKInsufficientResult());
			return result;
		}

        public static BRAFV600EKResultCollection GetDetectedResults()
        {
            BRAFV600EKResultCollection result = new BRAFV600EKResultCollection();            
            result.Add(new BRAFV600EKDetectedCRCResult());
			result.Add(new BRAFV600EKDetectedLynchSyndromeResult());
            result.Add(new BRAFV600EKDetectedMetastaticMelanomaResult());
            result.Add(new BRAFV600EKDetectedPapillaryThyroidResult());
			return result;
        }

        public static BRAFV600EKResultCollection GetNotDetectedResults()
        {
            BRAFV600EKResultCollection result = new BRAFV600EKResultCollection();
            result.Add(new BRAFV600EKNotDetectedCRCResult());
			result.Add(new BRAFV600EKNotDetectedLynchSyndromeResult());
			result.Add(new BRAFV600EKNotDetectedMetastaticMelanomaResult());
            result.Add(new BRAFV600EKNotDetectedPapillaryThyroidResult());            
            return result;
        }        

		public static BRAFV600EKResultCollection GetAll()
		{
			BRAFV600EKResultCollection result = new BRAFV600EKResultCollection();
			result.Add(new BRAFV600EKNotDetectedCRCResult());
			result.Add(new BRAFV600EKNotDetectedLynchSyndromeResult());
			result.Add(new BRAFV600EKNotDetectedMetastaticMelanomaResult());
			result.Add(new BRAFV600EKNotDetectedPapillaryThyroidResult());
			result.Add(new BRAFV600EKDetectedCRCResult());
			result.Add(new BRAFV600EKDetectedLynchSyndromeResult());
			result.Add(new BRAFV600EKDetectedMetastaticMelanomaResult());
			result.Add(new BRAFV600EKDetectedPapillaryThyroidResult());
			result.Add(new BRAFV600EKIndeterminateResult());
			result.Add(new BRAFV600EKInsufficientResult());
			return result;
		}        
	}
}
