using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Login
{
    public class SerumLabel : FixedDocument
    {               
        public SerumLabel(string title, string cptCode)
        {            
            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();

            StackPanel rowPanel = new StackPanel();
            rowPanel.Orientation = Orientation.Horizontal;            
            rowPanel.Margin = new Thickness(5);
            
            SerumLabelPanel panel1 = new SerumLabelPanel(title, cptCode);
            SerumLabelPanel panel2 = new SerumLabelPanel(title, cptCode);
            SerumLabelPanel panel3 = new SerumLabelPanel(title, cptCode);
            SerumLabelPanel panel4 = new SerumLabelPanel(title, cptCode);

            rowPanel.Children.Add(panel1);
            rowPanel.Children.Add(panel2);
            rowPanel.Children.Add(panel3);
            rowPanel.Children.Add(panel4);
            
            fixedPage.Children.Add(rowPanel);
            ((System.Windows.Markup.IAddChild)pageContent).AddChild(fixedPage);
            this.Pages.Add(pageContent);
        }
    }
}
