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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for AutoCompleteTest.xaml
    /// </summary>
    public partial class AutoCompleteTest : Window
    {
        private List<string> m_Data;

        public AutoCompleteTest()
        {
            this.m_Data = new List<string>();

            this.m_Data.Add("Afzaal");
            this.m_Data.Add("Ahmad");
            this.m_Data.Add("Zeeshan");
            this.m_Data.Add("Daniyal");
            this.m_Data.Add("Rizwan");
            this.m_Data.Add("John");
            this.m_Data.Add("Doe");
            this.m_Data.Add("Johanna Doe");
            this.m_Data.Add("Pakistan");
            this.m_Data.Add("Microsoft");
            this.m_Data.Add("Programming");
            this.m_Data.Add("Visual Studio");
            this.m_Data.Add("Sofiya");
            this.m_Data.Add("Rihanna");
            this.m_Data.Add("Eminem");            

            InitializeComponent();
        }

        private void TextBoxAutoComplet_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;            

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                resultStack.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                border.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list   
            resultStack.Children.Clear();

            // Add the result   
            foreach (var obj in this.m_Data)
            {
                if (obj.ToLower().StartsWith(query.ToLower()))
                {
                    // The word starts with this... Autocomplete must work   
                    addItem(obj);
                    found = true;
                }
            }

            if (!found)
            {
                resultStack.Children.Add(new TextBlock() { Text = "No results found." });
            }
        }

        private void addItem(string text)
        {
            TextBlock block = new TextBlock();

            // Add the text   
            block.Text = text;

            // A little style...   
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            // Mouse events   
            block.MouseLeftButtonUp += (sender, e) =>
            {
                this.TextBoxAutoComplete.Text = (sender as TextBlock).Text;
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.PeachPuff;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            // Add to the panel   
            resultStack.Children.Add(block);
        }
    }
}
