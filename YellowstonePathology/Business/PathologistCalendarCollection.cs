using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace YellowstonePathology.Business
{
    public class PathologistCalendarCollection : ObservableCollection<PathologistCalendar>
    {
        private List<string> m_Pathologists;

        public PathologistCalendarCollection()
        {
            this.m_Pathologists = new List<string>();
            this.m_Pathologists.Add("Dr Bibbey");
            this.m_Pathologists.Add("Dr Brown");
            this.m_Pathologists.Add("Dr Durden");
            this.m_Pathologists.Add("Dr Emerick");
            this.m_Pathologists.Add("Dr Luem");
            this.m_Pathologists.Add("Dr Messner");
            this.m_Pathologists.Add("Dr Nero");
            this.m_Pathologists.Add("Dr Schneider");
        }

        public static PathologistCalendarCollection Load(DateTime startDate, DateTime endDate)
        {
            PathologistCalendarCollection result = new PathologistCalendarCollection();
            MySqlCommand cmd = new MySqlCommand("Select JSONValue from tblPathologistCalendar where CalendarDate between @StartDate and @EndDate;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.PathologistCalendar pathologistCalendar = JsonConvert.DeserializeObject<PathologistCalendar>(dr[0].ToString(), new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });

                        result.Add(pathologistCalendar);
                    }
                }
            }
            return result;
        }

        public List<string> Pathologists
        {
            get { return this.m_Pathologists; }
        }

        public void Save()
        {
            foreach(PathologistCalendar pathologistCalendar in this)
            {
                pathologistCalendar.Save();
            }
        }

        public void AddMonth(DateTime startDate)
        {
            if(startDate.Day != 1)
            {
                startDate = startDate.AddDays(-(startDate.Day - 1));
            }
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            if (this.CanAddMonth(startDate, endDate) == true)
            {
                for(DateTime curDate = startDate; curDate <= endDate; curDate = curDate.AddDays(1))
                {
                    PathologistCalendar pathologistCalendar = new PathologistCalendar(curDate, this.m_Pathologists);
                    pathologistCalendar.Save();
                }
            }
        }

        private bool CanAddMonth(DateTime startDate, DateTime endDate)
        {
            PathologistCalendarCollection tmp = PathologistCalendarCollection.Load(startDate, endDate);
            return tmp.Count > 0 ? false : true;
        }
    }
}
