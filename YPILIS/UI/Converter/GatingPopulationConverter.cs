using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class GatingPopulationConverter : IValueConverter 
    {        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int databaseValue = System.Convert.ToInt32(value);
            int radioButtonValue = System.Convert.ToInt32(parameter);            

            if (databaseValue == radioButtonValue)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
        }
    }
}
