using System;
using YellowstonePathology.Business.Persistence;


namespace YellowstonePathology.UI
{
    public class EmbeddingAutopsyItem
    {
        private string m_AliquotOrderId;
        private string m_PFirstName;
        private string m_PLastName;
        private DateTime m_AccessionTime;
        private string m_Description;

        public EmbeddingAutopsyItem()
        {

        }

        [PersistentProperty()]
        public string AliquotOrderId
        {
            get { return this.m_AliquotOrderId; }
            set { this.m_AliquotOrderId = value; }
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
        public DateTime AccessionTime
        {
            get { return this.m_AccessionTime; }
            set { this.m_AccessionTime = value; }
        }

        [PersistentProperty()]
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }
    }
}
