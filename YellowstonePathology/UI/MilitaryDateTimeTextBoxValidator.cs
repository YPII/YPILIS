using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class MilitaryDateTimeTextBoxValidator
    {
        private static Color ErrorColor = (Color)ColorConverter.ConvertFromString("#d4244b");

        private TextBox m_MilitaryDateTimeTextBox;
        private Brush m_CurrentForegroundColor;

        public MilitaryDateTimeTextBoxValidator(TextBox militaryDateTimeTextBox)
        {
            this.m_MilitaryDateTimeTextBox = militaryDateTimeTextBox;
            this.m_MilitaryDateTimeTextBox.GotFocus += new RoutedEventHandler(MilitaryDateTimeTextBox_GotFocus);
            this.m_MilitaryDateTimeTextBox.LostFocus += new RoutedEventHandler(MilitaryDateTimeTextBox_LostFocus);
            this.m_CurrentForegroundColor = this.m_MilitaryDateTimeTextBox.Foreground;
        }

        private void MilitaryDateTimeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.IsValid() == false)
            {
                this.m_MilitaryDateTimeTextBox.Foreground = new SolidColorBrush(ErrorColor);
            }            
        }

        private void MilitaryDateTimeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.m_MilitaryDateTimeTextBox.Foreground = this.m_CurrentForegroundColor;
        }

        public bool IsOkToUpdateBindingTarget()
        {
            return this.IsValid();
        }

        private bool IsValid()
        {
            bool result = true;
            string formattedDateString = YellowstonePathology.Business.Helper.DateTimeExtensions.FormatDateTimeString(this.m_MilitaryDateTimeTextBox.Text);
            if (YellowstonePathology.Business.Helper.DateTimeExtensions.IsCorrectFormat(formattedDateString) == true)
            {
                this.m_MilitaryDateTimeTextBox.Text = formattedDateString;
                if (YellowstonePathology.Business.Helper.DateTimeExtensions.IsStringAValidDate(formattedDateString) == false)
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void UpdateBindingSource()
        {
            this.m_MilitaryDateTimeTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
