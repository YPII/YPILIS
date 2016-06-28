using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{	
	public class SystemUserIdConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            string result = string.Empty;
            if (value != null)
            {
                int systemUserId = (int)value;
                if (systemUserId != 0)
                {
					YellowstonePathology.Business.User.SystemUserCollection systemUserCollection = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection;                    
					YellowstonePathology.Business.User.SystemUser user = systemUserCollection.GetSystemUserById(systemUserId);
					result = user.DisplayName;
                }
            }
            return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            return null;
		}
	}
}
