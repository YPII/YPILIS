using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class PersistentClass : System.Attribute
    {        
        private string m_StorageName;
        private string m_BaseStorageName;
        private bool m_HasPersistentBaseClass;
        private bool m_IsPersisted;
        private bool m_IsManyToManyRelationship;
        private string m_Database;        

		public PersistentClass(bool hasPersitentBase, string baseStorageName, string database)
        {
            this.m_StorageName = null;
            this.m_BaseStorageName = baseStorageName;
            this.m_HasPersistentBaseClass = hasPersitentBase;
            this.m_IsPersisted = false;
            this.m_IsManyToManyRelationship = false;
			this.m_Database = database;            
        }        

        public PersistentClass(string storageName, string database)
        {
            this.m_StorageName = storageName;
            this.m_BaseStorageName = null;
            this.m_HasPersistentBaseClass = false;
            this.m_IsPersisted = true;
            this.m_IsManyToManyRelationship = false;
			this.m_Database = database;            
        }
        
        public PersistentClass(string storageName, bool isManyToManyRelationship, string database)
        {
            this.m_StorageName = storageName;
            this.m_BaseStorageName = null;
            this.m_HasPersistentBaseClass = false;
            this.m_IsPersisted = true;
            this.m_IsManyToManyRelationship = isManyToManyRelationship;
			this.m_Database = database;            
        }

        public PersistentClass(string storageName, string baseStorageName, string database)
        {
            this.m_StorageName = storageName;
            this.m_BaseStorageName = baseStorageName;
            this.m_HasPersistentBaseClass = true;
            this.m_IsPersisted = true;
			this.m_Database = database;            
        }        

        public string StorageName
        {
            get { return this.m_StorageName; }
        }

        public string BaseStorageName
        {
            get { return this.m_BaseStorageName; }
        }

        public bool HasPersistentBaseClass
        {
            get { return this.m_HasPersistentBaseClass; }
        }

        public bool IsPersisted
        {
            get { return this.m_IsPersisted; }
        }

        public bool IsManyToManyRelationship
        {
            get { return this.m_IsManyToManyRelationship; }
        }        

        public string Database
        {
            get { return this.m_Database; }
        }        
    }
}

