using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Mongo
{
    public class DocumentTracker
    {
        private YellowstonePathology.Business.Mongo.Server m_MongoServer;
        private DocumentStatusEnum m_Status;
        private string m_CollectionName;
        private object m_Object;

        public DocumentTracker(YellowstonePathology.Business.Mongo.Server mongoServer)
        {            
            this.m_MongoServer = mongoServer;         
        }

        public void RegisterInsert(object o)
        {
            this.m_Object = o;            
            PersistentDocumentCollectionName persistentDocumentCollectionName = (PersistentDocumentCollectionName)o.GetType().GetCustomAttributes(typeof(PersistentDocumentCollectionName), false).Single();
            this.m_CollectionName = persistentDocumentCollectionName.CollectionName;
            INotifyPropertyChanged io = (INotifyPropertyChanged)o;
            io.PropertyChanged += new PropertyChangedEventHandler(PropertyChanged);
            this.m_Status = DocumentStatusEnum.Inserted;
        }

        public void Register(object o)
        {
            this.m_Object = o;
            PersistentDocumentCollectionName persistentDocumentCollectionName = (PersistentDocumentCollectionName)o.GetType().GetCustomAttributes(typeof(PersistentDocumentCollectionName), false).Single();
            this.m_CollectionName = persistentDocumentCollectionName.CollectionName;
            INotifyPropertyChanged io = (INotifyPropertyChanged)o;
            io.PropertyChanged += new PropertyChangedEventHandler(PropertyChanged);
            this.m_Status = DocumentStatusEnum.Unchanged;
        }        

        public DocumentStatusEnum Status
        {
            get { return this.m_Status; }
            set { this.m_Status = value; }
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.m_Status = DocumentStatusEnum.Updated;
        }

        public void SubmitChanges()
        {
            switch (this.m_Status)
            {
                case DocumentStatusEnum.Deleted:
                    this.HandleDeletedDocument();
                    break;
                case DocumentStatusEnum.Inserted:
                    this.HandlInsertedDocument();
                    break;
                case DocumentStatusEnum.Updated:
                    this.HandleUpdatedDocument();
                    break;
            }
            this.m_Status = DocumentStatusEnum.Submitted;
        }

        private void HandlInsertedDocument()
        {
            MongoCollection mongoCollection = this.m_MongoServer.Database.GetCollection<BsonDocument>(this.m_CollectionName);
            BsonDocument bsonDocument = YellowstonePathology.Business.Mongo.BSONBuilder.Build(this.m_Object);
            mongoCollection.Insert(bsonDocument);
        }

        private void HandleDeletedDocument()
        {
            MongoCollection mongoCollection = this.m_MongoServer.Database.GetCollection<BsonDocument>(this.m_CollectionName);
            PropertyInfo documentIdPropertyInfo = this.m_Object.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(YellowstonePathology.Business.Persistence.PersistentDocumentIdProperty))).Single();
            string _id = (string)documentIdPropertyInfo.GetValue(this.m_Object, null);            
            QueryDocument queryDocument = new QueryDocument("_id", ObjectId.Parse(_id));
            mongoCollection.Remove(queryDocument);
        }

        private void HandleUpdatedDocument()
        {
            MongoCollection mongoCollection = this.m_MongoServer.Database.GetCollection<BsonDocument>(this.m_CollectionName);            
            BsonDocument bsonDocument = YellowstonePathology.Business.Mongo.BSONBuilder.Build(this.m_Object);            
            mongoCollection.Save(bsonDocument);
        }
    }
}
