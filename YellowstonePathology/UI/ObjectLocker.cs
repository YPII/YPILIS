using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class ObjectLocker
    {
        private static ObjectLocker instance;

        private Dictionary<string, object> m_RegisteredHistologyLoginObjects;

        private ObjectLocker()
        {
            this.m_RegisteredHistologyLoginObjects = new Dictionary<string, object>();
        }

        public void Register(object objectToRegister)
        {
            
        }

        public void Deregister()
        {

        }

        public static ObjectLocker Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ObjectLocker();
                }
                return instance;
            }
        }
    }
}
