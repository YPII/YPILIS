using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class PlacentaClientOrderDetail
	{
		#region GrossExamValidation
		public static Business.Validation.ValidationResult IsGrossExamDataTypeValid(bool grossExam)
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Business.Validation.ValidationResult IsGrossExamDomainValid()
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			if (this.GrossExam == false &&
				this.CompleteExam == false &&
				this.Cytogenetics == false &&
				string.IsNullOrEmpty(this.OtherExam) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "A Pathology Test Order must be selected.";
			}
			return validationResult;
		}
		#endregion

		#region CompleteExamValidation
		public static Business.Validation.ValidationResult IsCompleteExamDataTypeValid(bool completeExam)
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Business.Validation.ValidationResult IsCompleteExamDomainValid()
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			if (this.GrossExam == false &&
				this.CompleteExam == false &&
				this.Cytogenetics == false &&
				string.IsNullOrEmpty(this.OtherExam) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "A Pathology Test Order must be selected.";
			}
			return validationResult;
		}
		#endregion

		#region CytogeneticsValidation
		public static Business.Validation.ValidationResult IsCytogeneticsDataTypeValid(bool cytogenetics)
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Business.Validation.ValidationResult IsCytogeneticsDomainValid()
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			if (this.GrossExam == false &&
				this.CompleteExam == false &&
				this.Cytogenetics == false &&
				string.IsNullOrEmpty(this.OtherExam) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "A Pathology Test Order must be selected.";
			}
			return validationResult;
		}
		#endregion

		#region OtherExamValidation
		public static Business.Validation.ValidationResult IsOtherExamDataTypeValid(string otherExam)
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Business.Validation.ValidationResult IsOtherExamDomainValid()
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			if (this.GrossExam == false &&
				this.CompleteExam == false &&
				this.Cytogenetics == false &&
				string.IsNullOrEmpty(this.OtherExam) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "A Pathology Test Order must be selected.";
			}
			return validationResult;
		}
		#endregion
	}
}
