using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class PersistenceHelper
    {
        public static string GetTableName(Type type)
        {            
            PersistentClass persistentClassAttribute = (PersistentClass)type.GetCustomAttributes(typeof(PersistentClass), false).Single();
            return persistentClassAttribute.StorageName;
        }

        public static List<PropertyInfo> GetPropertiesToHandle(Type type)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)type.GetCustomAttributes(typeof(PersistentClass), false).Single();

            PropertyInfo[] properties = type.GetProperties()
                .Where(
                    prop => prop.GetCustomAttributes(typeof(PersistentProperty), true)
                        .Any()
                        )
                .ToArray();

            List<PropertyInfo> result = new List<PropertyInfo>();
            foreach (PropertyInfo property in properties)
            {
                if (persistentClassAttribute.HasPersistentBaseClass == true)
                {
                    if (property.DeclaringType == type)
                    {
                        result.Add(property);
                    }
                }
                else
                {
                    result.Add(property);
                }
            }

            return result;
        }

        public static Type GetRootType(object o)
        {
            Type result = o.GetType();
            while (true)
            {
                if (result.BaseType.Name == typeof(object).Name)
                {
                    break;
                }
                else
                {
                    result = result.BaseType;
                }
            }
            return result;
        }

        public static bool ArePersistentPropertiesEqual(object object1, object object2)
        {
            bool result = true;

            if (object1 == null && object2 == null) return true;

            Type type = object1.GetType();
            PropertyInfo keyProperty = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(YellowstonePathology.Business.Persistence.PersistentPrimaryKeyProperty))).Single();

            if (ArePropertiesEqual(keyProperty, object1, object2) == false)
            {
                result = false;
            }
            else
            {                                
                PropertyInfo[] properties = type.GetProperties()
                .Where(
                    prop => prop.GetCustomAttributes(typeof(PersistentProperty), true)                        
                        .Any()
                        )
                .ToArray();

                foreach (PropertyInfo property in properties)
                {
                    if (ArePropertiesEqual(property, object1, object2) == false)
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        public static bool ArePropertiesEqual(PropertyInfo property, object object1, object object2)
        {
            bool result = true;            

            object object1Value = null;
            object object2Value = null;

            if (object1 != null)
            {
                object1Value = property.GetValue(object1, null);
            }
            if (object2 != null)
            {
                object2Value = property.GetValue(object2, null);
            }

            if (object1Value != null)
            {
                if (object1Value.Equals(object2Value) == false)
                {
                    result = false;
                }
            }
            else
            {
                if (object2Value != null)
                {
                    result = false;
                }
            }

            return result;
        }        
    }
}
