using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{	
	public class CytologyPanelBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{            
            System.Windows.Media.Color colorPrimaryScreening = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#F9EDFA");
            System.Windows.Media.Color colorCytotechReview = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#F7F8DD");
            System.Windows.Media.Color colorPathologistReview = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#F7F8DD");            
            
            System.Windows.Media.SolidColorBrush whiteBrush = System.Windows.Media.Brushes.WhiteSmoke;

            System.Windows.Media.SolidColorBrush primaryScreeningBrush = new System.Windows.Media.SolidColorBrush(colorPrimaryScreening);
            System.Windows.Media.SolidColorBrush cytotechReviewBrush = new System.Windows.Media.SolidColorBrush(colorCytotechReview);
            System.Windows.Media.SolidColorBrush pathologistReviewBrush = new System.Windows.Media.SolidColorBrush(colorPathologistReview);            

            string screeningType = (string)value;
            System.Windows.Media.SolidColorBrush result = null;            
            switch (screeningType)
            {
                case "Primary Screening":
                    result = primaryScreeningBrush;
                    break;
                case "Cytotech Review":
                    result = cytotechReviewBrush;
                    break;
                case "Pathologist Review":
                    result = pathologistReviewBrush;
                    break;                
                default:
                    result = whiteBrush;
                    break;
            }
            return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            return string.Empty;
		}
	}	
}
