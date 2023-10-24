using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.ValidationRules
{
    class IntValidation : ValidationRule
    {
        int m_Min;
        int m_Max;

        public IntValidation()
        {

        }

        public int Min
        {
            get { return this.m_Min; }
            set { this.m_Min = value; }
        }

        public int Max
        {
            get { return this.m_Max; }
            set { this.m_Max = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int result = -1;
            try
            {
                if (((string)value).Length > 0)
                {                    
                    result = Int32.Parse((String)value);
                }                
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }            
            if ((result < Min) || (result > Max))
            {
                return new ValidationResult(false,
                  "Please enter a value in the range: " + Min + " - " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
