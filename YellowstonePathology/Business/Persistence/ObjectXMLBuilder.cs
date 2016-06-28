using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Persistence
{
    public class ObjectXMLBuilder
    {
        public ObjectXMLBuilder()
        {
            
        }        

        public object Build(XElement element)
        {
            object result = null;
            result = this.BuildObject(element);
            this.BuildChildElements(result, element);
            return result;
        }

        public void BuildChildElements(object parentObject, XElement parentElement)
        {                        
            IEnumerable<XElement> childElements = parentElement.Elements();
            foreach (XElement childElement in childElements)
            {
                if (this.HasBuildAttribute(childElement) == true)
                {
                    object childObject = null;
                    BuildTypeEnum buildType = (BuildTypeEnum)Enum.Parse(typeof(BuildTypeEnum), childElement.Attribute("BuildType").Value);
                    switch (buildType)
                    {
                        case BuildTypeEnum.Object:
                            childObject = this.BuildObject(childElement);                            
                            this.BuildChildElements(childObject, childElement);
                            break;
                        case BuildTypeEnum.Collection:
                            childObject = this.BuildCollection(childElement);
                            break;
                    }

                    PropertyInfo propertyInfo = parentObject.GetType().GetProperty(childElement.Name.ToString());
                    propertyInfo.SetValue(parentObject, childObject, null);
                }
            }            
        }

        private object BuildObject(XElement xElement)
        {
            string assemblyQualifiedTypeName = xElement.Attribute("AssemblyQualifiedTypeName").Value;
            Type objectType = Type.GetType(assemblyQualifiedTypeName);

            object result = Activator.CreateInstance(objectType);

            YellowstonePathology.Business.Persistence.XmlPropertyWriter rootXmlPropertyWriter = new XmlPropertyWriter(xElement, result);
            rootXmlPropertyWriter.Write();

            return result;
        }

        private object BuildCollection(XElement xElement)
        {
            string assemblyQualifiedTypeName = xElement.Attribute("AssemblyQualifiedTypeName").Value;
            Type objectType = Type.GetType(assemblyQualifiedTypeName);
            Type itemType = objectType.BaseType.GetGenericArguments()[0];

            object result = Activator.CreateInstance(objectType);
            IList castedResult = (IList)result;                                    
            
            IEnumerable<XElement> childElements = xElement.Elements();
            foreach (XElement childElement in childElements)
            {
                object childObject = this.BuildObject(childElement);
                castedResult.Add(childObject);
                this.BuildChildElements(childObject, childElement);
            }                        

            return result;
        }

        private bool HasBuildAttribute(XElement xElement)
        {
            bool result = false;
            IEnumerable<XAttribute> attributes = xElement.Attributes();
            foreach (XAttribute xAttribtue in attributes)
            {
                if (xAttribtue.Name == "BuildType")
                {
                    result = true;
                }
            }
            return result;
        }

        /*
        private void HandleChildCollections(Type type, object obj, XElement xElement)
        {
            List<PropertyInfo> childCollections = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();
            foreach (PropertyInfo propertyInfo in childCollections)
            {
                IList childObjectCollection = (IList)Activator.CreateInstance(propertyInfo.PropertyType);
                Type baseType = propertyInfo.PropertyType.BaseType;
                Type genericType = baseType.GetGenericArguments()[0];
                IEnumerable iEnumerable = xElement.Elements(genericType.Name);

                foreach (XElement collectionElement in iEnumerable)
                {
                    object childObject = Activator.CreateInstance(genericType);
                    XmlPropertyWriter childCollectionPropertyWriter = new XmlPropertyWriter(collectionElement, childObject);
                    childCollectionPropertyWriter.Write();
                    childObjectCollection.Add(childObject);
                    this.HandleChildCollections(genericType, childObject, collectionElement);
                }

                propertyInfo.SetValue(obj, childObjectCollection, null);                
            }            
        }
        */
    }
}
