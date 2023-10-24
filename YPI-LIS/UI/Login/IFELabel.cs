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
    public class IFELabel : FixedDocument
    {               
        public IFELabel()
        {            
            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();

            StackPanel rowPanel = new StackPanel();
            rowPanel.Orientation = Orientation.Horizontal;            
            rowPanel.Margin = new Thickness(5);

            IFELabelPanel panel1 = new IFELabelPanel();
            IFELabelPanel panel2 = new IFELabelPanel();
            IFELabelPanel panel3 = new IFELabelPanel();
            IFELabelPanel panel4 = new IFELabelPanel();

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
