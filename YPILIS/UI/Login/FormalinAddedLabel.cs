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
    public class FormalinAddedLabel : FixedDocument
    {
        public FormalinAddedLabel()
        {            
            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();

            StackPanel rowPanel = new StackPanel();
            rowPanel.Orientation = Orientation.Horizontal;            
            rowPanel.Margin = new Thickness(10,5,5,5);

            FormalinAddedLabelPanel panel1 = new FormalinAddedLabelPanel();
            FormalinAddedLabelPanel panel2 = new FormalinAddedLabelPanel();
            FormalinAddedLabelPanel panel3 = new FormalinAddedLabelPanel();
            FormalinAddedLabelPanel panel4 = new FormalinAddedLabelPanel();

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
