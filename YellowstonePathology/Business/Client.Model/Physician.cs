using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;

namespace YellowstonePathology.Business.Client.Model
{    
    public class Physician : INotifyPropertyChanged
    {
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        int m_PhysicianID;
        string m_FirstName;
        string m_LastName;
        string m_FullName;        
        bool m_Active;

        public Physician()
        {

        }
        
        public int PhysicianID
        {
            get { return this.m_PhysicianID; }
            set
            {
                if (this.m_PhysicianID != value)
                {
                    this.m_PhysicianID = value;
                    this.NotifyPropertyChanged("PhysicianID");
                }
            }
        }
        
        public string FirstName
        {
            get { return this.m_FirstName; }
            set
            {
                if (this.m_FirstName != value)
                {
                    this.m_FirstName = value;
                    this.NotifyPropertyChanged("FirstName");
                }
            }
        }
        
        public string LastName
        {
            get { return this.m_LastName; }
            set
            {
                if (this.m_LastName != value)
                {
                    this.m_LastName = value;
                    this.NotifyPropertyChanged("LastName");
                }
            }
        }

        [Column(Name = "FullName", Storage = "m_FullName")]
        public string FullName
        {
            get { return this.m_FullName; }
            set
            {
                if (this.m_FullName != value)
                {
                    this.m_FullName = value;
                    this.NotifyPropertyChanged(FullName);
                }
            }
        }

        [Column(Name = "Active", Storage = "m_Active")]
        public bool Active
        {
            get { return this.m_Active; }
            set
            {
                if (this.m_Active != value)
                {
                    this.m_Active = value;
                    this.NotifyPropertyChanged("Active");
                }
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
