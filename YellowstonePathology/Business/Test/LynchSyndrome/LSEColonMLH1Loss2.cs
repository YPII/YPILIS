﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColonMLH1Loss2 : LSERule
	{
		public LSEColonMLH1Loss2()
		{
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Loss;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Intact;

            this.m_BRAFResult = TestResult.NotDetected;
            this.m_MethResult = TestResult.NotDetected;
            this.m_BRAFRequired = true;
            this.m_MethRequired = true;
            this.m_Result = "Loss of nuclear expression of MLH1 mismatch repair proteins." + Environment.NewLine + 
                "BRAF mutation V600E NOT DETECTED." + Environment.NewLine + "MLH1 promoter methylation NOT DETECTED.";
            this.m_Interpretation = "This staining pattern is highly suggestive of Lynch Syndrome and is associated with germline MLH1 mutations.  " +
                "Recommend genetic counseling and further evaluation.";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
