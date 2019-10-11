﻿using System;
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
        //private Client m_Client;
        //private Domain.Physician m_Physician;
        //private int m_DistributionClientId;
        //private string m_DistributionClientName;
        private int m_ClientId;
        private string m_ClientName;
        private string m_ClientDistributionType;
        private string m_ClientAlternateDistributionType;
        private int m_DistributionClientId;
        private string m_DistributionClientName;
        private string m_DistributionClientDistributionType;
        private string m_DistributionClientAlternateDistributionType;
        private string m_DisplayName;

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

        /*public Client Client
        {
            get { return this.m_Client; }
            set
            {
                if (this.m_Client != value)
                {
                    this.m_Client = value;
                    this.NotifyPropertyChanged("Client");
                }
            }
        }

        [PersistentProperty()]
        public Domain.Physician Physician
        {
            get { return this.m_Physician; }
            set
            {
                if (this.m_Physician != value)
                {
                    this.m_Physician = value;
                    this.NotifyPropertyChanged("Physician");
                }
            }
        }*/

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
    }
}
