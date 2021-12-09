using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class TestX : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_CurrentTime;

        public TestX()
        {
            this.m_CurrentTime = DateTime.Now.ToLongTimeString();
            InitializeComponent();
            this.DataContext = this;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public string CurrentTime
        {
            get { return this.m_CurrentTime;}
            set 
            {  
                if(this.m_CurrentTime != value)
                {
                    this.m_CurrentTime = value;
                    this.NotifyPropertyChanged("CurrentTime");
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.m_CurrentTime = DateTime.Now.ToLongTimeString();
            this.NotifyPropertyChanged("CurrentTime");
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
