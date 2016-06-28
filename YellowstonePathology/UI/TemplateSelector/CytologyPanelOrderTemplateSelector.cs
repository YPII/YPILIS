using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace YellowstonePathology.UI.TemplateSelector
{
    public class CytologyPanelOrderTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            YellowstonePathology.UI.MainWindow mainWindow = Application.Current.MainWindow as YellowstonePathology.UI.MainWindow;
            UserControl userControl = null;
            if (((TabItem)mainWindow.TabControlLeftWorkspace.SelectedItem).Content.GetType().Name == "CytologyWorkspace")
            {
                userControl = mainWindow.CytologyWorkspace.CytologyResultsWorkspace;
            }
            else if (((TabItem)mainWindow.TabControlLeftWorkspace.SelectedItem).Content.GetType().Name == "PathologistWorkspace")
            {
                //userControl = mainWindow.PathologistWorkspace.PathologistWorkspaceUI.CytologyResultsWorkspace;
				userControl = mainWindow.PathologistWorkspace.CytologyResultsWorkspace;
			}

            if (item != null)
            {
                YellowstonePathology.Business.Test.PanelOrder panelOrder = (YellowstonePathology.Business.Test.PanelOrder)item;
                switch (panelOrder.PanelId)
                {
                    case 39:
                        return userControl.FindResource("AcidWashPanelOrderDataTemplate") as DataTemplate;
                    case 38:
                        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                        if (cytologyPanelOrder.ScreeningType.ToUpper() == "DOT REVIEW")
                        {
                            return userControl.FindResource("CytologyDotReviewDataTemplate") as DataTemplate;
                        }
                        return userControl.FindResource("CytologyPanelOrderDataTemplate") as DataTemplate;
                }
            }
            return userControl.FindResource("CytologyPanelOrderDataTemplate") as DataTemplate;
        }
    }
}
