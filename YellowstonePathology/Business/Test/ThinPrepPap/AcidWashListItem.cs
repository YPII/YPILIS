using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class AcidWashListItem
    {
        private string m_MasterAccessionNo;
        private string m_ReportNo;
        private DateTime m_OrderDate;
        private string m_PLastName;
        private string m_PFirstName;
        private string m_PMiddleInitial;
        private bool m_Accepted;

        public AcidWashListItem()
        {

        }

        public string PatientName
        {
            get
            {
                return Helper.PatientHelper.GetPatientDisplayName(this.m_PLastName, this.m_PFirstName, this.m_PMiddleInitial);
            }
        }

        [PersistentProperty()]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        [PersistentProperty()]
        public string PLastName
        {
            set { this.m_PLastName = value; }
        }

        [PersistentProperty()]
        public string PFirstName
        {
            set { this.m_PFirstName = value; }
        }

        [PersistentProperty()]
        public string PMiddleInitial
        {
            set { this.m_PMiddleInitial = value; }
        }

        [PersistentProperty()]
        public DateTime OrderDate
        {
            get { return this.m_OrderDate; }
            set { this.m_OrderDate = value; }
        }

        [PersistentProperty()]
        public bool Accepted
        {
            get { return this.m_Accepted; }
            set { this.m_Accepted = value; }
        }
    }
}
