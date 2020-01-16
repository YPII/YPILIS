using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Cytology.Model
{
    public partial class OrderType : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_OrderCode;
        private string m_Description;        

        public OrderType(string orderCode, string description)
        {
            this.m_OrderCode = orderCode;
            this.m_Description = description;
        }

		[PersistentProperty()]
        public string OrderCode
        {
            get { return this.m_OrderCode; }
            set
            {
                if (this.m_OrderCode != value)
                {
                    this.m_OrderCode = value;
                    this.NotifyPropertyChanged("OrderCode");
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
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

        public string DisplayString
        {
            get { return this.m_OrderCode + " - " + this.m_Description; }
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
