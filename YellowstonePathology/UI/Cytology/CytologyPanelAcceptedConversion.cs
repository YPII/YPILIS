using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Cytology
{
	[ValueConversion(typeof(Nullable<DateTime>), typeof(string))]
	class CytologyPanelAcceptedDateConversion : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            if (value == null)
            {
                return "Not Accepted";
            }
            DateTime dateTime = (DateTime)value;
            return dateTime.ToString();    
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            return value;
		}
	}	
}
