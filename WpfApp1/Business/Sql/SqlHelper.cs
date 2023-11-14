using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Sql
{
    public class SQLHelper
    {
        public static void GetSaveCommandText(Type type, object domainObject, RowOperationTypeEnum rowOperationType, List<string> sqlStatements)
        {
            switch (rowOperationType)
            {
                case RowOperationTypeEnum.Insert:
                    sqlStatements.Add(GetInsertCommand(type, domainObject));
                    MethodInfo method = type.GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
                    Object cloneObjectValue = method.Invoke(domainObject, new object[0]);
                    PropertyInfo domainObjectCloneProperty = type.GetProperty("Clone");
                    domainObjectCloneProperty.SetValue(domainObject, cloneObjectValue);
                    break;
                case RowOperationTypeEnum.Update:                    
                    //if (changedProperties.Count == 0) return;
                    //sqlStatements.Add(GetUpdateCommand(type, domainObject, changedProperties));
                    break;
                case RowOperationTypeEnum.Delete:
                    sqlStatements.Add(GetDeleteCommand(type, domainObject));
                    break;
                default:
                    throw new Exception("This row operation type is not handled.");
            }
        }        

        public static bool AreStringsEqual(Object originalValue, Object currentValue)
        {
            bool result = false;
            string ov = (string)originalValue;
            string cc = (string)currentValue;
            if (string.Compare(ov, cc) == 0) result = true;
            return result;
        }

        public static bool AreJObjectsEqual(Object originalValue, Object currentValue)
        {
            bool result = false;
            JObject ov = (JObject)originalValue;
            JObject cc = (JObject)currentValue;
            if (JToken.DeepEquals(ov, cc) == true) result = true;
            return result;
        }

        public static bool AreIntsEqual(Object originalValue, Object currentValue)
        {
            bool result = false;
            int ov = (int)originalValue;
            int cc = (int)currentValue;
            if (ov == cc) result = true;
            return result;
        }

        public static bool AreDoublesEqual(Object originalValue, Object currentValue)
        {
            bool result = false;
            double ov = (double)originalValue;
            double cc = (double)currentValue;
            if (ov == cc) result = true;
            return result;
        }

        public static bool AreNullableDatesEqual(Object originalValue, Object currentValue)
        {
            bool result = false;
            Nullable<DateTime> ov = (Nullable<DateTime>)originalValue;
            Nullable<DateTime> cc = (Nullable<DateTime>)currentValue;
            if (Nullable.Compare<DateTime>(ov, cc) == 0) result = true;
            return result;
        }

        public static bool AreNullableIntsEqual(Object originalValue, Object currentValue)
        {
            bool result = false;
            Nullable<Int32> ov = (Nullable<Int32>)originalValue;
            Nullable<Int32> cc = (Nullable<Int32>)currentValue;
            if (Nullable.Compare<Int32>(ov, cc) == 0) result = true;
            return result;
        }

        public static bool AreNullableDoublesEqual(Object originalValue, Object currentValue)
        {
            bool result = false;
            Nullable<Double> ov = (Nullable<Double>)originalValue;
            Nullable<Double> cc = (Nullable<Double>)currentValue;
            if (Nullable.Compare<Double>(ov, cc) == 0) result = true;
            return result;
        }

        public static bool AreDatesEqual(Object originalValue, Object currentValue)
        {
            bool result = false;
            DateTime ov = (DateTime)originalValue;
            DateTime cc = (DateTime)currentValue;
            if (DateTime.Compare(ov, cc) == 0) result = true;
            return result;
        }

        public static bool AreBooleansEqual(Object originalValue, Object currentValue)
        {
            bool result = false;
            bool ov = (bool)originalValue;
            bool cc = (bool)currentValue;
            if (ov == cc) result = true;
            return result;
        }

        public static string GetInsertCommand(Type type, object domainObject)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)type.GetCustomAttribute(typeof(PersistentClass));
            string commandText = "Insert " + persistentClassAttribute.StorageName + " ([FIELDLIST]) values ([VALUELIST]);";
            commandText = commandText.Replace("[FIELDLIST]", GetSqlInsertFieldList(type));
            commandText = commandText.Replace("[VALUELIST]", GetSqlInsertValueList(type, domainObject));
            return commandText;
        }

        public static string GetUpdateCommand(Type type, object domainObject, List<PropertyInfo> changeProperties)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)type.GetCustomAttribute(typeof(PersistentClass));
            PropertyInfo primaryKeyProperty = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            PersistentProperty persistentProperty = (PersistentProperty)primaryKeyProperty.GetCustomAttribute(typeof(PersistentProperty));

            string commandText = "Update " + persistentClassAttribute.StorageName + " set [FIELDLIST] where " + primaryKeyProperty.Name + " = '" + primaryKeyProperty.GetValue(domainObject) + "';";
            commandText = commandText.Replace("[FIELDLIST]", GetSqlUpdateFieldList(type, domainObject, changeProperties));
            return commandText;
        }

        public static string GetDeleteCommand(Type type, object domainObject)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)type.GetCustomAttribute(typeof(PersistentClass));
            PropertyInfo primaryKeyProperty = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            PersistentProperty persistentProperty = (PersistentProperty)primaryKeyProperty.GetCustomAttribute(typeof(PersistentProperty));
            string commandText = "Delete from " + persistentClassAttribute.StorageName + " where " + primaryKeyProperty.Name + " = '" + primaryKeyProperty.GetValue(domainObject) + "';";
            return commandText;
        }

        public static string GetSqlInsertFieldList(Type type)
        {
            string result = "";
            List<PropertyInfo> propertyList = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty))).ToList();
            PropertyInfo primaryKeyProperty = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            PersistentPrimaryKeyProperty persistentPrimaryKeyProperty = (PersistentPrimaryKeyProperty)primaryKeyProperty.GetCustomAttribute(typeof(PersistentPrimaryKeyProperty));

            foreach (PropertyInfo property in propertyList)
            {
                PersistentProperty persistentProperty = (PersistentProperty)property.GetCustomAttribute(typeof(PersistentProperty));                
                result = result + "`" + property.Name + "`, ";                
            }
            result = result.Substring(0, result.Length - 2);
            return result;
        }

        public static string GetSqlUpdateFieldList(Type type, object domainObject, List<PropertyInfo> propertyList)
        {
            string result = "";
            PropertyInfo primaryKeyProperty = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            foreach (PropertyInfo property in propertyList)
            {
                if (property.Name != primaryKeyProperty.Name)
                {
                    PersistentProperty persistentProperty = (PersistentProperty)property.GetCustomAttribute(typeof(PersistentProperty));
                    switch (property.PropertyType.Name)
                    {
                        case "Nullable`1":
                            Type nullableType = property.PropertyType.GetGenericArguments()[0];
                            switch (nullableType.Name)
                            {
                                case "Int32":
                                    if (property.GetValue(domainObject) == null)
                                    {
                                        result = result + "`" + property.Name + "`=null, ";
                                    }
                                    else
                                    {
                                        Int32 nullableInt32 = Int32.Parse(property.GetValue(domainObject).ToString());
                                        result = result + "`" + property.Name + "`= " + nullableInt32 + ", ";
                                    }
                                    break;
                                case "DateTime":
                                    if (property.GetValue(domainObject) == null)
                                    {
                                        result = result + "`" + property.Name + "`=null, ";
                                    }
                                    else
                                    {
                                        DateTime nullableDateTime = DateTime.Parse(property.GetValue(domainObject).ToString());
                                        result = result + "`" + property.Name + "`='" + nullableDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                    }
                                    break;
                                case "Double":
                                    if (property.GetValue(domainObject) == null)
                                    {
                                        result = result + "`" + property.Name + "`=null, ";
                                    }
                                    else
                                    {
                                        Double nullableDouble = Double.Parse(property.GetValue(domainObject).ToString());
                                        result = result + "`" + property.Name + "`= " + nullableDouble + ", ";
                                    }
                                    break;
                                default:
                                    throw new Exception("Nullable type not implemented yet.");
                            }

                            break;
                        case "Boolean":
                            result = result + "`" + property.Name + "` = '" + Convert.ToInt32(property.GetValue(domainObject)) + "', ";
                            break;
                        case "DateTime":
                            DateTime dateTime = (DateTime)property.GetValue(domainObject);
                            result = result + "`" + property.Name + "` = '" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                            break;
                        default:
                            if (property.GetValue(domainObject) == null || property.GetValue(domainObject).ToString() == "")
                            {
                                result = result + "`" + property.Name + "`=null, ";
                            }
                            else
                            {
                                result = result + "`" + property.Name + "` = '" + property.GetValue(domainObject).ToString().Replace("'", "''") + "', ";
                            }
                            break;
                    }
                }
            }
            result = result.Substring(0, result.Length - 2);
            return result;
        }

        public static string GetSqlInsertValueList(Type type, object domainObject)
        {
            StringBuilder result = new StringBuilder();
            List<PropertyInfo> propertyList = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty))).ToList();
            PropertyInfo primaryKeyProperty = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            PersistentPrimaryKeyProperty persistentPrimaryKeyProperty = (PersistentPrimaryKeyProperty)primaryKeyProperty.GetCustomAttribute(typeof(PersistentPrimaryKeyProperty));
            foreach (PropertyInfo property in propertyList)
            {                
                switch (property.PropertyType.Name)
                {
                    case "Nullable`1":
                        Type nullableType = property.PropertyType.GetGenericArguments()[0];
                        switch (nullableType.Name)
                        {
                            case "Int32":
                                if (property.GetValue(domainObject) == null)
                                {
                                    result.Append("null, ");
                                }
                                else
                                {
                                    Int32 nullableInt32 = Int32.Parse(property.GetValue(domainObject).ToString());
                                    result.Append(property.GetValue(domainObject).ToString() + ", ");
                                }
                                break;
                            case "DateTime":
                                if (property.GetValue(domainObject) == null)
                                {
                                    result.Append("null, ");
                                }
                                else
                                {
                                    DateTime date = DateTime.Parse(property.GetValue(domainObject).ToString());
                                    result.Append("'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                                }
                                break;
                            case "Double":
                                if (property.GetValue(domainObject) == null)
                                {
                                    result.Append("null, ");
                                }
                                else
                                {
                                    Double dble = Convert.ToDouble(property.GetValue(domainObject).ToString());
                                    result.Append($"{dble.ToString()}, ");
                                }
                                break;
                        }
                        break;
                    case "Boolean":
                        result.Append("'" + Convert.ToInt32(property.GetValue(domainObject)) + "', ");
                        break;
                    default:
                        if (property.GetValue(domainObject) == null || property.GetValue(domainObject).ToString() == "")
                        {
                            result.Append("null, ");
                        }
                        else
                        {
                            result.Append("'" + property.GetValue(domainObject).ToString().Replace("'", "''") + "', ");
                        }
                        break;
                }
            }
            result = result.Remove(result.Length - 2, 2);
            return result.ToString();
        }
    }
}
