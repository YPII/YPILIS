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

namespace YellowstonePathology.UI.Gross
{    
    public partial class SecondaryWindow : Window
    {
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;

        public SecondaryWindow()
        {            
            InitializeComponent();
            this.m_PageNavigator = new Navigation.PageNavigator(this.MainContent);
        }

        public YellowstonePathology.UI.Navigation.PageNavigator PageNavigator
        {
            get { return this.m_PageNavigator; }
        }
    }
}
