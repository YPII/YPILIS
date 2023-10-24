using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{
	public class CytologyLoginIcd9Converter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
			{
				return value;
			}

			switch (value.ToString().ToUpper())
			{
				case "R":
					return "V76.2";
				case "H":
					return "V15.89";
				default:
					return value.ToString().ToUpper();
			}
		}
	}
}
