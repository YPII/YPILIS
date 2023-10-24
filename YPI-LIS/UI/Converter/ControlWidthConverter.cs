using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{
	public class ControlWidthConverter : IValueConverter 
	{
		private Thickness m_Margin = new Thickness(20.0);
		public Thickness Margin
		{
			get { return m_Margin; }
			set { m_Margin = value; }
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType != typeof(double))
			{
				return null;
			}
			double dParentWidth = Double.Parse(value.ToString());
			double dAdjustedWidth = dParentWidth-m_Margin.Left-m_Margin.Right;
			return (dAdjustedWidth < 0 ? 0 : dAdjustedWidth);
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
