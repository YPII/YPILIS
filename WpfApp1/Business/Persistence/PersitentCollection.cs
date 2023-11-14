using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class PersistentCollection : System.Attribute
    {       
        private bool m_IsBuildOnly;

        public PersistentCollection()
        {
            this.m_IsBuildOnly = false;
        }

        public PersistentCollection(bool isBuildOnly)
        {
            this.m_IsBuildOnly = isBuildOnly;
        }

        public bool IsBuildOnly
        {
            get { return this.m_IsBuildOnly; }
        }
    }
}

