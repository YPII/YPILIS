using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{    
    public class DotDotDotConverter : IValueConverter
    {        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            if (value == null) return string.Empty;

            int stringLength = 12;
            string result = value.ToString();
            if (result.Length > stringLength)
            {
                result = result.Substring(0, stringLength) + "...";
            }
            return result;         
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {			
			return string.Empty;
        }
    }
}
