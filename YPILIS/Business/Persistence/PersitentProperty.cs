using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{    
    public class PersistentProperty : System.Attribute
    {        
        private bool m_IsInheritingClassKey;

        public PersistentProperty()
        {
            this.m_IsInheritingClassKey = false;
        }

        public PersistentProperty(bool isInheritingClassKey)
        {
            this.m_IsInheritingClassKey = isInheritingClassKey;
        }

        public bool IsInheritingClassKey
        {
            get { return this.m_IsInheritingClassKey; }
        }
    }
}

