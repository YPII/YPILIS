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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.AppMessaging
{	
	public partial class LockRequestSentPage : UI.PageControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NevermindEventHandler(object sender, EventArgs e);
        public event NevermindEventHandler Nevermind;
        
        private string m_CountDownMessage;
        private int m_CurrentCountDown;        
        private string m_DisplayMessage;
        private bool m_StopTimerOnNextTick;

        public LockRequestSentPage(string address, string masterAccessionNo, System.Windows.Visibility closeButtonVisibility, System.Windows.Visibility nevermindButtonVisibility)
		{
            this.m_DisplayMessage = "A resquest was sent to " + address + " for the lock on " + masterAccessionNo + ".";
            this.m_StopTimerOnNextTick = false;

            InitializeComponent();

            DataContext = this;
            this.ButtonClose.Visibility = closeButtonVisibility;
            this.ButtonNevermind.Visibility = nevermindButtonVisibility;
            this.StartCountDownTimer();        
		}

        public override void BeforeNavigatingAway()
        {
            this.m_StopTimerOnNextTick = true;
        }

        public string DisplayMessage
        {
            get { return this.m_DisplayMessage; }
        }

        private void StartCountDownTimer()
        {
            System.Windows.Threading.DispatcherTimer dispatchTimer;

            this.m_CurrentCountDown = 15;

            dispatchTimer = new System.Windows.Threading.DispatcherTimer();
            dispatchTimer.Interval = new TimeSpan(0, 0, 1);
            dispatchTimer.Tick += DispatchTimer_Tick;
            dispatchTimer.Start();
        }        

        public string CountDownMessage
        {
            get { return this.m_CountDownMessage; }            
        }

        private void DispatchTimer_Tick(object sender, EventArgs e)
        {
            this.m_CurrentCountDown -= 1;
            this.m_CountDownMessage = "Please wait " + this.m_CurrentCountDown;            

            System.Windows.Threading.DispatcherTimer dispatchTimer = (System.Windows.Threading.DispatcherTimer)sender;

            if (this.m_StopTimerOnNextTick == true || this.m_CurrentCountDown == 0)
            {
                dispatchTimer.Stop();
                this.m_CountDownMessage = string.Empty;
            }            

            this.NotifyPropertyChanged("CountDownMessage");
        }                               		                           

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.m_StopTimerOnNextTick = true;
        	Window.GetWindow(this).Close();
        }

        private void ButtonNevermind_Click(object sender, RoutedEventArgs e)
        {
            this.m_StopTimerOnNextTick = true;
            if (this.Nevermind != null) this.Nevermind(this, new EventArgs());
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
    }
}
