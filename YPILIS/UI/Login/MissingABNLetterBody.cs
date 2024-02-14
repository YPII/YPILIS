using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class MissingABNLetterBody
	{
		const string StartStatement = "We are in receipt of physician/provider orders and a patient specimen on patient_name. However, we " +
			"cannot proceed with testing of this specimen because we are missing important information. Please provide information detailed below " +
			"and fax back to Yellowstone Pathology at (406)238-6361 to ensure timely processing of the specimen: ";

		public void GetLetterBody(StringBuilder result, string patientName, bool missingABNDate, bool missingABNEstimatedCost, bool missingABNIdentificationNumber,
			bool missingABNLaboratory, bool missingABNNotifier, bool missingABNOptionBoxChecked, bool missingABNPatientName)
		{
			result.AppendLine(StartStatement);
			result.Replace("patient_name", patientName);
			result.AppendLine();
			this.MissingABNInformation(result);

			if(missingABNDate) this.MissingABNDate(result);
			if(missingABNEstimatedCost) this.MissingABNEstimatedCost(result);
			if(missingABNIdentificationNumber) this.MissingABNIdentificationNumber(result);
			if(missingABNLaboratory) this.MissingABNLaboratory(result);
			if(missingABNNotifier) this.MissingABNNotifier(result);
			if(missingABNOptionBoxChecked) this.MissingABNOptionBoxChecked(result);
			if (missingABNPatientName) this.MissingABNPatientName(result);
			this.AddSignatureLine(result);
		}

		public void MissingABNDate(StringBuilder result)
		{
				result.AppendLine("____________________ Date");
				result.AppendLine();
		}

		public void MissingABNEstimatedCost(StringBuilder result)
		{
			result.AppendLine("____________________ Estimate Cost");
			result.AppendLine("The patient was notified of the estimated cost of the testing at the time the ABN was executed.");
			result.AppendLine();
		}

		public void MissingABNIdentificationNumber(StringBuilder result)
		{
			result.AppendLine("____________________ Identification Number");
			result.AppendLine();
		}

		public void MissingABNLaboratory(StringBuilder result)
		{
			result.AppendLine("____________________ Laboratory Tests");
			result.AppendLine();
		}

		public void MissingABNNotifier(StringBuilder result)
		{
			result.AppendLine("____________________ Notifier");
			result.AppendLine();
		}

		public void MissingABNOptionBoxChecked(StringBuilder result)
		{
			result.AppendLine("____________________ Option Box Checked");
			result.AppendLine();
		}

		public void MissingABNPatientName(StringBuilder result)
		{
			result.AppendLine("____________________ Patient Name");
			result.AppendLine();
		}

		public void MissingABNInformation(StringBuilder result)
		{
			result.AppendLine("Missing ABN Information:");
			result.AppendLine();
		}

		private void AddSignatureLine(StringBuilder result)
		{
			result.AppendLine();
			result.AppendLine();
			result.AppendLine("____________________________________");
			result.AppendLine("Signature of person completing form.");
		}
	}
}
