using System;
using YellowstonePathology.Business.Persistence;


namespace YellowstonePathology.Business.MaterialTracking.Model
{
    public class BlockSentNotReturned
    {
        private string m_FacilityId;
        private string m_AliquotId;
        private DateTime m_LogDate;

        public BlockSentNotReturned() { }

        [PersistentProperty()]
        public string FacilityId
        {
            get { return this.m_FacilityId; }
            set { this.m_FacilityId = value; }
        }

        [PersistentProperty()]
        public string AliquotId
        {
            get { return this.m_AliquotId; }
            set { this.m_AliquotId = value; }
        }

        [PersistentProperty()]
        public DateTime LogDate
        {
            get { return this.m_LogDate; }
            set { this.m_LogDate = value; }
        }
    }
}
