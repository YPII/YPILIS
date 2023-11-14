using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class PersistentDocumentCollectionName : System.Attribute
    {
        private string m_CollectionName;

        public PersistentDocumentCollectionName(string collectionName)
        {
            this.m_CollectionName = collectionName;
        }

        public string CollectionName
        {
            get { return this.m_CollectionName; }
        }
    }
}

