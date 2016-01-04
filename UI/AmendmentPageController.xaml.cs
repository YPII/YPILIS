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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for AmendmentPageController.xaml
    /// </summary>
    public partial class AmendmentPageController : Window
    {       
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;

        public AmendmentPageController()
        {
            InitializeComponent();

            this.m_PageNavigator = new UI.Navigation.PageNavigator(this.MainContent);
            this.Closing += new System.ComponentModel.CancelEventHandler(AmendmentPageController_Closing);
        }

        public YellowstonePathology.UI.Navigation.PageNavigator PageNavigator
        {
            get { return this.m_PageNavigator; }
        }

        private void AmendmentPageController_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.m_PageNavigator.Close();
        }
    }
}
