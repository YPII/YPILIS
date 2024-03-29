﻿using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Search
{
	public class ReportSearchItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

        private string m_HasComment;
        private string m_MasterAccessionNo;
		private string m_ReportNo;
		private Nullable<DateTime> m_AccessionDate;
		private int m_PanelSetId;
		private string m_PatientName;
		private string m_PLastName;
		private string m_PFirstName;
		private string m_ClientName;
		private string m_PhysicianName;
		private string m_ForeignAccessionNo;		
        private Nullable<DateTime> m_FinalTime;
        private string m_PanelSetName;
        private string m_SpecimenDescription;
        private string m_Result;
        private string m_Indication;

        private bool m_HasDataError;
		private Nullable<DateTime> m_PBirthdate;
		private string m_AccessioningFacilityId;
		private string m_OrderedBy;
		private bool m_Verified;
		private bool m_Finalized;
        private bool m_IsPosted;
		private bool m_HoldDistribution;
        private bool m_IsLockAquiredByMe;
        private bool m_LockAquired;
		private string m_SVHMedicalRecord;
		private string m_PatientId;
		private bool m_ITAudited;

        public ReportSearchItem()
		{
            this.m_HasComment = "?";
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
        
        public string HasComment
        {
            get { return this.m_HasComment; }
            set
            {
                if (value != this.m_HasComment)
                {
                    this.m_HasComment = value;
                    this.NotifyPropertyChanged("HasComment");
                }
            }
        }

        [PersistentProperty()]
		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set
			{
				if (value != this.m_MasterAccessionNo)
				{
					this.m_MasterAccessionNo = value;
					this.NotifyPropertyChanged("MasterAccessionNo");
				}
			}
		}

        [PersistentProperty()]
        public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if (value != this.m_ReportNo)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
				}
			}
		}

        [PersistentProperty()]
        public Nullable<DateTime> AccessionDate
		{
			get { return this.m_AccessionDate; }
			set
			{
				if (value != this.m_AccessionDate)
				{
					this.m_AccessionDate = value;
					this.NotifyPropertyChanged("AccessionDate");
				}
			}
		}

        [PersistentProperty()]
        public int PanelSetId
		{
			get { return this.m_PanelSetId; }
			set
			{
				if (value != this.m_PanelSetId)
				{
					this.m_PanelSetId = value;
					this.NotifyPropertyChanged("PanelSetId");
				}
			}
		}

        [PersistentProperty()]
        public string PatientName
		{
			get { return this.m_PatientName; }
			set
			{
				if (value != this.m_PatientName)
				{
					this.m_PatientName = value;
					this.NotifyPropertyChanged("PatientName");
				}
			}
		}

		[PersistentProperty()]
		public string SVHMedicalRecord
		{
			get { return this.m_SVHMedicalRecord; }
			set
			{
				if (value != this.m_SVHMedicalRecord)
				{
					this.m_SVHMedicalRecord = value;
					this.NotifyPropertyChanged("SVHMedicalRecord");
				}
			}
		}

		[PersistentProperty()]
		public string PatientId
		{
			get { return this.m_PatientId; }
			set
			{
				if (value != this.m_PatientId)
				{
					this.m_PatientId = value;
					this.NotifyPropertyChanged("m_PatientId");
				}
			}
		}

		[PersistentProperty()]
        public string PLastName
		{
			get { return this.m_PLastName; }
			set
			{
				if (value != this.m_PLastName)
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
				if (value != this.m_PFirstName)
				{
					this.m_PFirstName = value;
					this.NotifyPropertyChanged("PFirstName");
				}
			}
		}

        [PersistentProperty()]
        public string ClientName
		{
			get { return this.m_ClientName; }
			set
			{
				if (value != this.m_ClientName)
				{
					this.m_ClientName = value;
					this.NotifyPropertyChanged("ClientName");
				}
			}
		}

        [PersistentProperty()]
        public string PhysicianName
		{
			get { return this.m_PhysicianName; }
			set
			{
				if (value != this.m_PhysicianName)
				{
					this.m_PhysicianName = value;
					this.NotifyPropertyChanged("PhysicianName");
				}
			}
		}

        [PersistentProperty()]
        public string ForeignAccessionNo
		{
			get { return this.m_ForeignAccessionNo; }
			set
			{
				if (value != this.m_ForeignAccessionNo)
				{
					this.m_ForeignAccessionNo = value;
					this.NotifyPropertyChanged("ForeignAccessionNo");
				}
			}
		}

        [PersistentProperty()]
        public Nullable<DateTime> FinalTime
        {
            get { return this.m_FinalTime; }
            set
            {
                if (value != this.m_FinalTime)
                {
                    this.m_FinalTime = value;
                    this.NotifyPropertyChanged("FinalTime");
                }
            }
        }

        [PersistentProperty()]
        public string PanelSetName
		{
			get { return this.m_PanelSetName; }
			set
			{
				if (value != this.m_PanelSetName)
				{
					this.m_PanelSetName = value;
					this.NotifyPropertyChanged("PanelSetName");
				}
			}
		}

        [PersistentProperty()]
        public string OrderedBy
		{
			get { return this.m_OrderedBy; }
			set
			{
				if (value != this.m_OrderedBy)
				{
					this.m_OrderedBy = value;
					this.NotifyPropertyChanged("OrderedBy");
				}
			}
		}

        [PersistentProperty()]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (value != this.m_Result)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        [PersistentProperty()]
        public string Indication
        {
            get { return this.m_Indication; }
            set
            {
                if (value != this.m_Indication)
                {
                    this.m_Indication = value;
                    this.NotifyPropertyChanged("Indication");
                }
            }
        }

        public bool HasDataError
		{
			get { return this.m_HasDataError; }
			set
			{
				this.m_HasDataError = value;
				this.NotifyPropertyChanged("Color");
			}
		}

        [PersistentProperty()]
        public Nullable<DateTime> PBirthdate
		{
			get { return this.m_PBirthdate; }
			set
			{
				if (value != this.m_PBirthdate)
				{
					this.m_PBirthdate = value;
					this.NotifyPropertyChanged("PBirthdate");
				}
			}
		}

		public string AccessioningFacilityId
		{
            get { return this.m_AccessioningFacilityId; }
			set
			{
                if (value != this.m_AccessioningFacilityId)
				{
                    this.m_AccessioningFacilityId = value;
                    this.NotifyPropertyChanged("AccessioningFacilityId");
				}
			}
		}

        [PersistentProperty()]
        public string SpecimenDescription
        {
            get { return this.m_SpecimenDescription; }
            set
            {
                if (value != this.m_SpecimenDescription)
                {
                    this.m_SpecimenDescription = value;
                    this.NotifyPropertyChanged("SpecimenDescription");
                }
            }
        }

		public bool Finalized
		{
			get { return this.m_Finalized; }
			set
			{
				this.m_Finalized = value;
				this.NotifyPropertyChanged("Finalized");
			}
		}

		public bool Verified
		{
			get { return this.m_Verified; }
			set
			{
				this.m_Verified = value;
				this.NotifyPropertyChanged("Verified");
			}
		}

        [PersistentProperty()]
        public bool IsPosted
        {
            get { return this.m_IsPosted; }
            set
            {
                this.m_IsPosted = value;
                this.NotifyPropertyChanged("IsPosted");
            }
        }

		[PersistentProperty()]
		public bool ITAudited
		{
			get { return this.m_ITAudited; }
			set
			{
				this.m_ITAudited = value;
				this.NotifyPropertyChanged("ITAudited");
			}
		}

		[PersistentProperty()]
		public bool HoldDistribution
		{
			get { return this.m_HoldDistribution; }
			set
			{
				this.m_HoldDistribution = value;
				this.NotifyPropertyChanged("HoldDistribution");
			}
		}

		public bool IsLockAquiredByMe
        {
            get { return this.m_IsLockAquiredByMe; }
            set
            {
                if (value != this.m_IsLockAquiredByMe)
                {
                    this.m_IsLockAquiredByMe = value;
                    this.NotifyPropertyChanged("IsLockAquiredByMe");
                }
            }
        }

        public bool LockAquired
        {
            get { return this.m_LockAquired; }
            set
            {
                if (value != this.m_LockAquired)
                {
                    this.m_LockAquired = value;
                    this.NotifyPropertyChanged("LockAquired");
                }
            }
        }
    }
}
