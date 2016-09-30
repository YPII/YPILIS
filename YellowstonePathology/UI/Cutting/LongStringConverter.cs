using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Cutting
{
	[ValueConversion(typeof(string), typeof(String))]
	public class LongStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            string result = string.Empty;
            if (value != null)
            {
                result = (string)value;
                if (result.Length > 8)
                {
                    result = result.Substring(0, 8) + " ..";
                }
            }            
            return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            return null;
		}
	}
}
