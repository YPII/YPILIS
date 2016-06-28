using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class ClientOrder
	{
		#region ValidateReportCopyTo
		public static YellowstonePathology.Business.Validation.ValidationResult IsReportCopyToDataTypeValid(string preOpDiagnosis)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsReportCopyToDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateClinicalHistory
		public static YellowstonePathology.Business.Validation.ValidationResult IsClinicalHistoryDataTypeValid(string postOpDiagnosis)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsClinicalHistoryDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion
	}
}
