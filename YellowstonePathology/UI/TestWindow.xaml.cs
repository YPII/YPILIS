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
using StackExchange.Redis;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private IDatabase m_DB;
        private ISubscriber m_SUB;

        public TestWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.m_DB = YellowstonePathology.Business.RedisConnection.Instance.GetDatabase();
            this.m_SUB = YellowstonePathology.Business.RedisConnection.Instance.GetSubscriber();
        }                

        private void Button_SubscribeClick(object sender, RoutedEventArgs e)
        {
            this.m_SUB.Subscribe("Messages", (channel, message) =>
            {
                MessageBox.Show((string)message);
            });
        }

        private void Button_PublishClick(object sender, RoutedEventArgs e)
        {
            string masterAccessionno = "16-12345";
            YellowstonePathology.Business.User.SystemUser user = YellowstonePathology.Business.User.SystemIdentity.Instance.User;
            UI.AppMessaging.AccessionLockMessage message = new AppMessaging.AccessionLockMessage(masterAccessionno, System.Environment.MachineName, user.UserName, AppMessaging.AccessionLockMessageIdEnum.ASK);
            string messageJSON = message.ToJSON();
            this.m_SUB.Publish(masterAccessionno, messageJSON);        
        }
    }
}
