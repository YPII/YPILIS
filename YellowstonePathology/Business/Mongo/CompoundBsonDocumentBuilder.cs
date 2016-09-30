using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Mongo
{
    public class CompoundBsonDocumentBuilder
    {                
        public static BsonDocument Build(object o)
        {
            BsonDocument document = BSONBuilder.Build(o);
            HandlePersistentChildCollections(o, document);                              
            return document;
        }

        private static void HandlePersistentChildCollections(object parentObject, BsonDocument parentDocument)
        {
            Type parentType = parentObject.GetType();
            List<PropertyInfo> childCollectionProperties = parentType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();

            foreach (PropertyInfo propertyInfo in childCollectionProperties)
            {
                IList childObjectCollection = (IList)propertyInfo.GetValue(parentObject, null);
                BsonArray array = new BsonArray();

                for (int i = 0; i < childObjectCollection.Count; i++)
                {
                    object childObject = childObjectCollection[i];
                    BsonDocument childDocument = BSONBuilder.Build(childObject);
                    array.Add(childDocument);                    

                    HandlePersistentChildCollections(childObject, childDocument);                    
                }

                parentDocument.Add(propertyInfo.Name, array);   
            }
        }
    }
}
