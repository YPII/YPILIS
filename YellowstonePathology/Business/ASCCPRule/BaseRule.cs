using System;
using System.ComponentModel;
using System.Linq;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class BaseRule : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_Description;        
        protected bool m_IsMatch;
        protected bool m_PerformHPV;
        protected bool m_ReflexToHPVGenotypes;
        protected string m_RepeatTestingInterval;
        protected bool m_RecomendColposcopy;

        public BaseRule()
        {
            
        }        
        
        public virtual WomanCollection RunSimulation()
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

        public bool IsMatch
        {
            get { return this.m_IsMatch; }
            set
            {
                if (this.m_IsMatch != value)
                {
                    this.m_IsMatch = value;
                    this.NotifyPropertyChanged("IsMatch");
                }
            }
        }

        public bool PerformHPV
        {
            get { return this.m_PerformHPV; }
            set
            {
                if (this.m_PerformHPV != value)
                {
                    this.m_PerformHPV = value;
                    this.NotifyPropertyChanged("PerformHPV");
                }
            }
        }

        public bool ReflexToHPVGenotypes
        {
            get { return this.m_ReflexToHPVGenotypes; }
            set
            {
                if (this.m_ReflexToHPVGenotypes != value)
                {
                    this.m_ReflexToHPVGenotypes = value;
                    this.NotifyPropertyChanged("ReflexToHPVGenotypes");
                }
            }
        }

        public string RepeatTestingInterval
        {
            get { return this.m_RepeatTestingInterval; }
            set
            {
                if (this.m_RepeatTestingInterval != value)
                {
                    this.m_RepeatTestingInterval = value;
                    this.NotifyPropertyChanged("RepeatTestingInterval");
                }
            }
        }

        public bool RecomendColposcopy
        {
            get { return this.m_RecomendColposcopy; }
            set
            {
                if (this.m_RecomendColposcopy != value)
                {
                    this.m_RecomendColposcopy = value;
                    this.NotifyPropertyChanged("RecomendColposcopy");
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
