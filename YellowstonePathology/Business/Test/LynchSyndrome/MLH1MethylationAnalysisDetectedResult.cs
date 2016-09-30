using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class MLH1MethylationAnalysisDetectedResult : MLH1MethylationAnalysisResult
	{
		public MLH1MethylationAnalysisDetectedResult()
		{
			this.m_Result = "Detected";
            this.m_ResultCode = "MLH1DTCTD";
            
			this.m_Interpretation = "Positivity for methylation of CpG island of MLH1 promoter has been detected in majority of sporadic colorectal " +
			"cancers that show high microsatellite instability.\r\n\r\n" +
			"The MLH1 gene, which is frequently mutated in hereditary nonpolyposis colon cancer (HNPCC), can also be methylated (hypermethylated) in its promoter " +
			"region in sporadic colon cancer. The methylation of the promoter of MLH1 gene results in silencing its expression and effects similar to those seen " +
			"when it is mutated.\r\n\r\n" +
			"MLH1 promoter methylation analysis is recommended for patients with microsatellite instability (MSI) or abnormal IHC results. MLH1 promoter methylation " +
			"has been reported in 20% of colon cancer. Data is controversial, but the presence of MLH1 promoter methylation in colon cancer suggests that the tumor " +
			"is less likely to be due to HNPCC (Lynch syndrome) and more likely to be sporadic colon cancer.";
		}
	}
}
