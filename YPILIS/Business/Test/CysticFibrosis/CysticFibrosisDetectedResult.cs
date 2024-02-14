using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisDetectedResult : CysticFibrosisResult
	{		
		public CysticFibrosisDetectedResult()
		{
			this.m_ResultCode = "CSTCFBHRSSDTCTD";
            this.m_Result = "Detected";
			this.m_MutationsDetected = "XXX detected in 1 allele.  Patient is a carrier of a Cystic Fibrosis causing mutation.";
			this.m_Interpretation = "The American College of Medical Genetics (ACMG) and the American College of Obstetricians and Gynecologists (ACOG) " +
				"recommend genetic testing to determine carrier status for cystic fibrosis (CF), one of the most common life threatening autosomal recessive " +
				"conditions affecting Caucasians (1 in 3,000).  The eSensor Cystic Fibrosis Genotyping is a qualitative genotyping assay that simultaneously " +
				"detects the 23 most common mutations (frequency > 0.1%) to identify CF carriers among couples contemplating pregnancy, as well as newborn " +
				"screening for CF and in diagnostic confirmatory testing in newborns and children." + Environment.NewLine + Environment.NewLine + "Based on the results of the assay one copy of the XXX " +
				"detected.  Therefore this individual is a carrier of Cystic Fibrosis (CF). This interpretation is based on the assumption that this individual " +
				"is not clinically symptomatic with CF.  If this patient's reproductive partner is of European Caucasian descent, the risk that any offspring " +
				"of this patient will have Cystic Fibrosis is now estimated to be 1 in 100.  It is recommended that CF carrier testing by DNA mutation analysis " +
				"be offered to this patient's reproductive partner to more accurately assess their risk of having a child affected with Cystic Fibrosis.  " +
				"Additionally, these results could have implications for this patient's relatives.  Genetic counseling regarding the significance of these results is recommended.";
		}

		public override void SetResults(CysticFibrosisTestOrder testOrder, CysticFibrosisEthnicGroup cysticFibrosisEthnicGroup)
		{
			base.SetResults(testOrder, cysticFibrosisEthnicGroup);
			string mutationsDetected = this.GetMutationsDetected(testOrder);
			testOrder.MutationsDetected = this.m_MutationsDetected.Replace("XXX", mutationsDetected);
			testOrder.Interpretation = this.m_Interpretation.Replace("XXX", mutationsDetected);
			testOrder.Comment = null;
		}

		private string GetMutationsDetected(CysticFibrosisTestOrder testOrder)
		{
			StringBuilder result = new StringBuilder();
            CysticFibrosisDetectedResult cysticFibrosisDetectedResult = new CysticFibrosisDetectedResult();
            string compareString = cysticFibrosisDetectedResult.Result;

			CysticFibrosisGeneNames cysticFibrosisGeneNames = new CysticFibrosisGeneNames();
			if (string.Compare(testOrder.Result1898Plus1GtoA, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.Result1898Plus1GtoA]);
			if (string.Compare(testOrder.ResultR117H, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultR117H]);
			if (string.Compare(testOrder.Result621Plus1GtoT, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.Result621Plus1GtoT]);
			if (string.Compare(testOrder.ResultG551D, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultG551D]);
			if (string.Compare(testOrder.ResultDeltaI507, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultDeltaI507]);
			if (string.Compare(testOrder.Result711Plus1GtoT, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.Result711Plus1GtoT]);
			if (string.Compare(testOrder.ResultG85E, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultG85E]);
			if (string.Compare(testOrder.Result1717Minus1GtoA, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.Result1717Minus1GtoA]);
			if (string.Compare(testOrder.ResultR560T, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultR560T]);
			if (string.Compare(testOrder.ResultR334W, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultR334W]);
			if (string.Compare(testOrder.Result3659delC, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.Result3659delC]);
			if (string.Compare(testOrder.Result2184delA, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.Result2184delA]);
			if (string.Compare(testOrder.Result2789Plus5GtoA, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.Result2789Plus5GtoA]);
			if (string.Compare(testOrder.ResultW1282X, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultW1282X]);
			if (string.Compare(testOrder.Result3120Plus1GtoA, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.Result3120Plus1GtoA]);
			if (string.Compare(testOrder.ResultA455E, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultA455E]);
			if (string.Compare(testOrder.ResultDeltaF508, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultDeltaF508]);
			if (string.Compare(testOrder.ResultR1162X, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultR1162X]);
			if (string.Compare(testOrder.ResultR553X, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultR553X]);
			if (string.Compare(testOrder.Result3849Plus10KbCtoT, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.Result3849Plus10KbCtoT]);
			if (string.Compare(testOrder.ResultR347P, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultR347P]);
			if (string.Compare(testOrder.ResultG542X, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultG542X]);
			if (string.Compare(testOrder.ResultN1303K, compareString) == 0) this.BuildDetectedStatement(result, cysticFibrosisGeneNames[(int)CysticFibrosisGeneNameEnum.ResultN1303K]);

			if (result.ToString().Contains(","))
			{
				result.Append(" mutations were");
			}
			else
			{
				result.Append(" mutation was");
			}
			return result.ToString();
		}

		private void BuildDetectedStatement(StringBuilder statement, string testName)
		{
			if (statement.Length > 0)
			{
				statement.Append(", ");
			}
			statement.Append(testName);
		}
	}
}
