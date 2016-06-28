using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class FixationDetails : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
		private bool m_IsSelected;
		private DateTime? m_CollectionDate;

		public FixationDetails(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
		{
			this.m_SpecimenOrder = specimenOrder;
			this.m_IsSelected = true;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
		{
			get { return this.m_SpecimenOrder; }
		}

		public bool IsSelected
		{
			get { return this.m_IsSelected; }
			set
			{
				if (this.m_IsSelected != value)
				{
					this.m_IsSelected = value;
					NotifyPropertyChanged("IsSelected");
				}
			}
		}

		public DateTime? CollectionDate
		{
			get { return this.m_CollectionDate; }
		}

		public void SetCollectionDate(DateTime? collectionDate)
		{
			this.m_CollectionDate = collectionDate;
			NotifyPropertyChanged("CollectionDate");
		}
	}
}
