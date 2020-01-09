using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;
using System.Text.RegularExpressions;

namespace YellowstonePathology.Business.Client.Model
{
    public class PhysicianClientDistributionListItem
    {
        protected int m_PhysicianId;
        protected string m_PhysicianName;
        protected int m_ClientId;
        protected string m_ClientName;
        protected string m_DistributionType;
        protected string m_AlternateDistributionType;
        protected string m_FaxNumber;

        public PhysicianClientDistributionListItem()
        {

        }

        public virtual void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            this.m_ClientId = physicianClientDistribution.m_ClientId;
            this.m_ClientName = physicianClientDistribution.m_ClientName;
            this.m_PhysicianId = physicianClientDistribution.m_PhysicianId;
            this.m_PhysicianName = physicianClientDistribution.m_PhysicianName;
            this.m_FaxNumber = physicianClientDistribution.FaxNumber;
            this.m_DistributionType = physicianClientDistribution.DistributionType;
            this.m_AlternateDistributionType = physicianClientDistribution.AlternateDistributionType;
        }

        [PersistentProperty()]
        public int PhysicianId
        {
            get { return this.m_PhysicianId; }
            set { this.m_PhysicianId = value; }
        }

        [PersistentProperty()]
        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set { this.m_PhysicianName = value; }
        }

        [PersistentProperty()]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set { this.m_ClientId = value; }
        }

        [PersistentProperty()]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

        [PersistentProperty()]
        public string DistributionType
        {
            get { return this.m_DistributionType; }
            set { this.m_DistributionType = value; }
        }

        [PersistentProperty()]
        public string AlternateDistributionType
        {
            get { return this.m_AlternateDistributionType; }
            set { this.m_AlternateDistributionType = value; }
        }

        [PersistentProperty()]
        public string FaxNumber
        {
            get { return this.m_FaxNumber; }
            set { this.m_FaxNumber = value; }
        }

        public string FormattedFaxNumber
        {
            get
            {
                if (this.m_FaxNumber == null)
                    return string.Empty;

                switch (this.m_FaxNumber.Length)
                {
                    case 7:
                        return Regex.Replace(this.m_FaxNumber, @"(\d{3})(\d{4})", "$1-$2");
                    case 10:
                        return Regex.Replace(this.m_FaxNumber, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                    case 11:
                        return Regex.Replace(this.m_FaxNumber, @"(\d{1})(\d{3})(\d{3})(\d{4})", "$1-$2-$3-$4");
                    default:
                        return this.m_FaxNumber;
                }

            }
        }                

        public virtual void SetDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            throw new Exception("SetDistribution is Not Implemented Here.");
        }                
    }
}