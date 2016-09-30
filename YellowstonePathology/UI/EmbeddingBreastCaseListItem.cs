using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;


namespace YellowstonePathology.UI
{
    public class EmbeddingBreastCaseListItem
    {
        private string m_MasterAccessionNo;
        private string m_PFirstName;
        private string m_PLastName;
        private Nullable<DateTime> m_CollectionTime;
        private string m_ProcessorRun;
        private Nullable<DateTime> m_FixationStartTime;
        private Nullable<DateTime> m_FixationEndTime;
        private Nullable<int> m_FixationDuration;

        public EmbeddingBreastCaseListItem()
        {

        }

        [PersistentProperty()]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        [PersistentProperty()]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set { this.m_PFirstName = value; }
        }

        [PersistentProperty()]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set { this.m_PLastName = value; }
        }

        [PersistentProperty()]
        public Nullable<DateTime> CollectionTime
        {
            get { return this.m_CollectionTime; }
            set { this.m_CollectionTime = value; }
        }

        [PersistentProperty()]
        public string ProcessorRun
        {
            get { return this.m_ProcessorRun; }
            set { this.m_ProcessorRun = value; }
        }

        [PersistentProperty()]
        public Nullable<DateTime> FixationStartTime
        {
            get { return this.m_FixationStartTime; }
            set { this.m_FixationStartTime = value; }
        }

        [PersistentProperty()]
        public Nullable<DateTime> FixationEndTime
        {
            get { return this.m_FixationEndTime; }
            set { this.m_FixationEndTime = value; }
        }

        [PersistentProperty()]
        public Nullable<int> FixationDuration
        {
            get { return this.m_FixationDuration; }
            set { this.m_FixationDuration = value; }
        }
    }
}
