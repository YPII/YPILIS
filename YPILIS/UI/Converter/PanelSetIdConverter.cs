using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{
	[ValueConversion(typeof(Nullable<int>), typeof(string))]
	public class PanelSetIdConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            Nullable<int> panelSetId = (Nullable<int>)value;
            string result = "Not Selected";

            switch (panelSetId)
            {
                case 13:
                    result = "Surgical Pathology";
                    break;
                case 15:
                    result = "Thin Prep Pap";
                    break;             
            }                       
            return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            Nullable<int> result = null;
            switch (value.ToString())
            {
                case "Surgical Pathology":
                    result = 13;
                    break;
                case "Thin Prep Pap":
                    result = 15;
                    break;
            }
            return result;
		}
	}	
}
