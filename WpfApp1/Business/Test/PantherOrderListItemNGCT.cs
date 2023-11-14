using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
    public class PantherOrderListItemNGCT : PantherOrderListItem
    {
        private string m_NeisseriaGonorrhoeaeResult;
        private string m_ChlamydiaTrachomatisResult;        

        public PantherOrderListItemNGCT()
        {

        }

        [PersistentProperty()]
        public string NeisseriaGonorrhoeaeResult
        {
            get { return this.m_NeisseriaGonorrhoeaeResult; }
            set { this.m_NeisseriaGonorrhoeaeResult = value; }
        }

        [PersistentProperty()]
        public string ChlamydiaTrachomatisResult
        {
            get { return this.m_ChlamydiaTrachomatisResult; }
            set { this.m_ChlamydiaTrachomatisResult = value; }
        }        
    }
}
