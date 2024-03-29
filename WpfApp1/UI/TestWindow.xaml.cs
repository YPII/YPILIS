﻿using System;
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
using System.IO;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        //private IDatabase m_DB;
        //private ISubscriber m_SUB;

        public TestWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            //Business.RedisLocksConnection redis = new Business.RedisLocksConnection(Business.RedisDatabaseEnum.Default);
            //this.m_DB = redis.Db;
            //this.m_SUB = redis.Subscriber;
        }                

        private void Button_SubscribeClick(object sender, RoutedEventArgs e)
        {
            /*
            this.m_SUB.Subscribe("Messages", (channel, message) =>
            {
                MessageBox.Show((string)message);
            });
            */
        }

        private void Button_PublishClick(object sender, RoutedEventArgs e)
        {
            /*
            string masterAccessionno = "16-12345";
            YellowstonePathology.Business.User.SystemUser user = Business.User.SystemIdentity.Instance.User;
            UI.AppMessaging.AccessionLockMessage message = new AppMessaging.AccessionLockMessage(masterAccessionno, System.Environment.MachineName, user.UserName, AppMessaging.AccessionLockMessageIdEnum.ASK);
            string messageJSON = message.ToJSON();
            this.m_SUB.Publish(masterAccessionno, messageJSON);        
            */
        }

        private void Button_GetTestClick(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Stain.Model.StainCollection stainCollection = Business.Stain.Model.StainCollection.Instance;
        }
    }
}
