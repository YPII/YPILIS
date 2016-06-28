using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{
    public class SplitDateTimeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length != 2)
                throw new InvalidOperationException("The input to this converter must have 2 dates.");

            Nullable<DateTime> date = null;
            Nullable<DateTime> dateTime = null;

            string result = string.Empty;
            if (values[0] != null && values[0].GetType() == typeof(DateTime))
            {                
                date = (DateTime)values[0];                
            }
            if (values[1] != null && values[1].GetType() == typeof(DateTime))
            {
                dateTime = (DateTime)values[1];                
            }
            SplitDateTime splitDateTime = new SplitDateTime(date, dateTime);
            return splitDateTime.MergedDateString;            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {            
            object[] resultDates = new object[2];
            SplitDateTime splitDateTime = new SplitDateTime(value.ToString());
            if (splitDateTime.HasError == false)
            {
                resultDates[0] = splitDateTime.Date;
                resultDates[1] = splitDateTime.Time;
            }
            else
            {
                resultDates[0] = DependencyProperty.UnsetValue;
                resultDates[1] = DependencyProperty.UnsetValue;
            }
            return resultDates;
        }
    }
}
