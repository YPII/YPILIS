using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
    public class ClientDistribution : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private PhysicianClientDistribution m_PhysicianClientDistribution;
        private string m_ClientPhysicianClientId;
        private string m_DistributionClientPhysicianClientId;
        private int m_ClientId;
        private string m_ClientName;
        private string m_ClientDistributionType;
        private string m_ClientAlternateDistributionType;
        private int m_DistributionClientId;
        private string m_DistributionClientName;
        private string m_DistributionClientDistributionType;
        private string m_DistributionClientAlternateDistributionType;
        private int m_PhysicianId;
        private string m_DisplayName;
        private string m_SuggestedDistributionType;
        private string m_SuggestedAlternateDistributionType;

        public ClientDistribution()
        { }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public PhysicianClientDistribution PhysicianClientDistribution
        {
            get { return this.m_PhysicianClientDistribution; }
            set
            {
                if (this.m_PhysicianClientDistribution != value)
                {
                    this.m_PhysicianClientDistribution = value;
                    this.NotifyPropertyChanged("PhysicianClientDistribution");
                }
            }
        }

        [PersistentProperty()]
        public string ClientPhysicianClientId
        {
            get { return this.m_ClientPhysicianClientId; }
            set
            {
                if (this.m_ClientPhysicianClientId != value)
                {
                    this.m_ClientPhysicianClientId = value;
                    this.NotifyPropertyChanged("ClientPhysicianClientId");
                }
            }
        }

        [PersistentProperty()]
        public string DistributionClientPhysicianClientId
        {
            get { return this.m_DistributionClientPhysicianClientId; }
            set
            {
                if (this.m_DistributionClientPhysicianClientId != value)
                {
                    this.m_DistributionClientPhysicianClientId = value;
                    this.NotifyPropertyChanged("DistributionClientPhysicianClientId");
                }
            }
        }

        [PersistentProperty()]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set
            {
                if (this.m_ClientId != value)
                {
                    this.m_ClientId = value;
                    this.NotifyPropertyChanged("ClientId");
                }
            }
        }

        [PersistentProperty()]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set
            {
                if (this.m_ClientName != value)
                {
                    this.m_ClientName = value;
                    this.NotifyPropertyChanged("ClientName");
                }
            }
        }

        [PersistentProperty()]
        public string ClientDistributionType
        {
            get { return this.m_ClientDistributionType; }
            set
            {
                if (this.m_ClientDistributionType != value)
                {
                    this.m_ClientDistributionType = value;
                    this.NotifyPropertyChanged("ClientDistributionType");
                }
            }
        }

        [PersistentProperty()]
        public string ClientAlternateDistributionType
        {
            get { return this.m_ClientAlternateDistributionType; }
            set
            {
                if (this.m_ClientAlternateDistributionType != value)
                {
                    this.m_ClientAlternateDistributionType = value;
                    this.NotifyPropertyChanged("ClientAlternateDistributionType");
                }
            }
        }

        [PersistentProperty()]
        public int DistributionClientId
        {
            get { return this.m_DistributionClientId; }
            set
            {
                if (this.m_DistributionClientId != value)
                {
                    this.m_DistributionClientId = value;
                    this.NotifyPropertyChanged("DistributionClientId");
                }
            }
        }

        [PersistentProperty()]
        public string DistributionClientName
        {
            get { return this.m_DistributionClientName; }
            set
            {
                if (this.m_DistributionClientName != value)
                {
                    this.m_DistributionClientName = value;
                    this.NotifyPropertyChanged("DistributionClientName");
                }
            }
        }

        [PersistentProperty()]
        public string DistributionClientDistributionType
        {
            get { return this.m_DistributionClientDistributionType; }
            set
            {
                if (this.m_DistributionClientDistributionType != value)
                {
                    this.m_DistributionClientDistributionType = value;
                    this.NotifyPropertyChanged("DistributionClientDistributionType");
                }
            }
        }

        [PersistentProperty()]
        public string DistributionClientAlternateDistributionType
        {
            get { return this.m_DistributionClientAlternateDistributionType; }
            set
            {
                if (this.m_DistributionClientAlternateDistributionType != value)
                {
                    this.m_DistributionClientAlternateDistributionType = value;
                    this.NotifyPropertyChanged("DistributionClientAlternateDistributionType");
                }
            }
        }

        [PersistentProperty()]
        public string DisplayName
        {
            get { return this.m_DisplayName; }
            set
            {
                if (this.m_DisplayName != value)
                {
                    this.m_DisplayName = value;
                    this.NotifyPropertyChanged("DisplayName");
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
                    this.NotifyPropertyChanged("PhysicianId");
                }
            }
        }

        public string SuggestedDistributionType
        {
            get { return this.m_SuggestedDistributionType; }
            set
            {
                if (this.m_SuggestedDistributionType != value)
                {
                    this.m_SuggestedDistributionType = value;
                    this.NotifyPropertyChanged("SuggestedDistributionType");
                }
            }
        }

        public string SuggestedAlternateDistributionType
        {
            get { return this.m_SuggestedAlternateDistributionType; }
            set
            {
                if (this.m_SuggestedAlternateDistributionType != value)
                {
                    this.m_SuggestedAlternateDistributionType = value;
                    this.NotifyPropertyChanged("SuggestedAlternateDistributionType");
                }
            }
        }
    }
}
