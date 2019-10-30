using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data;
using MySql.Data.MySqlClient;
using System.ComponentModel;

namespace YellowstonePathology.Business.Calendar
{
    public class PathologistCalendarDay : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_CalendarDate;
        private PathologistClanendarStatus m_DrBibbeyStatus;
        private PathologistClanendarStatus m_DrBrownStatus;
        private PathologistClanendarStatus m_DrDurdenStatus;
        private PathologistClanendarStatus m_DrEmerickStatus;
        private PathologistClanendarStatus m_DrLuemStatus;
        private PathologistClanendarStatus m_DrMessnerStatus;
        private PathologistClanendarStatus m_DrNeroStatus;
        private PathologistClanendarStatus m_DrSchneiderStatus;

        public PathologistCalendarDay()
        { }

        public PathologistCalendarDay(DateTime calendarDay)
        {
            this.m_CalendarDate = calendarDay;
            this.m_DrBibbeyStatus = new Calendar.PathologistClanendarStatus("Dr Bibbey", "Billings");
            this.m_DrBrownStatus = new Calendar.PathologistClanendarStatus("Dr Brown", "Billings");
            this.m_DrDurdenStatus = new Calendar.PathologistClanendarStatus("Dr Durden", "Billings");
            this.m_DrEmerickStatus = new Calendar.PathologistClanendarStatus("Dr Emerick", "Bozeman");
            this.m_DrLuemStatus = new Calendar.PathologistClanendarStatus("Dr Luem", "Bozeman");
            this.m_DrMessnerStatus = new Calendar.PathologistClanendarStatus("Dr Messner", "Billings");
            this.m_DrNeroStatus = new Calendar.PathologistClanendarStatus("Dr Nero", "Bozeman");
            this.m_DrSchneiderStatus = new Calendar.PathologistClanendarStatus("Dr Schneider", "Billings");
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public DateTime CalendarDate
        {
            get { return this.m_CalendarDate; }
            set
            {
                if(this.m_CalendarDate != value)
                {
                    this.m_CalendarDate = value;
                    this.NotifyPropertyChanged("CalendarDate");
                }
            }
        }

        public string CalendarDisplayDate
        {
            get { return this.m_CalendarDate.ToString("ddd MMM d"); }
        }

        public PathologistClanendarStatus DrBibbeyStatus
        {
            get { return this.m_DrBibbeyStatus; }
            set
            {
                if (value != this.m_DrBibbeyStatus)
                {
                    this.m_DrBibbeyStatus = value;
                    this.NotifyPropertyChanged("DrBibbeyStatus");
                }
            }
        }

        public PathologistClanendarStatus DrBrownStatus
        {
            get { return this.m_DrBrownStatus; }
            set
            {
                if (value != this.m_DrBrownStatus)
                {
                    this.m_DrBrownStatus = value;
                    this.NotifyPropertyChanged("DrBrownStatus");
                }
            }
        }

        public PathologistClanendarStatus DrDurdenStatus
        {
            get { return this.m_DrDurdenStatus; }
            set
            {
                if (value != this.m_DrDurdenStatus)
                {
                    this.m_DrDurdenStatus = value;
                    this.NotifyPropertyChanged("DrDurdenStatus");
                }
            }
        }

        public PathologistClanendarStatus DrEmerickStatus
        {
            get { return this.m_DrEmerickStatus; }
            set
            {
                if (value != this.m_DrEmerickStatus)
                {
                    this.m_DrEmerickStatus = value;
                    this.NotifyPropertyChanged("DrEmerickStatus");
                }
            }
        }

        public PathologistClanendarStatus DrLuemStatus
        {
            get { return this.m_DrLuemStatus; }
            set
            {
                if (value != this.m_DrLuemStatus)
                {
                    this.m_DrLuemStatus = value;
                    this.NotifyPropertyChanged("DrLuemStatus");
                }
            }
        }

        public PathologistClanendarStatus DrMessnerStatus
        {
            get { return this.m_DrMessnerStatus; }
            set
            {
                if (value != this.m_DrMessnerStatus)
                {
                    this.m_DrMessnerStatus = value;
                    this.NotifyPropertyChanged("DrMessnerStatus");
                }
            }
        }

        public PathologistClanendarStatus DrNeroStatus
        {
            get { return this.m_DrNeroStatus; }
            set
            {
                if (value != this.m_DrNeroStatus)
                {
                    this.m_DrNeroStatus = value;
                    this.NotifyPropertyChanged("DrNeroStatus");
                }
            }
        }

        public PathologistClanendarStatus DrSchneiderStatus
        {
            get { return this.m_DrSchneiderStatus; }
            set
            {
                if (value != this.m_DrSchneiderStatus)
                {
                    this.m_DrSchneiderStatus = value;
                    this.NotifyPropertyChanged("DrSchneiderStatus");
                }
            }
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
