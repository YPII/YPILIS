using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class SpecimenOrderView : INotifyPropertyChanged, IComparable
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

		//private int m_NumberOfAliquots;
		//private int m_NumberOfBlocks;
		//private int m_NumberOfSlides;
		private bool m_IsVisible;
		private bool m_WaitingForScan;
        private bool m_IsSelected;

		private YellowstonePathology.Business.Specimen.Model.MediumCollection m_MediumCollection;

		public SpecimenOrderView(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_SpecimenOrder = specimenOrder;
            this.m_IsSelected = true;

			//this.m_NumberOfAliquots = 1;
			//this.m_NumberOfBlocks = 1;
			//this.m_NumberOfSlides = 1;

			this.m_MediumCollection = new Business.Specimen.Model.MediumCollection(this.m_SpecimenOrder, accessionOrder);
		}

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
		{
			get { return this.m_SpecimenOrder; }
		}

		public YellowstonePathology.Business.Specimen.Model.MediumCollection MediumCollection
		{
			get { return this.m_MediumCollection; }
		}

		public string DisplayStatus
		{
			get
			{
				string result = "Accessioned";
				if (this.SpecimenOrder.Verified)
				{
					result = "Verified";
				}
				return result;
			}
		}

        public bool IsSelected
        {
            get { return this.m_IsSelected; }
            set 
            { 
                this.m_IsSelected = value;
                this.NotifyPropertyChanged("IsSelected");
            }
        }

        /*
		public int NumberOfAliquots
		{
			get { return this.m_NumberOfAliquots; }
			set
			{
				if (this.m_NumberOfAliquots != value)
				{
					this.m_NumberOfAliquots = value;
					this.NotifyPropertyChanged("NumberOfAliquots");
				}
			}
		}
        */

        /*
		public int NumberOfBlocks
		{
			get { return this.m_NumberOfBlocks; }
			set
			{
				if (this.m_NumberOfBlocks != value)
				{
					this.m_NumberOfBlocks = value;
					this.NotifyPropertyChanged("NumberOfBlocks");
				}
			}
		}
        */

        /*
		public int NumberOfSlides
		{
			get { return this.m_NumberOfSlides; }
			set
			{
				if (this.m_NumberOfSlides != value)
				{
					this.m_NumberOfSlides = value;
					this.NotifyPropertyChanged("NumberOfSlides");
				}
			}
		}
        */

		public bool IsVisible
		{
			get { return this.m_IsVisible; }
			set
			{
				if (this.m_IsVisible != value)
				{
					this.m_IsVisible = value;
					this.NotifyPropertyChanged("IsVisible");
				}
			}
		}
        
		public bool WaitingForScan
		{
			get { return this.m_WaitingForScan; }
			set
			{
				if (this.m_WaitingForScan != value)
				{
					this.m_WaitingForScan = value;
					this.NotifyPropertyChanged("WaitingForScan");
				}
			}
		}        

		public void Refresh(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_MediumCollection.Refresh(accessionOrder);
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public int CompareTo(object compareObject)
		{
			SpecimenOrderView x = (SpecimenOrderView)compareObject;
			return this.SpecimenOrder.SpecimenNumber.CompareTo(x.SpecimenOrder.SpecimenNumber);
		}
	}
}
