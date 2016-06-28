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
using Newtonsoft.Json;
using StackExchange.Redis;

namespace YellowstonePathology.UI.AppMessaging
{	
	public partial class LockRequestReceivedPage : UI.PageControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void TakeHandler(object sender, UI.CustomEventArgs.AOAccessionLockMessageReturnEventArgs e);
        public event TakeHandler Take;

        public delegate void HoldHandler(object sender, UI.CustomEventArgs.AccessionLockMessageReturnEventArgs e);
        public event HoldHandler Hold;

        private Business.Test.AccessionOrder m_AccessionOrder;
        private AccessionLockMessage m_Message;
        private string m_DisplayMessage;

        private string m_CountDownMessage;
        private int m_CurrentCountDown;

        private bool m_StopTimerOnNextTick;   

        public LockRequestReceivedPage(Business.Test.AccessionOrder accessionOrder, AccessionLockMessage message)
        {
            this.m_CurrentCountDown = 15;
            this.m_AccessionOrder = accessionOrder;
            this.m_Message = message;            
            this.m_DisplayMessage = this.m_Message.From + " is asking for the lock on " + this.m_Message.MasterAccessionNo + ".";
            this.m_StopTimerOnNextTick = false;

            System.Windows.Threading.DispatcherTimer dispatchTimer = new System.Windows.Threading.DispatcherTimer();
            dispatchTimer.Interval = new TimeSpan(0, 0, 1);            
            dispatchTimer.Tick += DispatchTimer_Tick;
            dispatchTimer.Start();

            InitializeComponent();

            DataContext = this;                                
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
            
        }

        public string CountDownMessage
        {
            get { return this.m_CountDownMessage; }            
        }

        public void DispatchTimer_Tick(object sender, EventArgs e)
        {
            this.m_CurrentCountDown -= 1;
            this.m_CountDownMessage = "You have " + this.m_CurrentCountDown + " seconds to respond.";

            System.Windows.Threading.DispatcherTimer dispatchTimer = (System.Windows.Threading.DispatcherTimer)sender;

            if(this.m_StopTimerOnNextTick == true)
            {
                dispatchTimer.Stop();
            }
            else if (this.m_CurrentCountDown == 0)
            {
                dispatchTimer.Stop();
                this.m_CountDownMessage = string.Empty;                
                this.Take(this, new CustomEventArgs.AOAccessionLockMessageReturnEventArgs(this.m_AccessionOrder, this.m_Message));
            }

            this.NotifyPropertyChanged("CountDownMessage");
        }

        private void ButtonRespondTakeIt_Click(object sender, RoutedEventArgs e)
        {
            this.m_StopTimerOnNextTick = true;
            this.Take(this, new CustomEventArgs.AOAccessionLockMessageReturnEventArgs(this.m_AccessionOrder, this.m_Message));            
        }

        private void ButtonRespondHoldYourHorses_Click(object sender, RoutedEventArgs e)
        {
            this.m_StopTimerOnNextTick = true;
            this.Hold(this, new CustomEventArgs.AccessionLockMessageReturnEventArgs(this.m_Message));            
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
