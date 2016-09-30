using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace YellowstonePathology.UI.TemplateSelector
{
    public class TaskOrderDetailTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {            
            if (item != null)
            {                
                YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = (YellowstonePathology.Business.Task.Model.TaskOrderDetail)item;
                FrameworkElement element = container as FrameworkElement;

                if (taskOrderDetail.TaskId == "FDXSHPMNT")
                {
                    return element.FindResource("FedexShipmentTaskOrderDetailDataTemplate") as DataTemplate;
                }
                else
                {
                    return element.FindResource("GenericTaskOrderDetailDataTemplate") as DataTemplate;
                }                    
            }

            return null;          
        }
    }
}
