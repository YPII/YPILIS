using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace YellowstonePathology.Business.Mongo
{
    public class Gateway
    {
        public static void AddAllSQLTables(YellowstonePathology.Business.Mongo.TransferCollection transferCollection)
        {            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Table_Name, Column_Name " +
                "FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                "WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 " +
                "AND table_name like 'tbl%'";
            cmd.CommandType = CommandType.Text;

            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(Business.Mongo.TestServer.SQLTransferDatabasename);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Mongo.Transfer transfer = new Transfer();
                        transfer.ObjectId = BsonObjectId.GenerateNewId().ToString();
                        transfer.TableName = dr.GetString(0);
                        transfer.PrimaryKeyName = dr.GetString(1);                        
                        transferCollection.Add(transfer);
                    }
                }
            }            
        }

        public static bool HasSQLObjectId(string tableName)
        {
            bool result = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from sys.columns where Name = N'ObjectId' and Object_ID = Object_ID(N'" + tableName + "')";            
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = true;
            }

            return result;
        }

        public static bool HasSQLTimestamp(string tableName)
        {
            bool result = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from sys.columns where Name = N'Timestamp' and Object_ID = Object_ID(N'" + tableName + "')";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = true;
            }

            return result;
        }

        public static string GetTimestampColumnName(string tableName)
        {
            string result = null;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select Name from sys.columns where Name = N'Timestamp' and Object_ID = Object_ID(N'" + tableName + "')";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = (string)value;
            }

            return result;
        }        

        public static bool HasSQLDeleteTrigger(string tableName)
        {
            bool result = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select OBJECT_ID('trg_" + tableName + "_DeletedRows')";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = true;
            }

            return result;
        }

        public static bool HasTransferDBTSAttribute(string tableName)
        {
            bool result = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT objtype, objname, name, value " +
                "FROM fn_listextendedproperty('TransferDBTS' " +
                ",'schema', 'dbo' " +
                ",'table', '" + tableName + "'" +
                ",'column', 'TimeStamp');";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = true;
            }

            return result;
        }

        public static bool HasTransferTransferStraightAcrossAttribute(string tableName)
        {
            bool result = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT objtype, objname, name, value " +
                "FROM fn_listextendedproperty('TransferStraightAcross' " +
                ",'schema', 'dbo' " +
                ",'table', '" + tableName + "'" +
                ",'column', 'TimeStamp');";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = true;
            }

            return result;
        }

        public static int GetZeroXCount(string tableName)
        {
            int result = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select count(*) from " + tableName + " where substring(objectid, 1,2) = '0x'";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = (int)value;
            }

            return result;
        }

        public static int GetOutOfSyncCount(string tableName)
        {
            int result = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select count(*) from " + tableName + " where [TimeStamp] > " +
                "(SELECT convert(int, ep.value) " +
                "FROM sys.extended_properties AS ep " +
                "INNER JOIN sys.tables AS t ON ep.major_id = t.object_id " +
                "INNER JOIN sys.columns AS c ON ep.major_id = c.object_id AND ep.minor_id = c.column_id " +
                "WHERE class = 1 and t.Name = '" + tableName + "' and c.Name = 'TimeStamp' and ep.Name = 'TransferDBTS')";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = (int)value;
            }

            return result;
        }

        public static string GetTransferDBTS(string tableName)
        {
            string result = null;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT value " +
                "FROM fn_listextendedproperty('TransferDBTS' " +
                ",'schema', 'dbo' " +
                ",'table', '" + tableName + "' " +
                ",'column', 'TimeStamp');";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value == null || value is System.DBNull)
                {
                    result = "0X0";
                }
                else
                {
                    byte[] buffer = (byte[])value;
                    result = "0x" + BitConverter.ToString(buffer, 0).Replace("-", string.Empty);                    
                }
            }

            return result;
        }

        public static bool GetTransferStraighAcrossAttribute(string tableName)
        {
            bool result = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT value " +
                "FROM fn_listextendedproperty('TransferStraightAcross' " +
                ",'schema', 'dbo' " +
                ",'table', '" + tableName + "' " +
                ",'column', 'TimeStamp');";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                result = Convert.ToBoolean(cmd.ExecuteScalar());                
            }

            return result;
        }

        public static int GetSQLRowCount(string tableName)
        {
            int result = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select count(*) from " + tableName;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = Convert.ToInt32(value);
            }

            return result;
        }

        public static int GetSQLNullObjectIdCount(string tableName)
        {
            int result = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select count(*) from " + tableName + " where objectid is null";
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = Convert.ToInt32(value);
            }

            return result;
        }

        public static int GetSQLIndexCount(string tableName)
        {
            int result = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT count(*) " +
                "FROM sys.indexes ind " +
                "INNER JOIN sys.index_columns ic ON  ind.object_id = ic.object_id and ind.index_id = ic.index_id " +
                "INNER JOIN sys.columns col ON ic.object_id = col.object_id and ic.column_id = col.column_id " +
                "INNER JOIN sys.tables t ON ind.object_id = t.object_id " +
                "WHERE t.Name = '" + tableName + "'";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                var value = cmd.ExecuteScalar();
                if (value != null) result = Convert.ToInt32(value);
            }

            return result;
        }

        public static void AddSQLObjectIDColumn(string tableName)
        {            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS " +
                "WHERE TABLE_NAME = '" + tableName + "' AND COLUMN_NAME = 'ObjectId') " +
                "BEGIN " +
                "ALTER TABLE [dbo].[" + tableName + "] ADD " +
                "[ObjectId] varchar(50) NULL " +
                "END";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();                
            }            
        }

        public static void AddSQLTimestampColumn(string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS " +
                "WHERE TABLE_NAME = '" + tableName + "' AND COLUMN_NAME = 'Timestamp') " +
                "BEGIN " +
                "ALTER TABLE [dbo].[" + tableName + "] ADD " +
                "[Timestamp] Timestamp NULL " +
                "END";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void AddTransferDBTSAttribute(string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC sys.sp_addextendedproperty @name = N'TransferDBTS',  " +
                "@value = null,  " +
                "@level0type = N'SCHEMA', @level0name = dbo, " +
                "@level1type = N'TABLE',  @level1name = " + tableName + ", " +
                "@level2type = N'COLUMN', @level2name = [Timestamp];";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void AddTransferStraightAcrossAttribute(string tableName, bool result)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXEC sys.sp_addextendedproperty @name = N'TransferStraightAcross',  " +
                "@value = '" + result.ToString() + "',  " +
                "@level0type = N'SCHEMA', @level0name = dbo, " +
                "@level1type = N'TABLE',  @level1name = " + tableName + ", " +
                "@level2type = N'COLUMN', @level2name = [Timestamp];";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void DropTimestampColumn(string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "' AND COLUMN_NAME = 'Timestamp') " +
                "begin " +
                "ALTER TABLE dbo." + tableName + " DROP COLUMN Timestamp " +
                "end";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void SetTransferDBTS(string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_updateextendedproperty " +
                "'TransferDBTS', " +
                "@@DBTS, " +
                "'SCHEMA', 'dbo', " +
                "'TABLE', '" + tableName + "', " +
                "'COLUMN', 'TimeStamp'";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void SetTransferStraightAcrossAttribute(string tableName, bool result)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_updateextendedproperty " +
                "'TransferStraightAcross', '" + result.ToString() + "', " +
                "'SCHEMA', 'dbo', " +
                "'TABLE', '" + tableName + "', " +
                "'COLUMN', 'TimeStamp'";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateSQLObjectIDs(string tableName, string primaryKeyName, int maxUpdateCount)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "prcUpdateObjectId";
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tableName;
            cmd.Parameters.Add("@PrimaryKeyName", SqlDbType.VarChar).Value = primaryKeyName;
            cmd.Parameters.Add("@MaxUpdateCount", SqlDbType.Int).Value = maxUpdateCount;
            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void FixZeroX(string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Update " + tableName + " set ObjectId = substring(objectid, 3, len(objectid)) where substring(ObjectId, 1, 2) = '0x'";
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tableName;            
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }                

        public static void AddSQLDeleteTrigger(string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "prcCreateTrigger";
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tableName;            
            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void DropSQLDeleteTrigger(string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "prcDropTrigger";
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = tableName;
            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void DropObjectId(string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS " +
                "WHERE TABLE_NAME = '" + tableName + "' AND COLUMN_NAME = 'ObjectId') " +
                "BEGIN " +
                "ALTER TABLE [dbo].[" + tableName + "] DROP COLUMN [ObjectId] " +
                "END";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }          
        }

        public static Business.ReportDistribution.Model.ReportDistributionLogEntryCollection GetReportDistributionLogEntryCollectionGTETime(DateTime startTime)
        {
            Business.ReportDistribution.Model.ReportDistributionLogEntryCollection result = new ReportDistribution.Model.ReportDistributionLogEntryCollection();

            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(Business.Mongo.TestServer.LISDatabaseName);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>("ReportDistributionLogEntry");            

            BsonDateTime bsonStartDate = BsonDateTime.Create(startTime);
            MongoCursor cursor = collection.FindAs<BsonDocument>(Query.GTE("Date", bsonStartDate.ToUniversalTime()));            

            foreach (BsonDocument bsonDocument in cursor)
            {
                YellowstonePathology.Business.ReportDistribution.Model.ReportDistributionLogEntry reportDistributionLogEntry = new ReportDistribution.Model.ReportDistributionLogEntry();
                YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, reportDistributionLogEntry);
                result.Add(reportDistributionLogEntry);
            }
            
            return result;
        }

        public static TransferCollection GetTransferCollection()
        {
            TransferCollection result = new TransferCollection();

            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(Business.Mongo.TestServer.SQLTransferDatabasename);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>("Transfer");                        
            MongoCursor cursor = collection.FindAllAs(typeof(BsonDocument)).SetSortOrder(SortBy.Ascending("TableName"));

            foreach (BsonDocument bsonDocument in cursor)
            {
                Transfer transfer = new Transfer();
                YellowstonePathology.Business.Mongo.BSONPropertyWriter.Write(bsonDocument, transfer);
                result.Add(transfer);
            }

            return result;
        }

        public static long GetMongoDocumentCount(string collectionName, string databaseName)
        {
            long result = 0;
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(databaseName);
            MongoCollection mongoCollection = server.Database.GetCollection<BsonDocument>(collectionName);
            result = mongoCollection.Count();
            return result;
        }

        public static int GetMongoIndexCount(string collectionName, string databaseName)
        {
            int result = 0;
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(databaseName);
            MongoCollection mongoCollection = server.Database.GetCollection<BsonDocument>(collectionName);
            GetIndexesResult getIndexesResult = mongoCollection.GetIndexes();
            result = getIndexesResult.Count;
            return result;
        }

        public static long GetExtendedDocumentCount(Transfer transfer)
        {
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(Business.Mongo.TestServer.SQLTransferDatabasename);
            MongoCollection collection = server.Database.GetCollection<BsonDocument>(transfer.TableName);
            long count = collection.Count(Query.And(Query.EQ("HasBeenExtended", true), Query.EQ("ExtendedFrom", transfer.TableName)));            
            return count;
        }

        public static void DropMongoCollection(string tableName)
        {
            YellowstonePathology.Business.Mongo.Server transferServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.SQLTransferDatabasename);
            MongoCollection transferCollection = transferServer.Database.GetCollection<BsonDocument>(tableName);
            transferCollection.Drop();

            YellowstonePathology.Business.Mongo.Server lisServer = new Business.Mongo.TestServer(YellowstonePathology.Business.Mongo.MongoTestServer.LISDatabaseName);
            MongoCollection lisCollection = lisServer.Database.GetCollection<BsonDocument>(tableName.Substring(3));
            lisCollection.Drop();            
        }

        public static void TransferTableToMongo(string tableName, string databaseName, string collectionName, System.ComponentModel.BackgroundWorker backgroundWorker)
        {            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from " + tableName;            
            cmd.CommandType = CommandType.Text;

            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(databaseName);
            MongoCollection mongoCollection = server.Database.GetCollection<BsonDocument>(collectionName);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                int transferredRowCount = 1;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BsonDocument bsonDocument = YellowstonePathology.Business.Mongo.BSONDRBuilder.Build(dr);                        
                        mongoCollection.Insert(bsonDocument);

                        if (transferredRowCount > 100 && transferredRowCount % 1000 == 0)
                        {
                            backgroundWorker.ReportProgress(0, "Transfering " + tableName + ", " + transferredRowCount.ToString() + " rows transferred.");
                        }
                        transferredRowCount += 1;
                    }
                }
            }
        }

        public static void Synchronize(string tableName, string databaseName, string collectionName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from " + tableName + " where [TimeStamp] > " +
                "(SELECT convert(int, ep.value) " +
                "FROM sys.extended_properties AS ep " +
                "INNER JOIN sys.tables AS t ON ep.major_id = t.object_id " +
                "INNER JOIN sys.columns AS c ON ep.major_id = c.object_id AND ep.minor_id = c.column_id " +
                "WHERE class = 1 and t.Name = '" + tableName + "' and c.Name = 'TimeStamp' and ep.Name = 'TransferDBTS')";

            cmd.CommandType = CommandType.Text;

            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(databaseName);            

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BsonDocument bsonDocument = YellowstonePathology.Business.Mongo.BSONDRBuilder.Build(dr);
                        BsonObjectId bsonObjectId = (BsonObjectId)bsonDocument.GetValue("_id");
                        MongoCollection mongoCollection = server.Database.GetCollection<BsonDocument>(collectionName);
                        mongoCollection.Update(Query.EQ("_id", bsonObjectId),
                            Update.Replace(bsonDocument), 
                            UpdateFlags.Upsert);
                    }
                }
            }
        }

        public static void DeletePSOIHCollections()
        {
            List<string> psoihList = GetPSOInheritedCollectionNames();
            foreach (string s in psoihList)
            {
                YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(Business.Mongo.TestServer.LISDatabaseName);            
                MongoCollection mongoCollection = server.Database.GetCollection<BsonDocument>(s);
                mongoCollection.Drop();
            }
        }

        public static List<string> GetPSOInheritedCollectionNames()
        {
            List<string> result = new List<string>();
            string assemblyName = @"C:\SVN\LIS\Business\bin\Debug\BusinessObjects.dll";
            Assembly assembly = Assembly.LoadFile(assemblyName);
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                object[] customAttributes = type.GetCustomAttributes(typeof(YellowstonePathology.Business.Persistence.PersistentClass), false);
                if (customAttributes.Length > 0)
                {
                    foreach (object o in customAttributes)
                    {
                        if (o is YellowstonePathology.Business.Persistence.PersistentClass)
                        {
                            YellowstonePathology.Business.Persistence.PersistentClass persistentClass = (YellowstonePathology.Business.Persistence.PersistentClass)o;
                            if (string.IsNullOrEmpty(persistentClass.StorageName) == false)
                            {
                                if (persistentClass.BaseStorageName == "tblPanelSetOrder" && string.IsNullOrEmpty(persistentClass.StorageName) == false)
                                {
                                    string collectionName = persistentClass.StorageName.Substring(3);
                                    result.Add(collectionName);
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }       

        public static void BuildIndexes(string tableName, string collectionName, string databaseName)
        {
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(databaseName);
            MongoCollection mongoCollection = server.Database.GetCollection<BsonDocument>(collectionName);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT col.name " +
                "FROM sys.indexes ind " +
                "INNER JOIN sys.index_columns ic ON  ind.object_id = ic.object_id and ind.index_id = ic.index_id " +
                "INNER JOIN sys.columns col ON ic.object_id = col.object_id and ic.column_id = col.column_id " +
                "INNER JOIN sys.tables t ON ind.object_id = t.object_id " +
                "WHERE t.Name = '" + tableName + "'";

            cmd.CommandType = CommandType.Text;                        

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        mongoCollection.CreateIndex(dr.GetString(0));
                    }
                }
            }
        }

        public static void ExtendDocuments(Transfer transfer)
        {
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(Business.Mongo.TestServer.LISDatabaseName);            
            MongoCollection mongoCollection = server.Database.GetCollection<BsonDocument>(transfer.BaseTableName.Substring(3));

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from " + transfer.BaseTableName + " btn join " + transfer.TableName + " dtn on btn." + transfer.PrimaryKeyName + " = dtn." + transfer.PrimaryKeyName;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BsonDocument bsonDocument = YellowstonePathology.Business.Mongo.BSONDRBuilder.Build(dr);
                        bsonDocument.Add("ExtendedFrom", transfer.TableName);
                        bsonDocument.Add("HasBeenExtended", true);
                        mongoCollection.Save(bsonDocument);
                    }
                }
            }
        }

        public static YellowstonePathology.Business.Test.AccessionOrder GetAccessionOrderByMasterAccessionNo(string masterAccessionNo)
        {            
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(Business.Mongo.TestServer.LISDatabaseName);
            MongoCollection mongoCollection = server.Database.GetCollection<BsonDocument>("AccessionOrder");
            BsonDocument bsonDocument = mongoCollection.FindOneAs<BsonDocument>(Query.EQ("MasterAccessionNo", BsonValue.Create(masterAccessionNo)));
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)YellowstonePathology.Business.Mongo.BSONObjectBuilder.Build(bsonDocument, typeof(YellowstonePathology.Business.Test.AccessionOrder));
            return accessionOrder;
        }

        public static void ExtendCytologyDocuments()
        {
            YellowstonePathology.Business.Mongo.Server server = new Business.Mongo.TestServer(Business.Mongo.TestServer.LISDatabaseName);            
            MongoCollection mongoCollection = server.Database.GetCollection<BsonDocument>("PanelSetOrder");

            SqlCommand cmdPSO = new SqlCommand();
            cmdPSO.CommandText = "Select * from tblPanelSetOrder pso join tblPanelSetOrderCytology psoc on pso.ReportNo = psoc.ReportNo";
            cmdPSO.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmdPSO.Connection = cn;
                using (SqlDataReader dr = cmdPSO.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BsonDocument bsonPSO = YellowstonePathology.Business.Mongo.BSONDRBuilder.Build(dr);
                        bsonPSO.Add("ExtendedFrom", "PanelSetOrderCytology");
                        bsonPSO.Add("HasBeenExtended", true);
                        string reportNo = bsonPSO.GetValue("ReportNo").AsString;
                        WritePODocument(reportNo, bsonPSO);
                        mongoCollection.Save(bsonPSO);                        
                    }
                }
            }
        }

        private static void WritePODocument(string reportNo, BsonDocument bsonPSO)
        {
            SqlCommand cmdPO = new SqlCommand();
            cmdPO.CommandText = "Select * from tblPanelOrder po join tblPanelOrderCytology poc on po.PanelOrderId = poc.PanelOrderId where po.ReportNo = '" + reportNo + "'";
            cmdPO.CommandType = CommandType.Text;

            BsonArray poArray = new BsonArray();

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmdPO.Connection = cn;
                using (SqlDataReader dr = cmdPO.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BsonDocument bsonPO = YellowstonePathology.Business.Mongo.BSONDRBuilder.Build(dr);
                        bsonPO.Add("ExtendedFrom", "PanelOrderCytology");
                        bsonPO.Add("HasBeenExtended", true);
                        poArray.Add(bsonPO);
                    }
                }
            }

            bsonPSO.Add("PanelOrderCollection", poArray);
        }
    }
}
