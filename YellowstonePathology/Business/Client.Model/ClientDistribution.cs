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
        private Client m_Client;
        private Domain.Physician m_Physician;
        private int m_DistributionClientId;
        private string m_DistributionClientName;

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

        public Client Client
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
    }
}
