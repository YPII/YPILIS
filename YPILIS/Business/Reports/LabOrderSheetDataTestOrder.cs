using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Reports
{
    public class LabOrderSheetDataTestOrder
    {
        private string m_TestName;
        private bool m_OrderedAsDual;
        private string m_Description;
        private string m_Comment;
        private string m_PanelOrderId;

        public LabOrderSheetDataTestOrder()
        {

        }

        [PersistentProperty()]
        public string TestName
        {
            get { return this.m_TestName; }
            set { this.m_TestName = value; }
        }

        [PersistentProperty()]
        public bool OrderedAsDual
        {
            get { return this.m_OrderedAsDual; }
            set { this.m_OrderedAsDual = value; }
        }

        [PersistentProperty()]
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        [PersistentProperty()]
        public string Comment
        {
            get { return this.m_Comment; }
            set { this.m_Comment = value; }
        }

        [PersistentProperty()]
        public string PanelOrderId
        {
            get { return this.m_PanelOrderId; }
            set { this.m_PanelOrderId = value; }
        }
    }
}
