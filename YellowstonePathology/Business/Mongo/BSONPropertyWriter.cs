using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;

namespace YellowstonePathology.Business.Mongo
{
    public class BSONPropertyWriter
    {                
        public static void Write(BsonDocument bsonDocument, object objectToWriteTo)
        {
            Type objectType = objectToWriteTo.GetType();
            PropertyInfo documentIdPropertyInfo = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(YellowstonePathology.Business.Persistence.PersistentDocumentIdProperty))).Single();            

            PropertyInfo[] properties = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(YellowstonePathology.Business.Persistence.PersistentProperty)) 
                || Attribute.IsDefined(prop, typeof(YellowstonePathology.Business.Persistence.PersistentPrimaryKeyProperty))).ToArray();            

            foreach (PropertyInfo property in properties)
            {                
                Type dataType = property.PropertyType;
                if (property.Name == "ObjectId")
                {
                    WriteObjectId(objectToWriteTo, bsonDocument);
                }
                else if (dataType == typeof(string))
                {
                    WriteString(property, objectToWriteTo, bsonDocument);
                }
                else if (dataType == typeof(int))
                {
                    WriteInt(property, objectToWriteTo, bsonDocument);
                }
                else if (dataType == typeof(Int64))
                {
                    WriteInt64(property, objectToWriteTo, bsonDocument);
                }
                else if (dataType == typeof(Nullable<int>))
                {
                    WriteInt(property, objectToWriteTo, bsonDocument);
                }
                else if (dataType == typeof(DateTime))
                {
                    WriteDateTime(property, objectToWriteTo, bsonDocument);
                }
                else if (dataType == typeof(bool))
                {
                    WriteBoolean(property, objectToWriteTo, bsonDocument);
                }
                else if (dataType == typeof(Nullable<bool>))
                {
                    WriteBoolean(property, objectToWriteTo, bsonDocument);
                }
                else if (dataType == typeof(Nullable<DateTime>))
                {
                    WriteDateTime(property, objectToWriteTo, bsonDocument);
                }
                else if (dataType.IsEnum == true)
                {
                    WriteEnum(dataType, property, objectToWriteTo, bsonDocument);
                }
				else if (dataType == typeof(double))
				{
					WriteDouble(property, objectToWriteTo, bsonDocument);
				}
				else
                {
                    throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
                }
            }            
        }

        private static void WriteString(PropertyInfo property, object result, BsonDocument document)
        {
            if (document.GetValue(property.Name) != null)
            {
                BsonValue bsonValue = document.GetValue(property.Name);
                if (bsonValue is BsonNull == false)
                {
                    property.SetValue(result, bsonValue.AsString, null);
                }
            }             
        }

        private static void WriteObjectId(object result, BsonDocument document)
        {            
            PropertyInfo objectIdProperty = result.GetType().GetProperty("ObjectId");
            BsonObjectId bsonObjectId = (BsonObjectId)document.GetValue("_id");
            objectIdProperty.SetValue(result, bsonObjectId.ToString(), null);            
        }        

        private static void WriteInt(PropertyInfo property, object result, BsonDocument document)
        {
            if (document.GetValue(property.Name) != null)
            {
                BsonValue bsonValue = document.GetValue(property.Name);
                if (bsonValue is BsonNull == false)
                {
                    int intValue = bsonValue.AsInt32;
                    property.SetValue(result, intValue, null);
                }
            }
        }

        private static void WriteInt64(PropertyInfo property, object result, BsonDocument document)
        {
            if (document.GetValue(property.Name) != null)
            {
                BsonValue bsonValue = document.GetValue(property.Name);
                if (bsonValue is BsonNull == false)
                {
                    long int64Value = bsonValue.AsInt64;
                    property.SetValue(result, int64Value, null);
                }
            }
        }

        private static void WriteDateTime(PropertyInfo property, object result, BsonDocument document)
        {
            if (document.GetValue(property.Name) != BsonNull.Value)
            {
                BsonDateTime bsonDateTime = (BsonDateTime)document.GetValue(property.Name);                
                DateTime dateTime = bsonDateTime.AsDateTime;
                property.SetValue(result, dateTime, null);                
            }
        }

        private static void WriteBoolean(PropertyInfo property, object result, BsonDocument document)
        {
			BsonBoolean bsonBoolean = document.GetValue(property.Name).AsBoolean;
			property.SetValue(result, bsonBoolean.ToBoolean(), null);
        }

        private static void WriteEnum(Type type, PropertyInfo property, object result, BsonDocument document)
        {
            BsonValue bsonValue = document.GetValue(property.Name);
            object enumObject = Enum.Parse(type, bsonValue.ToString());
            property.SetValue(result, enumObject, null);
        }

		private static void WriteDouble(PropertyInfo property, object result, BsonDocument document)
		{
			BsonDouble bsonDouble = document.GetValue(property.Name).AsDouble;
			property.SetValue(result, bsonDouble.ToDouble(), null);
		}
	}
}
