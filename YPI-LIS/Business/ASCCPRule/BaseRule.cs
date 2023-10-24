using System;
using System.ComponentModel;
using System.Linq;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class BaseRule : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        protected string m_Description;                        

        public BaseRule()
        {
            
        }        

        public virtual void Finalize(Woman woman)
        {
            this.FinalizePap(woman);
            this.FinalizeHPV(woman);
            this.FinalizeGenotyping(woman);
        }
        
        public virtual bool IsMatch(Woman woman)
        {
            throw new Exception("Not implemented here.");
        }
        
        public virtual void FinalizePap(Woman woman)
        {
            throw new Exception("Not implemented here.");
        }

        public virtual void FinalizeHPV(Woman woman)
        {
            throw new Exception("Not implemented here.");
        }

        public virtual void FinalizeGenotyping(Woman woman)
        {
            throw new Exception("Not implemented here.");
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
