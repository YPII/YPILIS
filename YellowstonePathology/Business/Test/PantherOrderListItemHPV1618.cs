using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
    public class PantherOrderListItemHPV1618 : PantherOrderListItem
    {
        private string m_HPV16Result;
        private string m_HPV18Result;

        public PantherOrderListItemHPV1618()
        {

        }

        [PersistentProperty()]
        public string HPV16Result
        {
            get { return this.m_HPV16Result; }
            set { this.m_HPV16Result = value; }
        }

        [PersistentProperty()]
        public string HPV18Result
        {
            get { return this.m_HPV18Result; }
            set { this.m_HPV18Result = value; }
        }
    }
}
