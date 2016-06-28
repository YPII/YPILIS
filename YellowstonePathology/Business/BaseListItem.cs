using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business
{
    public class BaseListItem : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        bool m_IsFilling;

        public BaseListItem()
        {

        }

        public bool IsFilling
        {
            get { return this.m_IsFilling; }
            set { this.m_IsFilling = value; }
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
