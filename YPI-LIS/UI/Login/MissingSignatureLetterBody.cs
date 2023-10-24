using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class MissingSignatureLetterBody
	{
		const string MissingSignatureStatement = "We are in receipt of physician/provider orders and a patient specimen on patient_name" +
			", for provider_name.  To comply with Medicare regulations, a signature and legible printing of the ordering provider name is required .\n\n" +
			"Please provide the information detailed below and fax to Yellowstone Pathology at (406)-238-6361.";

		public MissingSignatureLetterBody()
		{
		}

		public void GetLetterBody(StringBuilder result, string testName, string patientName, string providerName)
		{
			result.AppendLine(MissingSignatureStatement.Replace("patient_name", patientName));
			result.Replace("provider_name", providerName);
			result.AppendLine();
			result.AppendLine(this.GetLetterBodyForFax(testName));
		}
		public string GetLetterBodyForFax(string testName)
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine("Provider Signature: _________________________________________");
			result.AppendLine();
			result.AppendLine("       Printed Name: _________________________________________");
			result.AppendLine();
			result.AppendLine("Test Name: " + testName);
			return result.ToString();
		}
	}
}
