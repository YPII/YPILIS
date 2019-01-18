﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColonAllIntact : LSERule
	{

		public LSEColonAllIntact()
		{
            this.m_ResultName = "All Intact";
            this.m_Indication = LSEType.COLON;

            this.m_Result = "Intact nuclear expression of MLH1, MSH2, MSH6, and PMS2 mismatch repair proteins.";
            this.m_Interpretation = "The results are compatible with a sporadic tumor and indicate a low risk for Lynch Syndrome.  " +
                "If clinical suspicion for Lynch Syndrome is high, microsatellite instability (MSI) testing by PCR is recommended. " +
                "If MSI testing is desired, please contact Yellowstone Pathology with the request.";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
		}

        public override bool IsIHCMatch(IHCResult ihcResult)
        {
            bool result = false;
            if (ihcResult.MLH1Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.MSH2Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.MSH6Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.PMS2Result.LSEResult == LSEResultEnum.Intact)
            {
                result = true;
            }
            return result;
        }
    }
}
