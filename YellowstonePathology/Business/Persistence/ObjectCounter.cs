using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class ObjectCounter
    {
        private Type m_ObjectType;
        private int m_ObjectCount;

        public ObjectCounter(Type objectType)
        {
            this.m_ObjectType = objectType;
            this.m_ObjectCount = 1;
        }

        public Type ObjectType
        {
            get { return this.m_ObjectType; }
        }

        public int ObjectCount
        {
            get { return this.m_ObjectCount; }
        }

        public void Update(object objectToCount)
        {
            this.m_ObjectCount++;
        }
    }
}
