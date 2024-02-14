using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;

namespace YellowstonePathology.UI.Cutting
{
    public class TestOrderPanelSetOrderViewColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            System.Windows.Media.BrushConverter brushConverter = new System.Windows.Media.BrushConverter();
            System.Windows.Media.SolidColorBrush result = brushConverter.ConvertFromString("#FFE2FF") as System.Windows.Media.SolidColorBrush;
            bool cuttingIsComplete = System.Convert.ToBoolean(value);
            if (cuttingIsComplete == true)
            {
                result = result = brushConverter.ConvertFromString("#b4f8b7") as System.Windows.Media.SolidColorBrush;
            }
            return result;       
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
