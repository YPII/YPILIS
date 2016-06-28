using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;

namespace YellowstonePathology.Business.Persistence
{
    public class JSONDeserializer
    {
        public static object DeserializeJSONCollectionFile(string filePath)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string collectionToCreate = string.Empty;

            using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream(filePath)))
            {
                collectionToCreate = sr.ReadToEnd();
            }

            Dictionary<string, object> collectionDictionary = javaScriptSerializer.DeserializeObject(collectionToCreate) as Dictionary<string, object>;
            string collectionTypeString = collectionDictionary.Keys.First();
            Type collectionType = Type.GetType(collectionTypeString);
            object result = Activator.CreateInstance(collectionType);

            object[] objectArray = collectionDictionary.First().Value as object[];
            foreach (object o in objectArray)
            {
                Dictionary<string, object> objectDictionary = (Dictionary<string, object>)o;
                string objectTypeString = objectDictionary.Keys.First();
                Type objectType = Type.GetType(objectTypeString);
                object objectToAdd = Activator.CreateInstance(objectType);
                Dictionary<string, object> properties = objectDictionary.First().Value as Dictionary<string, object>;
                YellowstonePathology.Business.Persistence.JSONReaderPropertyWriter jsonReaderPropertyWriter = new Persistence.JSONReaderPropertyWriter(objectToAdd, properties);
                jsonReaderPropertyWriter.WriteProperties();
                result.GetType().GetMethod("Add").Invoke(result, new Object[] { objectToAdd });
            }
            return result;
        }
    }
}
