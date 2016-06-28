using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.UI.Mongo
{
    public class ClassHelper
    {
        public static List<string> GetPanelSetOrderDerivedTableNames()
        {
            List<string> result = new List<string>();

            Assembly assembly = Assembly.LoadFile(@"C:\SVN\LIS\Business\bin\Debug\BusinessObjects.dll");
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                object[] customAttributes = type.GetCustomAttributes(typeof(YellowstonePathology.Business.Persistence.PersistentClass), false);
                if (customAttributes.Length > 0)
                {
                    foreach (object o in customAttributes)
                    {
                        if (o is YellowstonePathology.Business.Persistence.PersistentClass)
                        {
                            YellowstonePathology.Business.Persistence.PersistentClass persistentClass = (YellowstonePathology.Business.Persistence.PersistentClass)o;
                            if (string.IsNullOrEmpty(persistentClass.StorageName) == false &&  
                                persistentClass.HasPersistentBaseClass == true && 
                                persistentClass.BaseStorageName == "tblPanelSetOrder")
                            {                                
                                result.Add(persistentClass.StorageName);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
