using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Cutting
{
	[ValueConversion(typeof(YellowstonePathology.Business.Slide.Model.SlideStatusEnum), typeof(System.Windows.Media.SolidColorBrush))]
	public class SlideOrderStatusColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            YellowstonePathology.Business.Slide.Model.SlideStatusEnum slideStatus = (YellowstonePathology.Business.Slide.Model.SlideStatusEnum)Enum.Parse(typeof(YellowstonePathology.Business.Slide.Model.SlideStatusEnum), value.ToString());
            System.Windows.Media.SolidColorBrush result = System.Windows.Media.Brushes.Yellow;  
          
            switch (slideStatus)
            {
                case YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Created:
                    result = System.Windows.Media.Brushes.Gray;
                    break;
                case YellowstonePathology.Business.Slide.Model.SlideStatusEnum.PrintRequested:
                    result = System.Windows.Media.Brushes.MediumPurple;
                    break;                                
                case YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Printed:
                    result = System.Windows.Media.Brushes.Yellow;
                    break;
                case YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Validated:
                    result = System.Windows.Media.Brushes.Green;
                    break;
                case YellowstonePathology.Business.Slide.Model.SlideStatusEnum.ClientAccessioned:
                    result = System.Windows.Media.Brushes.SteelBlue;
                    break;   
                default:
                    result = System.Windows.Media.Brushes.Gray;
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
