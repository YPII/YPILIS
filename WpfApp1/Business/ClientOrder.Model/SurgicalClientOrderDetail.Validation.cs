using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class SurgicalClientOrderDetail
	{
		#region OrderImmediateExamValidation
		public static Business.Validation.ValidationResult IsOrderImmediateExamDataTypeValid(bool? orderImmediateExam)
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Business.Validation.ValidationResult IsOrderImmediateExamDomainValid()
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			if (this.OrderImmediateExam.HasValue)
			{
				validationResult.IsValid = true;
			}
			else
			{
				validationResult.IsValid = false;
				validationResult.Message = "You must indicate if you need an Immediate exam or not.";
			}
			return validationResult;
		}
		#endregion

		#region OrderFrozenSectionValidation
		public static Business.Validation.ValidationResult IsOrderFrozenSectionDataTypeValid(bool? orderFrozenSection)
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Business.Validation.ValidationResult IsOrderFrozenSectionDomainValid()
		{
			Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			if (this.OrderImmediateExam.HasValue &&
				this.OrderImmediateExam.Value == true)
			{
				if (this.OrderFrozenSection.HasValue == false)
				{
					validationResult.IsValid = false;
					validationResult.Message = "Order Frozen Section not selected.";
				}
			}
			return validationResult;
		}
		#endregion

		#region CallbackNumberValidation
		public static YellowstonePathology.Business.Validation.ValidationResult IsCallbackNumberDataTypeValid(string callbackNumber)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsCallbackNumberDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			if (this.OrderImmediateExam.HasValue &&
				this.OrderImmediateExam.Value == true)
			{
				if(string.IsNullOrEmpty(this.CallbackNumber) == true)
				{
					validationResult.IsValid = false;
					validationResult.Message = "The callback phone number field cannot be left blank when an Immediate Exam is ordered.";
				}
			}
			return validationResult;
		}
		#endregion
	}
}
