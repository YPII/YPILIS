using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.TemplateSelector
{
	public class ClientOrderMediaTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (item != null)
			{
				UserControl userControl = null;
				foreach (Window window in Application.Current.Windows)
				{					
					if (window.GetType() == typeof(Login.Receiving.LoginPageWindow))
					{
                        Login.Receiving.LoginPageWindow controller = (Login.Receiving.LoginPageWindow)window;
						userControl = (UserControl)controller.MainContent.Content;
						break;
					}
					else if (window.GetType() == typeof(YellowstonePathology.UI.Login.Receiving.LoginPageWindow))
					{
						YellowstonePathology.UI.Login.Receiving.LoginPageWindow controller = (YellowstonePathology.UI.Login.Receiving.LoginPageWindow)window;
						userControl = (UserControl)controller.MainContent.Content;
						break;
					}
				}
				if (item.GetType() == typeof(YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia))
				{
					YellowstonePathology.Business.ClientOrder.Model.ClientOrderMediaEnum clientOrderMediaEnum = ((YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia)item).ClientOrderMediaEnum;
					switch (clientOrderMediaEnum)
					{						
						default:					
							if (userControl.GetType() == typeof(Login.Receiving.ItemsReceivedPage))
							{
								return ((Login.Receiving.ItemsReceivedPage)userControl).MainGrid.FindResource("SpecimenTemplate") as DataTemplate;
							}
							return null;
					}
				}				
			}
			return null;
		}
	}
}
