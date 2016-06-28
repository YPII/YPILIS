using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace YellowstonePathology.Business.Persistence
{
    public class JSONObjectStreamWriter
    {                
        public static string Write(StringWriter result, object objectToWrite)
        {            
            result.WriteLine("{");
            JSONStreamWriter.WritProperties(result, objectToWrite);

            Type parentObjectType = objectToWrite.GetType();

            List<PropertyInfo> childCollectionProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();
            for (int i = 0; i < childCollectionProperties.Count; i++)
            {
                IList childCollectionObject = (IList)childCollectionProperties[i].GetValue(objectToWrite, null);

                if(i == 0)
                {
                    result.Write(", ");
                }

                result.Write("\"" + childCollectionObject.GetType().Name + "\":");                

                if (childCollectionObject.Count != 0)
                {
                    result.WriteLine("[");                                
                    for (int j = 0; j < childCollectionObject.Count; j++)
                    {
                        object collectionItem = childCollectionObject[j];
                        Write(result, collectionItem);

                        if (j != childCollectionObject.Count - 1)
                        {
                            result.Write(", ");
                        }
                    }
                    result.Write("]");
                }
                else
                {
                    result.Write("null");
                }                

                if(i != childCollectionProperties.Count - 1)
                {
                    result.Write(",");
                }
            }
            
            result.Write("}");
            return result.ToString();
        }              
    }
}
