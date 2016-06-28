using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.Login
{
    public class CytologySlideLabelPanel : System.Windows.Controls.StackPanel
    {
        TextBlock m_Title;
        TextBlock m_CRCLine1;
        TextBlock m_CRCLine2;
        TextBlock m_Name;
        TextBlock m_AcidWash;

        public CytologySlideLabelPanel(string crcLine1, string crcLine2, string name, bool acidWash)
        {   
            //Remove CRC if ACID Wash Label
            if (acidWash == true) 
            {
                crcLine2 = crcLine2.Substring(0, 4) + "XXX";
            }

            this.Orientation = Orientation.Vertical;
            this.Width = 75;
            this.Margin = new Thickness(3, 3, 23, 5);

            this.m_Title = new TextBlock();
            this.m_Title.HorizontalAlignment = HorizontalAlignment.Center;
            this.m_Title.Text = "YPII-Blgs";
            this.m_Title.Margin = new Thickness(1, 1, 1, 5);

            this.m_CRCLine1 = new TextBlock();
            this.m_CRCLine1.HorizontalAlignment = HorizontalAlignment.Center;
            this.m_CRCLine1.FontFamily = new System.Windows.Media.FontFamily("OCRAMCE");
            //this.m_CRCLine1.FontWeight = FontWeights.Bold;
            this.m_CRCLine1.FontSize = 13;
            this.m_CRCLine1.Margin = new Thickness(1, 1, 1, 0);
            this.m_CRCLine1.Text = crcLine1;

            this.m_CRCLine2 = new TextBlock();
            this.m_CRCLine2.HorizontalAlignment = HorizontalAlignment.Center;
            this.m_CRCLine2.FontFamily = new System.Windows.Media.FontFamily("OCRAMCE");
            //this.m_CRCLine2.FontWeight = FontWeights.Bold;
            this.m_CRCLine2.FontSize = 13;
            this.m_CRCLine2.Margin = new Thickness(1, 0, 1, 1);
            this.m_CRCLine2.Text = crcLine2;

            this.m_AcidWash = new TextBlock();
            this.m_AcidWash.HorizontalAlignment = HorizontalAlignment.Center;            
            this.m_AcidWash.FontSize = 9;
            this.m_AcidWash.Margin = new Thickness(1, 0, 1, 1);
            this.m_AcidWash.Text = "Acid Wash";

            string trimmedName = string.Empty;
            if (string.IsNullOrEmpty(name) == false)
            {
                if (name.Length >= 11)
                {
                    trimmedName = name.Substring(0, 10);
                }
                else
                {
                    trimmedName = name;
                }
            }
            else
            {
                trimmedName = name;
            }

            this.m_Name = new TextBlock();
            this.m_Name.HorizontalAlignment = HorizontalAlignment.Center;
            this.m_Name.Text = trimmedName;
            this.m_Name.Margin = new Thickness(1, 1, 1, 1);

            this.Children.Add(this.m_Title);
            this.Children.Add(this.m_CRCLine1);
            this.Children.Add(this.m_CRCLine2);
            this.Children.Add(this.m_Name);

            if (acidWash == true)
            {
                this.Children.Add(this.m_AcidWash);
            }
        }
    }
}
