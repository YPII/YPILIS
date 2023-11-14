using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;

namespace YellowstonePathology.UI.Converter
{
	public class ItemsReceivedColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            bool isReceived = System.Convert.ToBoolean(value);

            System.Windows.Media.BrushConverter brushConverter = new System.Windows.Media.BrushConverter();
            System.Windows.Media.SolidColorBrush brush = null; 
            
            if (isReceived == true)
            {
                brush = brushConverter.ConvertFromString("#9aeb93") as System.Windows.Media.SolidColorBrush;
            }
            else
            {
                brush = brushConverter.ConvertFromString("#eb93a2") as System.Windows.Media.SolidColorBrush;
            }			
			
			return brush;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
