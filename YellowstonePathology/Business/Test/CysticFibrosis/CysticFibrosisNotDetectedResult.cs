using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisNotDetectedResult : CysticFibrosisResult
	{		
		public CysticFibrosisNotDetectedResult()
		{
			this.m_ResultCode = "CSTCFBHRSSNTDTCTD";
			this.m_Result =  "Not Detected";
			this.m_MutationsDetected = "No Cystic Fibrosis gene mutations detected.";

			this.m_Interpretation = "The American College of Medical Genetics (ACMG) and the American College of Obstetricians and Gynecologists (ACOG) recommend " +
				"genetic testing to determine carrier status for cystic fibrosis (CF), one of the most common life threatening autosomal recessive conditions " +
				"affecting Caucasians (1 in 3,000).  The eSensor Cystic Fibrosis Genotyping is a qualitative genotyping assay that simultaneously detects the " +
				"23 most common mutations (frequency > 0.1%) to identify CF carriers among couples contemplating pregnancy, as well as newborn screening for " +
				"CF and in diagnostic confirmatory testing in newborns and children. ";
		}

		public override void SetResults(CysticFibrosisTestOrder testOrder, CysticFibrosisEthnicGroup cysticFibrosisEthnicGroup)
		{
            testOrder.Interpretation = this.m_Interpretation + Environment.NewLine + Environment.NewLine + cysticFibrosisEthnicGroup.GetInterpretation();
            testOrder.Comment = cysticFibrosisEthnicGroup.GetResidualRiskStatement();
			base.SetResults(testOrder, cysticFibrosisEthnicGroup);			
		}
	}
}
