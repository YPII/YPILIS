using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business
{
    public class PathologistCalendar
    {
        private DateTime m_CalendarDate;
        private CalendarPathologistCollection m_CalendarPathologistCollection;

        public PathologistCalendar()
        {
            this.m_CalendarPathologistCollection = new CalendarPathologistCollection();
        }

        public PathologistCalendar(DateTime calendarDate, List<string> pathologists)
        {
            this.m_CalendarDate = calendarDate;
            this.m_CalendarPathologistCollection = new CalendarPathologistCollection();
            foreach (string pathologist in pathologists)
            {
                CalendarPathologist calendarPathologist = new CalendarPathologist();
                calendarPathologist.PathologistName = pathologist;
                calendarPathologist.Status = "In";
                this.m_CalendarPathologistCollection.Add(calendarPathologist);
            }
        }

        public DateTime CalendarDate
        {
            get { return this.m_CalendarDate; }
            set { this.m_CalendarDate = value; }
        }

        public CalendarPathologistCollection CalendarPathologistCollection
        {
            get { return this.m_CalendarPathologistCollection; }
            set { this.m_CalendarPathologistCollection = value; }
        }

        public string CalendarDisplayDate
        {
            get { return this.m_CalendarDate.ToString("ddd MMM d"); }
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
            MySqlCommand cmd = new MySqlCommand("Insert tblPathologistCalendar (CalendarDate, JSONValue) values (@CalendarDate, @JSONValue) ON DUPLICATE KEY UPDATE CalendarDate = @CalendarDate, JSONValue = @JSONValue;");
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
