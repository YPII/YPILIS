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
    public partial class PageNavigationWindow : Window
    {
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public PageNavigationWindow(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {            
            this.m_SystemIdentity = systemIdentity;
            InitializeComponent();            
            this.m_PageNavigator = new UI.Navigation.PageNavigator(this.MainContent);
            this.Closing += new System.ComponentModel.CancelEventHandler(LoginPageWindow_Closing);
        }

        public YellowstonePathology.UI.Navigation.PageNavigator PageNavigator
        {
            get { return this.m_PageNavigator; }
        }

        private void LoginPageWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.m_PageNavigator.Close();            
        }        

		public YellowstonePathology.Business.User.SystemIdentity SystemIdentity
		{
			set { this.m_SystemIdentity = value; }
		}        
    }
}
