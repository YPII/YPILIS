using System;
using System.ComponentModel;

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
