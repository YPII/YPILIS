using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Mongo
{
    public class BSONDRBuilder
    {         
        public static BsonDocument Build(SqlDataReader dr)
        {            
            BsonDocument bsonDocument = new BsonDocument();                        

            for(int i=0; i<dr.FieldCount; i++)
            {
                Type propertyType = dr.GetFieldType(i);
                if (dr.GetName(i) == "ObjectId")
                {
                    WriteObjectId(dr, i, bsonDocument);
                }
                else if (dr.GetName(i) == "Timestamp")
                {
                    WriteTimestamp(dr, i, bsonDocument);
                }
                else if (dr.GetName(i) == "ReportNo")
                {
                    WriteReportNo(dr, i, bsonDocument);
                }
                else if (dr.GetName(i) == "PanelOrderId")
                {
                    WriteReportNo(dr, i, bsonDocument);
                }
                else if (propertyType == typeof(string))
                {
                    WriteString(dr, i, bsonDocument);
                }
                else if (propertyType == typeof(int))
                {
                    WriteInt(dr, i, bsonDocument);
                }
                else if (propertyType == typeof(double))
                {
                    WriteDouble(dr, i, bsonDocument);
                }
                else if (propertyType == typeof(decimal))
                {
                    WriteDouble(dr, i, bsonDocument);
                }
                else if (propertyType == typeof(Nullable<int>))
                {
                    WriteInt(dr, i, bsonDocument);
                }
                else if (propertyType == typeof(DateTime))
                {
                    WriteDateTime(dr, i, bsonDocument);
                }
                else if (propertyType == typeof(bool))
                {
                    WriteBoolean(dr, i, bsonDocument);
                }
                else if (propertyType == typeof(Nullable<bool>))
                {
                    WriteBoolean(dr, i, bsonDocument);
                }
                else if (propertyType == typeof(Nullable<DateTime>))
                {
                    WriteDateTime(dr, i, bsonDocument);
                }
                else
                {
                    throw new Exception("This Data Type is Not Implemented: " + dr.GetFieldType(i).ToString() + ": " + dr.GetName(i));
                }                
            }

            return bsonDocument;
        }        

        private static void WriteString(SqlDataReader dr, int index, BsonDocument document)
        {
            if (dr.GetValue(index) != DBNull.Value)
            {
                document.Add(dr.GetName(index), dr.GetString(index));
            }
            else
            {
                document.Add(dr.GetName(index), BsonNull.Value);
            }
        }

        private static void WriteInt(SqlDataReader dr, int index, BsonDocument document)
        {
            if (dr.GetValue(index) != DBNull.Value)
            {
                BsonInt32 bsonInt32 = BsonInt32.Create(dr.GetValue(index));
                document.Add(dr.GetName(index), bsonInt32);
            }
            else
            {
                document.Add(dr.GetName(index), BsonNull.Value);
            }
        }

        private static void WriteObjectId(SqlDataReader dr, int index, BsonDocument document)
        {            
            string result = (string)dr.GetValue(index);
            BsonObjectId bsonObjectId = BsonObjectId.Parse(result);
            document.Add("_id", bsonObjectId);            
        }

        private static void WriteTimestamp(SqlDataReader dr, int index, BsonDocument document)
        {
            //do nothing
        }

        private static void WriteReportNo(SqlDataReader dr, int index, BsonDocument document)
        {            
            BsonValue outValue = null;
            bool exists = document.TryGetValue("ReportNo", out outValue);
            if (exists == false)
            {
                WriteString(dr, index, document);
            }
        }

        private static void WritePanelOrderId(SqlDataReader dr, int index, BsonDocument document)
        {
            BsonValue outValue = null;
            bool exists = document.TryGetValue("PanelOrderId", out outValue);
            if (exists == false)
            {
                WriteString(dr, index, document);
            }
        }

        private static void WriteDouble(SqlDataReader dr, int index, BsonDocument document)
        {
            if (dr.GetValue(index) != DBNull.Value)
            {
                BsonDouble bsonDouble = BsonDouble.Create(dr.GetValue(index));
                document.Add(dr.GetName(index), bsonDouble);
            }
            else
            {
                document.Add(dr.GetName(index), BsonNull.Value);
            }
        }
        
        private static void WriteDateTime(SqlDataReader dr, int index, BsonDocument document)
        {
            if (dr.GetValue(index) != DBNull.Value)
            {
                DateTime dateTime = dr.GetDateTime(index);                
                BsonDateTime bsonDateTime = BsonDateTime.Create(dateTime);
                document.Add(dr.GetName(index), bsonDateTime);
            }
            else
            {
                document.Add(dr.GetName(index), BsonNull.Value);
            }
        }

        private static void WriteBoolean(SqlDataReader dr, int index, BsonDocument document)
        {
            if (dr.GetValue(index) != DBNull.Value)
            {
                BsonBoolean bsonBoolean = BsonBoolean.Create(dr.GetBoolean(index));
                document.Add(dr.GetName(index), bsonBoolean);
            }
            else
            {
                document.Add(dr.GetName(index), BsonNull.Value);
            }
        }
    }
}
