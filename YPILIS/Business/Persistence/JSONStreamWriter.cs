using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace YellowstonePathology.Business.Persistence
{
    public static class JSONStreamWriter
    {
        public static void WritProperties(StringWriter result, object o)
        {            
            PropertyInfo[] properties = o.GetType().GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty)) || Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).ToArray();

            for (int i=0; i<properties.Length; i++)
            {
                PropertyInfo property = properties[i];                
                Type dataType = property.PropertyType;

                if(property.Name == "ObjectId")
                {
                    WriteObjectId(result, property, o);
                }
                else if (dataType == typeof(string))
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

                if (i != properties.Length - 1)
                {
                    result.Write(", ");
                }                          
            }                     
        }

        private static void WriteObjectId(StringWriter result, PropertyInfo property, object o)
        {            
            if (property.GetValue(o, null) != null)
            {
                result.Write("\"_id\": ObjectId(\"" + property.GetValue(o, null).ToString() + "\")");
            }
            else
            {
                result.Write("\"" + property.Name + "\": null");
            }
        }

        private static void WriteString(StringWriter result, PropertyInfo property, object o)
        {            
            if (property.GetValue(o, null) != null)
            {             
                result.Write("\"" + property.Name + "\": \"" + EscapeJSON(property.GetValue(o, null).ToString()) + "\"");
            }
            else
            {             
                result.Write("\"" + property.Name + "\": null");
            }
        }

        private static void WriteNumber(StringWriter result, PropertyInfo property, object o)
        {
            if (property.GetValue(o, null) != null)
            {             
                result.Write("\"" + property.Name + "\": " + property.GetValue(o, null));
            }
            else
            {             
                result.Write("\"" + property.Name + "\": null");
            }
        }

        private static void WriteDate(StringWriter result, PropertyInfo property, object o)
        {
            if (property.GetValue(o, null) != null)
            {
                DateTime dotNetDate = DateTime.Parse(property.GetValue(o, null).ToString());
                long totalMiliseconds = (long)(dotNetDate - new DateTime(1970, 1, 1)).TotalMilliseconds;
                result.Write("\"" + property.Name + "\": {\"$date\": " + totalMiliseconds.ToString() + "}");                
            }
            else
            {                
                result.Write("\"" + property.Name + "\": null");
            }
        }

        private static void WriteBoolean(StringWriter result, PropertyInfo property, object o)
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
                result.Write("\"" + property.Name + "\": " + jsonValue + "");
            }
            else
            {                
                result.Write("\"" + property.Name + "\": null");
            }           
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
    }
}
