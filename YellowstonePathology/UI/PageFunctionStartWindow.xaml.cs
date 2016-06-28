using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PageFunctionStartWindow.xaml
    /// </summary>
    public partial class PageFunctionStartWindow : Window
    {
        PageFunction<bool> m_Page;

        public PageFunctionStartWindow(PageFunction<bool> page)
        {
            this.m_Page = page;
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PageFunctionStartWindow_Loaded);
        }

        private void PageFunctionStartWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.NavigationFrame.Navigate(this.m_Page);
        }
    }
}
