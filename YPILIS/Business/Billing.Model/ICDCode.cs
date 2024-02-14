using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Billing.Model
{
    public class ICDCode
    {
        string m_ICDCodeId;
        string m_Code;
        string m_Category;
        string m_Description;

        public ICDCode()
        {

        }

        public string ICDCodeId
        {
            get { return this.m_ICDCodeId; }
            set { this.m_ICDCodeId = value; }
        }

        public string Code
        {
            get { return this.m_Code; }
            set { this.m_Code = value; }
        }

        public string Category
        {
            get { return this.m_Category; }
            set { this.m_Category = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public static ICDCode Clone(ICDCode icdCodeIn)
        {
            return (ICDCode)icdCodeIn.MemberwiseClone();
        }

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string result = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);
            return result;
        }

        public void Save()
        {
            string jString = this.ToJSON();
            MySqlCommand cmd = new MySqlCommand("Insert tblICDCode (ICDCode, JSONValue) values (@ICDCode, @JSONValue) ON DUPLICATE KEY UPDATE ICDCode = @ICDCode, JSONValue = @JSONValue;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@JSONValue", jString);
            cmd.Parameters.AddWithValue("@ICDCode", this.m_Code);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
