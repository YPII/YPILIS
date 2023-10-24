using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{	
	public class CytologyLoginSpecimenSourceConverter : IValueConverter
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
			string source = value.ToString();
			if (source.IndexOf("v ") != -1)
			{
				source = source.Replace("v ", "Vaginal. ");
			}
			if (source.IndexOf("c ") != -1)
			{
				source = source.Replace("c ", "Cervical. ");
			}
			if (source.IndexOf("e ") != -1)
			{
				source = source.Replace("e ", "Endocervical. ");
			}
			if (source.IndexOf("ns ") != -1)
			{
				source = source.Replace("ns ", "Not Specified. ");
			}
			return source;
		}
	}
}
