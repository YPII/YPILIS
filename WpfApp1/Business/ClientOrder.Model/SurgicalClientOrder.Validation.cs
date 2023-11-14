using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class SurgicalClientOrder
	{
		#region ValidatePreOpDiagnosis
		public static YellowstonePathology.Business.Validation.ValidationResult IsPreOpDiagnosisDataTypeValid(string preOpDiagnosis)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsPreOpDiagnosisDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			if (string.IsNullOrEmpty(this.m_PreOpDiagnosis) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "Please enter a preOp diagnosis.";
			}
			return validationResult;
		}
		#endregion

		#region ValidatePostOpDiagnosis
		public static YellowstonePathology.Business.Validation.ValidationResult IsPostOpDiagnosisDataTypeValid(string postOpDiagnosis)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsPostOpDiagnosisDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion
	}
}
