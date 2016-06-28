using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FNAAdequacyAssessment
{    
    public partial class FNAAdequacyAssessmentTestOrder
	{
		public static YellowstonePathology.Business.Validation.ValidationResult IsDateDataTypeValid(string date)
        {
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
            validationResult.IsValid = false;

            if (string.IsNullOrEmpty(date) == false)
            {
                if (YellowstonePathology.Business.Helper.DateTimeExtensions.IsStringAValidDate(date) == true)
                {
                    validationResult.IsValid = true;
                    validationResult.Message = "The start date entered is not a valid date.";
                }
            }
            
            return validationResult;
        }

		public YellowstonePathology.Business.Validation.ValidationResult IsEndDateGreaterThanStartDate()
        {
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
            validationResult.IsValid = true;
            
            if (this.m_StartDate.HasValue == true && this.m_EndDate.HasValue == true)
            {
                if (this.m_EndDate < this.m_StartDate)
                {
                    validationResult.IsValid = false;
                    validationResult.Message = "The end date must be greater than the start date";
                }
            }
            
            return validationResult;
        }

		public YellowstonePathology.Business.Validation.ValidationResult IsStartDateBlank()
        {
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
            validationResult.IsValid = true;

            if (this.m_StartDate.HasValue == true && this.m_EndDate.HasValue == true)
            {
                if (this.m_EndDate < this.m_StartDate)
                {
                    validationResult.IsValid = false;
                    validationResult.Message = "The start date must not be blank.";                
                }
            }

            return validationResult;
        }

		public YellowstonePathology.Business.Validation.ValidationResult IsEndDateBlank()
        {
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
            validationResult.IsValid = true;

            if (this.m_StartDate.HasValue == false)
            {                
                validationResult.IsValid = false;
                validationResult.Message = "The end date must not be blank.";                
            }

            return validationResult;
        }

		public YellowstonePathology.Business.Validation.ValidationResult DoesStartDateHaveATime()
        {
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
            validationResult.IsValid = true;

            if (YellowstonePathology.Business.Helper.DateTimeExtensions.DoesDateHaveTime(this.m_StartDate) == false)
            {
                validationResult.IsValid = false;
                validationResult.Message = "The start date needs to have a time entered as well as the date.";
            }

            return validationResult;
        }

		public YellowstonePathology.Business.Validation.ValidationResult DoesEndDateHaveATime()
        {
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
            validationResult.IsValid = true;
            
            if (YellowstonePathology.Business.Helper.DateTimeExtensions.DoesDateHaveTime(this.m_EndDate) ==  false)
            {
                validationResult.IsValid = false;
                validationResult.Message = "The end date needs to have a time entered as well as the date.";
            }

            return validationResult;
        }
	}
}
