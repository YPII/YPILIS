using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;

namespace YellowstonePathology.UI.Cutting
{
	public class OrderedAsDualConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            string result = null;
            bool orderedAsDual = System.Convert.ToBoolean(value);
            if (orderedAsDual == true)
            {
                result = "Ordered As Dual";
            }
            return result;       
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
