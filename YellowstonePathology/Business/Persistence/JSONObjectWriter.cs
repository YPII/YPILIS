using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace YellowstonePathology.Business.Persistence
{
    public class JSONObjectWriter
    {
        public static void WriteIndented(StringBuilder result, object objectToWrite, int indentCount)
        {
            JSONWriter.WriteIndented(result, objectToWrite, indentCount);
            //HandlePersistentChildCollections(objectToWrite, oString, null);
        }

        public static String Write(object objectToWrite)
        {
            StringBuilder oString = new StringBuilder();            
            Type objectType = objectToWrite.GetType();
            oString.Append(JSONWriter.Write(objectToWrite));
            HandlePersistentChildCollections(objectToWrite, oString, null);            
            return oString.ToString();
        }                  

        public static string WriteV2(StringBuilder result, object objectToWrite)
        {            
            result.Append("{");
            JSONWriter.WritProperties(result, objectToWrite);

            Type parentObjectType = objectToWrite.GetType();

            List<PropertyInfo> childCollectionProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();
            for (int i = 0; i < childCollectionProperties.Count; i++)
            {
                IList childCollectionObject = (IList)childCollectionProperties[i].GetValue(objectToWrite, null);

                if(i == 0)
                {
                    result.Append(", ");
                }

                result.Append("\"" + childCollectionObject.GetType().Name + "\":");

                if (childCollectionObject.GetType().Name == "PanelOrderCollection")
                {
                    string xx = result.ToString();
                }

                if (childCollectionObject.Count != 0)
                {
                    result.Append("[");                                
                    for (int j = 0; j < childCollectionObject.Count; j++)
                    {
                        object collectionItem = childCollectionObject[j];
                        WriteV2(result, collectionItem);

                        if (j != childCollectionObject.Count - 1)
                        {
                            result.Append(", ");
                        }
                    }
                    result.Append("]");
                }
                else
                {
                    result.Append("null");
                }                

                if(i != childCollectionProperties.Count - 1)
                {
                    result.Append(",");
                }
            }
            
            result.Append("}");
            return result.ToString();
        }
       
        private static void HandlePersistentChildCollections(object parentObject, StringBuilder parentStringBuilder, object previousObject)
        {
            if (parentObject != null)
            {
                Type parentObjectType = parentObject.GetType();

                List<PropertyInfo> childCollectionProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();
                for (int j=0; j<childCollectionProperties.Count; j++)
                {                    
                    IList childCollectionObject = (IList)childCollectionProperties[j].GetValue(parentObject, null);

                    string lastCharacters = parentStringBuilder.ToString().Substring(parentStringBuilder.Length - 50);
                    string previousObjectName = null;
                    if(previousObject != null)
                    {
                        previousObjectName = previousObject.GetType().Name;
                    }

                    Console.WriteLine(childCollectionObject.GetType().Name + ", " + previousObjectName + ":  " + lastCharacters);

                    parentStringBuilder.Insert(parentStringBuilder.Length - 1, "*" + childCollectionObject.GetType().Name + "*");
                    StringBuilder collectionStringBuilder = new StringBuilder(", \"" + childCollectionObject.GetType().Name + "\":[");                     

                    for (int i = 0; i < childCollectionObject.Count; i++)
                    {                        
                        object collectionItem = childCollectionObject[i];
                        StringBuilder childStringBuilder = new StringBuilder(JSONWriter.Write(collectionItem));                        
                        HandlePersistentChildCollections(collectionItem, childStringBuilder, childCollectionObject);
                        collectionStringBuilder.Append(childStringBuilder);

                        if (i < childCollectionObject.Count - 1)
                        {
                            collectionStringBuilder.Append(", ");
                        }                         
                    }

                    if(j == childCollectionObject.Count - 1)
                    {
                        collectionStringBuilder.Append("]");                        
                    }
                    else
                    {
                        collectionStringBuilder.Append("], *NEXTCOLLECTION*");
                    }

                    parentStringBuilder.Replace("*" + childCollectionObject.GetType().Name + "*", collectionStringBuilder.ToString());
                }                
            }
        }                                                
    }
}
