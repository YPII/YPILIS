using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Data;
using System.Xml.Serialization;
using System.ComponentModel;

namespace YellowstonePathology.Business.Search
{
	public class ReportSearchItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

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
		private string m_ColorCode;
		private Nullable<DateTime> m_FinalDate;
		private string m_PanelSetName;
        private string m_SpecimenDescription;

		private bool m_HasDataError;
		private Nullable<DateTime> m_PBirthdate;
		private string m_AccessioningFacilityId;
		private string m_OrderedBy;
		private bool m_Verified;
		private bool m_Finalized;
        private bool m_IsPosted;
        private bool m_IsLockAquiredByMe;
        private bool m_LockAquired;

        public ReportSearchItem()
		{
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

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

		public Nullable<DateTime> FinalDate
		{
			get { return this.m_FinalDate; }
			set
			{
				if (value != this.m_FinalDate)
				{
					this.m_FinalDate = value;
					this.NotifyPropertyChanged("FinalDate");
				}
			}
		}

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

		public string ColorCode
		{
			get { return this.m_ColorCode; }
			set
			{
				if (value != this.m_ColorCode)
				{
					this.m_ColorCode = value;
					this.NotifyPropertyChanged("ColorCode");
					this.NotifyPropertyChanged("Color");
					this.NotifyPropertyChanged("BackgroundColor");
				}
			}
		}

		public string Color
		{
			get
			{
				if (string.IsNullOrEmpty(ColorCode))
				{
					return "Black";
				}
				return ColorCode;
			}
		}

		public string BackgroundColor
		{
			get
			{
				if (m_ColorCode == "White")
				{
					return "Gainsboro";
				}
				return "Transparent";
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

        public bool IsPosted
        {
            get { return this.m_IsPosted; }
            set
            {
                this.m_IsPosted = value;
                this.NotifyPropertyChanged("IsPosted");
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
