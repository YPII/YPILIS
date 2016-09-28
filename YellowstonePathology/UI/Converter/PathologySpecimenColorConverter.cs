using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;

namespace YellowstonePathology.UI.Converter
{	
	public class PathologistSpecimenColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string result = "#ffffff";

			if (value != null)
			{
                bool isSelected = (bool)value;

				if (isSelected == true)
				{
					result = "#fbfac5";
				}
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
