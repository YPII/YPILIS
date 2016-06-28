using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;

namespace YellowstonePathology.Business.Mongo
{
    public class BSONBuilder
    {                
        public static BsonDocument Build(object o)
        {
            Type objectType = o.GetType();
            BsonDocument bsonDocument = new BsonDocument();

            if(objectType != typeof(YellowstonePathology.Business.Mongo.Transfer))
            {                
                bsonDocument.Add("AssemblyQualifiedName", objectType.AssemblyQualifiedName);   
            }

            PropertyInfo[] properties = objectType.GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(YellowstonePathology.Business.Persistence.PersistentProperty)) || 
                    Attribute.IsDefined(prop, typeof(YellowstonePathology.Business.Persistence.PersistentPrimaryKeyProperty))).ToArray();

            foreach (PropertyInfo property in properties)
            {
                Type dataType = property.PropertyType;
                if (property.Name == "ObjectId")
                {
                    WriteObjectId(property, o, bsonDocument);
                }
                else if (dataType == typeof(string))
                {
                    WriteString(property, o, bsonDocument);
                }
                else if (dataType == typeof(int))
                {
                    WriteInt(property, o, bsonDocument);
                }
                else if (dataType == typeof(Int64))
                {
                    WriteInt64(property, o, bsonDocument);
                }
                else if (dataType == typeof(double))
                {
                    WriteDouble(property, o, bsonDocument);
                }
                else if (dataType == typeof(Nullable<int>))
                {
                    WriteInt(property, o, bsonDocument);
                }
                else if (dataType == typeof(DateTime))
                {
                    WriteDateTime(property, o, bsonDocument);
                }
                else if (dataType == typeof(bool))
                {
                    WriteBoolean(property, o, bsonDocument);
                }
                else if (dataType == typeof(Nullable<bool>))
                {
                    WriteBoolean(property, o, bsonDocument);
                }
                else if (dataType == typeof(Nullable<DateTime>))
                {
                    WriteDateTime(property, o, bsonDocument);
                }
                else if(dataType.IsEnum == true)
                {
                    WriteEnum(property, o, bsonDocument);
                }
                else
                {
                    throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
                }
            }

            return bsonDocument;
        }

        private static void WriteObjectId(PropertyInfo property, object o, BsonDocument document)
        {
            string objectId = property.GetValue(o, null).ToString();
            BsonObjectId bsonObjectId = BsonObjectId.Parse(objectId);
            document.Add("_id", bsonObjectId);         
        }

        private static void WriteString(PropertyInfo property, object o, BsonDocument document)
        {
            if (property.GetValue(o, null) != null)
            {
                document.Add(property.Name, property.GetValue(o, null).ToString());                
            }
            else
            {                
                document.Add(property.Name, BsonNull.Value);
            }
        }

        private static void WriteEnum(PropertyInfo property, object o, BsonDocument document)
        {   
            string enumValue = property.GetValue(o, null).ToString();
            document.Add(property.Name, enumValue);            
        }

        private static void WriteInt(PropertyInfo property, object o, BsonDocument document)
        {
            if (property.GetValue(o, null) != null)
            {
                BsonInt32 bsonInt = BsonInt32.Create(property.GetValue(o, null));
                document.Add(property.Name, bsonInt);
            }
            else
            {
                document.Add(property.Name, BsonNull.Value);
            }
        }

        private static void WriteInt64(PropertyInfo property, object o, BsonDocument document)
        {
            if (property.GetValue(o, null) != null)
            {
                BsonInt64 bsonInt64 = BsonInt64.Create(property.GetValue(o, null));
                document.Add(property.Name, bsonInt64);
            }
            else
            {
                document.Add(property.Name, BsonNull.Value);
            }
        }

        private static void WriteDouble(PropertyInfo property, object o, BsonDocument document)
        {
            if (property.GetValue(o, null) != null)
            {
                BsonDouble bsonDouble = BsonDouble.Create(property.GetValue(o, null));
                document.Add(property.Name, bsonDouble);
            }
            else
            {
                document.Add(property.Name, BsonNull.Value);
            }
        }

        private static void WriteDateTime(PropertyInfo property, object o, BsonDocument document)
        {
            if (property.GetValue(o, null) != null)
            {
                DateTime dateTime = DateTime.Parse(property.GetValue(o, null).ToString());                
                BsonDateTime bsonDateTime = BsonDateTime.Create(dateTime);
                document.Add(property.Name, bsonDateTime);
            }
            else
            {
                document.Add(property.Name, BsonNull.Value);
            }
        }

        private static void WriteBoolean(PropertyInfo property, object o, BsonDocument document)
        {
            if (property.GetValue(o, null) != null)
            {
                BsonBoolean bsonBoolean = BsonBoolean.Create(property.GetValue(o, null));
                document.Add(property.Name, bsonBoolean);
            }
            else
            {
                document.Add(property.Name, BsonNull.Value);
            }
        }
    }
}
