using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    [DataContract]
	[PersistentClass("tblCytologyClientOrder", "tblClientOrder", "YPIDATA")]
	public partial class CytologyClientOrder : ClientOrder
	{
        public CytologyClientOrder()
        {

        }

		public CytologyClientOrder(string objectId) : base(objectId)
		{

		}

		private string m_LMP;
		private bool m_CervixPresent;
		private bool m_CervicalEndoCervical;
		private bool m_Vaginal;
		private bool m_Hysterectomy;
		private bool m_AbnormalBleeding;
		private bool m_BirthControl;
		private bool m_HormoneTherapy;
		private bool m_PreviousNormalPap;
		private string m_PreviousNormalPapDate;
		private bool m_PreviousAbnormalPap;
		private string m_PreviousAbnormalPapDate;
		private bool m_PreviousBiopsy;
		private string m_PreviousBiopsyDate;
		private bool m_Prenatal;
		private bool m_Postpartum;
		private bool m_Postmenopausal;
		private string m_Icd9Code;
		private bool m_ReflexHPV;
		private bool m_RoutineHPVTesting;
		private bool m_NGCTTesting;
		private string m_ScreeningType;
		private bool m_TrichomonasVaginalis;

		[PersistentProperty]
		[DataMember]
		public string LMP
		{
			get { return this.m_LMP; }
			set
			{
				if (this.m_LMP != value)
				{
					this.m_LMP = value;
					this.NotifyPropertyChanged("LMP");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool CervixPresent
		{
			get { return this.m_CervixPresent; }
			set
			{
				if (this.m_CervixPresent != value)
				{
					this.m_CervixPresent = value;
					this.NotifyPropertyChanged("CervixPresent");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool CervicalEndoCervical
		{
			get { return this.m_CervicalEndoCervical; }
			set
			{
				if (this.m_CervicalEndoCervical != value)
				{
					this.m_CervicalEndoCervical = value;
					this.NotifyPropertyChanged("CervicalEndoCervical");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool Vaginal
		{
			get { return this.m_Vaginal; }
			set
			{
				if (this.m_Vaginal != value)
				{
					this.m_Vaginal = value;
					this.NotifyPropertyChanged("Vaginal");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool Hysterectomy
		{
			get { return this.m_Hysterectomy; }
			set
			{
				if (this.m_Hysterectomy != value)
				{
					this.m_Hysterectomy = value;
					this.NotifyPropertyChanged("Hysterectomy");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool AbnormalBleeding
		{
			get { return this.m_AbnormalBleeding; }
			set
			{
				if (this.m_AbnormalBleeding != value)
				{
					this.m_AbnormalBleeding = value;
					this.NotifyPropertyChanged("AbnormalBleeding");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool BirthControl
		{
			get { return this.m_BirthControl; }
			set
			{
				if (this.m_BirthControl != value)
				{
					this.m_BirthControl = value;
					this.NotifyPropertyChanged("BirthControl");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool HormoneTherapy
		{
			get { return this.m_HormoneTherapy; }
			set
			{
				if (this.m_HormoneTherapy != value)
				{
					this.m_HormoneTherapy = value;
					this.NotifyPropertyChanged("HormoneTherapy");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool PreviousNormalPap
		{
			get { return this.m_PreviousNormalPap; }
			set
			{
				if (this.m_PreviousNormalPap != value)
				{
					this.m_PreviousNormalPap = value;
					this.NotifyPropertyChanged("PreviousNormalPap");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public string PreviousNormalPapDate
		{
			get { return this.m_PreviousNormalPapDate; }
			set
			{
				if (this.m_PreviousNormalPapDate != value)
				{
					this.m_PreviousNormalPapDate = value;
					this.NotifyPropertyChanged("PreviousNormalPapDate");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool PreviousAbnormalPap
		{
			get { return this.m_PreviousAbnormalPap; }
			set
			{
				if (this.m_PreviousAbnormalPap != value)
				{
					this.m_PreviousAbnormalPap = value;
					this.NotifyPropertyChanged("PreviousAbnormalPap");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public string PreviousAbnormalPapDate
		{
			get { return this.m_PreviousAbnormalPapDate; }
			set
			{
				if (this.m_PreviousAbnormalPapDate != value)
				{
					this.m_PreviousAbnormalPapDate = value;
					this.NotifyPropertyChanged("PreviousAbnormalPapDate");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool PreviousBiopsy
		{
			get { return this.m_PreviousBiopsy; }
			set
			{
				if (this.m_PreviousBiopsy != value)
				{
					this.m_PreviousBiopsy = value;
					this.NotifyPropertyChanged("PreviousBiopsy");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public string PreviousBiopsyDate
		{
			get { return this.m_PreviousBiopsyDate; }
			set
			{
				if (this.m_PreviousBiopsyDate != value)
				{
					this.m_PreviousBiopsyDate = value;
					this.NotifyPropertyChanged("PreviousBiopsyDate");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool Prenatal
		{
			get { return this.m_Prenatal; }
			set
			{
				if (this.m_Prenatal != value)
				{
					this.m_Prenatal = value;
					this.NotifyPropertyChanged("Prenatal");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool Postpartum
		{
			get { return this.m_Postpartum; }
			set
			{
				if (this.m_Postpartum != value)
				{
					this.m_Postpartum = value;
					this.NotifyPropertyChanged("Postpartum");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool Postmenopausal
		{
			get { return this.m_Postmenopausal; }
			set
			{
				if (this.m_Postmenopausal != value)
				{
					this.m_Postmenopausal = value;
					this.NotifyPropertyChanged("Postmenopausal");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public string Icd9Code
		{
			get { return this.m_Icd9Code; }
			set
			{
				if (this.m_Icd9Code != value)
				{
					this.m_Icd9Code = value;
					this.NotifyPropertyChanged("Icd9Code");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool ReflexHPV
		{
			get { return this.m_ReflexHPV; }
			set
			{
				if (this.m_ReflexHPV != value)
				{
					this.m_ReflexHPV = value;
					this.NotifyPropertyChanged("ReflexHPV");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool RoutineHPVTesting
		{
			get { return this.m_RoutineHPVTesting; }
			set
			{
				if (this.m_RoutineHPVTesting != value)
				{
					this.m_RoutineHPVTesting = value;
					this.NotifyPropertyChanged("RoutineHPVTesting");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool NGCTTesting
		{
			get { return this.m_NGCTTesting; }
			set
			{
				if (this.m_NGCTTesting != value)
				{
					this.m_NGCTTesting = value;
					this.NotifyPropertyChanged("NGCTTesting");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public string ScreeningType
		{
			get { return this.m_ScreeningType; }
			set
			{
				if (this.m_ScreeningType != value)
				{
					this.m_ScreeningType = value;
					this.NotifyPropertyChanged("ScreeningType");
				}
			}
		}

		[PersistentProperty]
		[DataMember]
		public bool TrichomonasVaginalis
		{
			get { return this.m_TrichomonasVaginalis; }
			set
			{
				if (this.m_TrichomonasVaginalis != value)
				{
					this.m_TrichomonasVaginalis = value;
					this.NotifyPropertyChanged("TrichomonasVaginalis");
				}
			}
		}
	}
}
