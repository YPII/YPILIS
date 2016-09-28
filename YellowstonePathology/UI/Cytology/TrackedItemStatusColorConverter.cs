using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Cytology
{
	[ValueConversion(typeof(YellowstonePathology.Business.TrackedItemStatusEnum), typeof(System.Windows.Media.SolidColorBrush))]
	public class TrackedItemStatusColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            if (value == null) return System.Windows.Media.Brushes.Gray;

            YellowstonePathology.Business.TrackedItemStatusEnum slideStatus = (YellowstonePathology.Business.TrackedItemStatusEnum)Enum.Parse(typeof(YellowstonePathology.Business.TrackedItemStatusEnum), value.ToString());
            System.Windows.Media.SolidColorBrush result = System.Windows.Media.Brushes.Yellow;  
          
            switch (slideStatus)
            {
                case YellowstonePathology.Business.TrackedItemStatusEnum.Created:
                    result = System.Windows.Media.Brushes.Gray;
                    break;
                case YellowstonePathology.Business.TrackedItemStatusEnum.PrintRequested:
                    result = System.Windows.Media.Brushes.MediumPurple;
                    break;
                case YellowstonePathology.Business.TrackedItemStatusEnum.Printed:
                    result = System.Windows.Media.Brushes.Yellow;
                    break;
                case YellowstonePathology.Business.TrackedItemStatusEnum.Validated:
                    result = System.Windows.Media.Brushes.Green;
                    break;
                case YellowstonePathology.Business.TrackedItemStatusEnum.ClientAccessioned:
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
