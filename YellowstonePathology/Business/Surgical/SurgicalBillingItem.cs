using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Surgical
{	
	public class SurgicalBillingItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ReportNo;
		private bool m_Locked;
		private int m_Levels;
		private int m_Blocks;
		private int m_Slides;
		private int m_SpecialStains;
		private int m_Immuno;
		private int m_WrightStain;
		private int m_PapStain;
		private int m_ThinPrep;
		private int m_Decal;
		private string m_HistoTech;
		private string m_AccessionDate;

		public SurgicalBillingItem()
		{
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
		public bool Locked
		{
			get { return this.m_Locked; }
			set
			{
				if (this.m_Locked != value)
				{
					this.m_Locked = value;
					this.NotifyPropertyChanged("Locked");
				}
			}
		}

		[PersistentProperty()]
		public int Levels
		{
			get { return this.m_Levels; }
			set
			{
				if (this.m_Levels != value)
				{
					this.m_Levels = value;
					this.NotifyPropertyChanged("Levels");
				}
			}
		}

		[PersistentProperty()]
		public int Blocks
		{
			get { return this.m_Blocks; }
			set
			{
				if (this.m_Blocks != value)
				{
					this.m_Blocks = value;
					this.NotifyPropertyChanged("Blocks");
				}
			}
		}

		[PersistentProperty()]
		public int Slides
		{
			get { return this.m_Slides; }
			set
			{
				if (this.m_Slides != value)
				{
					this.m_Slides = value;
					this.NotifyPropertyChanged("Slides");
				}
			}
		}

		[PersistentProperty()]
		public int SpecialStains
		{
			get { return this.m_SpecialStains; }
			set
			{
				if (this.m_SpecialStains != value)
				{
					this.m_SpecialStains = value;
					this.NotifyPropertyChanged("SpecialStains");
				}
			}
		}

		[PersistentProperty()]
		public int Immuno
		{
			get { return this.m_Immuno; }
			set
			{
				if (this.m_Immuno != value)
				{
					this.m_Immuno = value;
					this.NotifyPropertyChanged("Immuno");
				}
			}
		}

		[PersistentProperty()]
		public int WrightStain
		{
			get { return this.m_WrightStain; }
			set
			{
				if (this.m_WrightStain != value)
				{
					this.m_WrightStain = value;
					this.NotifyPropertyChanged("WrightStain");
				}
			}
		}

		[PersistentProperty()]
		public int PapStain
		{
			get { return this.m_PapStain; }
			set
			{
				if (this.m_PapStain != value)
				{
					this.m_PapStain = value;
					this.NotifyPropertyChanged("PapStain");
				}
			}
		}

		[PersistentProperty()]
		public int ThinPrep
		{
			get { return this.m_ThinPrep; }
			set
			{
				if (this.m_ThinPrep != value)
				{
					this.m_ThinPrep = value;
					this.NotifyPropertyChanged("ThinPrep");
				}
			}
		}

		[PersistentProperty()]
		public int Decal
		{
			get { return this.m_Decal; }
			set
			{
				if (this.m_Decal != value)
				{
					this.m_Decal = value;
					this.NotifyPropertyChanged("Decal");
				}
			}
		}

		[PersistentProperty()]
		public string HistoTech
		{
			get { return this.m_HistoTech; }
			set
			{
				if (this.m_HistoTech != value)
				{
					this.m_HistoTech = value;
					this.NotifyPropertyChanged("HistoTech");
				}
			}
		}

		[PersistentProperty()]
		public string AccessionDate
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

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
