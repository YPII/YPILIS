using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace YellowstonePathology.UI.TemplateSelector
{
	public class OrderItemTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			YellowstonePathology.UI.Common.OrderDialog openWindow = null;
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType().ToString() == "YellowstonePathology.UI.Common.OrderDialog")
				{
					openWindow = window as YellowstonePathology.UI.Common.OrderDialog;
					break;
				}
			}
			if (item != null)
			{
				System.Xml.Linq.XContainer xContainer = item as System.Xml.Linq.XContainer;
				XElement templateNameElement = xContainer.Element("TemplateName");
				if (templateNameElement != null) return ((System.Windows.Controls.Grid)openWindow.Content).FindResource(templateNameElement.Value) as DataTemplate;
				return ((System.Windows.Controls.Grid)openWindow.Content).FindResource("StandardCheckBoxTemplate") as DataTemplate;
			}
			return ((YellowstonePathology.UI.Common.OrderDialog)openWindow.Content).FindResource("StandardCheckBoxTemplate") as DataTemplate;
		}

	}
}
