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

namespace YellowstonePathology.UI.Surgical
{    
    public partial class PQRSMeasureDialog : Window
    {
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;		        

        public PQRSMeasureDialog()
        {            
            InitializeComponent();
			this.m_PageNavigator = new YellowstonePathology.UI.Navigation.PageNavigator(this.MainContent);
        }        

        public YellowstonePathology.UI.Navigation.PageNavigator PageNavigator
        {
            get { return this.m_PageNavigator; }
        }                
    }
}
