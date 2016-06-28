using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.PanelSet.Model
{
	[PersistentClass("tblAutopsyResult", "YPIDATA")]
	public class AutopsyResult : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private int m_AutopsyResultId;
		private string m_PanelSetResultOrderId;
		private string m_Findings;
		private string m_CauseOfDeath;
		private string m_MannerOfDeath;
		private Nullable<DateTime> m_DateTimeOfDeath;
		private string m_LocationOfAutopsy;
		private string m_RequestingProvider;
		private string m_AutopsyAssistants;
		private string m_LimitsOfExamination;
		private string m_CassetteSummary;
		private string m_Microscopic;
		private string m_AncillaryTests;
		private string m_PlaceOfDeath;
		private string m_AutopsyLocation;
		private string m_Diagnosis;
		private string m_ClinicalHistory;
		private string m_GrossExamination;
		private string m_InternalExamination;
		private string m_ExternalExamination;
		private string m_MicroscopicExamination;
		private string m_SummaryComment;

		public AutopsyResult()
		{
		}

		public AutopsyResult(string panelSetResultOrderId)
		{
			this.m_PanelSetResultOrderId = panelSetResultOrderId;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[PersistentPrimaryKeyProperty(true)]
		public int AutopsyResultId
		{
			get { return this.m_AutopsyResultId; }
			set
			{
				if (this.m_AutopsyResultId != value)
				{
					this.m_AutopsyResultId = value;
					this.NotifyPropertyChanged("AutopsyResultId");
				}
			}
		}

		[PersistentProperty()]
		public string PanelSetResultOrderId
		{
			get { return this.m_PanelSetResultOrderId; }
			set
			{
				if (this.m_PanelSetResultOrderId != value)
				{
					this.m_PanelSetResultOrderId = value;
					this.NotifyPropertyChanged("PanelSetResultOrderId");
				}
			}
		}

		[PersistentProperty()]
		public string Findings
		{
			get { return this.m_Findings; }
			set
			{
				if (this.m_Findings != value)
				{
					this.m_Findings = value;
					this.NotifyPropertyChanged("Findings");
				}
			}
		}

		[PersistentProperty()]
		public string CauseOfDeath
		{
			get { return this.m_CauseOfDeath; }
			set
			{
				if (this.m_CauseOfDeath != value)
				{
					this.m_CauseOfDeath = value;
					this.NotifyPropertyChanged("CauseOfDeath");
				}
			}
		}

		[PersistentProperty()]
		public string MannerOfDeath
		{
			get { return this.m_MannerOfDeath; }
			set
			{
				if (this.m_MannerOfDeath != value)
				{
					this.m_MannerOfDeath = value;
					this.NotifyPropertyChanged("MannerOfDeath");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> DateTimeOfDeath
		{
			get { return this.m_DateTimeOfDeath; }
			set
			{
				if (this.m_DateTimeOfDeath != value)
				{
					this.m_DateTimeOfDeath = value;
					this.NotifyPropertyChanged("DateTimeOfDeath");
				}
			}
		}

		[PersistentProperty()]
		public string LocationOfAutopsy
		{
			get { return this.m_LocationOfAutopsy; }
			set
			{
				if (this.m_LocationOfAutopsy != value)
				{
					this.m_LocationOfAutopsy = value;
					this.NotifyPropertyChanged("LocationOfAutopsy");
				}
			}
		}

		[PersistentProperty()]
		public string RequestingProvider
		{
			get { return this.m_RequestingProvider; }
			set
			{
				if (this.m_RequestingProvider != value)
				{
					this.m_RequestingProvider = value;
					this.NotifyPropertyChanged("RequestingProvider");
				}
			}
		}

		[PersistentProperty()]
		public string AutopsyAssistants
		{
			get { return this.m_AutopsyAssistants; }
			set
			{
				if (this.m_AutopsyAssistants != value)
				{
					this.m_AutopsyAssistants = value;
					this.NotifyPropertyChanged("AutopsyAssistants");
				}
			}
		}

		[PersistentProperty()]
		public string LimitsOfExamination
		{
			get { return this.m_LimitsOfExamination; }
			set
			{
				if (this.m_LimitsOfExamination != value)
				{
					this.m_LimitsOfExamination = value;
					this.NotifyPropertyChanged("LimitsOfExamination");
				}
			}
		}

		[PersistentProperty()]
		public string CassetteSummary
		{
			get { return this.m_CassetteSummary; }
			set
			{
				if (this.m_CassetteSummary != value)
				{
					this.m_CassetteSummary = value;
					this.NotifyPropertyChanged("CassetteSummary");
				}
			}
		}

		[PersistentProperty()]
		public string Microscopic
		{
			get { return this.m_Microscopic; }
			set
			{
				if (this.m_Microscopic != value)
				{
					this.m_Microscopic = value;
					this.NotifyPropertyChanged("Microscopic");
				}
			}
		}

		[PersistentProperty()]
		public string AncillaryTests
		{
			get { return this.m_AncillaryTests; }
			set
			{
				if (this.m_AncillaryTests != value)
				{
					this.m_AncillaryTests = value;
					this.NotifyPropertyChanged("AncillaryTests");
				}
			}
		}

		[PersistentProperty()]
		public string PlaceOfDeath
		{
			get { return this.m_PlaceOfDeath; }
			set
			{
				if (this.m_PlaceOfDeath != value)
				{
					this.m_PlaceOfDeath = value;
					this.NotifyPropertyChanged("PlaceOfDeath");
				}
			}
		}

		[PersistentProperty()]
		public string AutopsyLocation
		{
			get { return this.m_AutopsyLocation; }
			set
			{
				if (this.m_AutopsyLocation != value)
				{
					this.m_AutopsyLocation = value;
					this.NotifyPropertyChanged("AutopsyLocation");
				}
			}
		}

		[PersistentProperty()]
		public string Diagnosis
		{
			get { return this.m_Diagnosis; }
			set
			{
				if (this.m_Diagnosis != value)
				{
					this.m_Diagnosis = value;
					this.NotifyPropertyChanged("Diagnosis");
				}
			}
		}

		[PersistentProperty()]
		public string ClinicalHistory
		{
			get { return this.m_ClinicalHistory; }
			set
			{
				if (this.m_ClinicalHistory != value)
				{
					this.m_ClinicalHistory = value;
					this.NotifyPropertyChanged("ClinicalHistory");
				}
			}
		}

		[PersistentProperty()]
		public string GrossExamination
		{
			get { return this.m_GrossExamination; }
			set
			{
				if (this.m_GrossExamination != value)
				{
					this.m_GrossExamination = value;
					this.NotifyPropertyChanged("GrossExamination");
				}
			}
		}

		[PersistentProperty()]
		public string InternalExamination
		{
			get { return this.m_InternalExamination; }
			set
			{
				if (this.m_InternalExamination != value)
				{
					this.m_InternalExamination = value;
					this.NotifyPropertyChanged("InternalExamination");
				}
			}
		}

		[PersistentProperty()]
		public string ExternalExamination
		{
			get { return this.m_ExternalExamination; }
			set
			{
				if (this.m_ExternalExamination != value)
				{
					this.m_ExternalExamination = value;
					this.NotifyPropertyChanged("ExternalExamination");
				}
			}
		}

		[PersistentProperty()]
		public string MicroscopicExamination
		{
			get { return this.m_MicroscopicExamination; }
			set
			{
				if (this.m_MicroscopicExamination != value)
				{
					this.m_MicroscopicExamination = value;
					this.NotifyPropertyChanged("MicroscopicExamination");
				}
			}
		}

		[PersistentProperty()]
		public string SummaryComment
		{
			get { return this.m_SummaryComment; }
			set
			{
				if (this.m_SummaryComment != value)
				{
					this.m_SummaryComment = value;
					this.NotifyPropertyChanged("SummaryComment");
				}
			}
		}
	}
}
