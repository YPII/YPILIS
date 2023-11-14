using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class PersistentUpdateTracker
    {
        private object m_ObjectToUpdate;
        private object m_OriginalValues;

        public PersistentUpdateTracker(object objectToUpdate)
        {
            this.m_ObjectToUpdate = objectToUpdate;
            ObjectCloner objectCloner = new ObjectCloner();
            this.m_OriginalValues = objectCloner.Clone(this.m_ObjectToUpdate);
        }

        public object ObjectToUpdate
        {
            get { return this.m_ObjectToUpdate; }
        }

        public object OriginalValues
        {
            get { return this.m_OriginalValues; }
        }
    }
}
