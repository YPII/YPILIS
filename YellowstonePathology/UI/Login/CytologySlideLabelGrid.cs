using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.Login
{
    public class CytologySlideLabelGrid : System.Windows.Controls.Grid
    {
        //TextBlock m_Title;
        TextBlock m_CRCLine1;
        TextBlock m_CRCLine2;
        //TextBlock m_Name;
        //TextBlock m_AcidWash;

        public CytologySlideLabelGrid(string crcLine1, string crcLine2, string name, bool acidWash)
        {                                       
            this.Margin = new Thickness(2, 2, 2, 2);

            RowDefinition rowDefinition1 = new RowDefinition();
            rowDefinition1.Height = GridLength.Auto;

            RowDefinition rowDefinition2 = new RowDefinition();
            rowDefinition2.Height = GridLength.Auto;

            RowDefinition rowDefinition3 = new RowDefinition();            

            this.RowDefinitions.Add(rowDefinition1);
            this.RowDefinitions.Add(rowDefinition2);
            this.RowDefinitions.Add(rowDefinition3);

            ColumnDefinition columneDefinition1 = new ColumnDefinition();
            columneDefinition1.Width = new GridLength(200);
            this.ColumnDefinitions.Add(columneDefinition1);

            //this.m_Title = new TextBlock();
            //this.m_Title.HorizontalAlignment = HorizontalAlignment.Center;
            //this.m_Title.Text = "YPII-Blgs";
            //this.m_Title.Margin = new Thickness(1, 1, 1, 5);

            this.m_CRCLine1 = new TextBlock();
            this.m_CRCLine1.HorizontalAlignment = HorizontalAlignment.Center;
            this.m_CRCLine1.FontFamily = new System.Windows.Media.FontFamily("OCRAMCE");            
            this.m_CRCLine1.FontSize = 13;
            this.m_CRCLine1.Margin = new Thickness(1, 1, 1, 0);
            this.m_CRCLine1.Text = crcLine1;
            this.m_CRCLine1.SetValue(Grid.ColumnProperty, 0);
            this.m_CRCLine1.SetValue(Grid.RowProperty, 0);

            this.m_CRCLine2 = new TextBlock();
            this.m_CRCLine2.HorizontalAlignment = HorizontalAlignment.Center;
            this.m_CRCLine2.FontFamily = new System.Windows.Media.FontFamily("OCRAMCE");            
            this.m_CRCLine2.FontSize = 13;
            this.m_CRCLine2.Margin = new Thickness(1, 0, 1, 1);
            this.m_CRCLine2.Text = crcLine2;
            this.m_CRCLine2.SetValue(Grid.ColumnProperty, 0);
            this.m_CRCLine2.SetValue(Grid.RowProperty, 1);

            //this.m_AcidWash = new TextBlock();
            //this.m_AcidWash.HorizontalAlignment = HorizontalAlignment.Center;            
            //this.m_AcidWash.FontSize = 9;
            //this.m_AcidWash.Margin = new Thickness(1, 0, 1, 1);
            //this.m_AcidWash.Text = "Acid Wash";

            //string trimmedName = this.GetName(name);
            
            //this.m_Name = new TextBlock();
            //this.m_Name.HorizontalAlignment = HorizontalAlignment.Center;
            //this.m_Name.Text = trimmedName;
            //this.m_Name.Margin = new Thickness(1, 1, 1, 1);

            //this.Children.Add(this.m_Title);
            this.Children.Add(this.m_CRCLine1);
            this.Children.Add(this.m_CRCLine2);
            //this.Children.Add(this.m_Name);

            //if (acidWash == true)
            //{
            //    this.Children.Add(this.m_AcidWash);
            //}
        }

        private string GetName(string name)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(name) == false)
            {
                if (name.Length >= 11)
                {
                    result = name.Substring(0, 10);
                }
                else
                {
                    result = name;
                }
            }
            else
            {
                result = name;
            }
            return result;
        }
    }
}
