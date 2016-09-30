using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.Login
{
    public class IFELabelPanel : System.Windows.Controls.StackPanel
    {
        public IFELabelPanel()
        {            
            this.Orientation = Orientation.Vertical;
            this.Width = 75;
            this.Height = 80;
            this.Margin = new Thickness(3, 3, 23, 7);

            TextBlock topLabel = new TextBlock();
            topLabel.Text = "IFE";
            topLabel.TextAlignment = TextAlignment.Center;
            topLabel.FontWeight = FontWeights.Bold;            
            topLabel.Margin = new Thickness(2, 0, 2, 0);
            this.Children.Add(topLabel);

            TextBlock code1 = new TextBlock();
            code1.TextAlignment = TextAlignment.Center;
            code1.Text = "86334-26 x1";
            code1.FontWeight = FontWeights.Normal;
            code1.FontSize = 8;
            code1.Margin = new Thickness(2, 0, 2, 0);
            this.Children.Add(code1);

            TextBlock code2 = new TextBlock();
            code2.TextAlignment = TextAlignment.Center;
            code2.Text = "82784-26 x3";
            code2.FontWeight = FontWeights.Normal;
            code2.FontSize = 8;
            code2.Margin = new Thickness(2, 0, 2, 0);
            this.Children.Add(code2);

            TextBlock md = new TextBlock();
            md.Text = "MD";
            md.TextAlignment = TextAlignment.Right;
            md.FontWeight = FontWeights.Bold;
            md.Margin = new Thickness(2, 5, 2, 2);
            this.Children.Add(md);

            TextBlock date = new TextBlock();
            date.Text = "  /    /16";
            date.TextAlignment = TextAlignment.Center;
            date.FontWeight = FontWeights.Bold;
            date.Margin = new Thickness(2, 7, 2, 2);
            this.Children.Add(date);       
        }
    }
}
