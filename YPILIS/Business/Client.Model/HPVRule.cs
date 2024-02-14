using System;
using System.ComponentModel;
using System.Linq;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRule : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_Description;

        public HPVRule()
        { }

        public HPVRule(string description)
        {
            this.m_Description = description;
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

        public virtual bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            throw new Exception("Not implemented in the HPVRule base class");
        }
    }
}
