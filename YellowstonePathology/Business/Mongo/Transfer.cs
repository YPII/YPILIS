using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Mongo
{
    [PersistentDocumentCollectionName("Transfer")]
    public class Transfer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_ObjectId;
        protected string m_TableName;
        protected string m_AssemblyQualifiedName;
        protected string m_PrimaryKeyName;        
        protected bool m_HasCLSObjectId;
        protected bool m_HasSQLObjectId;
        protected bool m_HasSQLTimestamp;
        protected string m_TimestampColumnName;
        protected bool m_HasTransferDBTSAttribute;
        protected string m_TransferDBTS;
        protected bool m_HasSQLDeleteTrigger;        
        protected int m_SQLRowCount;
        protected Int64 m_MongoDocumentCount;
        protected int m_SQLNullObjectIdCount;
        protected int m_ZeroXCount;
        protected int m_OutOfSyncCount;
        protected int m_SQLIndexCount;        
        protected int m_MongoIndexCount;
        protected bool m_HasBaseClass;
        protected string m_BaseTableName;                
        protected long m_ExtendedDocumentCount;
        protected bool m_HasTransferStraightAcrossAttribute;
        protected bool m_TransferStraightAcross;

        public Transfer()
        {
            
        }

        public static Transfer New(string tableName, string className)
        {            
            MongoDB.Bson.BsonObjectId bsonObjectId = MongoDB.Bson.BsonObjectId.GenerateNewId();
            Transfer transfer = new Transfer();
            transfer.m_ObjectId = bsonObjectId.ToString();
            transfer.m_TableName = tableName;            
            transfer.m_HasCLSObjectId = false;
            transfer.m_HasSQLObjectId = false;
            transfer.m_HasSQLTimestamp = false;
            transfer.m_TimestampColumnName = null;
            transfer.m_HasTransferDBTSAttribute = false;
            transfer.m_TransferDBTS = null;
            transfer.m_HasSQLDeleteTrigger = false;            
            transfer.m_SQLRowCount = 0;
            transfer.m_MongoDocumentCount = 0;
            transfer.m_SQLNullObjectIdCount = 0;
            transfer.m_OutOfSyncCount = 0;
            transfer.m_SQLIndexCount = 0;
            transfer.m_MongoIndexCount = 0;
            transfer.m_HasBaseClass = false;
            transfer.m_BaseTableName = null;                        
            transfer.m_ExtendedDocumentCount = 0;
            transfer.m_HasTransferStraightAcrossAttribute = false;
            transfer.m_TransferStraightAcross = false;
            return transfer;
        }

        [PersistentDocumentIdProperty()]
        public string ObjectId
        {
            get { return this.m_ObjectId; }
            set 
            {
                if (this.m_ObjectId != value)
                {                    
                    this.m_ObjectId = value;
                    this.NotifyPropertyChanged("ObjectId");
                }
            }
        }       

        [PersistentProperty()]
        public string AssemblyQualifiedName
        {
            get { return this.m_AssemblyQualifiedName; }
            set
            {
                if (this.m_AssemblyQualifiedName != value)
                {
                    this.m_AssemblyQualifiedName = value;
                    this.NotifyPropertyChanged("AssemblyQualifiedName");
                }
            }
        }

        [PersistentProperty()]
        public string TableName
        {
            get { return this.m_TableName; }
            set 
            {
                if (this.m_TableName != value)
                {
                    this.m_TableName = value;
                    this.NotifyPropertyChanged("TableName");
                }
            }
        }

        [PersistentProperty()]
        public string PrimaryKeyName
        {
            get { return this.m_PrimaryKeyName; }
            set
            {
                if (this.m_PrimaryKeyName != value)
                {
                    this.m_PrimaryKeyName = value;
                    this.NotifyPropertyChanged("PrimaryKeyName");
                }
            }
        }

        [PersistentProperty()]
        public bool HasCLSObjectId
        {
            get { return this.m_HasCLSObjectId; }
            set
            {
                if (this.m_HasCLSObjectId != value)
                {
                    this.m_HasCLSObjectId = value;
                    this.NotifyPropertyChanged("HasCLSObjectId");
                }
            }
        }

        [PersistentProperty()]
        public bool HasSQLObjectId
        {
            get { return this.m_HasSQLObjectId; }
            set
            {
                if (this.m_HasSQLObjectId != value)
                {
                    this.m_HasSQLObjectId = value;
                    this.NotifyPropertyChanged("HasSQLObjectId");
                }
            }
        }

        [PersistentProperty()]
        public bool HasSQLTimestamp
        {
            get { return this.m_HasSQLTimestamp; }
            set
            {
                if (this.m_HasSQLTimestamp != value)
                {
                    this.m_HasSQLTimestamp = value;
                    this.NotifyPropertyChanged("HasSQLTimestamp");
                }
            }
        }

        [PersistentProperty()]
        public string TimestampColumnName
        {
            get { return this.m_TimestampColumnName; }
            set
            {
                if (this.m_TimestampColumnName != value)
                {
                    this.m_TimestampColumnName = value;
                    this.NotifyPropertyChanged("TimestampColumnName");
                }
            }
        }

        [PersistentProperty()]
        public bool HasTransferDBTSAttribute
        {
            get { return this.m_HasTransferDBTSAttribute; }
            set
            {
                if (this.m_HasTransferDBTSAttribute != value)
                {
                    this.m_HasTransferDBTSAttribute = value;
                    this.NotifyPropertyChanged("HasTransferDBTSAttribute");
                }
            }
        }

        [PersistentProperty()]
        public string TransferDBTS
        {
            get { return this.m_TransferDBTS; }
            set
            {
                if (this.m_TransferDBTS != value)
                {
                    this.m_TransferDBTS = value;
                    this.NotifyPropertyChanged("TransferDBTS");
                }
            }
        }

        [PersistentProperty()]
        public bool HasSQLDeleteTrigger
        {
            get { return this.m_HasSQLDeleteTrigger; }
            set
            {
                if (this.m_HasSQLDeleteTrigger != value)
                {
                    this.m_HasSQLDeleteTrigger = value;
                    this.NotifyPropertyChanged("HasSQLDeleteTrigger");
                }
            }
        }        

        [PersistentProperty()]
        public int SQLRowCount
        {
            get { return this.m_SQLRowCount; }
            set
            {
                if (this.m_SQLRowCount != value)
                {
                    this.m_SQLRowCount = value;
                    this.NotifyPropertyChanged("SQLRowCount");
                }
            }
        }

        [PersistentProperty()]
        public Int64 MongoDocumentCount
        {
            get { return this.m_MongoDocumentCount; }
            set
            {
                if (this.m_MongoDocumentCount != value)
                {
                    this.m_MongoDocumentCount = value;
                    this.NotifyPropertyChanged("MongoDocumentCount");
                }
            }
        }

        [PersistentProperty()]
        public int SQLNullObjectIdCount
        {
            get { return this.m_SQLNullObjectIdCount; }
            set
            {
                if (this.m_SQLNullObjectIdCount != value)
                {
                    this.m_SQLNullObjectIdCount = value;
                    this.NotifyPropertyChanged("SQLNullObjectIdCount");
                }
            }
        }

        [PersistentProperty()]
        public int ZeroXCount
        {
            get { return this.m_ZeroXCount; }
            set
            {
                if (this.m_ZeroXCount != value)
                {
                    this.m_ZeroXCount = value;
                    this.NotifyPropertyChanged("ZeroXCount");
                }
            }
        }

        [PersistentProperty()]
        public int OutOfSyncCount
        {
            get { return this.m_OutOfSyncCount; }
            set
            {
                if (this.m_OutOfSyncCount != value)
                {
                    this.m_OutOfSyncCount = value;
                    this.NotifyPropertyChanged("OutOfSyncCount");
                }
            }
        }

        [PersistentProperty()]
        public int SQLIndexCount
        {
            get { return this.m_SQLIndexCount; }
            set
            {
                if (this.m_SQLIndexCount != value)
                {
                    this.m_SQLIndexCount = value;
                    this.NotifyPropertyChanged("SQLIndexCount");
                }
            }
        }

        [PersistentProperty()]
        public int MongoIndexCount
        {
            get { return this.m_MongoIndexCount; }
            set
            {
                if (this.m_MongoIndexCount != value)
                {
                    this.m_MongoIndexCount = value;
                    this.NotifyPropertyChanged("MongoIndexCount");
                }
            }
        }

        [PersistentProperty()]
        public bool HasBaseClass
        {
            get { return this.m_HasBaseClass; }
            set
            {
                if (this.m_HasBaseClass != value)
                {
                    this.m_HasBaseClass = value;
                    this.NotifyPropertyChanged("HasBaseClass");
                }
            }
        }

        [PersistentProperty()]
        public string BaseTableName
        {
            get { return this.m_BaseTableName; }
            set
            {
                if (this.m_BaseTableName != value)
                {
                    this.m_BaseTableName = value;
                    this.NotifyPropertyChanged("BaseTableName");
                }
            }
        }        
        
        [PersistentProperty()]
        public long ExtendedDocumentCount
        {
            get { return this.m_ExtendedDocumentCount; }
            set
            {
                if (this.m_ExtendedDocumentCount != value)
                {
                    this.m_ExtendedDocumentCount = value;
                    this.NotifyPropertyChanged("ExtendedDocumentCount");
                }
            }
        }

        [PersistentProperty()]
        public bool HasTransferStraightAcrossAttribute
        {
            get { return this.m_HasTransferStraightAcrossAttribute; }
            set
            {
                if (this.m_HasTransferStraightAcrossAttribute != value)
                {
                    this.m_HasTransferStraightAcrossAttribute = value;
                    this.NotifyPropertyChanged("HasTransferStraightAcrossAttribute");
                }
            }
        }

        [PersistentProperty()]
        public bool TransferStraightAcross
        {
            get { return this.m_TransferStraightAcross; }
            set
            {
                if (this.m_TransferStraightAcross != value)
                {
                    this.m_TransferStraightAcross = value;
                    this.NotifyPropertyChanged("TransferStraightAcross");
                }
            }
        }

        public void UpdateHasCLSObjectId()
        {
            Type type = Type.GetType(this.m_AssemblyQualifiedName);
            List<PropertyInfo> propertyList = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentDocumentIdProperty))).ToList();
            if (propertyList.Count != 0) this.m_HasCLSObjectId = true;
            this.NotifyPropertyChanged("HasCLSObjectId");
        }

        public void UpdateHasSQLObjectId()
        {
            this.m_HasSQLObjectId = YellowstonePathology.Business.Mongo.Gateway.HasSQLObjectId(this.m_TableName);
            this.NotifyPropertyChanged("HasSQLObjectId");
        }

        public void UpdateHasSQLTimestamp()
        {
            this.m_HasSQLTimestamp = YellowstonePathology.Business.Mongo.Gateway.HasSQLTimestamp(this.m_TableName);
            this.m_TimestampColumnName = YellowstonePathology.Business.Mongo.Gateway.GetTimestampColumnName(this.m_TableName);
            this.NotifyPropertyChanged("HasSQLTimestamp");
            this.NotifyPropertyChanged("TimestampColumnName");
        }

        public void UpdateTimestampColumnName()
        {
            this.m_TimestampColumnName = YellowstonePathology.Business.Mongo.Gateway.GetTimestampColumnName(this.m_TableName);
            this.NotifyPropertyChanged("TimestampColumnName");
        }

        public void UpdateHasSQLDeleteTrigger()
        {
            this.m_HasSQLDeleteTrigger = YellowstonePathology.Business.Mongo.Gateway.HasSQLDeleteTrigger(this.m_TableName);
            this.NotifyPropertyChanged("HasSQLDeleteTrigger");
        }

        public void UpdateTransferDBTSAttribute()
        {
            this.m_HasTransferDBTSAttribute = YellowstonePathology.Business.Mongo.Gateway.HasTransferDBTSAttribute(this.m_TableName);
            if(this.m_HasTransferDBTSAttribute == true)
            {
                this.m_TransferDBTS = YellowstonePathology.Business.Mongo.Gateway.GetTransferDBTS(this.m_TableName);
            }
            this.NotifyPropertyChanged("TransferDBTS");
            this.NotifyPropertyChanged("HasTransferDBTSAttribute");
        }

        public void SetTransferStraightAccrossAttribute(bool result)
        {
            if (YellowstonePathology.Business.Mongo.Gateway.HasTransferTransferStraightAcrossAttribute(this.TableName) == true)
            {
                YellowstonePathology.Business.Mongo.Gateway.SetTransferStraightAcrossAttribute(this.m_TableName, result);
            }
            else
            {
                YellowstonePathology.Business.Mongo.Gateway.AddTransferStraightAcrossAttribute(this.m_TableName, result);
            }

            this.m_TransferStraightAcross = result;
            this.NotifyPropertyChanged("TransferStraightAcross");            
        }

        public void UpdateTransferStraightAccrossAttribute()
        {
            this.m_HasTransferStraightAcrossAttribute = YellowstonePathology.Business.Mongo.Gateway.HasTransferTransferStraightAcrossAttribute(this.m_TableName);
            if (this.m_HasTransferStraightAcrossAttribute == true)
            {
                this.m_TransferStraightAcross = YellowstonePathology.Business.Mongo.Gateway.GetTransferStraighAcrossAttribute(this.m_TableName);
            }
            this.NotifyPropertyChanged("TransferStraightAcross");
            this.NotifyPropertyChanged("HasTransferStraightAcrossAttribute");            
        }

        public void UpdateZeroXCount()
        {
            this.m_ZeroXCount = YellowstonePathology.Business.Mongo.Gateway.GetZeroXCount(this.m_TableName);
            this.NotifyPropertyChanged("ZeroXCount");
        }

        public void UpdateOutOfSyncCount()
        {
            this.m_OutOfSyncCount = YellowstonePathology.Business.Mongo.Gateway.GetOutOfSyncCount(this.m_TableName);
            this.NotifyPropertyChanged("OutOfSyncCount");
        }

        public void FixZeroX()
        {
            YellowstonePathology.Business.Mongo.Gateway.FixZeroX(this.m_TableName);            
        }

        public void UpdateSQLRowCount()
        {
            this.m_SQLRowCount = YellowstonePathology.Business.Mongo.Gateway.GetSQLRowCount(this.m_TableName);
            this.NotifyPropertyChanged("SQLRowCount");
        }

        public void SetTransferDBTS()
        {
            YellowstonePathology.Business.Mongo.Gateway.SetTransferDBTS(this.m_TableName);            
        }

        public void UpdateTransferDBTS()
        {
            this.m_TransferDBTS = YellowstonePathology.Business.Mongo.Gateway.GetTransferDBTS(this.m_TableName).ToString();
            this.NotifyPropertyChanged("TransferDBTS");
        }        

        public void UpdateSQLNullObjectIdCount()
        {            
            this.m_SQLNullObjectIdCount = YellowstonePathology.Business.Mongo.Gateway.GetSQLNullObjectIdCount(this.m_TableName);
            this.NotifyPropertyChanged("SQLNullObjectIdCount");
        }

        public void UpdateMongoDocumentCount()
        {            
            this.m_MongoDocumentCount = YellowstonePathology.Business.Mongo.Gateway.GetMongoDocumentCount(this.GetCollectionName(), this.GetDatabaseName());
            this.NotifyPropertyChanged("MongoDocumentCount");
        }

        public void AddSQLObjectIdColumn()
        {
            YellowstonePathology.Business.Mongo.Gateway.AddSQLObjectIDColumn(this.m_TableName);
            this.m_HasSQLObjectId = true;
            this.NotifyPropertyChanged("HasSQLObjectId");            
        }

        public void AddSQLTimeStampColumn()
        {
            YellowstonePathology.Business.Mongo.Gateway.AddSQLTimestampColumn(this.m_TableName);
            this.m_HasSQLTimestamp = true;
            this.NotifyPropertyChanged("HasSQLTimestamp");
        }

        public void AddTransferDBTSAttribute()
        {
            if (this.m_HasTransferDBTSAttribute == false)
            {
                YellowstonePathology.Business.Mongo.Gateway.AddTransferDBTSAttribute(this.m_TableName);
                this.m_HasTransferDBTSAttribute = true;
                this.NotifyPropertyChanged("HasTransferDBTSAttribute");
            }
        }        

        public void AddSQLDeleteTrigger()
        {
            YellowstonePathology.Business.Mongo.Gateway.AddSQLDeleteTrigger(this.m_TableName);
            this.m_HasSQLDeleteTrigger = true;
            this.NotifyPropertyChanged("HasSQLDeleteTrigger");
        }

        public void DropTimeStampField()
        {
            YellowstonePathology.Business.Mongo.Gateway.DropTimestampColumn(this.m_TableName);
            this.m_HasSQLTimestamp = false;
            this.NotifyPropertyChanged("HasSQLTimestamp");
        }

        public void DropSQLDeleteTrigger()
        {
            YellowstonePathology.Business.Mongo.Gateway.DropSQLDeleteTrigger(this.m_TableName);
            this.m_HasSQLDeleteTrigger = false;
            this.NotifyPropertyChanged("HasSQLDeleteTrigger");
        }

        public void DropObjectId()
        {
            YellowstonePathology.Business.Mongo.Gateway.DropObjectId(this.m_TableName);            
        }

        public void UpdateSQLObjectIDs(int maxUpdateCount)
        {
            YellowstonePathology.Business.Mongo.Gateway.UpdateSQLObjectIDs(this.m_TableName, this.m_PrimaryKeyName, maxUpdateCount);
            this.UpdateSQLNullObjectIdCount();            
        }

        public void TransferTableToMongo(BackgroundWorker backgroundWorker)
        {                                    
            YellowstonePathology.Business.Mongo.Gateway.TransferTableToMongo(this.m_TableName, this.GetDatabaseName(), this.GetCollectionName(), backgroundWorker);
            this.UpdateMongoDocumentCount();
            this.NotifyPropertyChanged("MongoDocumentCount");
        }

        public void Synchronize()
        {            
            YellowstonePathology.Business.Mongo.Gateway.Synchronize(this.m_TableName, this.GetDatabaseName(), this.GetCollectionName());
            this.SetTransferDBTS();
            this.UpdateTransferDBTS();
            this.UpdateOutOfSyncCount();            
        }

        public void DropMongoCollection()
        {
            YellowstonePathology.Business.Mongo.Gateway.DropMongoCollection(this.m_TableName);
            this.m_MongoDocumentCount = 0;
            this.NotifyPropertyChanged("MongoDocumentCount");
        }

        public void UpdateSQLIndexCount()
        {
            this.m_SQLIndexCount = YellowstonePathology.Business.Mongo.Gateway.GetSQLIndexCount(this.m_TableName);
            this.NotifyPropertyChanged("SQLIndexCount");
        }

        public void UpdateMongoIndexCount()
        {
            this.m_MongoIndexCount = YellowstonePathology.Business.Mongo.Gateway.GetMongoIndexCount(this.GetCollectionName(), this.GetDatabaseName());
            this.NotifyPropertyChanged("MongoIndexCount");
        }

        public void UpdateExtendedDocumentCount()
        {
            if (this.HasBaseClass == true)
            {
                this.m_ExtendedDocumentCount = YellowstonePathology.Business.Mongo.Gateway.GetExtendedDocumentCount(this);
                this.NotifyPropertyChanged("ExtendedDocumentCount");
            }
        }

        public void BuildMongoIndexes()
        {
            
            YellowstonePathology.Business.Mongo.Gateway.BuildIndexes(this.m_TableName, this.GetCollectionName(), this.GetDatabaseName());
            this.UpdateMongoIndexCount();
            this.NotifyPropertyChanged("MongoIndexCount");
        }

        public virtual void ExtendDocuments()
        {
            YellowstonePathology.Business.Mongo.Gateway.ExtendDocuments(this);
            this.UpdateExtendedDocumentCount();
        }

        public string GetDatabaseName()
        {
            string databaseName = YellowstonePathology.Business.Mongo.MongoTestServer.SQLTransferDatabasename;            
            if (this.m_TransferStraightAcross == true)
            {
                databaseName = YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName;                
            }
            return databaseName;
        }

        public string GetCollectionName()
        {            
            string collectionName = this.m_TableName;
            if (this.m_TransferStraightAcross == true)
            {         
                collectionName = this.m_TableName.Substring(3);
            }
            return collectionName;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
