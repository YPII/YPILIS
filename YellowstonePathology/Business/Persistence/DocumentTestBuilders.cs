using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;

namespace YellowstonePathology.Business.Persistence
{
    public class DocumentTestBuilders : Document
    {
        public DocumentTestBuilders(object v, object c)
        {
            this.m_Value = v;
            this.m_Clone = c;
        }
        public override bool IsDirty()
        {
            bool result = false;
            YellowstonePathology.Business.Persistence.SqlCommandSubmitter sqlCommandSubmitter = this.GetSqlCommands(this.m_Value);
            if (sqlCommandSubmitter.HasChanges() == true)
            {
                sqlCommandSubmitter.LogCommands();
                result = true;
            }
            return result;
        }

        public bool Compare()
        {
            bool result = true;
            if(this.m_Value != null && this.m_Clone != null)
            {
                Type type = this.m_Value.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if(property.Name == "Item")
                    {
                        continue;
                    }

                    if (property.PropertyType.FullName.Contains("System") == false || 
                        property.PropertyType.FullName.Contains("Collection") == true || property.PropertyType.FullName.Contains("List") == true)
                    {
                        if (property.PropertyType.FullName.Contains("Collection") == false && property.PropertyType.FullName.Contains("List") == false)
                        {
                            object object1 = property.GetValue(this.m_Value, null);
                            object object2 = property.GetValue(this.m_Clone, null);
                            DocumentTestBuilders dtb = new DocumentTestBuilders(object1, object2);
                            result = dtb.Compare();
                            if (result == false)
                                break;
                        }
                        else
                        {
                            this.HandleCollection(property, this.m_Value, this.m_Clone);
                        }
                    }
                    else
                    {
                        PersistenceHelper persistenceHelper = new PersistenceHelper();
                        result = PersistenceHelper.ArePropertiesEqual(property, this.m_Value, this.m_Clone);
                        if (result == false)
                            break;
                    }
                }
            }
            else
            {
                result = this.CheckNulls(this.m_Value, this.m_Clone);
            }
            return result;
        }

        private bool HandleCollection(PropertyInfo property, object object1, object object2)
        {
            bool result = true;

            if(property.Name == "ValidationErrors")
            {
                return result;
            }

            IList collectionObjects1 = (IList)property.GetValue(object1, null);
            IList collectionObjects2 = (IList)property.GetValue(object2, null);

            if (collectionObjects1 == null || collectionObjects2 == null)
            {
                result = CheckNulls(collectionObjects1, collectionObjects2);
            }
            else
            {
                if (collectionObjects1.Count != collectionObjects2.Count)
                {
                    result = false;
                }
                else
                {
                    for (int idx = 0; idx < collectionObjects1.Count; idx++)
                    {
                        DocumentTestBuilders dtb = new DocumentTestBuilders(collectionObjects1[idx], collectionObjects2[idx]);
                        result = dtb.Compare();
                        if (result == false)
                            break;
                    }
                }
            }
            return result;
        }

        private bool CheckNulls(object object1, object object2)
        {
            bool result = true;

            if (object1 == null && object2 != null)
                result = false;
            else if (object1 != null && object2 == null)
                result = false;

            return result;
        }
    }
}
