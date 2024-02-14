using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Gross
{
	public class GrossVerifiedColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{			
			string item = value.ToString();			
			System.Windows.Media.SolidColorBrush result = System.Windows.Media.Brushes.Yellow;
			switch (item)
			{
				case"Created":
					result = System.Windows.Media.Brushes.White;
					break;
				case "Printed":
					result = System.Windows.Media.Brushes.Red;
					break;
				case "Validated":
					result = System.Windows.Media.Brushes.Green;
					break;
				case "PrintRequested":
					result = System.Windows.Media.Brushes.MediumPurple;
					break;
			}
			return result;			
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
