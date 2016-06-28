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

namespace YellowstonePathology.UI.AppMessaging
{
    /// <summary>
    /// Interaction logic for MessagingDialog.xaml
    /// </summary>
    public partial class MessagingDialog : Window
    {
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;        

        public MessagingDialog()
        {
            InitializeComponent();

            this.m_PageNavigator = new UI.Navigation.PageNavigator(this.MainContent);
            this.m_PageNavigator.PrimaryMonitorWindow = this;
            this.Closing += MessagingDialog_Closing;
        }

        private void MessagingDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        public YellowstonePathology.UI.Navigation.PageNavigator PageNavigator
        {
            get { return this.m_PageNavigator; }
        }

        private void LoginPageWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.m_PageNavigator.Close();
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();            
        }
    }
}
