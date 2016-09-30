using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{	
	public class MilitaryDateTimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
			{
				return string.Empty;
			}

			DateTime dateTime = (DateTime)value;
			//if(dateTime.ToShortTimeString() == "12:00 AM")
			//{
                //return dateTime.ToString("MM/dd/yyy");
			//}

			return dateTime.ToString("MM/dd/yyyy HH:mm");
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
				string fmtString = strValue.Trim();				
                if (strValue.IndexOf("/") == -1)
                {                    
                    fmtString = fmtString.Insert(2, "/");
                    fmtString = fmtString.Insert(5, "/");                    
                }
                else if (fmtString.IndexOf(" ") != -1)
                {
                    int indexOfSpace = fmtString.IndexOf(" ");                    
                    if (fmtString.IndexOf(":") == -1)
                    {
                        string timeSubstring = fmtString.Substring(indexOfSpace + 1);
                        if (timeSubstring.Length == 4)
                        {
                            fmtString = fmtString.Insert(indexOfSpace + 3, ":");
                        }
                    }
                }
                return fmtString;
			}
		}
	}
}
