using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.Login
{
    public class FormalinAddedLabelPanel : System.Windows.Controls.StackPanel
    {
        public FormalinAddedLabelPanel()
        {            
            this.Orientation = Orientation.Vertical;
            this.Width = 78;
            this.Height = 80;
            this.Margin = new Thickness(0, 3, 23, 7);

            TextBlock line1 = new TextBlock();
            line1.Text = "Formalin";
            line1.TextAlignment = TextAlignment.Left;
            line1.FontWeight = FontWeights.Bold;
            line1.Margin = new Thickness(2, 0, 2, 0);
            this.Children.Add(line1);

            TextBlock line2 = new TextBlock();
            line2.Text = "Added";
            line2.TextAlignment = TextAlignment.Left;
            line2.FontWeight = FontWeights.Bold;
            line2.Margin = new Thickness(2, 0, 2, 0);
            this.Children.Add(line2);            
        }
    }
}
