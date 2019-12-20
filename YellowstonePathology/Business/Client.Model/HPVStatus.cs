using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStatus
    {
        private string m_MasterAccessionNo;
        private string m_HPVStandingOrderCode;
        private string m_ClientName;
        private string m_PhysicianName;
        private bool m_HPVRequired;
        private bool m_HPVOrdered;

        public HPVStatus()
        { }

        [PersistentProperty()]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        [PersistentProperty()]
        public string HPVStandingOrderCode
        {
            get { return this.m_HPVStandingOrderCode; }
            set { this.m_HPVStandingOrderCode = value; }
        }

        [PersistentProperty()]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

        [PersistentProperty()]
        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set { this.m_PhysicianName = value; }
        }

        public bool HPVRequired
        {
            get { return this.m_HPVRequired; }
            set { this.m_HPVRequired = value; }
        }

        public bool HPVOrdered
        {
            get { return this.m_HPVOrdered; }
            set { this.m_HPVOrdered = value; }
        }
    }
}
