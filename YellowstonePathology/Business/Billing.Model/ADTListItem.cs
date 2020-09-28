using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;
using System.ComponentModel;

namespace YellowstonePathology.Business.Billing.Model
{
    public class ADTListItem
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private string m_PLastName;
        private string m_PFirstName;
        private Nullable<DateTime> m_PBirthdate;
        private string m_MedicalRecord;
        private string m_Account;
        private DateTime m_DateReceived;
        private string m_YPIPatientId;

        public ADTListItem()
        {

        }        

        [PersistentProperty()]        
        public string PLastName
        {
            get { return this.m_PLastName; }
            set
            {
                if (this.m_PLastName != value)
                {
                    this.m_PLastName = value;
                    this.NotifyPropertyChanged("PLastName");
                }
            }
        }

        [PersistentProperty()]        
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set
            {
                if (this.m_PFirstName != value)
                {
                    this.m_PFirstName = value;
                    this.NotifyPropertyChanged("PFirstName");
                }
            }
        }

        [PersistentProperty()]        
        public Nullable<DateTime> PBirthdate
        {
            get { return this.m_PBirthdate; }
            set
            {
                if (this.m_PBirthdate != value)
                {
                    this.m_PBirthdate = value;
                    this.NotifyPropertyChanged("PBirthdate");
                }
            }
        }

        [PersistentProperty()]        
        public string MedicalRecord
        {
            get { return this.m_MedicalRecord; }
            set
            {
                if (this.m_MedicalRecord != value)
                {
                    this.m_MedicalRecord = value;
                    this.NotifyPropertyChanged("MedicalRecord");
                }
            }
        }

        [PersistentProperty()]
        public string Account
        {
            get { return this.m_Account; }
            set
            {
                if (this.m_Account != value)
                {
                    this.m_Account = value;
                    this.NotifyPropertyChanged("Account");
                }
            }
        }

        [PersistentProperty()]
        public DateTime DateReceived
        {
            get { return this.m_DateReceived; }
            set
            {
                if (this.m_DateReceived != value)
                {
                    this.m_DateReceived = value;
                    this.NotifyPropertyChanged("DateReceived");
                }
            }
        }

        [PersistentProperty()]
        public string YPIPatientId
        {
            get { return this.m_YPIPatientId; }
            set
            {
                if (this.m_YPIPatientId != value)
                {
                    this.m_YPIPatientId = value;
                    this.NotifyPropertyChanged("YPIPatientId");
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
