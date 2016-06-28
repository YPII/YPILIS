using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
    [PersistentClass("tblAccessionOrder", "YPIDATA")]
    public class AccessionOrderView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_MasterAccessionNo;
        private Nullable<DateTime> m_AccessionDate;
        private Nullable<DateTime> m_AccessionTime;
        private string m_AccessioningFacilityId;
        private string m_PatientId;
        private string m_PFirstName;
        private string m_PLastName;
        private string m_PMiddleInitial;
        private string m_PSSN;
        private string m_PSex;
        private Nullable<DateTime> m_PBirthdate;

        public AccessionOrderView()
        {

        }

		[PersistentDocumentIdProperty()]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
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

        [PersistentProperty()]
        public Nullable<DateTime> AccessionTime
        {
            get { return this.m_AccessionTime; }
            set
            {
                if (this.m_AccessionTime != value)
                {
                    this.m_AccessionTime = value;
                    this.NotifyPropertyChanged("AccessionTime");
                }
            }
        }

        [PersistentProperty()]
        public string AccessioningFacilityId
        {
            get { return this.m_AccessioningFacilityId; }
            set
            {
                if (this.m_AccessioningFacilityId != value)
                {
                    this.m_AccessioningFacilityId = value;
                    this.NotifyPropertyChanged("AccessioningFacilityId");
                }
            }
        }

        [PersistentProperty()]
        public string PatientId
        {
            get { return this.m_PatientId; }
            set
            {
                if (this.m_PatientId != value)
                {
                    this.m_PatientId = value;
                    this.NotifyPropertyChanged("PatientId");
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
        public string PMiddleInitial
        {
            get { return this.m_PMiddleInitial; }
            set
            {
                if (this.m_PMiddleInitial != value)
                {
                    this.m_PMiddleInitial = value;
                    this.NotifyPropertyChanged("PMiddleInitial");
                }
            }
        }

        [PersistentProperty()]
        public string PSSN
        {
            get { return this.m_PSSN; }
            set
            {
                if (this.m_PSSN != value)
                {
                    this.m_PSSN = value;
                    this.NotifyPropertyChanged("PSSN");
                }
            }
        }

        [PersistentProperty()]
        public string PSex
        {
            get { return this.m_PSex; }
            set
            {
                if (this.m_PSex != value)
                {
                    this.m_PSex = value;
                    this.NotifyPropertyChanged("PSex");
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
