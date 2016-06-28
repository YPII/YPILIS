using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;

namespace YellowstonePathology.UI.Gross
{
	public class ColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string item = value.ToString();            
			if(string.IsNullOrEmpty(item)) item = "Black";
			System.Windows.Media.BrushConverter brushConverter = new System.Windows.Media.BrushConverter();
			System.Windows.Media.SolidColorBrush brush = brushConverter.ConvertFromString(item) as System.Windows.Media.SolidColorBrush;
			return brush;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
