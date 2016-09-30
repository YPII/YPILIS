using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.User
{
    public class SystemIdentity : INotifyPropertyChanged
    {
        private static volatile SystemIdentity instance;
        private static object syncRoot = new Object();

        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;        

        private SystemUser m_User;
        private bool m_IsKnown;

        YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

        static SystemIdentity()
        {

        }

        private SystemIdentity()
        {
            this.m_User = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserByUserName(System.Windows.Forms.SystemInformation.UserName);
            this.m_IsKnown = true;

            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
            this.m_BarcodeScanPort.SecurityBadgeScanReceived += new Business.BarcodeScanning.BarcodeScanPort.SecurityBadgeScanReceivedHandler(BarcodeScanPort_SecurityBadgeScanReceived);
        }

        private void BarcodeScanPort_SecurityBadgeScanReceived(BarcodeScanning.Barcode barcode)
        {
            int systemUserId = Convert.ToInt32(barcode.ID);

            if (this.m_User.UserId == systemUserId)
            {
                this.m_User = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserByUserName(System.Windows.Forms.SystemInformation.UserName);
            }
            else
            {
                this.m_User = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(systemUserId);
            }

            this.NotifyPropertyChanged("User");
        }

        public static SystemIdentity Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SystemIdentity();
                    }
                }

                return instance;
            }
        }

        public YellowstonePathology.Business.User.SystemUser User
        {
            get { return this.m_User; }
        }

        public bool IsKnown
        {
            get { return this.m_IsKnown; }
        }       

        public void SetToLoggedInUser()
        {
            this.m_User = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserByUserName(System.Windows.Forms.SystemInformation.UserName);
            this.m_IsKnown = true;
            this.NotifyPropertyChanged("");            
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