using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business
{
    public class PathologistCalendar
    {
        private DateTime m_CalendarDate;
        private string m_PathologistName;
        private string m_Status;

        public PathologistCalendar()
        { }

        public DateTime CalendarDate
        {
            get { return this.m_CalendarDate; }
            set { this.m_CalendarDate = value; }
        }

        public string PathologistName
        {
            get { return this.m_PathologistName; }
            set { this.m_PathologistName = value; }
        }

        public string Status
        {
            get { return this.m_Status; }
            set { this.m_Status = value; }
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
            MySqlCommand cmd = new MySqlCommand("Insert tblPathologistCalendar (CalendarDate, JSONValue) values (@CalendarDate, @JSONValue) ON DUPLICATE KEY UPDATE CPTCode = @CalendarDate, JSONValue = @JSONValue;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@JSONValue", jString);
            cmd.Parameters.AddWithValue("@CalendarDate", this.m_CalendarDate);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
