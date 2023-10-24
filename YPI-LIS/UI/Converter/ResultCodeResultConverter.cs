using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;

namespace YellowstonePathology.UI.Converter
{
	public class ResultCodeResultConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (values[0] != null)
			{
				string result = values[0] as string;
				string resultCode = values[1] as string;
				string display = result + ", " + resultCode;
				return display;
			}
			return null;

		}

		public object[] ConvertBack(object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			if (values != null)
			{
				YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)values;
				object[] display = new string[2];
				display[0] = testResult.Result;
				display[1] = testResult.ResultCode;
				return display;
			}
			return null;
		}
	}
}
