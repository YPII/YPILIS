using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.SpecialStain
{
	public class StainResultOptionList : ObservableCollection<StainResultOption>
	{
		public StainResultOptionList()
        {
		}
	}

	public class StainResultOption : ListItem
	{
		private int m_StainResultOptionId;
		private string m_StainResult;

		public StainResultOption()
		{
		}

		[PersistentProperty()]
		public int StainResultOptionId
		{
			get
			{
				return this.m_StainResultOptionId;
			}
			set
			{
				if (this.m_StainResultOptionId != value)
				{
					this.m_StainResultOptionId = value;
					NotifyPropertyChanged("StainResultOptionId");
				}
			}
		}

		[PersistentProperty()]
		public string StainResult
		{
			get
			{
				return this.m_StainResult;
			}
			set
			{
				if (this.m_StainResult != value)
				{
					this.m_StainResult = value;
					NotifyPropertyChanged("StainResult");
				}
			}
		}
	}
}
