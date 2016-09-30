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
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PleaseWaitDialog.xaml
    /// </summary>
    public partial class PleaseWaitDialog : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        string m_Message;

        public PleaseWaitDialog()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void ShowMessage(string message)
        {
            this.Message = message;
            this.Visibility = Visibility.Visible;
        }
        
        public void HideMessage()
        {
            this.Message = string.Empty;
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        public string Message
        {
            get { return this.m_Message; }
            set
            {
                if (this.m_Message != value)
                {
                    this.m_Message = value;
                    this.NotifyPropertyChanged("Message");
                }
            }
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
    }
}
