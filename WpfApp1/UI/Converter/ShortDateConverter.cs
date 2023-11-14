using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;

namespace YellowstonePathology.UI.Converter
{    
    public class ShortDateConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            DateTime dateTime = (DateTime)value;
            return dateTime.ToShortDateString();         
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strValue = value.ToString();
            if (strValue == string.Empty)
            {
                return null;
            }
            else
            {                
                if (strValue.Length == 8)
                {                    
                    if (strValue.IndexOf("/") == -1)
                    {                        
                        string result = strValue.Insert(2, "/");
                        result = result.Insert(5, "/");                        
                        return result;
                    }
                    else
                    {
                        return strValue;
                    }
                }
                else
                {
                    return strValue;
                }
            }
        }
    }	
}
