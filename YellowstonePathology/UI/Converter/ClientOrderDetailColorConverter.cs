using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;

namespace YellowstonePathology.UI.Converter
{	
	public class ClientOrderDetailColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string result = "#FF0000";

			if (value != null)
			{
				YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail)value;

				if (clientOrderDetail.Submitted)
				{
					result = "#FF0000";
				}
				if (clientOrderDetail.Received)
				{
					result = "#FFFF00";
				}
				if (clientOrderDetail.Accessioned)
				{
					result = "#FF9E9E";  //"#77933C";
				}
				if (clientOrderDetail.Validated)
				{
					result = "#77933C"; //"#FFD700";
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
