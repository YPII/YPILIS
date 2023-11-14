using System;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{
    public class NullableDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = string.Empty;
            double? nullableDouble = (double?)value;
            if (nullableDouble.HasValue == true)
            {
                result = Math.Round(nullableDouble.Value, 2).ToString();
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double? result = null;
            string textValue = value.ToString();
            if (string.IsNullOrEmpty(textValue) == false)
            {
                result = Math.Round(Double.Parse(textValue), 2);
            }
            return result;
        }
    }
}
