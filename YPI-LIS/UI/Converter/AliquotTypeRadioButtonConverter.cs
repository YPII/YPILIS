using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{		
	public class AliquotTypeRadioButtonConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			int result = 0;
			if (value.ToString() == "Intraoperative")
			{
				result = 1;
			}
			else if (value.ToString() == "CB")
			{
				result = 2;
			}
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string result = "Surgical Block";
			if ((int)value == 1)
			{
				result = "Intraoperative";
			}
			else if ((int)value == 2)
			{
				result = "CB";
			}
			return result;
		}
	}
}
