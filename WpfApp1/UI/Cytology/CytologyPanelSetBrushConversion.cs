using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Cytology
{
	[ValueConversion(typeof(YellowstonePathology.Business.Interface.IPanelSetOrder), typeof(System.Windows.Media.SolidColorBrush))]
	class CytologyPanelSetBrushConversion : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{            
            System.Windows.Media.Color colorFinal = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#000000");
            System.Windows.Media.Color colorNotFinal = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#E10A38");                                   

            System.Windows.Media.SolidColorBrush brushFinal = new System.Windows.Media.SolidColorBrush(colorFinal);
            System.Windows.Media.SolidColorBrush brushNotFinal = new System.Windows.Media.SolidColorBrush(colorNotFinal);

            YellowstonePathology.Business.Interface.IPanelSetOrder panelSetOrder = (YellowstonePathology.Business.Interface.IPanelSetOrder)value;
            if (panelSetOrder != null)
            {
                if (panelSetOrder.Final == true)
                {
                    return brushFinal;
                }
                else
                {
                    return brushNotFinal;
                }
            }
            else
            {
                return colorFinal;
            }
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            return string.Empty;
		}
	}	
}
