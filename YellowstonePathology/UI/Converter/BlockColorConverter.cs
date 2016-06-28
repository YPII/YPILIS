using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;

namespace YellowstonePathology.UI.Converter
{	
	public class BlockColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string result = "#FFFFFF";

			if (value != null)
			{
				YellowstonePathology.Business.Common.Block block = (YellowstonePathology.Business.Common.Block)value;

				if (block.Verified)
				{
					result = "#77933C";
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
