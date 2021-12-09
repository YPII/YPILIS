using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for RecutCommentsDialog.xaml
    /// </summary>
    public partial class RecutCommentsDialog : Window
    {
        private UI.Surgical.StainOrder m_StainOrder;

        public RecutCommentsDialog(UI.Surgical.StainOrder stainOrder)
        {
            this.m_StainOrder = stainOrder;
            InitializeComponent();
        }        

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {            
            string selectedComment = this.GetSelectedComment();
            if (string.IsNullOrEmpty(selectedComment) == false)
                this.m_StainOrder.PanelOrderComment = selectedComment;
            this.Close();
        }

        private string GetSelectedComment()
        {
            string result = null;
            IEnumerable<RadioButton> radioButtons = FindVisualChildren<RadioButton>(this);
            foreach (RadioButton radioButton in radioButtons)
            {
                if (radioButton.IsChecked == true)
                {
                    result = $"{radioButton.Tag.ToString()}: {radioButton.Content.ToString()}";
                    break;
                }                    
            }
            return result;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
