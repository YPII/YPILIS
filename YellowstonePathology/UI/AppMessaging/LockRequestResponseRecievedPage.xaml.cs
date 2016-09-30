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
	public partial class LockRequestResponseReceivedPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void LockWasReleasedEventHandler(object sender, EventArgs e);
        public event LockWasReleasedEventHandler LockWasReleased;

        public delegate void HoldYourHorsesEventHandler(object sender, EventArgs e);
        public event HoldYourHorsesEventHandler HoldYourHorses;

        private AccessionLockMessage m_Message;
        private string m_DisplayMessage;    

        public LockRequestResponseReceivedPage(AccessionLockMessage message, System.Windows.Visibility closeButtonVisibility, System.Windows.Visibility nextButtonVisibility)
        {
            this.m_Message = message;
            this.SetDisplayMessage(message);

            InitializeComponent();
            DataContext = this;

            this.ButtonClose.Visibility = closeButtonVisibility;
            this.ButtonNext.Visibility = nextButtonVisibility;
        }  

        private void SetDisplayMessage(AccessionLockMessage message)
        {
            if(message.MessageId == AccessionLockMessageIdEnum.GIVE)
            {
                this.m_DisplayMessage = this.m_Message.From + " says " + this.m_Message.MasterAccessionNo + " is all yours.";
            }
            else if (message.MessageId == AccessionLockMessageIdEnum.HOLD)
            {
                this.m_DisplayMessage = this.m_Message.From + " says hold your horses I'm working on " + this.m_Message.MasterAccessionNo + ".";
            }
        }
        
        public string DisplayMessage
        {
            get { return this.m_DisplayMessage; }
        }                                                                		                   

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {                     
            if(this.m_Message.MessageId == AccessionLockMessageIdEnum.GIVE)
            {
                if (this.LockWasReleased != null) this.LockWasReleased(this, new EventArgs());
            }
            else if (this.m_Message.MessageId == AccessionLockMessageIdEnum.HOLD)
            {
                if (this.HoldYourHorses != null) this.HoldYourHorses(this, new EventArgs());
            } 
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
