using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;


namespace YellowstonePathology.UI
{
    public class EmbeddingNotScannedListItem
    {
        private string m_AliquotOrderId;
        private string m_PanelSetName;
        private string m_Description;        

        public EmbeddingNotScannedListItem()
        {

        }

        [PersistentProperty()]
        public string AliquotOrderId
        {
            get { return this.m_AliquotOrderId; }
            set { this.m_AliquotOrderId = value; }
        }

        [PersistentProperty()]
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set { this.m_PanelSetName = value; }
        }

        [PersistentProperty()]
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }
    }
}
