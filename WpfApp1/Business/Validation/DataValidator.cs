using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Validation
{
    public class DataValidator
    {
		private List<DataValidatorItem> m_DataValidatorList;

		public DataValidator()
        {
			this.m_DataValidatorList = new List<DataValidatorItem>();
        }

		public ValidationResult ValidateDataTypes()
        {
			ValidationResult result = new ValidationResult();
			result.IsValid = true;
			result.Message = string.Empty;
			foreach (DataValidatorItem item in this.m_DataValidatorList)
            {
                if (item.DataTypeValidationResult.IsValid == false)
                {
					result.IsValid = false;
					result.Message += item.DataTypeValidationResult.Message + "\r\n";
				}
            }
            return result;
        }

		public ValidationResult ValidateDomain()
		{
			ValidationResult result = new ValidationResult();
			result.IsValid = true;
			result.Message = string.Empty;
			foreach (DataValidatorItem item in this.m_DataValidatorList)
			{
				item.DomainValidationResult = (ValidationResult)item.DomainValidationMethod.Invoke();
				if (item.DomainValidationResult.IsValid == false)
				{
					result.IsValid = false;
					result.Message += item.DomainValidationResult.Message + "\r\n";
				}
			}
			return result;
		}

        public void UpdateValidBindingSources()
        {
			foreach (DataValidatorItem item in this.m_DataValidatorList)
            {
                if (item.DataTypeValidationResult.IsValid == true)
                {
                    item.BindingExpression.UpdateSource();
                }
            }
        }

		public void Add(DataValidatorItem dataValidatorItem)
		{
			this.m_DataValidatorList.Add(dataValidatorItem);
		}
    }
}
