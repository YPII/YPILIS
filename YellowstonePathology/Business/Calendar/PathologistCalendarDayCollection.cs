using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace YellowstonePathology.Business.Calendar
{
    public class PathologistCalendarDayCollection : ObservableCollection<PathologistCalendarDay>
    {
        private List<string> m_Pathologists;

        public PathologistCalendarDayCollection()
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

        public static PathologistCalendarDayCollection Load(DateTime startDate, DateTime endDate)
        {
            PathologistCalendarDayCollection result = new PathologistCalendarDayCollection();
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
                        PathologistCalendarDay pathologistCalendarDay = JsonConvert.DeserializeObject<PathologistCalendarDay>(dr[0].ToString(), new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });

                        result.Add(pathologistCalendarDay);
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
            foreach (PathologistCalendarDay pathologistCalendarDay in this)
            {
                pathologistCalendarDay.Save();
            }
        }

        public void AddMonth(DateTime startDate)
        {
            if (startDate.Day != 1)
            {
                startDate = startDate.AddDays(-(startDate.Day - 1));
            }
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            if (this.CanAddMonth(startDate, endDate) == true)
            {
                for (DateTime curDate = startDate; curDate <= endDate; curDate = curDate.AddDays(1))
                {
                    if (curDate.DayOfWeek == DayOfWeek.Saturday || curDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        continue;
                    }
                    else
                    {
                        PathologistCalendarDay pathologistCalendarDay = new PathologistCalendarDay(curDate);
                        pathologistCalendarDay.Save();
                    }
                }
            }
        }

        private bool CanAddMonth(DateTime startDate, DateTime endDate)
        {
            PathologistCalendarDayCollection tmp = PathologistCalendarDayCollection.Load(startDate, endDate);
            return tmp.Count > 0 ? false : true;
        }

        public static PathologistsByLocation PathologistsCountByLocationOnDate(DateTime day)
        {
            PathologistsByLocation result = new Calendar.PathologistsByLocation();
            DateTime correctedDay = new DateTime(day.Year, day.Month, day.Day);

            DateTime nextWorkDay = correctedDay.AddDays(1);
            if(nextWorkDay.DayOfWeek == DayOfWeek.Saturday)
            {
                nextWorkDay = nextWorkDay.AddDays(2);
            }
            else if(nextWorkDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextWorkDay = nextWorkDay.AddDays(1);
            }

            PathologistCalendarDayCollection pathologistCalendarDayCollection = PathologistCalendarDayCollection.Load(nextWorkDay, nextWorkDay);

            foreach (PathologistCalendarDay pathologistCalendarDay in pathologistCalendarDayCollection)
            {
                List<PathologistCalendarStatus> statusList = new List<Calendar.PathologistCalendarStatus>();
                statusList.Add(pathologistCalendarDay.DrBibbeyStatus);
                statusList.Add(pathologistCalendarDay.DrBrownStatus);
                statusList.Add(pathologistCalendarDay.DrDurdenStatus);
                statusList.Add(pathologistCalendarDay.DrEmerickStatus);
                statusList.Add(pathologistCalendarDay.DrLuemStatus);                
                statusList.Add(pathologistCalendarDay.DrNeroStatus);
                statusList.Add(pathologistCalendarDay.DrSchneiderStatus);

                foreach (PathologistCalendarStatus pathologistCalendarStatus in statusList)
                {
                    if (pathologistCalendarStatus.Status == "Billings")
                    {
                        result.BillingsCount += 1;
                    }
                    else if (pathologistCalendarStatus.Status == "Bozeman")
                    {
                        result.BozemanCount += 1;
                    }
                }
            }

            return result;
        }
    }
}
