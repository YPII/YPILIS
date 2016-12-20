using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Specimen.Model
{
	public class SpecimenTypeList : ObservableCollection<SpecimenTypeListItem>
	{

		public void SetDescriptionById(int specimenTypeId, string description)
		{
			foreach (SpecimenTypeListItem item in this)
			{
				if (item.SpecimenTypeId == specimenTypeId)
				{
					item.Description = description;
					break;
				}
			}
		}

        public string GetDescriptionById(int specimenTypeId)
        {
            string description = string.Empty;
            foreach (SpecimenTypeListItem item in this)
            {
                if (item.SpecimenTypeId == specimenTypeId)
                {
                    description = item.Description;
                    break;
                }
            }
            return description;
        }
	}

	public class SpecimenTypeListItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_SpecimenTypeId;
		private string m_Description;

		public SpecimenTypeListItem()
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
		public int SpecimenTypeId
		{
			get { return this.m_SpecimenTypeId; }
			set
			{
				if (this.m_SpecimenTypeId != value)
				{
					this.m_SpecimenTypeId = value;
					NotifyPropertyChanged("SpecimenTypeId");
				}
			}
		}

		[PersistentProperty()]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if (this.m_Description != value)
				{
					this.m_Description = value;
					NotifyPropertyChanged("Description");
				}
			}
		}
	}
}
