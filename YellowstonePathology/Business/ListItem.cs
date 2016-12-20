using System;
using System.ComponentModel;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business
{
	public class ListItem : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        public ListItem()
        {

        }

        public virtual void Fill(MySqlDataReader dr)
        {
            BaseData.FillListItem(this, dr);
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
