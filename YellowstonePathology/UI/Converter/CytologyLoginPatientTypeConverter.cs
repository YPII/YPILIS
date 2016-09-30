using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{	
	public class CytologyLoginPatientTypeConverter : IValueConverter
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
			string patientType = value.ToString().ToUpper();
			switch (patientType)
			{
				case "IP":
				case "I":
				case "C":
					patientType = "IP";
					break;
				case "NOT SELECTED":
					patientType = "Not Selected";
					break;
				default:
					patientType = "OP";
					break;
			}
			return patientType;
		}
	}
}
