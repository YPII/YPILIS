using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{
    [ValueConversion(typeof(double), typeof(string))]
    public class GatingPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString() == "0")
            {
                return "< 1%";
            }
            else
            {
                double percent = System.Convert.ToDouble(value);
                string strPercent = System.Convert.ToString(Math.Round((percent * 100), 2)) + "%";
                return strPercent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString() == "< 1%")
            {
                return "0";
            }
            else
            {
                return value.ToString().Replace("%", "");
            }
        }
    }
}