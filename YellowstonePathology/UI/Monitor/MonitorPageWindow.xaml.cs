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

namespace YellowstonePathology.UI.Monitor
{    
    public partial class MonitorPageWindow : Window
    {
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;        

        public MonitorPageWindow()
        {                        
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
            Business.Persistence.DocumentGateway.Instance.Push(this);
        }        		
    }
}
