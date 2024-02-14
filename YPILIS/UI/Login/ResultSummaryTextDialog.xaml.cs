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

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for PleaseWaitDialog.xaml
    /// </summary>
    public partial class ResultSummaryTextDialog : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ResultSummaryText;

        public ResultSummaryTextDialog(string resultSummaryText)
        {
            this.m_ResultSummaryText = resultSummaryText;
            InitializeComponent();
            this.DataContext = this;
        }

        public string ResultSummaryText
        {
            get { return this.m_ResultSummaryText; }
            set
            {
                if (this.m_ResultSummaryText != value)
                {
                    this.m_ResultSummaryText = value;
                    this.NotifyPropertyChanged("ResultSummaryText");
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
