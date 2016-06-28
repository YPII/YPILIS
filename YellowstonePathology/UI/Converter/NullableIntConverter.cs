using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{    
    public class NullableIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = string.Empty;
            Nullable<int> nullableInt = (Nullable<int>)value;
            if (nullableInt.HasValue == true)
            {
                result = nullableInt.Value.ToString();
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Nullable<int> result = null;
            string textValue = value.ToString();
            if (string.IsNullOrEmpty(textValue) == false)
            {
                result = Int32.Parse(textValue);
            }
            return result;
        }
    }
}
