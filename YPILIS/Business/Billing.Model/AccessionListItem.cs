using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;
using System.ComponentModel;

namespace YellowstonePathology.Business.Billing.Model
{
    public class AccessionListItem
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Nullable<DateTime> m_AccessionDate;
        private string m_MasterAccessionNo;
        private string m_ReportNo;
        private string m_PLastName;
        private string m_PFirstName;
        private Nullable<DateTime> m_PBirthdate;
        private string m_MedicalRecord;
        private string m_Account;

        public AccessionListItem()
        {

        }

        [PersistentProperty()]
        public Nullable<DateTime> AccessionDate
        {
            get { return this.m_AccessionDate; }
            set
            {
                if (this.m_AccessionDate != value)
                {
                    this.m_AccessionDate = value;
                    this.NotifyPropertyChanged("AccessionDate");
                }
            }
        }

        [PersistentPrimaryKeyProperty(false)]        
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        [PersistentPrimaryKeyProperty(false)]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
