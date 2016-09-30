using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;

namespace YellowstonePathology.UI.Converter
{	
	public class CaseDocumentColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string result = "#F5F5F5";

			if (value != null)
			{
				YellowstonePathology.Business.Document.CaseDocument caseDocument = (YellowstonePathology.Business.Document.CaseDocument)value;

				if (caseDocument.Received)
				{
					result = "#FFFF00";
				}
				if (caseDocument.Verified)
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
