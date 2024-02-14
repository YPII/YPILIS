using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace YellowstonePathology.Business
{
    public class ObjectXMLBuilder
    {
        public ObjectXMLBuilder()
        {
            
        }

        public object Build(Type type, XElement xElement)
        {                        
            object result = this.BuildObject(type, xElement);
            return result;
        }

        private object BuildObject(Type type, XElement xElement)
        {
            object result = null;            
            result = Activator.CreateInstance(type);
            YellowstonePathology.Business.Persistence.XmlPropertyWriter rootXmlPropertyWriter = new Persistence.XmlPropertyWriter(xElement, result);
            rootXmlPropertyWriter.Write();
            this.HandleChildCollections(xElement, result);
            return result;            
        }

        private void HandleChildCollections(XElement document, object parentObject)
        {
            IEnumerable<XNode> xNodes = document.Nodes();
            foreach (XElement element in xNodes)
            {
                if (element.Attribute("ObjectType") != null)
                {
                    if (element.Attribute("ObjectType").Value == "Collection")
                    {
                        string assemblyQualifiedClassNameCollection = element.Attribute("AssemblyQualifiedClassName").Value;
                        Type collectionType = Type.GetType(assemblyQualifiedClassNameCollection);
                        System.Collections.IList childObjectCollection = (System.Collections.IList)Activator.CreateInstance(collectionType);

                        IEnumerable<XNode> childNodes = element.Nodes();
                        foreach (XElement childElement in childNodes)
                        {
                            string assemblyQualifiedClassNameChild = childElement.Attribute("AssemblyQualifiedClassName").Value;
                            Type childType = Type.GetType(assemblyQualifiedClassNameChild);
                            object childObject = Activator.CreateInstance(childType);
                            YellowstonePathology.Business.Persistence.XmlPropertyWriter rootXmlPropertyWriter = new Persistence.XmlPropertyWriter(childElement, childObject);
                            rootXmlPropertyWriter.Write();
                            childObjectCollection.Add(childObject);
                        }

                        PropertyInfo collectionPropertyInfo = parentObject.GetType().GetProperty(element.Name.ToString());
                        collectionPropertyInfo.SetValue(parentObject, childObjectCollection, null);   
                    }
                }
            }            
        }  
    }
}
