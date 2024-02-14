using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Billing
{
    public class CptCodeItem : INotifyPropertyChanged
    {
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        int m_Quantity;
        string m_Description;        

        public CptCodeItem()
        {

        }

        public int Quantity
        {
            get { return this.m_Quantity; }
            set
            {
                if (this.m_Quantity != value)
                {
                    this.m_Quantity = value;
                    this.NotifyPropertyChanged("Quantity");
                }
            }
        }

        public string Description
        {
            get { return this.m_Description; }
            set
            {
                if (this.m_Description != value)
                {
                    this.m_Description = value;
                    this.NotifyPropertyChanged("Description");
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
