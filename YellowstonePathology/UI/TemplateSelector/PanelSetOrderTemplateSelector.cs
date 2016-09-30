using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.TemplateSelector
{
    public class PanelSetOrderTemplateSelector : DataTemplateSelector
    {
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (item != null)
			{
				YellowstonePathology.UI.MainWindow mainWindow = Application.Current.MainWindow as YellowstonePathology.UI.MainWindow;
				YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(((YellowstonePathology.Business.Test.PanelSetOrder)item).PanelSetId);
                if (panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument ||
                    panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.RetiredTestDocument)
				{
					return mainWindow.LabWorkspace.FindResource("DataTemplatePublishedDocument") as DataTemplate;
				}
				else
				{
					switch (panelSet.PanelSetId)
					{
						case 15: //PanelSetOrderCytology
							return mainWindow.LabWorkspace.FindResource("DataTemplateCytologyResult") as DataTemplate;
						default:
							return mainWindow.LabWorkspace.FindResource("DataTemplateResultPath") as DataTemplate;
					}
				}
			}
			return null;
		}
    }
}
