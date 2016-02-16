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

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for LoginPageWindow.xaml
    /// </summary>
    public partial class LoginPageWindow : Window, YellowstonePathology.Business.Persistence.IDocumentWriter
    {
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;		
        private bool m_ReadOnly;      

		public LoginPageWindow()
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
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);          
        }		
        
        public bool ReadOnly
        {
            get { return this.m_ReadOnly; }
        }                 
    }
}
