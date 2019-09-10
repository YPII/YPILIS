using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business
{
    public class YPHoliday : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_HolidayDate;
        private string m_HolidayName;
        private bool m_IsAWorkDay;

        public YPHoliday()
        { }

        public YPHoliday(string holidayName, DateTime holidayDate, bool isAWorkDay)
        {
            this.m_HolidayDate = holidayDate;
            this.m_HolidayName = holidayName;
            this.m_IsAWorkDay = isAWorkDay;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public DateTime HolidayDate
        {
            get { return this.m_HolidayDate; }
            set
            {
                if(this.m_HolidayDate != value)
                {
                    this.m_HolidayDate = value;
                    this.NotifyPropertyChanged("HolidayDate");
                }
            }
        }

        public string HolidayName
        {
            get { return this.m_HolidayName; }
            set
            {
                if (this.m_HolidayName != value)
                {
                    this.m_HolidayName = value;
                    this.NotifyPropertyChanged("HolidayName");
                }
            }
        }

        public bool IsAWorkDay
        {
            get { return this.m_IsAWorkDay; }
            set
            {
                if (this.m_IsAWorkDay != value)
                {
                    this.m_IsAWorkDay = value;
                    this.NotifyPropertyChanged("IsAWorkDay");
                }
            }
        }

        public string DisplayDate
        {
            get { return this.m_HolidayDate.ToString("dddd, dd MMMM yyyy"); }
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
            MySqlCommand cmd = new MySqlCommand("Insert tblYPHoliday (HolidayDate, JSonValue) values (@HolidayDate, @JSONValue) ON DUPLICATE KEY UPDATE HolidayDate = @HolidayDate, JSONValue = @JSONValue;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@JSONValue", jString);
            cmd.Parameters.AddWithValue("@HolidayDate", this.m_HolidayDate);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
