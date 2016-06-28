using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
namespace YellowstonePathology.UI.Converter
{    
    public class CytologyScreeningCommentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)value;
            string result = string.Empty;
            if (!string.IsNullOrEmpty(cytologyPanelOrder.ScreenerComment))
            {
                result = "(" + cytologyPanelOrder.ScreenerComment + ")";
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
