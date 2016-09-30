using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{        
    public class PhoneNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string phoneNumber = (string)value;            
            if (string.IsNullOrEmpty(phoneNumber) == false && phoneNumber.Length == 10)
            {
                return "(" + phoneNumber.Substring(0, 3) + ") " +
                    phoneNumber.Substring(3, 3) + "-" + phoneNumber.Substring(6, 4);
            }
            else
            {
                return phoneNumber;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string phoneNumber = (string)value;
            phoneNumber = phoneNumber.Replace(")", "");
            phoneNumber = phoneNumber.Replace("(", "");
            phoneNumber = phoneNumber.Replace("-", "");
            return phoneNumber;
        }
    }
}
