using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business
{
    [Serializable()]
    public class BaseData : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public static string SqlConnectionString
        {
            get 
            {                				
				return Properties.Settings.Default.CurrentConnectionString;
			}
        }
        public const string DocumentPathRoot = @"\\CFileServer\Documents";

		public static string SurgicalSourceTable = "tblSurgical";
		public static bool SurgicalSourceTableDetermined = false;

        protected bool m_LockAquired = false;
        protected string m_LockMessage = string.Empty;        
		protected YellowstonePathology.Business.User.SystemUser m_CurrentUser;

        public BaseData()
        {
            this.m_CurrentUser = YellowstonePathology.Business.User.SystemIdentity.Instance.User;
		}

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        
        public static void Update(object dataObject)
        {            
            
            Type objectType = dataObject.GetType();
            
            PropertyInfo propertyInfoPropertyChangedList = objectType.GetProperty("PropertyChangedList");
            List<PropertyChangedItem> propertyChangedList = (List<PropertyChangedItem>)propertyInfoPropertyChangedList.GetValue(dataObject, null);            

            MySqlCommand cmd = new MySqlCommand();
            string sql = string.Empty;            

            if (propertyChangedList.Count != 0)
            {
                YellowstonePathology.Business.CustomAttributes.SqlTableAttribute tableAttribute = (YellowstonePathology.Business.CustomAttributes.SqlTableAttribute)Attribute.GetCustomAttribute(objectType, typeof(YellowstonePathology.Business.CustomAttributes.SqlTableAttribute));
                sql = "Update " + tableAttribute.TableName + " Set ";

                MySqlParameter keyParam = new MySqlParameter();
                keyParam.ParameterName = "@" + tableAttribute.KeyFieldName;
                keyParam.Size = tableAttribute.KeyFieldLength;

                PropertyInfo keyPropertyInfo = objectType.GetProperty(tableAttribute.KeyFieldName);
                object keyValue = keyPropertyInfo.GetValue(dataObject, null);
                SetParameterValue(keyParam, tableAttribute.KeyFieldSqlDbType, keyValue);
                cmd.Parameters.Add(keyParam);

                foreach (PropertyChangedItem item in propertyChangedList)
                {
                    PropertyInfo propertyInfo = objectType.GetProperty(item.PropertyName);
                    object value = propertyInfo.GetValue(dataObject, null);

                    YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute fieldAttribute = (YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute));
                    sql += fieldAttribute.FieldName + " = @" + fieldAttribute.FieldName + ", ";

                    MySqlParameter param = new MySqlParameter();
                    param.ParameterName = "@" + fieldAttribute.FieldName;
                    SetParameterValue(param, fieldAttribute.SqlDbType, value);
                    cmd.Parameters.Add(param);
                }

                sql = sql.Remove(sql.Length - 2, 1);
                sql += "Where " + tableAttribute.KeyFieldName + " = @" + tableAttribute.KeyFieldName;                

                using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
                {                    
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = sql;                    
                    cmd.ExecuteNonQuery();                    
                }
                propertyChangedList.Clear();
            }
        }

        public static void Insert(object dataObject)
        {
            Type objectType = dataObject.GetType();
            PropertyInfo propertyInfoPropertyChangedList = objectType.GetProperty("PropertyChangedList");
            List<PropertyChangedItem> propertyChangedList = (List<PropertyChangedItem>)propertyInfoPropertyChangedList.GetValue(dataObject, null);

            MySqlCommand cmd = new MySqlCommand();
            string sql = string.Empty;
            string fieldList = string.Empty;
            string valueList = string.Empty;

            if (propertyChangedList.Count != 0)
            {

                YellowstonePathology.Business.CustomAttributes.SqlTableAttribute tableAttribute = (YellowstonePathology.Business.CustomAttributes.SqlTableAttribute)Attribute.GetCustomAttribute(objectType, typeof(YellowstonePathology.Business.CustomAttributes.SqlTableAttribute));
                sql = "Insert " + tableAttribute.TableName + " ";              

                foreach (PropertyChangedItem item in propertyChangedList)
                {
                    PropertyInfo propertyInfo = objectType.GetProperty(item.PropertyName);
                    object value = propertyInfo.GetValue(dataObject, null);

                    YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute fieldAttribute = (YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute));
                    fieldList += fieldAttribute.FieldName + ", ";
                    valueList += "@" + fieldAttribute.FieldName + ", ";

                    MySqlParameter param = new MySqlParameter();
                    param.ParameterName = "@" + fieldAttribute.FieldName;
                    SetParameterValue(param, fieldAttribute.SqlDbType, value);
                    cmd.Parameters.Add(param);
                }

                fieldList = fieldList.Remove(fieldList.Length - 2, 1);
                valueList = valueList.Remove(valueList.Length - 2, 1);
                sql += "(" + fieldList + ") values (" + valueList + ") ";                
                using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
                {                    
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = sql;                    
                    cmd.ExecuteNonQuery();
                }
                propertyChangedList.Clear();
            }
        }

        public static void SetParameterValue(MySqlParameter param, SqlDbType sqlDbType, object value)
        {
            switch (sqlDbType)
            {
                case SqlDbType.VarChar:
                case SqlDbType.NChar:
                case SqlDbType.NVarChar:
                case SqlDbType.NText:
                case SqlDbType.Char:
                case SqlDbType.Text:
                    SetSqlStringValue(param, value);
                    break;
                case SqlDbType.Int:
                case SqlDbType.BigInt:
                case SqlDbType.TinyInt:
                    SetSqlIntValue(param, value);
                    break;
                case SqlDbType.Decimal:
                case SqlDbType.Float:
                    SetSqlDoubleValue(param, value);
                    break;
                case SqlDbType.Bit:
                    SetSqlBitValue(param, value);
                    break;
                case SqlDbType.DateTime:
                    SetSqlDateValue(param, value);
                    break;
                default:
                    MessageBox.Show("Error Setting Parameter Value. " + param.ParameterName);
                    break;
            }
        }

        public static string GetStringValue(string fieldName, MySqlDataReader dr)
        {            
			if (dr[fieldName] == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                return dr[fieldName].ToString();
            }
        }

        public static string GetPatientName(MySqlDataReader dr)
        {
            string patientName = string.Empty;
            if (dr["PFirstName"] != DBNull.Value)
            {
                patientName += dr["PFirstName"].ToString();
            }
            if (dr["PLastName"] != DBNull.Value)
            {
                patientName += " " + dr["PLastName"].ToString();
            }
            return patientName;
        }

        public static int GetIntValue(string fieldName, MySqlDataReader dr)
        {
            if (dr[fieldName] == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dr[fieldName].ToString());
            }
        }

        public static Nullable<DateTime> GetDateTimeValue(string fieldName, MySqlDataReader dr)
        {
            if (dr[fieldName] == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(dr[fieldName].ToString());
            }
        }

        public static bool GetBoolValue(string fieldName, MySqlDataReader dr)
        {
            if (dr[fieldName] == DBNull.Value)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(dr[fieldName].ToString());
            }
        }

        public static long GetLongValue(string fieldName, MySqlDataReader dr)
        {
            if (dr[fieldName] == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dr[fieldName].ToString());
            }
        }

        public static double GetDoubleValue(string fieldName, MySqlDataReader dr)
        {
            if (dr[fieldName] == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(dr[fieldName].ToString());
            }
        }

        public static string GetSqlBit(bool value)
        {
            if (value == true)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public static void SetSqlStringValue(MySqlParameter param, object value)
        {
            string strValue = (string)value;
            SetSqlStringValue(param, strValue);
        }

        public static void SetSqlStringValue(MySqlParameter param, string value)
        {
            if (value == string.Empty || value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }
        }

        public static void SetSqlIntValue(MySqlParameter param, object value)
        {
            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                int intValue = (int)value;
                param.Value = intValue;
            }
        }

        public static void SetSqlDoubleValue(MySqlParameter param, object value)
        {
            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                double floatValue = (double)value;
                param.Value = floatValue;
            }
        }

        public static void SetSqlBitValue(MySqlParameter param, object value)
        {
            bool boolValue = (bool)value;
            SetSqlBitValue(param, boolValue);
        }

        public static void SetSqlBitValue(MySqlParameter param, bool value)
        {
            if (value == true)
            {
                param.Value = 1;
            }
            else
            {
                param.Value = 0;
            }
        }

        public static void SetSqlDateValue(MySqlParameter param, object dateTime)
        {
            Nullable<DateTime> dateTimeValue = (Nullable<DateTime>)dateTime;
            SetSqlDateValue(param, dateTimeValue);
        }

        public static void SetSqlDateValue(MySqlParameter param, Nullable<DateTime> dateTime)
        {
            if (dateTime.HasValue == false)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                if (dateTime.Value.ToShortTimeString() == "12:00 PM")
                {
                    param.Value = dateTime.Value.ToString("MM/dd/yyy");
                }
                else
                {
                    param.Value = dateTime.Value.ToString("MM/dd/yyy HH:mm:ss ");
                }
            }
        }

        public static void SaveStoredProcedures()
        {
            string[] files = Directory.GetFiles(@"C:\VisualStudioProjects\YPIIApp\YPIIApp\SQLProcedures");
            foreach (string file in files)
            {
                StreamReader sr = new StreamReader(file);
                string text = string.Empty;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    text = text + "\n" + line;
                }

                using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
                {
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandText = text;
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(file + "  " + e.Message);
                    }
                }
                sr.Close();
            }
        }

        public static void GetStoredProcedures()
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select so.Name, sc.Text " +
                    "from SysObjects so " +
                    "join SysComments sc on so.Id = sc.Id " +
                    "where so.XType = 'P' and Category = 0";
                cmd.CommandType = CommandType.Text;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string text = dr["Text"].ToString();
                        string fileName = dr["name"].ToString() + ".txt";
                        StreamWriter sw = new StreamWriter(@"C:\VisualStudioProjects\YPIIApp\YPIIApp\SQLProcedures\" + fileName);
                        sw.WriteLine(text);
                        sw.Close();
                    }
                }
            }
        }

        public static void FillListItem(object dataObject, MySqlDataReader dr)
        {
            Type objectType = dataObject.GetType();
            PropertyInfo[] objectProperties = objectType.GetProperties();            

            foreach (PropertyInfo propertyInfo in objectProperties)
            {
                YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute sqlAttribute = (YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute));
                if (sqlAttribute != null)
                {
                    switch (sqlAttribute.SqlDbType)
                    {
                        case SqlDbType.VarChar:
                        case SqlDbType.NVarChar:
                        case SqlDbType.NText:
                            propertyInfo.SetValue(dataObject, BaseData.GetStringValue(sqlAttribute.FieldName, dr), null);
                            break;
                        case SqlDbType.Int:
                        case SqlDbType.BigInt:
                            propertyInfo.SetValue(dataObject, BaseData.GetIntValue(sqlAttribute.FieldName, dr), null);
                            break;
                        case SqlDbType.Float:
                            propertyInfo.SetValue(dataObject, BaseData.GetDoubleValue(sqlAttribute.FieldName, dr), null);
                            break;
                        case SqlDbType.Bit:
                            propertyInfo.SetValue(dataObject, BaseData.GetBoolValue(sqlAttribute.FieldName, dr), null);
                            break;
                        case SqlDbType.DateTime:
                        case SqlDbType.SmallDateTime:
                            propertyInfo.SetValue(dataObject, BaseData.GetDateTimeValue(sqlAttribute.FieldName, dr), null);
                            break;
                        default:
                            MessageBox.Show("Error Writing data from DataReader DataType " + sqlAttribute.SqlDbType + "  " + propertyInfo.Name + " " + objectType.Name);
                            break;
                    }
                }
            }            
        }

        public static void Fill(object dataObject, MySqlDataReader dr)
        {
            Type objectType = dataObject.GetType();
            PropertyInfo[] objectProperties = objectType.GetProperties();            
            PropertyInfo isFillingPropertyInfo = objectType.GetProperty("IsFilling");
            isFillingPropertyInfo.SetValue(dataObject, true, null);
            
            foreach (PropertyInfo propertyInfo in objectProperties)
            {                
                YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute sqlAttribute = (YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute));
                if (sqlAttribute != null)
                {
                    switch (sqlAttribute.SqlDbType)
                    {
                        case SqlDbType.VarChar:
                        case SqlDbType.NVarChar:
                        case SqlDbType.NText:
                            propertyInfo.SetValue(dataObject, BaseData.GetStringValue(sqlAttribute.FieldName, dr), null);                            
                            break;
                        case SqlDbType.Int:
                        case SqlDbType.BigInt:
                            propertyInfo.SetValue(dataObject, BaseData.GetIntValue(sqlAttribute.FieldName, dr), null);
                            break;
                        case SqlDbType.Float:
                            try
                            {
                                propertyInfo.SetValue(dataObject, BaseData.GetDoubleValue(sqlAttribute.FieldName, dr), null);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(propertyInfo.Name);
                            }
                            break;
                        case SqlDbType.Bit:
                            propertyInfo.SetValue(dataObject, BaseData.GetBoolValue(sqlAttribute.FieldName, dr), null);
                            break;
                        case SqlDbType.DateTime:
                        case SqlDbType.SmallDateTime:
                            propertyInfo.SetValue(dataObject, BaseData.GetDateTimeValue(sqlAttribute.FieldName, dr), null);
                            break;
                        default:
                            MessageBox.Show("Error Writing data from DataReader DataType " + sqlAttribute.SqlDbType + "  " + propertyInfo.Name + " " + objectType.Name);
                            break;
                    }
                }
            }
            isFillingPropertyInfo.SetValue(dataObject, false, null);
            MethodInfo notifyPropertyChangeMethodInfo = objectType.GetMethod("NotifyPropertyChanged");
            object[] parameters = new object[1];
            parameters[0] = string.Empty;
            notifyPropertyChangeMethodInfo.Invoke(dataObject, parameters);
        }        

        public static string GetShortDateString(Nullable<DateTime> date)
        {
            if (date.HasValue == true)
            {
                return date.Value.ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetShortDateTimeString(Nullable<DateTime> date)
        {
            if (date.HasValue == true)
            {
                return date.Value.ToString("MM/dd/yyy HH:mm");
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetShortTimeString(Nullable<DateTime> time)
        {
            if (time.HasValue == true)
            {
                return time.Value.ToShortTimeString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetMillitaryTimeString(Nullable<DateTime> time)
        {
            if (time.HasValue == true)
            {                
                return time.Value.ToString("HH:mm");
            }
            else
            {
                return string.Empty;
            }
        }        

        public static void SetSqlDateParameterValue(Nullable<DateTime> date, MySqlParameter param)
        {
            if (date.HasValue == true)
            {
                param.Value = date.Value.ToShortDateString();
            }
            else
            {
                param.Value = DBNull.Value;
            }
        }

		public static void SetSurgicalSource()
		{
			SurgicalSourceTableDetermined = true;
			SurgicalSourceTable = "tblAccessionOrder";
		}
    }
}
