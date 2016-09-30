using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace YellowstonePathology.UI.AppMessaging
{
	public class MessagingPath
	{
        private static volatile MessagingPath instance;
        private static object syncRoot = new Object();

        public delegate void NevermindEventHandler(object sender, EventArgs e);
        public event NevermindEventHandler Nevermind;

        public delegate void LockWasReleasedEventHandler(object sender, EventArgs e);
        public event LockWasReleasedEventHandler LockWasReleased;

        public delegate void HoldYourHorsesEventHandler(object sender, EventArgs e);
        public event HoldYourHorsesEventHandler HoldYourHorses;

        private Navigation.PageNavigator m_PageNavigator;
        private bool m_PageNavigatorWasPassedIn;

        private List<Action> m_LockAquiredActionList;
        private List<Action> m_LockReleasedActionList;

        private MessagingDialog m_MessagingDialog;
        private List<string> m_AlwaysHoldList;

        static MessagingPath()
        {

        }

        private MessagingPath()
        {
            this.m_PageNavigatorWasPassedIn = false;

            this.m_PageNavigatorWasPassedIn = false;
            this.m_LockAquiredActionList = new List<Action>();
            this.m_LockReleasedActionList = new List<Action>();

            this.m_AlwaysHoldList = new List<string>();
            this.m_AlwaysHoldList.Add("CUTTINGA");
            this.m_AlwaysHoldList.Add("CUTTINGB");
            this.m_AlwaysHoldList.Add("GROSSA");
            this.m_AlwaysHoldList.Add("GROSSB-PC");
            this.m_AlwaysHoldList.Add("GROSS2-PC");
            this.m_AlwaysHoldList.Add("CODYHISTOLOGY01");
            //this.m_AlwaysHoldList.Add("COMPILE");          
        }

        public static MessagingPath Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MessagingPath();
                    }
                }

                return instance;
            }
        }

        public void HandleMessageReceived(AccessionLockMessage message, Business.Test.AccessionOrder accessionOrder)
        {
            switch (message.MessageId)
            {
                case UI.AppMessaging.AccessionLockMessageIdEnum.ASK:
                    HandleASKRecieved(accessionOrder, message);
                    break;
                case UI.AppMessaging.AccessionLockMessageIdEnum.HOLD:
                    ShowResponseReceivedPage(message);
                    break;
                case UI.AppMessaging.AccessionLockMessageIdEnum.GIVE:
                    ShowResponseReceivedPage(message);
                    RunLockAquiredActionList();
                    break;
            }
        }

        public List<Action> LockAquiredActionList
        {
            get { return this.m_LockAquiredActionList; }
        }

        public List<Action> LockReleasedActionList
        {
            get { return this.m_LockReleasedActionList; }
        }           
        
        public void RunLockReleasedActionList()
        {
            foreach(Action action in this.m_LockReleasedActionList)
            {
                action.Invoke();
            }
        }

        public void RunLockAquiredActionList()
        {
            foreach (Action action in this.m_LockAquiredActionList)
            {
                action.Invoke();
            }
        }

        public void HandleASKRecieved(Business.Test.AccessionOrder accessionOrder, AccessionLockMessage message)
        {
            if (this.m_AlwaysHoldList.Exists(e => e == System.Environment.MachineName.ToUpper()))
            {
                UI.AppMessaging.AccessionLockMessage holdMessage = new AccessionLockMessage(message.MasterAccessionNo, AccessionLockMessage.GetMyAddress(), message.From, AccessionLockMessageIdEnum.HOLD);
                ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
                subscriber.Publish(holdMessage.MasterAccessionNo, JsonConvert.SerializeObject(holdMessage));
            }
            else
            {
                if(this.m_MessagingDialog == null) this.m_MessagingDialog = new MessagingDialog();
                this.m_PageNavigator = this.m_MessagingDialog.PageNavigator;
                this.m_MessagingDialog.Closed += MessagingDialog_Closed;

                AppMessaging.LockRequestReceivedPage lockRequestReceivedPage = new AppMessaging.LockRequestReceivedPage(accessionOrder, message);
                lockRequestReceivedPage.Take += LockRequestReceivedPage_Take;
                lockRequestReceivedPage.Hold += LockRequestReceivedPage_Hold;         

                this.m_MessagingDialog.PageNavigator.Navigate(lockRequestReceivedPage);
                this.m_MessagingDialog.Show();
            }           
        }        

        private void LockRequestReceivedPage_Hold(object sender, CustomEventArgs.AccessionLockMessageReturnEventArgs e)
        {
            UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(e.Message.MasterAccessionNo, AccessionLockMessage.GetMyAddress(), e.Message.From, AccessionLockMessageIdEnum.HOLD);
            ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
            subscriber.Publish(message.MasterAccessionNo, JsonConvert.SerializeObject(message));
            this.m_MessagingDialog.Close();
        }

        private void LockRequestReceivedPage_Take(object sender, CustomEventArgs.AOAccessionLockMessageReturnEventArgs e)
        {            
            this.RunLockReleasedActionList();
            this.m_MessagingDialog.Close();

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
            e.AccessionOrder.AccessionLock.TransferLock(e.Message.From);

            UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(e.Message.MasterAccessionNo, AccessionLockMessage.GetMyAddress(), e.Message.From, AccessionLockMessageIdEnum.GIVE);
            ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
            subscriber.Publish(message.MasterAccessionNo, JsonConvert.SerializeObject(message));
        }

        public void StartSendRequest(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, Navigation.PageNavigator pageNavigator)
        {
            if (accessionOrder.AccessionLock.IsLockStillAquired() == true)
            {
                this.m_PageNavigator = pageNavigator;
                this.m_PageNavigatorWasPassedIn = true;

                UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(accessionOrder.MasterAccessionNo, AccessionLockMessage.GetMyAddress(), accessionOrder.AccessionLock.Address, AccessionLockMessageIdEnum.ASK);
                ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
                subscriber.Publish(message.MasterAccessionNo, JsonConvert.SerializeObject(message));

                this.ShowLockRequestSentPage(accessionOrder);
            }
            else
            {
                accessionOrder.AccessionLock.RefreshLock();
                this.RunLockAquiredActionList();
                this.m_MessagingDialog.Close();
                this.m_MessagingDialog = null;
            }            
        }

        public void Start(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {                        
            if(accessionOrder.AccessionLock.IsLockAquiredByMe == false)
            {
                if(this.m_MessagingDialog == null) this.m_MessagingDialog = new MessagingDialog();
                this.m_PageNavigator = this.m_MessagingDialog.PageNavigator;
                this.m_MessagingDialog.Closed += MessagingDialog_Closed;

                UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(accessionOrder.MasterAccessionNo, AccessionLockMessage.GetMyAddress(), accessionOrder.AccessionLock.Address, AccessionLockMessageIdEnum.ASK);
                AppMessaging.LockRequestPage lockRequestPage = new AppMessaging.LockRequestPage(accessionOrder);
                lockRequestPage.RequestLock += LockRequestPage_RequestLock;

                this.m_MessagingDialog.PageNavigator.Navigate(lockRequestPage);
                this.m_MessagingDialog.Show();
            }            
        }

        private void MessagingDialog_Closed(object sender, EventArgs e)
        {            
            this.m_MessagingDialog = null;
        }

        private void LockRequestPage_RequestLock(object sender, CustomEventArgs.AccessionOrderReturnEventArgs e)
        {            
            if(e.AccessionOrder.AccessionLock.IsLockStillAquired() == true)
            {
                UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(e.AccessionOrder.MasterAccessionNo, AppMessaging.AccessionLockMessage.GetMyAddress(), e.AccessionOrder.AccessionLock.Address, AccessionLockMessageIdEnum.ASK);
                ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
                subscriber.Publish(message.MasterAccessionNo, JsonConvert.SerializeObject(message));
                this.ShowLockRequestSentPage(e.AccessionOrder);
            }
            else
            {
                e.AccessionOrder.AccessionLock.RefreshLock();
                this.RunLockAquiredActionList();
                this.m_MessagingDialog.Close();
                this.m_MessagingDialog = null;
            }            
        }

        private void ShowLockRequestSentPage(Business.Test.AccessionOrder accessionOrder)
        {
            LockRequestSentPage lockRequestSentPage = null;
            if(this.m_PageNavigatorWasPassedIn == true)
            {
                lockRequestSentPage = new LockRequestSentPage(accessionOrder.AccessionLock.Address, accessionOrder.MasterAccessionNo, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            }
            else
            {
                lockRequestSentPage = new LockRequestSentPage(accessionOrder.AccessionLock.Address, accessionOrder.MasterAccessionNo, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
            }

            lockRequestSentPage.Nevermind += LockRequestSentPage_Nevermind;
            this.m_PageNavigator.Navigate(lockRequestSentPage);
        }

        private void LockRequestSentPage_Nevermind(object sender, EventArgs e)
        {
            if (this.Nevermind != null) this.Nevermind(this, e);
        }        

        public void ShowResponseReceivedPage(AccessionLockMessage message)
        {
            LockRequestResponseReceivedPage lockRequestResponseReceivedPage = null;
            if(this.m_PageNavigatorWasPassedIn == false)
            {
                lockRequestResponseReceivedPage = new LockRequestResponseReceivedPage(message, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
            }
            else
            {
                lockRequestResponseReceivedPage = new LockRequestResponseReceivedPage(message, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            }

            lockRequestResponseReceivedPage.LockWasReleased += LockRequestResponseReceivedPage_LockWasReleased;
            lockRequestResponseReceivedPage.HoldYourHorses += LockRequestResponseReceivedPage_HoldYourHorses;
            this.m_PageNavigator.Navigate(lockRequestResponseReceivedPage);            
        }

        private void LockRequestResponseReceivedPage_HoldYourHorses(object sender, EventArgs e)
        {
            if(this.HoldYourHorses != null) this.HoldYourHorses(this, new EventArgs());
        }

        private void LockRequestResponseReceivedPage_LockWasReleased(object sender, EventArgs e)
        {
            this.LockWasReleased(this, new EventArgs());
        }        
    }
}
