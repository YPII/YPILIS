using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class JSONReaderPropertyWriter
    {
        object m_ObjectToWriteTo;
        Dictionary<string, object> m_PropertyDictionary;
        Type m_ObjectType;

        public JSONReaderPropertyWriter(object objectToWriteTo, Dictionary<string, object> propertyDictionary)
        {
            this.m_ObjectToWriteTo = objectToWriteTo;
            this.m_PropertyDictionary = propertyDictionary;
            this.m_ObjectType = objectToWriteTo.GetType();
        }

        public void WriteProperties()
        {
            PropertyInfo[] properties = this.m_ObjectType.GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty)) || Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).ToArray();
            foreach (PropertyInfo property in properties)
            {
                string key = property.Name;
                object value = null;
                bool valueFound = this.m_PropertyDictionary.TryGetValue(key, out value);
                if (valueFound == true)
                {
                    property.SetValue(this.m_ObjectToWriteTo, value, null);
                }
            }
        }
    }
}
