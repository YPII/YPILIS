using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Surgical
{
	public class PathologistHistoryItemListItem : ListItem
    {
		private string m_Id;
		private string m_Item;
        private string m_Result;        

        public PathologistHistoryItemListItem()
        {
        }

		[PersistentProperty()]
        public string Id
        {
            get { return this.m_Id; }
            set
            {
                if (this.m_Id != value)
                {
					this.m_Id = value;
                    NotifyPropertyChanged("Id");
				}
            }
        }

		[PersistentProperty()]
        public string Item
        {
            get { return this.m_Item; }
            set
            {
                if (this.m_Item != value)
                {
					this.m_Item = value;
                    NotifyPropertyChanged("Item");
				}
            }
        }

		[PersistentProperty()]
		public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
					this.m_Result = value;
                    NotifyPropertyChanged("Result");
				}
            }
        }
    }
}
