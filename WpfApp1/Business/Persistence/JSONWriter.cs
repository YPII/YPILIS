using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public static class JSONWriter
    {
        public static void WritProperties(StringBuilder result, object o)
        {            
            PropertyInfo[] properties = o.GetType().GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty)) || Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).ToArray();

            for (int i=0; i<properties.Length - 1; i++)
            {
                PropertyInfo property = properties[i];
                if (property.Name != "JSON")
                {
                    Type dataType = property.PropertyType;
                    if (dataType == typeof(string))
                    {
                        WriteString(result, property, o);
                    }
                    else if (dataType == typeof(int))
                    {
                        WriteNumber(result, property, o);
                    }
                    else if (dataType == typeof(double))
                    {
                        WriteNumber(result, property, o);
                    }
                    else if (dataType == typeof(Nullable<int>))
                    {
                        WriteNumber(result, property, o);
                    }
                    else if (dataType == typeof(DateTime))
                    {
                        WriteDate(result, property, o);
                    }
                    else if (dataType == typeof(bool))
                    {
                        WriteBoolean(result, property, o);
                    }
                    else if (dataType == typeof(Nullable<bool>))
                    {
                        WriteBoolean(result, property, o);
                    }
                    else if (dataType == typeof(Nullable<DateTime>))
                    {
                        WriteDate(result, property, o);
                    }
                    else
                    {
                        throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
                    }
                }
            }

            if (properties.Length != 0)
            {
                result.Replace(",", string.Empty, result.Length - 3, 2);
            }            
        }

        public static string Write(object o)
        {
            Type type = o.GetType();
            StringBuilder result = new StringBuilder();                        
            result.Append("{");
            PropertyInfo[] properties = o.GetType().GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty)) || Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).ToArray();
            
            foreach (PropertyInfo property in properties)
            {                
                if(property.Name != "JSON")
                {
                    Type dataType = property.PropertyType;
                    if (dataType == typeof(string))
                    {
                        WriteString(result, property, o);
                    }
                    else if (dataType == typeof(int))
                    {
                        WriteNumber(result, property, o);
                    }
                    else if (dataType == typeof(double))
                    {
                        WriteNumber(result, property, o);
                    }
                    else if (dataType == typeof(Nullable<int>))
                    {
                        WriteNumber(result, property, o);
                    }
                    else if (dataType == typeof(DateTime))
                    {
                        WriteDate(result, property, o);
                    }
                    else if (dataType == typeof(bool))
                    {
                        WriteBoolean(result, property, o);
                    }
                    else if (dataType == typeof(Nullable<bool>))
                    {
                        WriteBoolean(result, property, o);
                    }
                    else if (dataType == typeof(Nullable<DateTime>))
                    {
                        WriteDate(result, property, o);
                    }
                    else
                    {
                        throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
                    }
                }            
            }
            
            if (properties.Length != 0)
            {
                result.Replace(",", string.Empty, result.Length - 3, 2);                
            }
                     
            
            result.Append("}");                              
            return result.ToString();
        }

        public static void WriteIndented(StringBuilder result, object o, int indentCount)
        {
            int currentIndent = indentCount;
            Type type = o.GetType();
            string qualifiedName = type.AssemblyQualifiedName;
            result.AppendLine();
            JSONIndenter.AddTabs(result, currentIndent++);
            result.Append("{");
            result.AppendLine();
            JSONIndenter.AddTabs(result, currentIndent);
            WriteMetaData(result, qualifiedName);
            result.AppendLine();
            JSONIndenter.AddTabs(result, currentIndent++);
            result.Append("{");
            PropertyInfo[] properties = o.GetType().GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty)) || Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).ToArray();

            foreach (PropertyInfo property in properties)
            {
                result.AppendLine();
                JSONIndenter.AddTabs(result, currentIndent);
                if (property.Name != "JSON")
                {
                    Type dataType = property.PropertyType;
                    if (dataType == typeof(string))
                    {
                        WriteString(result, property, o);
                    }
                    else if (dataType == typeof(int))
                    {
                        WriteNumber(result, property, o);
                    }
                    else if (dataType == typeof(double))
                    {
                        WriteNumber(result, property, o);
                    }
                    else if (dataType == typeof(Nullable<int>))
                    {
                        WriteNumber(result, property, o);
                    }
                    else if (dataType == typeof(DateTime))
                    {
                        WriteDate(result, property, o);
                    }
                    else if (dataType == typeof(bool))
                    {
                        WriteBoolean(result, property, o);
                    }
                    else if (dataType == typeof(Nullable<bool>))
                    {
                        WriteBoolean(result, property, o);
                    }
                    else if (dataType == typeof(Nullable<DateTime>))
                    {
                        WriteDate(result, property, o);
                    }
                    else if (dataType.BaseType == typeof(Enum))
                    {
                        WriteEnum(result, property, o);
                    }
                    else
                    {
                        throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
                    }
                }
            }

            if (properties.Length != 0)
            {
                result.Replace(",", string.Empty, result.Length - 3, 2);
            }

            result.AppendLine();
            JSONIndenter.AddTabs(result, --currentIndent);
            result.Append("}");
            result.AppendLine();
            JSONIndenter.AddTabs(result, --currentIndent);
            result.Append("}");
        }

        private static string EscapeJSON(string s)
        {            
            if (s == null || s.Length == 0)
            {
                return "";
            }

            char c = '\0';
            int i;
            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            String t;

            for (i = 0; i < len; i += 1)
            {
                c = s[i];
                switch (c)
                {
                    case '\\':
                    case '"':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    case '/':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    default:
                        if (c < ' ')
                        {
                            t = "000" + String.Format("X", c);
                            sb.Append("\\u" + t.Substring(t.Length - 4));
                        }
                        else {
                            sb.Append(c);
                        }
                        break;
                }
            }
            return sb.ToString();           
        }

        private static void WriteString(StringBuilder result, PropertyInfo property, object o)
        {
            if (property.GetValue(o, null) != null)
            {             
                result.Append("\"" + property.Name + "\": \"" + EscapeJSON(property.GetValue(o, null).ToString()) + "\", ");
            }
            else
            {             
                result.Append("\"" + property.Name + "\": null, ");
            }
        }

        private static void WriteNumber(StringBuilder result, PropertyInfo property, object o)
        {
            if (property.GetValue(o, null) != null)
            {             
                result.Append("\"" + property.Name + "\": " + property.GetValue(o, null) + ", ");
            }
            else
            {             
                result.Append("\"" + property.Name + "\": null, ");
            }
        }

        private static void WriteDate(StringBuilder result, PropertyInfo property, object o)
        {
            if (property.GetValue(o, null) != null)
            {
                DateTime dotNetDate = DateTime.Parse(property.GetValue(o, null).ToString());
                string jsonDate = dotNetDate.Year.ToString() + "-" + dotNetDate.Month.ToString() + "-" + dotNetDate.Day.ToString() + "T" + dotNetDate.Hour.ToString() + ":" + dotNetDate.Minute + ":" + dotNetDate.Second.ToString() + "." + dotNetDate.Millisecond + "Z";                
                result.Append("\"" + property.Name + "\": \"" + jsonDate + "\", ");
            }
            else
            {                
                result.Append("\"" + property.Name + "\": null, ");
            }
        }

        private static void WriteBoolean(StringBuilder result, PropertyInfo property, object o)
        {
            if (property.GetValue(o, null) != null)
            {
                string stringValue = property.GetValue(o, null).ToString();
                string jsonValue = null;
                if (stringValue.ToUpper() == "TRUE")
                {
                    jsonValue = "true";
                }
                else if (stringValue.ToUpper() == "FALSE")
                {
                    jsonValue = "false";
                }                
                result.Append("\"" + property.Name + "\": " + jsonValue + ", ");
            }
            else
            {                
                result.Append("\"" + property.Name + "\": null, ");
            }           
        }

        private static void WriteEnum(StringBuilder result, PropertyInfo property, object o)
        {
            if (property.GetValue(o, null) != null)
            {
                object value = property.GetValue(o, null);
                object underlyingValue = Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));
                result.Append("\"" + property.Name + "\": " + underlyingValue.ToString() + ", ");
            }
            else
            {
                result.Append("\"" + property.Name + "\": null, ");
            }
        }

        private static void WriteMetaData(StringBuilder result, string qualifiedName)
        {
            result.Append("\"" + qualifiedName + "\":");
        }
    }
}
