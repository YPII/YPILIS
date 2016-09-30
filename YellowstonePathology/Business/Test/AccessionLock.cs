using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test
{
    public class AccessionLock : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_MasterAccessionNo;
        private string m_Address;        
        private Nullable<DateTime> m_TimeAquired;

        public AccessionLock()
        {
            
        }

        public AccessionLock(HashEntry[] hashEntries)
        {            
            this.m_MasterAccessionNo = hashEntries[0].Value;
            this.m_Address = hashEntries[1].Value;
            this.m_TimeAquired = DateTime.Parse(hashEntries[2].Value);            
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if(this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }                
            }
        }

        public string Address
        {
            get { return this.m_Address; }
            set
            {
                if(this.m_Address != value)
                {
                    this.m_Address = value;
                    this.NotifyPropertyChanged("Address");
                }                
            }
        }                

        public Nullable<DateTime> TimeAquired
        {
            get { return this.m_TimeAquired; }
            set
            {
                if(this.m_TimeAquired != value)
                {
                    this.m_TimeAquired = value;
                    this.NotifyPropertyChanged("TimeAquired");
                }                
            }
        }

        public bool IsLockAquiredByMe
        {
            get
            {
                bool result = false;
                if(string.IsNullOrEmpty(this.m_Address) == false)
                {
                    string[] splitString = this.m_Address.Split(new char[] { '\\' });
                    if (splitString[0] == System.Environment.MachineName)
                    {
                        result = true;
                    }
                }                
                return result;
            }
        }

        public bool IsLockAquired
        {
            get
            {
                bool result = false;
                if (string.IsNullOrEmpty(this.m_Address) == false)
                {
                    result = true;
                }
                return result;
            }
        }         

        private string HashKey
        {
            get {  return "AccessionLock:" + this.m_MasterAccessionNo; }
        }      

        public void ReleaseLock()
        {           
            this.m_Address = null;
            this.m_TimeAquired = null;

            IDatabase db = Business.RedisConnection.Instance.GetDatabase();
            db.KeyDelete(this.HashKey);            

            var transaction = db.CreateTransaction();
            transaction.AddCondition(Condition.HashNotExists(this.HashKey, "MasterAccessionNo"));
            transaction.KeyDeleteAsync(this.HashKey);
            transaction.SetRemoveAsync("AccessionLocks", this.HashKey);
            bool committed = transaction.Execute();

            this.NotifyPropertyChanged(string.Empty);
        }

        public void TransferLock(string address)
        {
            IDatabase db = Business.RedisConnection.Instance.GetDatabase();
            HashEntry[] hashFields = new HashEntry[3];
            hashFields[0] = new HashEntry("MasterAccessionNo", this.MasterAccessionNo);
            hashFields[1] = new HashEntry("Address", address);
            hashFields[2] = new HashEntry("TimeAquired", DateTime.Now.ToString());
            db.HashSet(this.HashKey, hashFields);

            this.m_Address = address;
            this.m_TimeAquired = DateTime.Now;
            this.NotifyPropertyChanged(string.Empty);
        }

        public void RefreshLock()
        {
            this.TryHashSet();
            this.GetHash();
        }

        private void GetHash()
        {
            IDatabase db = Business.RedisConnection.Instance.GetDatabase();                        
            HashEntry[] hashFields = db.HashGetAll(this.HashKey);

            this.m_MasterAccessionNo = hashFields[0].Value;
            this.m_Address = hashFields[1].Value;                        
            this.m_TimeAquired = DateTime.Parse(hashFields[2].Value);
            this.NotifyPropertyChanged(string.Empty);
        }

        public bool IsLockStillAquired()
        {
            IDatabase db = Business.RedisConnection.Instance.GetDatabase();
            return db.KeyExists(this.HashKey);
        }

        private void TryHashSet()
        {
            IDatabase db = Business.RedisConnection.Instance.GetDatabase();
            HashEntry[] hashFields = new HashEntry[3];
            hashFields[0] = new HashEntry("MasterAccessionNo", this.m_MasterAccessionNo);
            hashFields[1] = new HashEntry("Address", UI.AppMessaging.AccessionLockMessage.GetMyAddress());            
            hashFields[2] = new HashEntry("TimeAquired", DateTime.Now.ToString());

            var transaction = db.CreateTransaction();
            transaction.AddCondition(Condition.HashNotExists(this.HashKey, "MasterAccessionNo"));
            transaction.HashSetAsync(this.HashKey, hashFields);
            transaction.SetAddAsync("AccessionLocks", this.HashKey);
            bool committed = transaction.Execute();
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
