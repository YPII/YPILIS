using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
    public class PantherAliquotListItem
    {
        private string m_MasterAccessionNo;        
        private DateTime m_AccessionTime;
        private string m_PLastName;
        private string m_PFirstName;
        private Nullable<DateTime> m_ValidationDate;
        private string m_ValidatedBy;

        public PantherAliquotListItem()
        {

        }

        [PersistentProperty()]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }        

        [PersistentProperty()]
        public DateTime AccessionTime
        {
            get { return m_AccessionTime; }
            set { this.m_AccessionTime = value; }
        }

        [PersistentProperty()]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set { this.m_PLastName = value; }
        }

        [PersistentProperty()]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set { this.m_PFirstName = value; }
        }

        [PersistentProperty()]
        public Nullable<DateTime> ValidationDate
        {
            get { return m_ValidationDate; }
            set { this.m_ValidationDate = value; }
        }

        [PersistentProperty()]
        public string ValidatedBy
        {
            get { return this.m_ValidatedBy; }
            set { this.m_ValidatedBy = value; }
        }
    }
}
