using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
    [PersistentClass("tblHPVStandingOrder", "YPIDATA")]
    public class HPVStandingOrder : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_HPVStandingOrderId;
        private int m_PhysicianId;
        private string m_Age;
        private string m_PAPResult;
        private string m_HPVTesting;
        private string m_Endocervical;
        private string m_HPVStandingOrderName;

        public HPVStandingOrder()
        { }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        [PersistentPrimaryKeyProperty(false)]
        public string HPVStandingOrderId
        {
            get { return this.m_HPVStandingOrderId; }
            set
            {
                if(this.m_HPVStandingOrderId != value)
                {
                    this.m_HPVStandingOrderId = value;
                    NotifyPropertyChanged("HPVStandingOrderId");
                }
            }
        }

        [PersistentProperty()]
        public int PhysicianId
        {
            get { return this.m_PhysicianId; }
            set
            {
                if (this.m_PhysicianId != value)
                {
                    this.m_PhysicianId = value;
                    NotifyPropertyChanged("PhysicianId");
                }
            }
        }

        [PersistentProperty()]
        public string Age
        {
            get { return this.m_Age; }
            set
            {
                if (this.m_Age != value)
                {
                    this.m_Age = value;
                    NotifyPropertyChanged("Age");
                }
            }
        }

        [PersistentProperty()]
        public string PAPResult
        {
            get { return this.m_PAPResult; }
            set
            {
                if (this.m_PAPResult != value)
                {
                    this.m_PAPResult = value;
                    NotifyPropertyChanged("PAPResult");
                }
            }
        }

        [PersistentProperty()]
        public string HPVTesting
        {
            get { return this.m_HPVTesting; }
            set
            {
                if (this.m_HPVTesting != value)
                {
                    this.m_HPVTesting = value;
                    NotifyPropertyChanged("HPVTesting");
                }
            }
        }

        [PersistentProperty()]
        public string Endocervical
        {
            get { return this.m_Endocervical; }
            set
            {
                if (this.m_Endocervical != value)
                {
                    this.m_Endocervical = value;
                    NotifyPropertyChanged("Endocervical");
                }
            }
        }

        [PersistentProperty()]
        public string HPVStandingOrderName
        {
            get{ return this.m_HPVStandingOrderName; }
            set
            {
                if (this.m_HPVStandingOrderName != value)
                {
                    this.m_HPVStandingOrderName = value;
                    NotifyPropertyChanged("HPVStandingOrderName");
                }
            }
        }
    }
}
