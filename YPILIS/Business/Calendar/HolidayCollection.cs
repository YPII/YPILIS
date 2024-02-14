﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Calendar
{
    public class HolidayCollection : ObservableCollection<Holiday>
    {
        public HolidayCollection()
        {

        }

        public bool Exists(DateTime holidayDate)
        {
            Holiday result = this.FirstOrDefault(y => y.HolidayDate == holidayDate);
            return result == null ? false : true;
        }

        public Holiday Get(DateTime holidayDate)
        {
            Holiday result = this.FirstOrDefault(y => y.HolidayDate == holidayDate);
            return result;
        }

        public static HolidayCollection GetAll()
        {
            HolidayCollection result = new HolidayCollection();
            DateTime startDate = new DateTime(2018, 1, 1);
            DateTime endDate = new DateTime(2120, 12, 31);
            result.Load(startDate, endDate);
            return result;
        }

        public static HolidayCollection GetByDateRange(DateTime startDate, DateTime endDate)
        {
            HolidayCollection result = new HolidayCollection();
            result.Load(startDate, endDate);
            return result;
        }

        private void Load(DateTime startDate, DateTime endDate)
        {
            this.ClearItems();
            MySqlCommand cmd = new MySqlCommand("Select JSONValue from tblHoliday where HolidayDate between @StartDate and @EndDate order by HolidayDate;");
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
                        Holiday ypHoliday = JsonConvert.DeserializeObject<Holiday>(dr[0].ToString(), new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace
                        });
                        this.Add(ypHoliday);
                    }
                }
            }
        }

        public void DeleteHoliday(Holiday holiday)
        {
            MySqlCommand cmd = new MySqlCommand("Delete from tblHoliday where HolidayDate = @HolidayDate;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@HolidayDate", holiday.HolidayDate);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            this.Remove(holiday);
        }

        public bool IsDateAHoliday(DateTime dateToCheck)
        {
            bool result = false;
            DateTime dt = new DateTime(dateToCheck.Year, dateToCheck.Month, dateToCheck.Day);
            if (this.Exists(dt) == true)
            {
                Holiday holiday = this.Get(dt);
                if (holiday.IsAWorkDay == false)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
