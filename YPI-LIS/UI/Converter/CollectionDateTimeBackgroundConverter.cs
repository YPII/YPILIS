using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{
    public class CollectionDateTimeBackgroundConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            string result = string.Empty;

            if (value != null)
            {
                DateTime collectionDateTime = DateTime.Parse(value.ToString());
                if (YellowstonePathology.Business.Helper.DateTimeExtensions.DoesDateHaveTime(collectionDateTime) == true)
                {
                    result = "#5CF353";
                }
                else
                {
                    result = "#EAF353";
                }
            }
            else
            {
                result = "#F35364";
            }

            System.Windows.Media.BrushConverter brushConverter = new System.Windows.Media.BrushConverter();
            System.Windows.Media.SolidColorBrush brush = brushConverter.ConvertFromString(result) as System.Windows.Media.SolidColorBrush;
            return brush;			
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            return null;
		}
	}
}
