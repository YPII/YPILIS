using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{	
	public class AliquotTypeIsIntraoperativeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool result = false;
			if (value.ToString() == "Intraoperative")
			{
			    result = true;
			}
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string result = "Surgical Block";
			if ((bool)value)
			{
				result = "Intraoperative";
			}
			return result;
		}
	}	
}
