using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Validation
{
    public class ValidationResultCollection : List<ValidationResult>
    {
        public ValidationResultCollection()
        {

        }

        public bool IsValid()
        {
            bool result = true;
            foreach (ValidationResult validationResult in this)
            {
                if (validationResult.IsValid == false)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public string GetMessage()
        {
            StringBuilder result = new StringBuilder();
            foreach (ValidationResult validationResult in this)
            {
                if (string.IsNullOrEmpty(validationResult.Message) == false)
                {
                    result.AppendLine(validationResult.Message);
                }
            }
            return result.ToString();
        }
    }
}
