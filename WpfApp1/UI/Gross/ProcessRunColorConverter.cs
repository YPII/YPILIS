using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Gross
{
	[ValueConversion(typeof(YellowstonePathology.Business.Slide.Model.SlideStatusEnum), typeof(System.Windows.Media.SolidColorBrush))]
    public class ProcessRunColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            DateTime incomingDate;
            bool parsed = DateTime.TryParse(value.ToString(), out incomingDate);

            Nullable<DateTime> processorStartTime;
            if (parsed == true)
            {
                processorStartTime = new DateTime(incomingDate.Ticks);
            }
            else
            {
                processorStartTime = null;
            }

            System.Windows.Media.SolidColorBrush result = System.Windows.Media.Brushes.Blue;
            if (processorStartTime.HasValue == true)
            {
                if (processorStartTime.Value.Day >= DateTime.Today.Day)
                {
                    result = System.Windows.Media.Brushes.Red;
                }
            }         

            return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            return string.Empty;
		}
	}	
}
