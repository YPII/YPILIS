using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;

namespace YellowstonePathology.UI.Test
{
    public class EGFRToALKReflexAnalysisStatusColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            System.Windows.Media.SolidColorBrush result = null;
            System.Windows.Media.BrushConverter brushConverter = new System.Windows.Media.BrushConverter();            

            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisElementStatusEnum status = (YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisElementStatusEnum)Enum.Parse(typeof(YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisElementStatusEnum), value.ToString());
            switch (status)
            {
                case Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisElementStatusEnum.Pending:
                    result = brushConverter.ConvertFromString("#98f6f6") as System.Windows.Media.SolidColorBrush;
                    break;
                case Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisElementStatusEnum.Ordered:
                    result = brushConverter.ConvertFromString("#f3f8aa") as System.Windows.Media.SolidColorBrush;
                    break;
                case Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisElementStatusEnum.OrderRequired:
                    result = brushConverter.ConvertFromString("#f8aab5") as System.Windows.Media.SolidColorBrush;
                    break;
                case Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisElementStatusEnum.Accepted:
                    result = brushConverter.ConvertFromString("#f8aaef") as System.Windows.Media.SolidColorBrush;
                    break;
                case Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisElementStatusEnum.Final:                    
                    result = brushConverter.ConvertFromString("#c4f8aa") as System.Windows.Media.SolidColorBrush;
                    break;
                case Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisElementStatusEnum.QNS:
                case Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisElementStatusEnum.NotIndicated:
                case Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisElementStatusEnum.NotOrdered:
                    result = System.Windows.Media.Brushes.White;
                    break;
                default:
                    result = System.Windows.Media.Brushes.White;
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
