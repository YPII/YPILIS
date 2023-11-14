using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{    
    public class SocialSecurityNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			if (value == null || (string)value == string.Empty)
			{
				return null;
			}

            if (value.ToString().Length == 9)
            {
                string result = value.ToString().Insert(3, "-");
                result = result.Insert(6, "-");
                return result;
            }
            else
            {
                return value;
            }
        }
    }
}