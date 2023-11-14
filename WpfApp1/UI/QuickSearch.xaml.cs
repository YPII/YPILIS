using System;
using System.Collections.Generic;
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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>

    public partial class QuickSearch : System.Windows.Controls.UserControl
    {

        public delegate void QuickSearchHandler();
        public event QuickSearchHandler doQuickSearch;

        public string SearchType;
        public DateTime AccessionDate;

        public QuickSearch()
        {
            InitializeComponent();
        }

        public void onQuickSearch_Click(object sender, RoutedEventArgs args)
        {
            this.AccessionDate = DateTime.Parse("3/15/07");
            this.SearchType = "AccessionDate";
            doQuickSearch();
        }
    }
}