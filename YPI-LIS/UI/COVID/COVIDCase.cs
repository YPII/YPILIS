using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.UI.COVID
{
	public class COVIDCase : INotifyPropertyChanged
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
		private Nullable<DateTime> m_FinalTime;
        private string m_PanelSetName;
        private string m_SpecimenDescription;
        private string m_Result;
        private Nullable<DateTime> m_PBirthdate;
		private string m_OrderedBy;
		private bool m_Finalized;
		private string m_ClinicalHistory;
		private string m_SpecialInstructions;
		private string m_ClientOrderId;
		private bool m_FirstTest;
		private bool m_EmployedInHealthcare;
		private bool m_Symptomatic;
		private Nullable<DateTime> m_DateOfSymptomaticOnset;
		private bool m_Hospitalized;
		private bool m_ICU;
		private bool m_ResidentInCongregateCare;
		private bool m_Pregnant;
		private string m_PRace;
		private string m_PEthnicity;
		private bool m_HighPriority;

		public COVIDCase()
		{
            
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
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

		[PersistentProperty()]
		public string SpecialInstructions
		{
			get { return this.m_SpecialInstructions; }
			set
			{
				if (value != this.m_SpecialInstructions)
				{
					this.m_SpecialInstructions = value;
					this.NotifyPropertyChanged("SpecialInstructions");
				}
			}
		}

		[PersistentProperty()]
		public string ClinicalHistory
		{
			get { return this.m_ClinicalHistory; }
			set
			{
				if (value != this.m_ClinicalHistory)
				{
					this.m_ClinicalHistory = value;
					this.NotifyPropertyChanged("ClinicalHistory");
				}
			}
		}

		[PersistentProperty()]
		public string ClientOrderId
		{
			get { return this.m_ClientOrderId; }
			set
			{
				if (value != this.m_ClientOrderId)
				{
					this.m_ClientOrderId = value;
					this.NotifyPropertyChanged("ClientOrderId");
				}
			}
		}

		[PersistentProperty()]		
		public bool FirstTest
		{
			get { return this.m_FirstTest; }
			set
			{
				if (this.m_FirstTest != value)
				{
					this.m_FirstTest = value;
					this.NotifyPropertyChanged("FirstTest");
				}
			}
		}

		[PersistentProperty()]
		public bool Symptomatic
		{
			get { return this.m_Symptomatic; }
			set
			{
				if (this.m_Symptomatic != value)
				{
					this.m_Symptomatic = value;
					this.NotifyPropertyChanged("Symptomatic");
				}
			}
		}

		[PersistentProperty()]

		public Nullable<DateTime> DateOfSymptomaticOnset
		{
			get { return this.m_DateOfSymptomaticOnset; }
			set
			{
				if (this.m_DateOfSymptomaticOnset != value)
				{
					this.m_DateOfSymptomaticOnset = value;
					this.NotifyPropertyChanged("DateOfSymptomaticOnset");
				}
			}
		}

		[PersistentProperty()]
		public bool EmployedInHealthcare
		{
			get { return this.m_EmployedInHealthcare; }
			set
			{
				if (this.m_EmployedInHealthcare != value)
				{
					this.m_EmployedInHealthcare = value;
					this.NotifyPropertyChanged("EmployedInHealthcare");
				}
			}
		}

		[PersistentProperty()]
		public bool Hospitalized
		{
			get { return this.m_Hospitalized; }
			set
			{
				if (this.m_Hospitalized != value)
				{
					this.m_Hospitalized = value;
					this.NotifyPropertyChanged("Hospitalized");
				}
			}
		}

		[PersistentProperty()]
		public bool ICU
		{
			get { return this.m_ICU; }
			set
			{
				if (this.m_ICU != value)
				{
					this.m_ICU = value;
					this.NotifyPropertyChanged("ICU");
				}
			}
		}

		[PersistentProperty()]
		public bool ResidentInCongregateCare
		{
			get { return this.m_ResidentInCongregateCare; }
			set
			{
				if (this.m_ResidentInCongregateCare != value)
				{
					this.m_ResidentInCongregateCare = value;
					this.NotifyPropertyChanged("ResidentInCongregateCare");
				}
			}
		}

		[PersistentProperty()]
		public bool Pregnant
		{
			get { return this.m_Pregnant; }
			set
			{
				if (this.m_Pregnant != value)
				{
					this.m_Pregnant = value;
					this.NotifyPropertyChanged("Pregnant");
				}
			}
		}

		[PersistentProperty()]
		public string PRace
		{
			get { return this.m_PRace; }
			set
			{
				if (this.m_PRace != value)
				{
					this.m_PRace = value;
					this.NotifyPropertyChanged("PRace");
				}
			}
		}

		[PersistentProperty()]
		public string PEthnicity
		{
			get { return this.m_PEthnicity; }
			set
			{
				if (this.m_PEthnicity != value)
				{
					this.m_PEthnicity = value;
					this.NotifyPropertyChanged("PEthnicity");
				}
			}
		}

		[PersistentProperty()]
		public bool HighPriority
		{
			get { return this.m_HighPriority; }
			set
			{
				if (this.m_HighPriority != value)
				{
					this.m_HighPriority = value;
					this.NotifyPropertyChanged("HighPriority");
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
    }
}
