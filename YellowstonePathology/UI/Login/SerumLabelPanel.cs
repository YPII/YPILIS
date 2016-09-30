using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.Login
{
    public class SerumLabelPanel : System.Windows.Controls.StackPanel
    {        
        public SerumLabelPanel(string title, string cptCode)
        {            
            this.Orientation = Orientation.Vertical;
            this.Width = 75;
            this.Height = 80;
            this.Margin = new Thickness(3, 3, 23, 7);

            TextBlock topLabel = new TextBlock();
            topLabel.Text = title;
            topLabel.TextAlignment = TextAlignment.Center;
            topLabel.FontWeight = FontWeights.Bold;            
            topLabel.Margin = new Thickness(2, 0, 2, 0);
            this.Children.Add(topLabel);

            TextBlock code = new TextBlock();
            code.TextAlignment = TextAlignment.Center;
            code.Text = cptCode;
            code.FontWeight = FontWeights.Bold;
            code.Margin = new Thickness(2, 0, 2, 0);
            this.Children.Add(code);

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
